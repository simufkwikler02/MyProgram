using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCabinetApp.CommandHandlers
{
    public class ListCommandHandler : ServiceCommandHandlerBase
    {
        private Action<IEnumerable<FileCabinetRecord>> printer;

        public ListCommandHandler(IFileCabinetService fileCabinetService, Action<IEnumerable<FileCabinetRecord>> printer)
            : base(fileCabinetService)
        {
            this.printer = printer;
        }

        public override void Handle(AppCommandRequest request)
        {
            if (request.Command.Equals("list", StringComparison.OrdinalIgnoreCase))
            {
                var records = this.service.GetRecords();
                this.printer(records);
            }
            else
            {
                base.Handle(request);
            }
        }
    }
}
