using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCabinetApp.CommandHandlers
{
    /// <summary>
    ///  Represents an abstract command handler with a property <see cref="Service" />.
    /// </summary>
    public abstract class ServiceCommandHandlerBase : CommandHandlerBase
    {
        /// <summary>Initializes a new instance of the <see cref="ServiceCommandHandlerBase" /> class.</summary>
        /// <param name="fileCabinetService">The file cabinet service.</param>
        protected ServiceCommandHandlerBase(IFileCabinetService? fileCabinetService)
        {
            this.Service = fileCabinetService;
        }

        /// <summary>Gets the file cabinet service.</summary>
        /// <value>The file cabinet service.</value>
        protected IFileCabinetService? Service { get; }
    }
}
