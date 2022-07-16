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
        private const string HintMessageRemove = "Use: remove [number]";
        private const string HintMessageExport = "Use: export [csv | xml] [directory]";
        private const string HintMessageImport = "Use: import [csv | xml] [directory]";
        private const int CommandHelpIndex = 0;
        private const int DescriptionHelpIndex = 1;
        private const int ExplanationHelpIndex = 2;

        private static IRecordValidator? recordValidator;
        private static IFileCabinetService? fileCabinetService;
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
            new Tuple<string, Action<string>>("export", Export),
            new Tuple<string, Action<string>>("import", Import),
            new Tuple<string, Action<string>>("remove", Remove),
            new Tuple<string, Action<string>>("purge", Purge),
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
            new string[] { "export", "export records", "The 'export' command exports records to the directory" },
            new string[] { "import", "import records", "The 'import' command imports records from the directory" },
            new string[] { "remove", "remove record", "The 'remove' command delete record from cabinet" },
            new string[] { "purge", "purge record", "The 'purge' command performs defragmentation of the data in the file (only 'file' type of service)" },
        };

        public static void Main(string[] args)
        {
            Program.FileCabinetServiceCreate(args);

            Console.WriteLine($"File Cabinet Application, developed by {Program.DeveloperName}");
            Console.WriteLine($"Using '{Program.recordValidator.ValidateInfo()}' validation rules.");
            Console.WriteLine($"Using '{Program.fileCabinetService.ServiceInfo()}' type of service.");
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

        private static void FileCabinetServiceCreate(string[] args)
        {
            if (args.Length == 0)
            {
                recordValidator = new DefaultValidator();
                fileCabinetService = new FileCabinetMemoryService(recordValidator);
            }

            for (int i = 0; i < args.Length; i++)
            {
                if (args[i].Split('=', 2)[0].Equals("--validation-rules", StringComparison.OrdinalIgnoreCase))
                {
                    recordValidator = Program.ValidationRules(new string[] { args[i] });
                }
                else if (string.Equals(args[i], "-v", StringComparison.OrdinalIgnoreCase))
                {
                    i++;
                    recordValidator = i >= args.Length ? new DefaultValidator() : Program.ValidationRules(new string[] { args[i - 1], args[i] });
                }
            }

            recordValidator = recordValidator ?? new DefaultValidator();

            for (int i = 0; i < args.Length; i++)
            {
                if (args[i].Split('=', 2)[0].Equals("--storage", StringComparison.OrdinalIgnoreCase))
                {
                    fileCabinetService = Program.FileCabinetServiceRules(new string[] { args[i] });
                }
                else if (string.Equals(args[i], "-s", StringComparison.OrdinalIgnoreCase))
                {
                    i++;
                    fileCabinetService = i >= args.Length ? new FileCabinetMemoryService(recordValidator) : Program.FileCabinetServiceRules(new string[] { args[i - 1], args[i] });
                }
            }

            fileCabinetService = fileCabinetService ?? new FileCabinetMemoryService(recordValidator);
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
            var recordsIsDelete = Program.fileCabinetService.GetStatDelete();
            Console.WriteLine($"{recordsCount} record(s). {recordsIsDelete} delete record(s)");
        }

        private static FileCabinetRecord EnterData()
        {
            Console.Write("First name: ");
            var firstName = ReadInput(StringConverter, FirstNameValidator);

            Console.Write("Last name: ");
            var lastName = ReadInput(StringConverter, LastNameValidator);

            Console.Write("Date of birth: ");
            var dateOfBirth = ReadInput(DateTimeConverter, DateOfBirthValidator);

            Console.Write("property1 (short): ");
            var property1 = ReadInput(Property1Converter, Property1Validator);

            Console.Write("property2 (decimal): ");
            var property2 = ReadInput(Property2Converter, Property2Validator);

            Console.Write("property3 (char): ");
            var property3 = ReadInput(Property3Converter, Property3Validator);

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
                if (!Program.fileCabinetService.IdExist(id))
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

        private static void Export(string parameters)
        {
            var inputs = parameters != null && parameters != string.Empty ? parameters.Split(' ', 2) : new string[] { string.Empty, string.Empty };
            if (inputs.Length <= 1)
            {
                PrintMissedCommandInfo(parameters);
                Console.WriteLine(HintMessageExport);
                return;
            }

            const int commandIndex = 0;
            const int pathIndex = 1;
            var command = inputs[commandIndex];
            var path = inputs[pathIndex];
            StreamWriter? fstream;
            FileInfo? fileInfo;
            DirectoryInfo? directory;
            try
            {
                fileInfo = new FileInfo(path);
                directory = fileInfo.Directory;
                if (!directory.Exists)
                {
                    throw new ArgumentException(nameof(directory));
                }

                if (fileInfo.Exists)
                {
                    Console.WriteLine($"File is exist - rewrite {path}? [Y/N]");
                    var com = Console.ReadLine() ?? string.Empty;
                    switch (com)
                    {
                        case "y":
                            goto case "Y";
                        case "Y":
                            break;
                        case "n":
                            goto case "N";
                        case "N":
                            Console.WriteLine("Export canceled");
                            return;
                        default:
                            PrintMissedCommandInfo(com);
                            return;
                    }
                }
            }
            catch
            {
                Console.WriteLine($"Export failed: can't open file {path}.");
                Console.WriteLine(HintMessageExport);
                return;
            }

            fstream = new StreamWriter(path, false);
            var snapshot = fileCabinetService.MakeSnapshot();
            switch (command.ToLower(CultureInfo.CurrentCulture))
            {
                case "csv":
                    snapshot.SaveToCsv(fstream);
                    break;
                case "xml":
                    snapshot.SaveToXml(fstream);
                    break;
                default:
                    PrintMissedCommandInfo(command);
                    fstream.Close();
                    return;
            }
            fstream.Close();
            Console.WriteLine($"All records are exported to file {path}.");
            fstream.Close();
        }

        private static void Import(string parameters)
        {
            var inputs = parameters != null && parameters != string.Empty ? parameters.Split(' ', 2) : new string[] { string.Empty, string.Empty };
            if (inputs.Length <= 1)
            {
                PrintMissedCommandInfo(parameters);
                Console.WriteLine(HintMessageExport);
                return;
            }

            const int commandIndex = 0;
            const int pathIndex = 1;
            var command = inputs[commandIndex];
            var path = inputs[pathIndex];
            FileStream? fstream;
            try
            {
                fstream = new FileStream(path, FileMode.Open);
                var snapshot = new FileCabinetServiceSnapshot();

                switch (command.ToLower(CultureInfo.CurrentCulture))
                {
                    case "csv":
                        snapshot.LoadFromCsv(new StreamReader(fstream));
                        fileCabinetService.Restore(snapshot);
                        break;
                    case "xml":
                        snapshot.LoadFromXml(new StreamReader(fstream));
                        fileCabinetService.Restore(snapshot);
                        break;
                    default:
                        PrintMissedCommandInfo(command);
                        fstream.Close();
                        return;
                }

                fstream.Close();
                Console.WriteLine($"from {path}");
            }
            catch
            {
                Console.WriteLine($"Import failed: can't open file {path}.");
                Console.WriteLine(HintMessageImport);
                return;
            }
        }

        private static void Remove(string parameters)
        {
            var command = parameters != null ? parameters : string.Empty;

            if (string.IsNullOrEmpty(command))
            {
                Console.WriteLine(Program.HintMessageRemove);
                return;
            }

            try
            {
                int id = Convert.ToInt32(command, CultureInfo.CurrentCulture);
                if (!Program.fileCabinetService.IdExist(id))
                {
                    Console.WriteLine($"record with number {id} is not exist.");
                    return;
                }

                Program.fileCabinetService.RemoveRecord(id);
            }
            catch
            {
                Console.WriteLine("incorrect format");
            }
        }

        private static void Purge(string parametrs)
        {
            Program.fileCabinetService.PurgeRecords();
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

        private static IFileCabinetService FileCabinetServiceRules(string[] input)
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

            recordValidator = recordValidator ?? new DefaultValidator();
            if (string.IsNullOrEmpty(command) || string.IsNullOrEmpty(rules))
            {
                return new FileCabinetMemoryService(recordValidator);
            }

            if (command == "-s" || command == "--storage")
            {
                if (rules.Equals("file", StringComparison.OrdinalIgnoreCase))
                {
                    return new FileCabinetFilesystemService(recordValidator);
                }

                if (rules.Equals("memory", StringComparison.OrdinalIgnoreCase))
                {
                    return new FileCabinetMemoryService(recordValidator);
                }
            }

            return new FileCabinetMemoryService(recordValidator);
        }

        private static Tuple<bool, string, string> StringConverter(string input)
        {
            var resurt = input ?? string.Empty;
            bool conversion = string.IsNullOrEmpty(resurt) ? false : true;
            input = conversion ? resurt : string.Empty;
            return Tuple.Create<bool, string, string>(conversion, input, resurt);
        }

        private static Tuple<bool, string, DateTime> DateTimeConverter(string input)
        {
            input = input ?? string.Empty;
            bool conversion = DateTime.TryParse(input, out var result);
            return Tuple.Create<bool, string, DateTime>(conversion, input, result);
        }

        private static Tuple<bool, string, short> Property1Converter(string input)
        {
            input = input ?? string.Empty;
            bool conversion = short.TryParse(input, out var result);
            return Tuple.Create<bool, string, short>(conversion, input, result);
        }

        private static Tuple<bool, string, decimal> Property2Converter(string input)
        {
            input = input ?? string.Empty;
            bool conversion = decimal.TryParse(input, out var result);
            return Tuple.Create<bool, string, decimal>(conversion, input, result);
        }

        private static Tuple<bool, string, char> Property3Converter(string input)
        {
            input = input ?? string.Empty;
            bool conversion = char.TryParse(input, out var result);
            return Tuple.Create<bool, string, char>(conversion, input, result);
        }

        private static Tuple<bool, string> FirstNameValidator(string input)
        {
            var resurt = input ?? string.Empty;
            bool validator = recordValidator.ValidateParametrsFirstName(resurt);
            return Tuple.Create<bool, string>(validator, resurt);
        }

        private static Tuple<bool, string> LastNameValidator(string input)
        {
            var resurt = input ?? string.Empty;
            bool validator = recordValidator.ValidateParametrsLastName(input);
            return Tuple.Create<bool, string>(validator, resurt);
        }

        private static Tuple<bool, string> DateOfBirthValidator(DateTime input)
        {
            var resurt = input.ToString(CultureInfo.CurrentCulture) ?? string.Empty;
            bool validator = recordValidator.ValidateParametrsDateOfBirth(input);
            return Tuple.Create<bool, string>(validator, resurt);
        }

        private static Tuple<bool, string> Property1Validator(short input)
        {
            var resurt = input.ToString(CultureInfo.CurrentCulture) ?? string.Empty;
            bool validator = recordValidator.ValidateParametrsProperty1(input);
            return Tuple.Create<bool, string>(validator, resurt);
        }

        private static Tuple<bool, string> Property2Validator(decimal input)
        {
            var resurt = input.ToString(CultureInfo.CurrentCulture) ?? string.Empty;
            bool validator = recordValidator.ValidateParametrsProperty2(input);
            return Tuple.Create<bool, string>(validator, resurt);
        }

        private static Tuple<bool, string> Property3Validator(char input)
        {
            var resurt = input.ToString(CultureInfo.CurrentCulture) ?? string.Empty;
            bool validator = recordValidator.ValidateParametrsProperty3(input);
            return Tuple.Create<bool, string>(validator, resurt);
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