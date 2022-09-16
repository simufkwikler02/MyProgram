using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCabinetApp.CommandHandlers
{
    /// <summary>
    ///   Represents the command handler interface.
    /// </summary>
    public interface ICommandHandler
    {
        /// <summary>Sets the next command handler.</summary>
        /// <param name="handler">The command handler.</param>
        /// <returns>
        ///     The next command handler.
        /// </returns>
        public ICommandHandler SetNext(ICommandHandler handler);

        /// <summary>Handles the specified request.</summary>
        /// <param name="request">The request.</param>
        public void Handle(AppCommandRequest request);
    }
}
