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
        private readonly int minLength = 2;
        private readonly int maxLength = 60;
        private readonly DateTime from = new DateTime(1950, 6, 1);
        private readonly DateTime to = DateTime.Now;
        private readonly short minProperty1 = -200;
        private readonly short maxProperty1 = 200;
        private readonly short minProperty2 = -50;
        private readonly short maxProperty2 = 50;
        private readonly char[] banSymbols = new char[] { 's', 'c', 'p' };

        public bool ValidateParametrs(FileCabinetRecord record)
        {
            var result = new FirstNameValidator(this.minLength, this.maxLength).ValidateParametrs(record);
            result &= new LastNameValidator(this.minLength, this.maxLength).ValidateParametrs(record);
            result &= new DateOfBirthValidator(this.from, this.to).ValidateParametrs(record);
            result &= new Property1Validator(this.minProperty1, this.maxProperty1).ValidateParametrs(record);
            result &= new Property2Validator(this.minProperty2, this.maxProperty2).ValidateParametrs(record);
            result &= new Property3Validator(this.banSymbols).ValidateParametrs(record);

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
