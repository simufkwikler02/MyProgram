using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCabinetApp.CommandHandlers
{
    /// <summary>
    ///  Provides a set of properties that are used to represent command input to the console.
    /// </summary>
    public class AppCommandRequest
    {
        /// <summary> Initializes a new instance of the <see cref="AppCommandRequest"/> class. </summary>
        /// <param name="command">The name of the command.</param>
        /// <param name="parametrs">The parameters of the command.</param>
        public AppCommandRequest(string command, string parametrs)
        {
            this.Command = command;
            this.Parameters = parametrs;
        }

        /// <summary> Gets the name of the command. </summary>
        /// <value>The name of the command.</value>
        public string Command { get; }

        /// <summary> Gets parameters of the command. </summary>
        /// <value>The parameters of the command.</value>
        public string Parameters { get; }
    }
}
