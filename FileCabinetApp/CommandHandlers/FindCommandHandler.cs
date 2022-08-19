using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCabinetApp.CommandHandlers
{
    public class FindCommandHandler : ServiceCommandHandlerBase
    {
        private const string HintMessageFind = "Use: find [firstname | lastname | dateofbirth] [text]";
        private Action<IEnumerable<FileCabinetRecord>> printer;

        public FindCommandHandler(IFileCabinetService? fileCabinetService, Action<IEnumerable<FileCabinetRecord>> printer)
            : base(fileCabinetService)
        {
            this.printer = printer;
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

                var commandsForFind = new Tuple<string, Func<string, IRecordIterator>>[]
                {
                new Tuple<string, Func<string, IRecordIterator>>("firstName", this.service.FindByFirstName),
                new Tuple<string, Func<string, IRecordIterator>>("lastName", this.service.FindByLastName),
                new Tuple<string, Func<string, IRecordIterator>>("dateofbirth", this.service.FindByDateoOfBirth),
                };

                var index = Array.FindIndex(commandsForFind, 0, commandsForFind.Length, i => i.Item1.Equals(command, StringComparison.OrdinalIgnoreCase));
                if (index >= 0)
                {
                    const int stringIndex = 1;
                    var stringFind = inputs.Length > 1 ? inputs[stringIndex] : string.Empty;
                    stringFind = stringFind.Trim('"');
                    var iterator = commandsForFind[index].Item2(stringFind);
                    var result = new List<FileCabinetRecord>();
                    while (iterator.HasMore())
                    {
                        var record = iterator.GetNext();
                        if (record is null)
                        {
                            break;
                        }

                        result.Add(record);
                    }

                    this.printer(result);
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
