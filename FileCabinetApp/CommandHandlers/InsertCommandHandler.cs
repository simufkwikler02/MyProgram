using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCabinetApp.CommandHandlers
{
    internal class InsertCommandHandler : ServiceCommandHandlerBase
    {
        private const string HintMessageInsert = "Use: insert (id, firstname, lastname, dateofbirth, Property1, Property2, Property3) values ('[value]', '[value]', ...)";
        private IRecordValidator? recordValidator;

        public InsertCommandHandler(IFileCabinetService? fileCabinetService, IRecordValidator? validate)
            : base(fileCabinetService)
        {
            this.recordValidator = validate;
        }

        public override void Handle(AppCommandRequest request)
        {
            if (request.Command.Equals("insert", StringComparison.OrdinalIgnoreCase))
            {
                try
                {
                    if (string.IsNullOrEmpty(request.Parameters))
                    {
                        Console.WriteLine(HintMessageInsert);
                        return;
                    }

                    var data = request.Parameters.Replace(",", " ", StringComparison.CurrentCulture).Replace("(", string.Empty, StringComparison.CurrentCulture).Replace(")", string.Empty, StringComparison.CurrentCulture).Replace("'", string.Empty, StringComparison.CurrentCulture).Split("values");

                    var name = data[0].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    var value = data[1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    if (value.Length != name.Length)
                    {
                        Console.WriteLine(HintMessageInsert);
                        return;
                    }

                    var record = new FileCabinetRecord();
                    var ind = Array.FindIndex(name, i => i.Equals("id", StringComparison.OrdinalIgnoreCase));
                    record.Id = Convert.ToInt32(value[ind], CultureInfo.CurrentCulture);

                    ind = Array.FindIndex(name, i => i.Equals("firstname", StringComparison.OrdinalIgnoreCase));
                    record.FirstName = value[ind];

                    ind = Array.FindIndex(name, i => i.Equals("lastname", StringComparison.OrdinalIgnoreCase));
                    record.LastName = value[ind];

                    ind = Array.FindIndex(name, i => i.Equals("dateofbirth", StringComparison.OrdinalIgnoreCase));
                    record.DateOfBirth = DateTime.Parse(value[ind], CultureInfo.CurrentCulture);

                    ind = Array.FindIndex(name, i => i.Equals("Property1", StringComparison.OrdinalIgnoreCase));
                    record.Property1 = Convert.ToInt16(value[ind], CultureInfo.CurrentCulture);

                    ind = Array.FindIndex(name, i => i.Equals("Property2", StringComparison.OrdinalIgnoreCase));
                    record.Property2 = Convert.ToDecimal(value[ind], CultureInfo.CurrentCulture);

                    ind = Array.FindIndex(name, i => i.Equals("Property3", StringComparison.OrdinalIgnoreCase));
                    record.Property3 = Convert.ToChar(value[ind], CultureInfo.CurrentCulture);

                    var number = this.service?.CreateRecord(record);
                    Console.WriteLine($"Record #{number} is created.");
                }
                catch
                {
                    Console.WriteLine("incorrect format");
                    return;
                }
            }
            else
            {
                base.Handle(request);
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
            bool validator = this.recordValidator.ValidateParametrFirstName(resurt);
            return Tuple.Create<bool, string>(validator, resurt);
        }

        private Tuple<bool, string> LastNameValidator(string input)
        {
            var resurt = input ?? string.Empty;
            bool validator = this.recordValidator.ValidateParametrLastName(resurt);
            return Tuple.Create<bool, string>(validator, resurt);
        }

        private Tuple<bool, string> DateOfBirthValidator(DateTime input)
        {
            var resurt = input.ToString(CultureInfo.CurrentCulture) ?? string.Empty;
            bool validator = this.recordValidator.ValidateParametrDateOfBirth(input);
            return Tuple.Create<bool, string>(validator, resurt);
        }

        private Tuple<bool, string> Property1Validator(short input)
        {
            var resurt = input.ToString(CultureInfo.CurrentCulture) ?? string.Empty;
            bool validator = this.recordValidator.ValidateParametrProperty1(input);
            return Tuple.Create<bool, string>(validator, resurt);
        }

        private Tuple<bool, string> Property2Validator(decimal input)
        {
            var resurt = input.ToString(CultureInfo.CurrentCulture) ?? string.Empty;
            bool validator = this.recordValidator.ValidateParametrProperty2(input);
            return Tuple.Create<bool, string>(validator, resurt);
        }

        private Tuple<bool, string> Property3Validator(char input)
        {
            var resurt = input.ToString(CultureInfo.CurrentCulture) ?? string.Empty;
            bool validator = this.recordValidator.ValidateParametrProperty3(input);
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
