using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCabinetApp.CommandHandlers
{
    public class StatCommandHandler : ServiceCommandHandlerBase
    {
        /// <summary>Initializes a new instance of the <see cref="StatCommandHandler" /> class.</summary>
        /// <param name="fileCabinetService">The file cabinet service.</param>
        public StatCommandHandler(IFileCabinetService? fileCabinetService)
            : base(fileCabinetService)
        {
        }

        /// <summary>Handles the specified request.</summary>
        /// <param name="request">The request.</param>
        public override void Handle(AppCommandRequest request)
        {
            if (request.Command?.Equals("stat", StringComparison.OrdinalIgnoreCase) ?? false)
            {
                var recordsCount = this.Service?.GetStat();
                var recordsIsDelete = this.Service?.GetStatDelete();
                Console.WriteLine($"{recordsCount} record(s). {recordsIsDelete} delete record(s)");
            }
            else
            {
                base.Handle(request);
            }
        }
    }
}
