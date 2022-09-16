using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCabinetApp.CommandHandlers
{
    /// <summary>
    ///   Represents the command handler "purge".
    /// </summary>
    public class PurgeCommandHandler : ServiceCommandHandlerBase
    {
        /// <summary>Initializes a new instance of the <see cref="PurgeCommandHandler" /> class.</summary>
        /// <param name="fileCabinetService">The file cabinet service.</param>
        public PurgeCommandHandler(IFileCabinetService? fileCabinetService)
            : base(fileCabinetService)
        {
        }

        /// <summary>Handles the specified request.</summary>
        /// <param name="request">The request.</param>
        public override void Handle(AppCommandRequest request)
        {
            if (request.Command.Equals("purge", StringComparison.OrdinalIgnoreCase))
            {
                this.Service?.PurgeRecords();
            }
            else
            {
                base.Handle(request);
            }
        }
    }
}
