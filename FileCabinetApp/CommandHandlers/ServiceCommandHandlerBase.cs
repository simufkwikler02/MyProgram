using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCabinetApp.CommandHandlers
{
    public abstract class ServiceCommandHandlerBase : CommandHandlerBase
    {
        protected IRecordValidator recordValidator;
        protected IFileCabinetService service;

        protected ServiceCommandHandlerBase(IFileCabinetService fileCabinetService, IRecordValidator validate)
        {
            this.service = fileCabinetService;
            this.recordValidator = validate;
        }

    }
}
