using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCabinetApp.CommandHandlers
{
    public abstract class CommandHandlerBase : ICommandHandler
    {
        private ICommandHandler? nextHandler;

        /// <summary>Sets the next command handler.</summary>
        /// <param name="handler">The command handler.</param>
        /// <returns>
        ///   The next command handler.
        /// </returns>
        public ICommandHandler SetNext(ICommandHandler handler)
        {
            this.nextHandler = handler;
            return handler;
        }

        /// <summary>Handles the specified request.</summary>
        /// <param name="request">The request.</param>
        public virtual void Handle(AppCommandRequest request)
        {
            if (this.nextHandler != null)
            {
                this.nextHandler.Handle(request);
            }
            else
            {
                PrintMissedCommandInfo(request.Command);
            }
        }

        /// <summary>Prints the missed command and the most similar commands.</summary>
        /// <param name="command">The missed command.</param>
        protected static void PrintMissedCommandInfo(string command)
        {
            Console.WriteLine($"There is no '{command}' command. See 'help' command.");
            Console.WriteLine();
            var helpcomand = new HelpCommandHandler().HelpComand(command);
            if (helpcomand.Count > 1)
            {
                Console.WriteLine();
                Console.WriteLine("The most similar commands are");
            }
            else if (helpcomand.Count == 1)
            {
                Console.WriteLine();
                Console.WriteLine("The most similar command is");
            }
            else
            {
                return;
            }

            foreach (var item in helpcomand)
            {
                Console.WriteLine($"        {item}");
            }

            Console.WriteLine();
        }
    }
}
