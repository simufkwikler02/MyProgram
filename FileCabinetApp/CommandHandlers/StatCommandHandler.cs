using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCabinetApp.CommandHandlers
{
    public class StatCommandHandler : CommandHandlerBase
    {
        public StatCommandHandler(IFileCabinetService fileCabinetService, IRecordValidator validate)
            : base(fileCabinetService, validate)
        {
        }

        public override void Handle(AppCommandRequest request)
        {
            if (request.Command.Equals("stat", StringComparison.OrdinalIgnoreCase))
            {
                var recordsCount = this.service.GetStat();
                var recordsIsDelete = this.service.GetStatDelete();
                Console.WriteLine($"{recordsCount} record(s). {recordsIsDelete} delete record(s)");
            }
            else
            {
                base.Handle(request);
            }
        }
    }
}
