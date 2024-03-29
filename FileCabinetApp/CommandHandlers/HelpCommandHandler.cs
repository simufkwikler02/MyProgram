﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCabinetApp.CommandHandlers
{
    /// <summary>
    ///   Represents the command handler "help".
    /// </summary>
    public class HelpCommandHandler : CommandHandlerBase
    {
        private const int CommandHelpIndex = 0;
        private const int DescriptionHelpIndex = 1;
        private const int ExplanationHelpIndex = 2;

        private string[][] helpMessages = new string[][]
        {
            new string[] { "help", "prints the help screen", "The 'help' command prints the help screen." },
            new string[] { "exit", "exits the application", "The 'exit' command exits the application." },
            new string[] { "stat", "prints statistics on records", "The 'stat' command prints statistics on records." },
            new string[] { "create", "saves data to record", "The 'create' command saves data to record" },
            new string[] { "find", "find records", "The 'find' command finds and prints records" },
            new string[] { "export", "export records", "The 'export' command exports records to the directory" },
            new string[] { "import", "import records", "The 'import' command imports records from the directory" },
            new string[] { "purge", "purge record", "The 'purge' command performs defragmentation of the data in the file (only 'file' type of service)" },
            new string[] { "insert", "insert record", "The 'insert' command create and saves data to record" },
            new string[] { "delete", "delete records", "The 'delete' command delete records from the cabinet" },
            new string[] { "update", "update records", "The 'update' command update records in the cabinet" },
            new string[] { "select", "print records", "The 'select' command prints selected records to the console" },
        };

        /// <summary>Handles the specified request.</summary>
        /// <param name="request">The request.</param>
        public override void Handle(AppCommandRequest request)
        {
            if (request.Command.Equals("help", StringComparison.OrdinalIgnoreCase))
            {
                if (!string.IsNullOrEmpty(request.Parameters))
                {
                    var index = Array.FindIndex(this.helpMessages, 0, this.helpMessages.Length, i => string.Equals(i[CommandHelpIndex], request.Parameters, StringComparison.OrdinalIgnoreCase));
                    if (index >= 0)
                    {
                        Console.WriteLine(this.helpMessages[index][ExplanationHelpIndex]);
                    }
                    else
                    {
                        Console.WriteLine($"There is no explanation for '{request.Parameters}' command.");
                    }
                }
                else
                {
                    Console.WriteLine("Available commands:");

                    foreach (var helpMessage in this.helpMessages)
                    {
                        Console.WriteLine("\t{0}\t- {1}", helpMessage[CommandHelpIndex], helpMessage[DescriptionHelpIndex]);
                    }
                }

                Console.WriteLine();
            }
            else
            {
                base.Handle(request);
            }
        }

        /// <summary>Finds the most similar commands.</summary>
        /// <param name="com">The incorrect command.</param>
        /// <returns>
        /// List of the most similar commands.
        /// </returns>
        public List<string> HelpComand(string com)
        {
            var command = new List<string>();
            foreach (var helpMessage in this.helpMessages)
            {
                if (helpMessage[0].IndexOf(com, StringComparison.CurrentCultureIgnoreCase) != -1)
                {
                    command.Add(helpMessage[0]);
                }
                else
                {
                    int contain = 0;
                    foreach (var sim in com.ToCharArray())
                    {
                        if (helpMessage[0].Contains(sim, StringComparison.CurrentCultureIgnoreCase))
                        {
                            contain++;
                        }
                    }

                    if (contain >= com.Length - 1)
                    {
                        command.Add(helpMessage[0]);
                    }
                }
            }

            return command;
        }
    }
}
