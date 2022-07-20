using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCabinetApp
{
    public class CustomValidator : CompositeValidator
    {
        private static readonly string rules = "custom";
        private static readonly int minLength = 2;
        private static readonly int maxLength = 60;
        private static readonly DateTime from = new DateTime(1950, 6, 1);
        private static readonly DateTime to = DateTime.Now;
        private static readonly short minProperty1 = -200;
        private static readonly short maxProperty1 = 200;
        private static readonly short minProperty2 = -50;
        private static readonly short maxProperty2 = 50;
        private static readonly char[] banSymbols = new char[] { 's', 'c', 'p' };

        public CustomValidator()
            : base(new IRecordValidator[]
            {
                new FirstNameValidator(minLength, maxLength),
                new LastNameValidator(minLength, maxLength),
                new DateOfBirthValidator(from, to),
                new Property1Validator(minProperty1, maxProperty1),
                new Property2Validator(minProperty2, maxProperty2),
                new Property3Validator(banSymbols),
            })
        {
        }

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
            return rules;
        }

        public DateTime DateTimeMin()
        {
            return to;
        }
    }
}
