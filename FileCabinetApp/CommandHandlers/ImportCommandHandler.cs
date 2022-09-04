using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCabinetApp.CommandHandlers
{
    public class ImportCommandHandler : ServiceCommandHandlerBase
    {
        private const string HintMessageImport = "Use: import [csv | xml] [directory]";

        public ImportCommandHandler(IFileCabinetService? fileCabinetService)
            : base(fileCabinetService)
        {
        }

        public override void Handle(AppCommandRequest request)
        {
            if (request.Command.Equals("import", StringComparison.OrdinalIgnoreCase))
            {
                var inputs = request.Parameters != null && request.Parameters != string.Empty ? request.Parameters.Split(' ', 2) : new string[] { string.Empty, string.Empty };
                if (inputs.Length <= 1)
                {
                    PrintMissedCommandInfo(request.Parameters ?? string.Empty);
                    Console.WriteLine(HintMessageImport);
                    return;
                }

                const int commandIndex = 0;
                const int pathIndex = 1;
                var command = inputs[commandIndex];
                var path = inputs[pathIndex];
                FileStream? fstream;
                try
                {
                    fstream = new FileStream(path, FileMode.Open);
                    var snapshot = new FileCabinetServiceSnapshot();

                    switch (command.ToLower(CultureInfo.CurrentCulture))
                    {
                        case "csv":
                            snapshot.LoadFromCsv(new StreamReader(fstream));
                            this.Service?.Restore(snapshot);
                            break;
                        case "xml":
                            snapshot.LoadFromXml(new StreamReader(fstream));
                            this.Service?.Restore(snapshot);
                            break;
                        default:
                            PrintMissedCommandInfo(command);
                            fstream.Close();
                            return;
                    }

                    fstream.Close();
                }
                catch
                {
                    Console.WriteLine($"Import failed: can't open file {path}.");
                    Console.WriteLine(HintMessageImport);
                    return;
                }
            }
            else
            {
                base.Handle(request);
            }
        }
    }
}
