﻿using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;
using ConsoleTables;
using FileCabinetApp.CommandHandlers;

namespace FileCabinetApp
{
    /// <summary>
    ///   The main class.
    /// </summary>
    public static class Program
    {
        private const string DeveloperName = "Azemsha Oleg";
        private const string HintMessage = "Enter your command, or enter 'help' to get help.";

        private static IRecordValidator? recordValidator;
        private static IFileCabinetService? fileCabinetService;
        private static bool isRunning = true;

        /// <summary>Defines the entry point of the application.</summary>
        /// <param name="args">The arguments.</param>
        public static void Main(string[] args)
        {
            Program.FileCabinetServiceCreate(args);
            Console.WriteLine($"File Cabinet Application, developed by {Program.DeveloperName}");
            Console.WriteLine($"Using '{Program.recordValidator?.ValidateInfo()}' validation rules.");
            Console.WriteLine($"Using '{Program.fileCabinetService?.ServiceInfo()}' type of service.");
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
            var helpHander = new HelpCommandHandler();
            var exitHander = new ExitCommandHandler(() => isRunning = false);
            var statHander = new StatCommandHandler(Program.fileCabinetService);
            var createHander = new CreateCommandHandler(Program.fileCabinetService, Program.recordValidator);
            var exportHandler = new ExportCommandHandler(Program.fileCabinetService);
            var importHandler = new ImportCommandHandler(Program.fileCabinetService);
            var purgeHandler = new PurgeCommandHandler(Program.fileCabinetService);
            var insertHandler = new InsertCommandHandler(Program.fileCabinetService);
            var deleteHandler = new DeleteCommandHandler(Program.fileCabinetService);
            var updateHandler = new UpdateCommandHandler(Program.fileCabinetService);
            var selectHandler = new SelectCommandHandler(Program.fileCabinetService, PrintTable);

            helpHander.SetNext(exitHander).SetNext(statHander).SetNext(createHander).SetNext(exportHandler).SetNext(importHandler).SetNext(purgeHandler).SetNext(insertHandler).SetNext(deleteHandler).SetNext(updateHandler).SetNext(selectHandler);
            return helpHander;
        }

        /// <summary>Reads and analyzes the command line arguments.</summary>
        /// <param name="args">The arguments.</param>
        private static void FileCabinetServiceCreate(string[] args)
        {
            if (args.Length == 0)
            {
                recordValidator = new ValidatorBuilder().CreateDefault();
                fileCabinetService = new FileCabinetMemoryService(recordValidator);
                return;
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
                    recordValidator = i >= args.Length ? new ValidatorBuilder().CreateDefault() : Program.ValidationRules(new string[] { args[i - 1], args[i] });
                }
            }

            recordValidator = recordValidator ?? new ValidatorBuilder().CreateDefault();

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

            for (int i = 0; i < args.Length; i++)
            {
                if (string.Equals(args[i], "-", StringComparison.OrdinalIgnoreCase))
                {
                    i++;
                    if (i < args.Length && string.Equals(args[i], "use-stopwatch", StringComparison.OrdinalIgnoreCase))
                    {
                        fileCabinetService = new ServiceMeter(fileCabinetService);
                    }
                }
            }

            for (int i = 0; i < args.Length; i++)
            {
                if (string.Equals(args[i], "-", StringComparison.OrdinalIgnoreCase))
                {
                    i++;
                    if (i < args.Length && string.Equals(args[i], "use-logger", StringComparison.OrdinalIgnoreCase))
                    {
                        fileCabinetService = new ServiceLogger(fileCabinetService);
                    }
                }
            }
        }

        /// <summary>Reads and analyzes the command line arguments.</summary>
        /// <param name="input">The arguments.</param>
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
                return new ValidatorBuilder().CreateDefault();
            }

            if (command == "-v" || command == "--validation-rules")
            {
                if (rules.Equals("custom", StringComparison.OrdinalIgnoreCase))
                {
                    return new ValidatorBuilder().CreateCustom();
                }

                if (rules.Equals("default", StringComparison.OrdinalIgnoreCase))
                {
                    return new ValidatorBuilder().CreateDefault();
                }
            }

            return new ValidatorBuilder().CreateDefault();
        }

        /// <summary>Reads and analyzes the command line arguments.</summary>
        /// <param name="input">The arguments.</param>
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

        /// <summary>Prints the records.</summary>
        /// <param name="tableName">The table header.</param>
        /// <param name="record">The record.</param>
        /// <exception cref="System.ArgumentNullException">record or tableName.</exception>
        private static void PrintTable(string[]? tableName, IEnumerable<FileCabinetRecord>? record)
        {
            if (record is null)
            {
                throw new ArgumentNullException(nameof(record));
            }

            if (tableName is null)
            {
                throw new ArgumentNullException(nameof(tableName));
            }

            var table = new ConsoleTable(tableName);
            foreach (var recordItem in record)
            {
                var result = new StringBuilder();
                foreach (var name in tableName)
                {
                    if (name.Equals("id", StringComparison.OrdinalIgnoreCase))
                    {
                        result.Append(recordItem.Id);
                    }

                    if (name.Equals("firstname", StringComparison.OrdinalIgnoreCase))
                    {
                        result.Append(recordItem.FirstName);
                    }

                    if (name.Equals("lastname", StringComparison.OrdinalIgnoreCase))
                    {
                        result.Append(recordItem.LastName);
                    }

                    if (name.Equals("dateofbirth", StringComparison.OrdinalIgnoreCase))
                    {
                        result.Append(recordItem.DateOfBirth);
                    }

                    if (name.Equals("property1", StringComparison.OrdinalIgnoreCase))
                    {
                        result.Append(recordItem.Property1);
                    }

                    if (name.Equals("property2", StringComparison.OrdinalIgnoreCase))
                    {
                        result.Append(recordItem.Property2);
                    }

                    if (name.Equals("property3", StringComparison.OrdinalIgnoreCase))
                    {
                        result.Append(recordItem.Property3);
                    }

                    result.Append('?');
                }

                var row = result.ToString().Split("?", StringSplitOptions.RemoveEmptyEntries);
                table.AddRow(row);
            }

            table.Write(Format.Alternative);
        }
    }
}