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

        /// <summary>Initializes a new instance of the <see cref="ExitCommandHandler" /> class.</summary>
        /// <param name="exit">The exit action.</param>
        public ExitCommandHandler(Action exit)
        {
            this.exit = exit;
        }

        /// <summary>Handles the specified request.</summary>
        /// <param name="request">The request.</param>
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
