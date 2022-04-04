using System.Collections.ObjectModel;
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

        private static FileCabinetService fileCabinetService = new FileCabinetService(new DefaultValidator());
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

        private static Func<string, Tuple<bool, string, string>> stringConverter = StringConverterFunc;
        private static Func<string, Tuple<bool, string, DateTime>> dateConverter = DateConverterFunc;
        private static Func<string, Tuple<bool, string, short>> property1Converter = Property1ConverterFunc;
        private static Func<string, Tuple<bool, string, decimal>> property2Converter = Property2ConverterFunc;
        private static Func<string, Tuple<bool, string, char>> property3Converter = Property3ConverterFunc;

        private static Func<string, Tuple<bool, string>> firstNameValidator = StringValidatorFunc;
        private static Func<string, Tuple<bool, string>> lastNameValidator = StringValidatorFunc;
        private static Func<DateTime, Tuple<bool, string>> dateOfBirthValidator = DateValidatorFunc;
        private static Func<short, Tuple<bool, string>> property1Validator = Property1ValidatorFunc;
        private static Func<decimal, Tuple<bool, string>> property2Validator = Property2ValidatorFunc;
        private static Func<char, Tuple<bool, string>> property3Validator = Property3ValidatorFunc;

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
            fileCabinetService = new FileCabinetService(args.Length == 0 ? new DefaultValidator() : Program.ValidationRules(args));
            Console.WriteLine($"File Cabinet Application, developed by {Program.DeveloperName}");
            Console.WriteLine($"Using {Program.fileCabinetService.ValidateInfo()} validation rules.");
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
            var firstName = ReadInput(stringConverter, firstNameValidator);

            Console.Write("Last name: ");
            var lastName = ReadInput(stringConverter, lastNameValidator);

            Console.Write("Date of birth: ");
            var dateOfBirth = ReadInput(dateConverter, dateOfBirthValidator);

            Console.Write("property1 (short): ");
            var property1 = ReadInput(property1Converter, property1Validator);

            Console.Write("property2 (decimal): ");
            var property2 = ReadInput(property2Converter, property2Validator);

            Console.Write("property3 (char): ");
            var property3 = ReadInput(property3Converter, property3Validator);

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

        private static void PrintRecords(ReadOnlyCollection<FileCabinetRecord> records)
        {
            if (records.Count == 0)
            {
                throw new ArgumentException("records.Count == 0", nameof(records));
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

            var commandsForFind = new Tuple<string, Func<string, ReadOnlyCollection<FileCabinetRecord>>>[]
            {
            new Tuple<string, Func<string, ReadOnlyCollection<FileCabinetRecord>>>("firstName", Program.fileCabinetService.FindByFirstName),
            new Tuple<string, Func<string, ReadOnlyCollection<FileCabinetRecord>>>("lastName", Program.fileCabinetService.FindByLastName),
            new Tuple<string, Func<string, ReadOnlyCollection<FileCabinetRecord>>>("dateofbirth", Program.fileCabinetService.FindByDateoOfBirth),
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

        private static IRecordValidator ValidationRules(string[] input)
        {
            var inputs = new string[] { string.Empty, string.Empty };
            if (input.Length == 1)
            {
                inputs = input[0].Split('=', 2);
            }
            else
            {
                inputs[0] = input[0];
                inputs[1] = input[1];
            }

            const int commandIndex = 0;
            const int commandrules = 1;
            var command = inputs[commandIndex];
            var rules = inputs[commandrules];
            if (string.IsNullOrEmpty(command) || string.IsNullOrEmpty(rules))
            {
                return new DefaultValidator();
            }

            if (command == "-v" || command == "--validation-rules")
            {
                if (rules.Equals("custom", StringComparison.OrdinalIgnoreCase))
                {
                    return new CustomValidator();
                }

                if (rules.Equals("default", StringComparison.OrdinalIgnoreCase))
                {
                    return new DefaultValidator();
                }
            }

            return new DefaultValidator();
        }

        private static Tuple<bool, string, string> StringConverterFunc(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return new Tuple<bool, string, string>(false, input, string.Empty);
            }
            else
            {
                return new Tuple<bool, string, string>(true, input, input);
            }
        }

        private static Tuple<bool, string, DateTime> DateConverterFunc(string input)
        {
            DateTime result;
            if (DateTime.TryParse(input, out result))
            {
                return new Tuple<bool, string, DateTime>(true, input, result);
            }
            else
            {
                return new Tuple<bool, string, DateTime>(false, input, result);
            }
        }

        private static Tuple<bool, string, short> Property1ConverterFunc(string input)
        {
            short result;
            if (short.TryParse(input, out result))
            {
                return new Tuple<bool, string, short>(true, input, result);
            }
            else
            {
                return new Tuple<bool, string, short>(false, input, result);
            }
        }

        private static Tuple<bool, string, decimal> Property2ConverterFunc(string input)
        {
            decimal result;
            if (decimal.TryParse(input, out result))
            {
                return new Tuple<bool, string, decimal>(true, input, result);
            }
            else
            {
                return new Tuple<bool, string, decimal>(false, input, result);
            }
        }

        private static Tuple<bool, string, char> Property3ConverterFunc(string input)
        {
            char result;
            if (char.TryParse(input, out result))
            {
                return new Tuple<bool, string, char>(true, input, result);
            }
            else
            {
                return new Tuple<bool, string, char>(false, input, result);
            }
        }

        private static Tuple<bool, string> StringValidatorFunc(string input)
        {
            var validate = fileCabinetService.ValidateParametrString(input);
            return new Tuple<bool, string>(validate, input);
        }

        private static Tuple<bool, string> DateValidatorFunc(DateTime input)
        {
            var validate = fileCabinetService.ValidateParametrDate(input);
            return new Tuple<bool, string>(validate, input.ToString(CultureInfo.CurrentCulture));
        }

        private static Tuple<bool, string> Property1ValidatorFunc(short input)
        {
            var validate = fileCabinetService.ValidateParametrProperty1(input);
            return new Tuple<bool, string>(validate, input.ToString(CultureInfo.CurrentCulture));
        }

        private static Tuple<bool, string> Property2ValidatorFunc(decimal input)
        {
            var validate = fileCabinetService.ValidateParametrProperty2(input);
            return new Tuple<bool, string>(validate, input.ToString(CultureInfo.CurrentCulture));
        }

        private static Tuple<bool, string> Property3ValidatorFunc(char input)
        {
            var validate = fileCabinetService.ValidateParametrProperty3(input);
            return new Tuple<bool, string>(validate, input.ToString(CultureInfo.CurrentCulture));
        }

        private static T ReadInput<T>(Func<string, Tuple<bool, string, T>> converter, Func<T, Tuple<bool, string>> validator)
        {
            do
            {
                T value;

                var input = Console.ReadLine() ?? string.Empty;
                var conversionResult = converter(input);

                if (!conversionResult.Item1)
                {
                    Console.WriteLine($"Conversion failed: {conversionResult.Item2}. Please, correct your input.");
                    continue;
                }

                value = conversionResult.Item3;

                var validationResult = validator(value);
                if (!validationResult.Item1)
                {
                    Console.WriteLine($"Validation failed: {validationResult.Item2}. Please, correct your input.");
                    continue;
                }

                return value;
            }
            while (true);
        }
    }
}