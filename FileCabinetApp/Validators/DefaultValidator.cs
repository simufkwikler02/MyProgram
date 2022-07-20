using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCabinetApp
{
    public class DefaultValidator : IRecordValidator
    {
        private readonly string rules = "default";
        private readonly DateTime minDate = new DateTime(1950, 6, 1);

        public bool ValidateParametrs(FileCabinetRecord record)
        {
            var result = new DefaultFirstNameValidator().ValidateParametrs(record);
            result &= new DefaultLastNameValidator().ValidateParametrs(record);
            result &= new DefaultDateOfBirthValidator().ValidateParametrs(record);
            result &= new DefaultProperty1Validator().ValidateParametrs(record);
            result &= new DefaultProperty2Validator().ValidateParametrs(record);
            result &= new DefaultProperty3Validator().ValidateParametrs(record);

            return result;
        }

        public string ValidateInfo()
        {
            return this.rules;
        }

        public DateTime DateTimeMin()
        {
            return this.minDate;
        }
    }
}
