using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCabinetApp.CommandHandlers
{
    public class DeleteCommandHandler : ServiceCommandHandlerBase
    {
        private const string HintMessageDelete = "Use: delete where [name] = '[value]'";

        public DeleteCommandHandler(IFileCabinetService? fileCabinetService)
            : base(fileCabinetService)
        {
        }

        public override void Handle(AppCommandRequest request)
        {
            if (request.Command.Equals("delete", StringComparison.OrdinalIgnoreCase))
            {
                if (string.IsNullOrEmpty(request.Parameters))
                {
                    Console.WriteLine(HintMessageDelete);
                    return;
                }

                var data = request.Parameters.Replace("where", string.Empty, StringComparison.OrdinalIgnoreCase).Replace("'", string.Empty, StringComparison.OrdinalIgnoreCase).Trim().Split("=", StringSplitOptions.TrimEntries);

                if (data.Length < 2)
                {
                    Console.WriteLine(HintMessageDelete);
                    return;
                }

                int index;
                List<int> indexDelete = new List<int>();
                do
                {
                    index = this.service.DeleteRecord(data[0], data[1]);

                    if (index != -1)
                    {
                        indexDelete.Add(index);
                    }
                }
                while (index != -1);

                if (indexDelete.Count > 1)
                {
                    Console.Write("Records ");
                    Console.Write($"#{indexDelete[0]}");
                    for (int i = 1; i < indexDelete.Count; i++)
                    {
                        Console.Write($", #{indexDelete[i]}");
                    }

                    Console.WriteLine(" are deleted.");
                }
                else if (indexDelete.Count == 1)
                {
                    Console.WriteLine($"Record #{indexDelete[0]} is deleted");
                }
                else
                {
                    Console.WriteLine("records are not found.");
                }
            }
            else
            {
                base.Handle(request);
            }
        }
    }
}
