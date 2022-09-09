using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCabinetApp.CommandHandlers
{
    public class CreateCommandHandler : ServiceCommandHandlerBase
    {
        private IRecordValidator? recordValidator;

        /// <summary>Initializes a new instance of the <see cref="CreateCommandHandler" /> class.</summary>
        /// <param name="fileCabinetService">The file cabinet service.</param>
        /// <param name="validate">The service validator.</param>
        public CreateCommandHandler(IFileCabinetService? fileCabinetService, IRecordValidator? validate)
            : base(fileCabinetService)
        {
            this.recordValidator = validate;
        }

        /// <summary>Handles the specified request.</summary>
        /// <param name="request">The request.</param>
        public override void Handle(AppCommandRequest request)
        {
            if (request.Command.Equals("create", StringComparison.OrdinalIgnoreCase))
            {
                try
                {
                    var record = this.EnterData();
                    var number = this.Service?.CreateRecord(record);
                    Console.WriteLine($"Record #{number} is created.");
                }
                catch
                {
                    Console.WriteLine("incorrect format, try again.");
                    this.Handle(request);
                }
            }
            else
            {
                base.Handle(request);
            }
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

        private FileCabinetRecord EnterData()
        {
            Console.Write("First name: ");
            var firstName = ReadInput(this.StringConverter, this.FirstNameValidator);

            Console.Write("Last name: ");
            var lastName = ReadInput(this.StringConverter, this.LastNameValidator);

            Console.Write("Date of birth: ");
            var dateOfBirth = ReadInput(this.DateTimeConverter, this.DateOfBirthValidator);

            Console.Write("property1 (short): ");
            var property1 = ReadInput(this.Property1Converter, this.Property1Validator);

            Console.Write("property2 (decimal): ");
            var property2 = ReadInput(this.Property2Converter, this.Property2Validator);

            Console.Write("property3 (char): ");
            var property3 = ReadInput(this.Property3Converter, this.Property3Validator);

            var data = new FileCabinetRecord(firstName, lastName, dateOfBirth, property1, property2, property3);
            return data;
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
            bool validator = this.recordValidator?.ValidateParametrFirstName(resurt) ?? false;
            return Tuple.Create<bool, string>(validator, resurt);
        }

        private Tuple<bool, string> LastNameValidator(string input)
        {
            var resurt = input ?? string.Empty;
            bool validator = this.recordValidator?.ValidateParametrLastName(resurt) ?? false;
            return Tuple.Create<bool, string>(validator, resurt);
        }

        private Tuple<bool, string> DateOfBirthValidator(DateTime input)
        {
            var resurt = input.ToString(CultureInfo.CurrentCulture) ?? string.Empty;
            bool validator = this.recordValidator?.ValidateParametrDateOfBirth(input) ?? false;
            return Tuple.Create<bool, string>(validator, resurt);
        }

        private Tuple<bool, string> Property1Validator(short input)
        {
            var resurt = input.ToString(CultureInfo.CurrentCulture) ?? string.Empty;
            bool validator = this.recordValidator?.ValidateParametrProperty1(input) ?? false;
            return Tuple.Create<bool, string>(validator, resurt);
        }

        private Tuple<bool, string> Property2Validator(decimal input)
        {
            var resurt = input.ToString(CultureInfo.CurrentCulture) ?? string.Empty;
            bool validator = this.recordValidator?.ValidateParametrProperty2(input) ?? false;
            return Tuple.Create<bool, string>(validator, resurt);
        }

        private Tuple<bool, string> Property3Validator(char input)
        {
            var resurt = input.ToString(CultureInfo.CurrentCulture) ?? string.Empty;
            bool validator = this.recordValidator?.ValidateParametrProperty3(input) ?? false;
            return Tuple.Create<bool, string>(validator, resurt);
        }
    }
}
