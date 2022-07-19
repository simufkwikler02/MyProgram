using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCabinetApp.CommandHandlers
{
    public class ExitCommandHandler : CommandHandlerBase
    {
        public ExitCommandHandler(IRecordValidator validate)
            : base(validate)
        {
        }

        public override void Handle(AppCommandRequest request)
        {
            if (request.Command.Equals("exit", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Exiting an application...");
                Program.isRunning = false;
            }
            else
            {
                base.Handle(request);
            }
        }
    }
}
