using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCabinetApp.CommandHandlers
{
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
            new string[] { "list", "prints a list of records", "The 'list' command prints a list of records" },
            new string[] { "edit", "edits records", "The 'edit' command edits records" },
            new string[] { "find", "find records", "The 'find' command finds and prints records" },
            new string[] { "export", "export records", "The 'export' command exports records to the directory" },
            new string[] { "import", "import records", "The 'import' command imports records from the directory" },
            new string[] { "remove", "remove record", "The 'remove' command delete record from cabinet" },
            new string[] { "purge", "purge record", "The 'purge' command performs defragmentation of the data in the file (only 'file' type of service)" },
        };

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
    }
}
