using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCabinetApp.CommandHandlers
{
    public class PurgeCommandHandler : CommandHandlerBase
    {
        public PurgeCommandHandler(IRecordValidator validate)
            : base(validate)
        {
        }

        public override void Handle(AppCommandRequest request)
        {
            if (request.Command.Equals("purge", StringComparison.OrdinalIgnoreCase))
            {
                Program.fileCabinetService.PurgeRecords();
            }
            else
            {
                base.Handle(request);
            }
        }
    }
}
