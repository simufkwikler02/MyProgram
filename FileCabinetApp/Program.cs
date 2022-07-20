using System.Collections.ObjectModel;
using System.Globalization;
using FileCabinetApp.CommandHandlers;

namespace FileCabinetApp
{
    public static class Program
    {
        private const string DeveloperName = "Azemsha Oleg";
        private const string HintMessage = "Enter your command, or enter 'help' to get help.";

        private static IRecordValidator? recordValidator;
        private static IFileCabinetService? fileCabinetService;
        private static bool isRunning = true;

        public static void Main(string[] args)
        {
            Program.FileCabinetServiceCreate(args);
            recordValidator = recordValidator ?? new DefaultValidator();
            fileCabinetService = fileCabinetService ?? new FileCabinetMemoryService(recordValidator);

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
                const int parametersIndex = 1;
                var command = inputs[commandIndex];

                if (string.IsNullOrEmpty(command))
                {
                    Console.WriteLine(Program.HintMessage);
                    continue;
                }

                var parameters = inputs.Length > 1 ? inputs[parametersIndex] : string.Empty;

                var commandHandler = CreateCommandHandlers();
                commandHandler.Handle(new AppCommandRequest(command, parameters));
            }
            while (isRunning);
        }

        private static ICommandHandler CreateCommandHandlers()
        {
            recordValidator = recordValidator ?? new DefaultValidator();
            var recordPrinter = new DefaultRecordPrinter();

            var helpHander = new HelpCommandHandler();
            var exitHander = new ExitCommandHandler(() => isRunning = false);
            var statHander = new StatCommandHandler(Program.fileCabinetService, Program.recordValidator);
            var createHander = new CreateCommandHandler(Program.fileCabinetService, Program.recordValidator);
            var listHander = new ListCommandHandler(Program.fileCabinetService, Program.recordValidator, recordPrinter);
            var editHander = new EditCommandHandler(Program.fileCabinetService, Program.recordValidator);
            var findHandler = new FindCommandHandler(Program.fileCabinetService, Program.recordValidator, recordPrinter);
            var exportHandler = new ExportCommandHandler(Program.fileCabinetService, Program.recordValidator);
            var importHandler = new ImportCommandHandler(Program.fileCabinetService, Program.recordValidator);
            var removeHandler = new RemoveCommandHandler(Program.fileCabinetService, Program.recordValidator);
            var purgeHandler = new PurgeCommandHandler(Program.fileCabinetService, Program.recordValidator);

            helpHander.SetNext(exitHander).SetNext(statHander).SetNext(createHander).SetNext(listHander).SetNext(editHander).SetNext(findHandler).SetNext(exportHandler).SetNext(importHandler).SetNext(removeHandler).SetNext(purgeHandler);
            return helpHander;
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
    }
}