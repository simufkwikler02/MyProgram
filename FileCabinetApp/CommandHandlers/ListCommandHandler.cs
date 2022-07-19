using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCabinetApp.CommandHandlers
{
    public class ListCommandHandler : CommandHandlerBase
    {
        public ListCommandHandler(IFileCabinetService fileCabinetService, IRecordValidator validate)
            : base(fileCabinetService, validate)
        {
        }

        public override void Handle(AppCommandRequest request)
        {
            if (request.Command.Equals("list", StringComparison.OrdinalIgnoreCase))
            {
                var records = this.service.GetRecords();
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
