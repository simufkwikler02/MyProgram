using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCabinetApp
{
    /// <summary>
    ///  Provides a set of methods and properties that can be used to create a validator.
    /// </summary>
    public class ValidatorBuilder
    {
        private readonly List<IRecordBlocksValidator> validators = new List<IRecordBlocksValidator>();
        private readonly ValidateParametrs parametrs = new ValidateParametrs();

        /// <summary>Adds a validator block in list to validate the first name.</summary>
        /// <param name="min">The minimum length first name.</param>
        /// <param name="max">The maximum length first name.</param>
        public void ValidateFirstName(int min, int max)
        {
            this.parametrs.MinLengthFirstName = min;
            this.parametrs.MaxLengthFirstName = max;
            this.validators.Add(new FirstNameValidator(min, max));
        }

        /// <summary>Adds a validator block in list to validate the last name.</summary>
        /// <param name="min">The minimum length last name.</param>
        /// <param name="max">The maximum length last name.</param>
        public void ValidateLastName(int min, int max)
        {
            this.parametrs.MinLengthLastName = min;
            this.parametrs.MaxLengthLastName = max;
            this.validators.Add(new LastNameValidator(min, max));
        }

        /// <summary>Adds a validator block in list to validate the date of birth.</summary>
        /// <param name="from">The minimum value date.</param>
        /// <param name="to">The maximum value date.</param>
        public void ValidateDateOfBirth(DateTime from, DateTime to)
        {
            this.parametrs.From = from;
            this.parametrs.To = to;
            this.validators.Add(new DateOfBirthValidator(from, to));
        }

        /// <summary>Adds a validator block in list to validate the property1.</summary>
        /// <param name="minProperty1">The minimum value property1.</param>
        /// <param name="maxProperty1">The maximum value property1.</param>
        public void ValidateProperty1(short minProperty1, short maxProperty1)
        {
            this.parametrs.MinProperty1 = minProperty1;
            this.parametrs.MaxProperty1 = maxProperty1;
            this.validators.Add(new Property1Validator(minProperty1, maxProperty1));
        }

        /// <summary>Adds a validator block in list to validate the property2.</summary>
        /// <param name="minProperty2">The minimum value property2.</param>
        /// <param name="maxProperty2">The maximum value property2.</param>
        public void ValidateProperty2(decimal minProperty2, decimal maxProperty2)
        {
            this.parametrs.MinProperty2 = minProperty2;
            this.parametrs.MaxProperty2 = maxProperty2;
            this.validators.Add(new Property2Validator(minProperty2, maxProperty2));
        }

        /// <summary>Adds a validator block in list to validate the property3.</summary>
        /// <param name="banSymbols">The ban symbols for property3.</param>
        public void ValidateProperty3(char[] banSymbols)
        {
            this.parametrs.BanSymbols = banSymbols;
            this.validators.Add(new Property3Validator(banSymbols));
        }

        /// <summary>Creates a validator from a list with validators blocks.</summary>
        /// <returns>
        ///   Instance <see cref="CompositeValidator" /> with personal settings.
        /// </returns>
        public IRecordValidator Create()
        {
            this.parametrs.ValidateInfo = "personal";
            return new CompositeValidator(this.validators, this.parametrs);
        }

        /// <summary>Creates a default validator.</summary>
        /// <returns>
        ///   Default validator <see cref="CompositeValidator" />.
        /// </returns>
        public IRecordValidator CreateDefault()
        {
            this.parametrs.SetDefaultParametrs();

            this.validators.Clear();
            this.validators.Add(new FirstNameValidator(this.parametrs.MinLengthFirstName, this.parametrs.MaxLengthFirstName));
            this.validators.Add(new LastNameValidator(this.parametrs.MinLengthLastName, this.parametrs.MaxLengthLastName));
            this.validators.Add(new DateOfBirthValidator(this.parametrs.From, this.parametrs.To));
            this.validators.Add(new Property1Validator(this.parametrs.MinProperty1, this.parametrs.MaxProperty1));
            this.validators.Add(new Property2Validator(this.parametrs.MinProperty2, this.parametrs.MaxProperty2));
            this.validators.Add(new Property3Validator(this.parametrs.BanSymbols));
            return new CompositeValidator(this.validators, this.parametrs);
        }

        /// <summary>Creates a custom validator.</summary>
        /// <returns>
        ///   Custom validator <see cref="CompositeValidator" />.
        /// </returns>
        public IRecordValidator CreateCustom()
        {
            this.parametrs.SetCustomParametrs();

            this.validators.Clear();
            this.validators.Add(new FirstNameValidator(this.parametrs.MinLengthFirstName, this.parametrs.MaxLengthFirstName));
            this.validators.Add(new LastNameValidator(this.parametrs.MinLengthLastName, this.parametrs.MaxLengthLastName));
            this.validators.Add(new DateOfBirthValidator(this.parametrs.From, this.parametrs.To));
            this.validators.Add(new Property1Validator(this.parametrs.MinProperty1, this.parametrs.MaxProperty1));
            this.validators.Add(new Property2Validator(this.parametrs.MinProperty2, this.parametrs.MaxProperty2));
            this.validators.Add(new Property3Validator(this.parametrs.BanSymbols));
            return new CompositeValidator(this.validators, this.parametrs);
        }
    }
}
