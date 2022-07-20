using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCabinetApp
{
    public class CustomValidator : IRecordValidator
    {
        private readonly string rules = "custom";
        private readonly DateTime minDate = new DateTime(1950, 6, 1);

        public bool ValidateParametrs(FileCabinetRecord record)
        {
            var result = new CustomFirstNameValidator().ValidateParametrs(record);
            result &= new CustomLastNameValidator().ValidateParametrs(record);
            result &= new CustomDateOfBirthValidator().ValidateParametrs(record);
            result &= new CustomProperty1Validator().ValidateParametrs(record);
            result &= new CustomProperty2Validator().ValidateParametrs(record);
            result &= new CustomProperty3Validator().ValidateParametrs(record);

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
