using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCabinetApp.CommandHandlers
{
    public class ListCommandHandler : CommandHandlerBase
    {
        public ListCommandHandler(IRecordValidator validate)
            : base(validate)
        {
        }

        public override void Handle(AppCommandRequest request)
        {
            if (request.Command.Equals("list", StringComparison.OrdinalIgnoreCase))
            {
                var records = Program.fileCabinetService.GetRecords();
                try
                {
                    PrintRecords(records);
                }
                catch
                {
                    Console.WriteLine("records were not created");
                }
            }
            else
            {
                base.Handle(request);
            }
        }
    }
}
