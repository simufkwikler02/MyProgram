using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCabinetApp.CommandHandlers
{
    public class ExportCommandHandler : ServiceCommandHandlerBase
    {
        private const string HintMessageExport = "Use: export [csv | xml] [directory]";

        public ExportCommandHandler(IFileCabinetService? fileCabinetService)
            : base(fileCabinetService)
        {
        }

        public override void Handle(AppCommandRequest request)
        {
            if (request.Command.Equals("export", StringComparison.OrdinalIgnoreCase))
            {
                var inputs = request.Parameters != null && request.Parameters != string.Empty ? request.Parameters.Split(' ', 2) : new string[] { string.Empty, string.Empty };
                if (inputs.Length <= 1)
                {
                    PrintMissedCommandInfo(request.Parameters);
                    Console.WriteLine(HintMessageExport);
                    return;
                }

                const int commandIndex = 0;
                const int pathIndex = 1;
                var command = inputs[commandIndex];
                var path = inputs[pathIndex];
                StreamWriter? fstream;
                FileInfo? fileInfo;
                DirectoryInfo? directory;
                try
                {
                    fileInfo = new FileInfo(path);
                    directory = fileInfo.Directory;
                    if (!directory.Exists)
                    {
                        throw new ArgumentException(nameof(directory));
                    }

                    if (fileInfo.Exists)
                    {
                        Console.WriteLine($"File is exist - rewrite {path}? [Y/N]");
                        var com = Console.ReadLine() ?? string.Empty;
                        switch (com)
                        {
                            case "y":
                                goto case "Y";
                            case "Y":
                                break;
                            case "n":
                                goto case "N";
                            case "N":
                                Console.WriteLine("Export canceled");
                                return;
                            default:
                                PrintMissedCommandInfo(com);
                                return;
                        }
                    }
                }
                catch
                {
                    Console.WriteLine($"Export failed: can't open file {path}.");
                    Console.WriteLine(HintMessageExport);
                    return;
                }

                fstream = new StreamWriter(path, false);
                var snapshot = this.service.MakeSnapshot();
                switch (command.ToLower(CultureInfo.CurrentCulture))
                {
                    case "csv":
                        snapshot.SaveToCsv(fstream);
                        break;
                    case "xml":
                        snapshot.SaveToXml(fstream);
                        break;
                    default:
                        PrintMissedCommandInfo(command);
                        fstream.Close();
                        return;
                }

                fstream.Close();
                Console.WriteLine($"All records are exported to file {path}.");
            }
            else
            {
                base.Handle(request);
            }
        }
    }
}
