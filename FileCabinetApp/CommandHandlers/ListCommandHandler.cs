using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCabinetApp.CommandHandlers
{
    public class ListCommandHandler : ServiceCommandHandlerBase
    {
        private IRecordPrinter printer;

        public ListCommandHandler(IFileCabinetService fileCabinetService, IRecordValidator validate, IRecordPrinter printer)
            : base(fileCabinetService, validate)
        {
            this.printer = printer;
        }

        public override void Handle(AppCommandRequest request)
        {
            if (request.Command.Equals("list", StringComparison.OrdinalIgnoreCase))
            {
                var records = this.service.GetRecords();
                this.printer.Print(records);
            }
            else
            {
                base.Handle(request);
            }
        }
    }
}
