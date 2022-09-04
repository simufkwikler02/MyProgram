using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCabinetApp.CommandHandlers
{
    public abstract class ServiceCommandHandlerBase : CommandHandlerBase
    {
        protected ServiceCommandHandlerBase(IFileCabinetService? fileCabinetService)
        {
            this.Service = fileCabinetService;
        }

        protected IFileCabinetService? Service { get; }
    }
}
