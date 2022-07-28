using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCabinetApp
{
    public class CompositeValidator
    {
        private List<IRecordValidator> validators;

        public CompositeValidator(IEnumerable<IRecordValidator> validators)
        {
            this.validators = new List<IRecordValidator>(validators);
        }

        public bool ValidateParametrs(FileCabinetRecord record)
        {
            var result = true;
            foreach (var validator in this.validators)
            {
                result &= validator.ValidateParametrs(record);
            }

            return result;
        }

        public bool ValidateParametrFirstName(string input)
        {
            var result = true;
            var ind = this.validators.FindIndex(i => i.GetType() == typeof(FirstNameValidator));
            var record = new FileCabinetRecord();
            record.FirstName = input;
            result &= this.validators[ind].ValidateParametrs(record);
            return result;
        }

        public bool ValidateParametrLastName(string input)
        {
            var result = true;
            var ind = this.validators.FindIndex(i => i.GetType() == typeof(LastNameValidator));
            var record = new FileCabinetRecord();
            record.LastName = input;
            result &= this.validators[ind].ValidateParametrs(record);
            return result;
        }

        public bool ValidateParametrDateOfBirth(DateTime input)
        {
            var result = true;
            var ind = this.validators.FindIndex(i => i.GetType() == typeof(DateOfBirthValidator));
            var record = new FileCabinetRecord();
            record.DateOfBirth = input;
            result &= this.validators[ind].ValidateParametrs(record);
            return result;
        }

        public bool ValidateParametrProperty1(short input)
        {
            var result = true;
            var ind = this.validators.FindIndex(i => i.GetType() == typeof(Property1Validator));
            var record = new FileCabinetRecord();
            record.Property1 = input;
            result &= this.validators[ind].ValidateParametrs(record);
            return result;
        }

        public bool ValidateParametrProperty2(decimal input)
        {
            var result = true;
            var ind = this.validators.FindIndex(i => i.GetType() == typeof(Property2Validator));
            var record = new FileCabinetRecord();
            record.Property2 = input;
            result &= this.validators[ind].ValidateParametrs(record);
            return result;
        }

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
