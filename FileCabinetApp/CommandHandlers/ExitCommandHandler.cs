using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCabinetApp.CommandHandlers
{
    public class ExitCommandHandler : CommandHandlerBase
    {
        private Action exit;

        public ExitCommandHandler(Action exit)
        {
            this.exit = exit;
        }

        public override void Handle(AppCommandRequest request)
        {
            if (request.Command.Equals("exit", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Exiting an application...");
                this.exit();
            }
            else
            {
                base.Handle(request);
            }
        }
    }
}
