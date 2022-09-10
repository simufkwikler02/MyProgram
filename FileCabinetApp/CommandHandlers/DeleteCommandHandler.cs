using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCabinetApp.CommandHandlers
{
    /// <summary>
    ///   Represents the command handler "delete".
    /// </summary>
    public class DeleteCommandHandler : ServiceCommandHandlerBase
    {
        private const string HintMessageDelete = "Use: delete where [name] = '[value]'";

        /// <summary>Initializes a new instance of the <see cref="DeleteCommandHandler" /> class.</summary>
        /// <param name="fileCabinetService">The file cabinet service.</param>
        public DeleteCommandHandler(IFileCabinetService? fileCabinetService)
            : base(fileCabinetService)
        {
        }

        /// <summary>Handles the specified request.</summary>
        /// <param name="request">The request.</param>
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

                List<int> indexDelete = new List<int>();

                var recordsDelete = this.Service?.FindRecords(data[0], data[1]) ?? new List<FileCabinetRecord>();

                foreach (var record in recordsDelete)
                {
                    var index = this.Service?.DeleteRecord(record) ?? -1;

                    if (index != -1)
                    {
                        indexDelete.Add(index);
                    }
                }

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
