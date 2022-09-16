using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCabinetApp
{
    /// <summary>
    ///   Represents a validator with a set of methods that can validate records.
    /// </summary>
    public class CompositeValidator : IRecordValidator
    {
        private readonly ValidateParametrs parametrs;
        private readonly List<IRecordBlocksValidator> validators;

        /// <summary>Initializes a new instance of the <see cref="CompositeValidator"/> class.</summary>
        /// <param name="validators">The list with validation blocks.</param>
        /// <param name="parametrs">The instance that contains validation rules.</param>
        public CompositeValidator(IEnumerable<IRecordBlocksValidator> validators, ValidateParametrs parametrs)
        {
            this.validators = new List<IRecordBlocksValidator>(validators);
            this.parametrs = parametrs;
        }

        /// <summary>Return the name of the validation rules.</summary>
        /// <returns>
        ///   The name of the validation rules <see cref="string"/>.
        /// </returns>
        public string ValidateInfo()
        {
            return this.parametrs.ValidateInfo;
        }

        /// <summary>Return instance that contains validation rules.</summary>
        /// <returns>
        ///   The instance <see cref="FileCabinetApp.ValidateParametrs"/>.
        /// </returns>
        public ValidateParametrs ParametrsInfo()
        {
            return this.parametrs;
        }

        /// <summary>Validates the record.</summary>
        /// <param name="record">The record.</param>
        /// <returns>
        ///   <see langword="true"/> if the record passed validation, <see langword="false"/> otherwise  .
        /// </returns>
        public bool ValidateParametrs(FileCabinetRecord record)
        {
            var result = true;
            foreach (var validator in this.validators)
            {
                result &= validator.ValidateParametrs(record);
            }

            return result;
        }

        /// <summary>Validates the first name.</summary>
        /// <param name="input">The first name.</param>
        /// <returns>
        ///   <see langword="true"/> if the input passed validation, <see langword="false"/> otherwise  .
        /// </returns>
        public bool ValidateParametrFirstName(string input)
        {
            var result = true;
            var ind = this.validators.FindIndex(i => i.GetType() == typeof(FirstNameValidator));
            var record = new FileCabinetRecord();
            record.FirstName = input;
            result &= this.validators[ind].ValidateParametrs(record);
            return result;
        }

        /// <summary>Validates the last name.</summary>
        /// <param name="input">The last name.</param>
        /// <returns>
        ///   <see langword="true"/> if the input passed validation, <see langword="false"/> otherwise  .
        /// </returns>
        public bool ValidateParametrLastName(string input)
        {
            var result = true;
            var ind = this.validators.FindIndex(i => i.GetType() == typeof(LastNameValidator));
            var record = new FileCabinetRecord();
            record.LastName = input;
            result &= this.validators[ind].ValidateParametrs(record);
            return result;
        }

        /// <summary>Validates the date of birth.</summary>
        /// <param name="input">The date of birth.</param>
        /// <returns>
        ///   <see langword="true"/> if the input passed validation, <see langword="false"/> otherwise  .
        /// </returns>
        public bool ValidateParametrDateOfBirth(DateTime input)
        {
            var result = true;
            var ind = this.validators.FindIndex(i => i.GetType() == typeof(DateOfBirthValidator));
            var record = new FileCabinetRecord();
            record.DateOfBirth = input;
            result &= this.validators[ind].ValidateParametrs(record);
            return result;
        }

        /// <summary>Validates the property1.</summary>
        /// <param name="input">The property1.</param>
        /// <returns>
        ///   <see langword="true"/> if the input passed validation, <see langword="false"/> otherwise  .
        /// </returns>
        public bool ValidateParametrProperty1(short input)
        {
            var result = true;
            var ind = this.validators.FindIndex(i => i.GetType() == typeof(Property1Validator));
            var record = new FileCabinetRecord();
            record.Property1 = input;
            result &= this.validators[ind].ValidateParametrs(record);
            return result;
        }

        /// <summary>Validates the property2.</summary>
        /// <param name="input">The property2.</param>
        /// <returns>
        ///   <see langword="true"/> if the input passed validation, <see langword="false"/> otherwise  .
        /// </returns>
        public bool ValidateParametrProperty2(decimal input)
        {
            var result = true;
            var ind = this.validators.FindIndex(i => i.GetType() == typeof(Property2Validator));
            var record = new FileCabinetRecord();
            record.Property2 = input;
            result &= this.validators[ind].ValidateParametrs(record);
            return result;
        }

        /// <summary>Validates the property3.</summary>
        /// <param name="input">The property3.</param>
        /// <returns>
        ///   <see langword="true"/> if the input passed validation, <see langword="false"/> otherwise  .
        /// </returns>
        public bool ValidateParametrProperty3(char input)
        {
            var result = true;
            var ind = this.validators.FindIndex(i => i.GetType() == typeof(Property3Validator));
            var record = new FileCabinetRecord();
            record.Property3 = input;
            result &= this.validators[ind].ValidateParametrs(record);
            return result;
        }
    }
}
