using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCabinetApp.CommandHandlers
{
    public class CommandHandler : CommandHandlerBase
    {
        private const string HintMessageFind = "Use: find [firstname | lastname | dateofbirth] [text]";
        private const string HintMessageEdit = "Use: edit [number]";
        private const string HintMessageRemove = "Use: remove [number]";
        private const string HintMessageExport = "Use: export [csv | xml] [directory]";
        private const string HintMessageImport = "Use: import [csv | xml] [directory]";
        private const int CommandHelpIndex = 0;
        private const int DescriptionHelpIndex = 1;
        private const int ExplanationHelpIndex = 2;

        private Tuple<string, Action<string>>[] commands;
        private IRecordValidator? recordValidator;

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

        public CommandHandler(IRecordValidator recordValidator)
        {
            this.recordValidator = recordValidator;
            this.commands = new Tuple<string, Action<string>>[]
            {
            new Tuple<string, Action<string>>("help", this.PrintHelp),
            new Tuple<string, Action<string>>("exit", this.Exit),
            new Tuple<string, Action<string>>("stat", this.Stat),
            new Tuple<string, Action<string>>("create", this.Create),
            new Tuple<string, Action<string>>("list", this.List),
            new Tuple<string, Action<string>>("edit", this.Edit),
            new Tuple<string, Action<string>>("find", this.Find),
            new Tuple<string, Action<string>>("export", this.Export),
            new Tuple<string, Action<string>>("import", this.Import),
            new Tuple<string, Action<string>>("remove", this.Remove),
            new Tuple<string, Action<string>>("purge", this.Purge),
            };
        }

        public void Handle(AppCommandRequest request)
        {
            var index = Array.FindIndex(this.commands, 0, this.commands.Length, i => i.Item1.Equals(request.Command, StringComparison.OrdinalIgnoreCase));
            if (index >= 0)
            {
                this.commands[index].Item2(request.Parameters);
            }
            else
            {
                PrintMissedCommandInfo(request.Command);
            }
        }

        private void PrintHelp(string parameters)
        {
            if (!string.IsNullOrEmpty(parameters))
            {
                var index = Array.FindIndex(this.helpMessages, 0, this.helpMessages.Length, i => string.Equals(i[CommandHelpIndex], parameters, StringComparison.OrdinalIgnoreCase));
                if (index >= 0)
                {
                    Console.WriteLine(this.helpMessages[index][ExplanationHelpIndex]);
                }
                else
                {
                    Console.WriteLine($"There is no explanation for '{parameters}' command.");
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

        private void Exit(string parameters)
        {
            Console.WriteLine("Exiting an application...");
            Program.isRunning = false;
        }

        private void Stat(string parameters)
        {
            var recordsCount = Program.fileCabinetService.GetStat();
            var recordsIsDelete = Program.fileCabinetService.GetStatDelete();
            Console.WriteLine($"{recordsCount} record(s). {recordsIsDelete} delete record(s)");
        }

        private void Create(string parameters)
        {
            try
            {
                var record = this.EnterData();
                var number = Program.fileCabinetService.CreateRecord(record);
                Console.WriteLine($"Record #{number} is created.");
            }
            catch
            {
                Console.WriteLine("incorrect format, try again.");
                this.Create(parameters);
            }
        }

        private FileCabinetRecord EnterData()
        {
            Console.Write("First name: ");
            var firstName = this.ReadInput(this.StringConverter, this.FirstNameValidator);

            Console.Write("Last name: ");
            var lastName = this.ReadInput(this.StringConverter, this.LastNameValidator);

            Console.Write("Date of birth: ");
            var dateOfBirth = this.ReadInput(this.DateTimeConverter, this.DateOfBirthValidator);

            Console.Write("property1 (short): ");
            var property1 = this.ReadInput(this.Property1Converter, this.Property1Validator);

            Console.Write("property2 (decimal): ");
            var property2 = this.ReadInput(this.Property2Converter, this.Property2Validator);

            Console.Write("property3 (char): ");
            var property3 = this.ReadInput(this.Property3Converter, this.Property3Validator);

            var data = new FileCabinetRecord(firstName, lastName, dateOfBirth, property1, property2, property3);
            return data;
        }

        private void List(string parameters)
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

        private void Edit(string parameters)
        {
            var command = parameters != null ? parameters : string.Empty;

            if (string.IsNullOrEmpty(command))
            {
                Console.WriteLine(HintMessageEdit);
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

        private void Find(string parameters)
        {
            var inputs = parameters != null ? parameters.Split(' ', 2) : new string[] { string.Empty, string.Empty };
            const int commandIndex = 0;
            var command = inputs[commandIndex];

            if (string.IsNullOrEmpty(command))
            {
                Console.WriteLine(HintMessageFind);
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

        private void Export(string parameters)
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
            var snapshot = Program.fileCabinetService.MakeSnapshot();
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

        private void Import(string parameters)
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
                        Program.fileCabinetService.Restore(snapshot);
                        break;
                    case "xml":
                        snapshot.LoadFromXml(new StreamReader(fstream));
                        Program.fileCabinetService.Restore(snapshot);
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

        private void Remove(string parameters)
        {
            var command = parameters != null ? parameters : string.Empty;

            if (string.IsNullOrEmpty(command))
            {
                Console.WriteLine(HintMessageRemove);
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

        private void Purge(string parametrs)
        {
            Program.fileCabinetService.PurgeRecords();
        }

        private Tuple<bool, string, string> StringConverter(string input)
        {
            var resurt = input ?? string.Empty;
            bool conversion = string.IsNullOrEmpty(resurt) ? false : true;
            input = conversion ? resurt : string.Empty;
            return Tuple.Create<bool, string, string>(conversion, input, resurt);
        }

        private Tuple<bool, string, DateTime> DateTimeConverter(string input)
        {
            input = input ?? string.Empty;
            bool conversion = DateTime.TryParse(input, out var result);
            return Tuple.Create<bool, string, DateTime>(conversion, input, result);
        }

        private Tuple<bool, string, short> Property1Converter(string input)
        {
            input = input ?? string.Empty;
            bool conversion = short.TryParse(input, out var result);
            return Tuple.Create<bool, string, short>(conversion, input, result);
        }

        private Tuple<bool, string, decimal> Property2Converter(string input)
        {
            input = input ?? string.Empty;
            bool conversion = decimal.TryParse(input, out var result);
            return Tuple.Create<bool, string, decimal>(conversion, input, result);
        }

        private Tuple<bool, string, char> Property3Converter(string input)
        {
            input = input ?? string.Empty;
            bool conversion = char.TryParse(input, out var result);
            return Tuple.Create<bool, string, char>(conversion, input, result);
        }

        private Tuple<bool, string> FirstNameValidator(string input)
        {
            var resurt = input ?? string.Empty;
            bool validator = this.recordValidator.ValidateParametrsFirstName(resurt);
            return Tuple.Create<bool, string>(validator, resurt);
        }

        private Tuple<bool, string> LastNameValidator(string input)
        {
            var resurt = input ?? string.Empty;
            bool validator = this.recordValidator.ValidateParametrsLastName(input);
            return Tuple.Create<bool, string>(validator, resurt);
        }

        private Tuple<bool, string> DateOfBirthValidator(DateTime input)
        {
            var resurt = input.ToString(CultureInfo.CurrentCulture) ?? string.Empty;
            bool validator = this.recordValidator.ValidateParametrsDateOfBirth(input);
            return Tuple.Create<bool, string>(validator, resurt);
        }

        private Tuple<bool, string> Property1Validator(short input)
        {
            var resurt = input.ToString(CultureInfo.CurrentCulture) ?? string.Empty;
            bool validator = this.recordValidator.ValidateParametrsProperty1(input);
            return Tuple.Create<bool, string>(validator, resurt);
        }

        private Tuple<bool, string> Property2Validator(decimal input)
        {
            var resurt = input.ToString(CultureInfo.CurrentCulture) ?? string.Empty;
            bool validator = this.recordValidator.ValidateParametrsProperty2(input);
            return Tuple.Create<bool, string>(validator, resurt);
        }

        private Tuple<bool, string> Property3Validator(char input)
        {
            var resurt = input.ToString(CultureInfo.CurrentCulture) ?? string.Empty;
            bool validator = this.recordValidator.ValidateParametrsProperty3(input);
            return Tuple.Create<bool, string>(validator, resurt);
        }

        private T ReadInput<T>(Func<string, Tuple<bool, string, T>> converter, Func<T, Tuple<bool, string>> validator)
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
