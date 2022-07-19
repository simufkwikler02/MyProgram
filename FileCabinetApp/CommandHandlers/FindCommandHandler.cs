using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FileCabinetApp.CommandHandlers
{
    public class FindCommandHandler : CommandHandlerBase
    {
        private const string HintMessageFind = "Use: find [firstname | lastname | dateofbirth] [text]";

        public FindCommandHandler(IFileCabinetService fileCabinetService, IRecordValidator validate)
            : base(fileCabinetService, validate)
        {
        }

        public override void Handle(AppCommandRequest request)
        {
            if (request.Command.Equals("find", StringComparison.OrdinalIgnoreCase))
            {
                var inputs = request.Parameters != null ? request.Parameters.Split(' ', 2) : new string[] { string.Empty, string.Empty };
                const int commandIndex = 0;
                var command = inputs[commandIndex];

                if (string.IsNullOrEmpty(command))
                {
                    Console.WriteLine(HintMessageFind);
                    return;
                }

                var commandsForFind = new Tuple<string, Func<string, ReadOnlyCollection<FileCabinetRecord>>>[]
                {
                new Tuple<string, Func<string, ReadOnlyCollection<FileCabinetRecord>>>("firstName", this.service.FindByFirstName),
                new Tuple<string, Func<string, ReadOnlyCollection<FileCabinetRecord>>>("lastName", this.service.FindByLastName),
                new Tuple<string, Func<string, ReadOnlyCollection<FileCabinetRecord>>>("dateofbirth",this.service.FindByDateoOfBirth),
                };

                var index = Array.FindIndex(commandsForFind, 0, commandsForFind.Length, i => i.Item1.Equals(command, StringComparison.OrdinalIgnoreCase));
                if (index >= 0)
                {
                    const int stringIndex = 1;
                    var stringFind = inputs.Length > 1 ? inputs[stringIndex] : string.Empty;
                    stringFind = stringFind.Trim('"');
                    try
                    {
                        PrintRecords(commandsForFind[index].Item2(stringFind));
                    }
                    catch
                    {
                        Console.WriteLine("records were not found");
                    }
                }
                else
                {
                    PrintMissedCommandInfo(command);
                }
            }
            else
            {
                base.Handle(request);
            }
        }
    }
}
