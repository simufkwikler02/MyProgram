using System.Globalization;

namespace FileCabinetApp
{
    public static class Program
    {
        private const string DeveloperName = "Azemsha Oleg";
        private const string HintMessage = "Enter your command, or enter 'help' to get help.";
        private const string HintMessageFind = "Use: find [firstname | lastname | dateofbirth] [text]";
        private const string HintMessageEdit = "Use: edit [number]";
        private const int CommandHelpIndex = 0;
        private const int DescriptionHelpIndex = 1;
        private const int ExplanationHelpIndex = 2;

        private static FileCabinetService fileCabinetService = new FileCabinetCustomService();
        private static bool isRunning = true;

        private static Tuple<string, Action<string>>[] commands = new Tuple<string, Action<string>>[]
        {
            new Tuple<string, Action<string>>("help", PrintHelp),
            new Tuple<string, Action<string>>("exit", Exit),
            new Tuple<string, Action<string>>("stat", Stat),
            new Tuple<string, Action<string>>("create", Create),
            new Tuple<string, Action<string>>("list", List),
            new Tuple<string, Action<string>>("edit", Edit),
            new Tuple<string, Action<string>>("find", Find),
        };

        private static string[][] helpMessages = new string[][]
        {
            new string[] { "help", "prints the help screen", "The 'help' command prints the help screen." },
            new string[] { "exit", "exits the application", "The 'exit' command exits the application." },
            new string[] { "stat", "prints statistics on records", "The 'stat' command prints statistics on records." },
            new string[] { "create", "saves data to record", "The 'create' command saves data to record" },
            new string[] { "list", "prints a list of records", "The 'list' command prints a list of records" },
            new string[] { "edit", "edits records", "The 'edit' command edits records" },
            new string[] { "find", "find records", "The 'find' command finds and prints records" },
        };

        public static void Main(string[] args)
        {
            Console.WriteLine($"File Cabinet Application, developed by {Program.DeveloperName}");
            Console.WriteLine(Program.HintMessage);
            Console.WriteLine();

            do
            {
                Console.Write("> ");
                var line = Console.ReadLine();
                var inputs = line != null ? line.Split(' ', 2) : new string[] { string.Empty, string.Empty };
                const int commandIndex = 0;
                var command = inputs[commandIndex];

                if (string.IsNullOrEmpty(command))
                {
                    Console.WriteLine(Program.HintMessage);
                    continue;
                }

                var index = Array.FindIndex(commands, 0, commands.Length, i => i.Item1.Equals(command, StringComparison.OrdinalIgnoreCase));
                if (index >= 0)
                {
                    const int parametersIndex = 1;
                    var parameters = inputs.Length > 1 ? inputs[parametersIndex] : string.Empty;
                    commands[index].Item2(parameters);
                }
                else
                {
                    PrintMissedCommandInfo(command);
                }
            }
            while (isRunning);
        }

        private static void PrintMissedCommandInfo(string command)
        {
            Console.WriteLine($"There is no '{command}' command.");
            Console.WriteLine();
        }

        private static void PrintHelp(string parameters)
        {
            if (!string.IsNullOrEmpty(parameters))
            {
                var index = Array.FindIndex(helpMessages, 0, helpMessages.Length, i => string.Equals(i[Program.CommandHelpIndex], parameters, StringComparison.OrdinalIgnoreCase));
                if (index >= 0)
                {
                    Console.WriteLine(helpMessages[index][Program.ExplanationHelpIndex]);
                }
                else
                {
                    Console.WriteLine($"There is no explanation for '{parameters}' command.");
                }
            }
            else
            {
                Console.WriteLine("Available commands:");

                foreach (var helpMessage in helpMessages)
                {
                    Console.WriteLine("\t{0}\t- {1}", helpMessage[Program.CommandHelpIndex], helpMessage[Program.DescriptionHelpIndex]);
                }
            }

            Console.WriteLine();
        }

        private static void Exit(string parameters)
        {
            Console.WriteLine("Exiting an application...");
            isRunning = false;
        }

        private static void Stat(string parameters)
        {
            var recordsCount = Program.fileCabinetService.GetStat();
            Console.WriteLine($"{recordsCount} record(s).");
        }

        private static FileCabinetRecord EnterData()
        {
            Console.Write("First name: ");
            var firstName = Console.ReadLine() ?? string.Empty;

            Console.Write("Last name: ");
            var lastName = Console.ReadLine() ?? string.Empty;

            Console.Write("Date of birth: ");
            var line = Console.ReadLine() ?? string.Empty;
            var dateOfBirth = DateTime.Parse(line, CultureInfo.CurrentCulture);

            Console.Write("property1 (short): ");
            line = Console.ReadLine() ?? string.Empty;
            var property1 = Convert.ToInt16(line, CultureInfo.CurrentCulture);

            Console.Write("property2 (decimal): ");
            line = Console.ReadLine() ?? string.Empty;
            var property2 = Convert.ToDecimal(line, CultureInfo.CurrentCulture);

            Console.Write("property3 (char): ");
            line = Console.ReadLine() ?? string.Empty;
            var property3 = Convert.ToChar(line, CultureInfo.CurrentCulture);

            var data = new FileCabinetRecord(firstName, lastName, dateOfBirth, property1, property2, property3);
            return data;
        }

        private static void Create(string parameters)
        {
            try
            {
                var record = EnterData();
                var number = Program.fileCabinetService.CreateRecord(record);
                Console.WriteLine($"Record #{number} is created.");
            }
            catch
            {
                Console.WriteLine("incorrect format, try again.");
                Program.Create(parameters);
            }
        }

        private static void PrintRecords(FileCabinetRecord[] records)
        {
            if (records.Length == 0)
            {
                throw new ArgumentException("records.Length == 0", nameof(records));
            }
            else
            {
                foreach (var record in records)
                {
                    string month = record.DateOfBirth.Month switch
                    {
                        1 => "Jan",
                        2 => "Feb",
                        3 => "Mar",
                        4 => "Apr",
                        5 => "May",
                        6 => "Jun",
                        7 => "Jul",
                        8 => "Aug",
                        9 => "Sep",
                        10 => "Oct",
                        11 => "Nov",
                        12 => "Dec",
                        _ => "incorrect format."
                    };
                    Console.WriteLine();
                    Console.WriteLine($"#{record.Id}, {record.FirstName}, {record.LastName}, {record.DateOfBirth.Year}-{month}-{record.DateOfBirth.Day}");
                    Console.WriteLine($"property1 (short):{record.Property1}  property2 (decimal):{record.Property2}  property3 (char):{record.Property3}");
                }
            }
        }

        private static void List(string parameters)
        {
            var records = Program.fileCabinetService.GetRecords();
            try
            {
                PrintRecords(records);
            }
            catch
            {
                Console.WriteLine("records were not created");
            }
        }

        private static void Edit(string parameters)
        {
            var command = parameters != null ? parameters : string.Empty;

            if (string.IsNullOrEmpty(command))
            {
                Console.WriteLine(Program.HintMessageEdit);
                return;
            }

            try
            {
                int id = Convert.ToInt32(command, CultureInfo.CurrentCulture);
                if (id < 1 || id > Program.fileCabinetService.GetStat())
                {
                    Console.WriteLine($"record with number {id} is not exist.");
                    return;
                }

                var newRecord = EnterData();
                Program.fileCabinetService.EditRecord(id, newRecord);
            }
            catch
            {
                Console.WriteLine("incorrect format");
            }
        }

        private static void Find(string parameters)
        {
            var inputs = parameters != null ? parameters.Split(' ', 2) : new string[] { string.Empty, string.Empty };
            const int commandIndex = 0;
            var command = inputs[commandIndex];

            if (string.IsNullOrEmpty(command))
            {
                Console.WriteLine(Program.HintMessageFind);
                return;
            }

            var commandsForFind = new Tuple<string, Func<string, FileCabinetRecord[]>>[]
            {
            new Tuple<string, Func<string, FileCabinetRecord[]>>("firstName", Program.fileCabinetService.FindByFirstName),
            new Tuple<string, Func<string, FileCabinetRecord[]>>("lastName", Program.fileCabinetService.FindByLastName),
            new Tuple<string, Func<string, FileCabinetRecord[]>>("dateofbirth", Program.fileCabinetService.FindByDateoOfBirth),
            };

            var index = Array.FindIndex(commandsForFind, 0, commandsForFind.Length, i => i.Item1.Equals(command, StringComparison.OrdinalIgnoreCase));
            if (index >= 0)
            {
                const int stringIndex = 1;
                var stringFind = inputs.Length > 1 ? inputs[stringIndex] : string.Empty;
                stringFind = stringFind.Trim('"');
                try
                {
                    PrintRecords(commandsForFind[index].Item2(stringFind));
                }
                catch
                {
                    Console.WriteLine("records were not found");
                }
            }
            else
            {
                PrintMissedCommandInfo(command);
            }
        }
    }
}