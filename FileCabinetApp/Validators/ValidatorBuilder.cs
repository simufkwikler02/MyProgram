using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCabinetApp
{
    public class ValidatorBuilder
    {
        private List<IRecordValidator> validators = new List<IRecordValidator>();

        public void ValidateFirstName(int min, int max)
        {
            this.validators.Add(new FirstNameValidator(min, max));
        }

        public void ValidateLastName(int min, int max)
        {
            this.validators.Add(new LastNameValidator(min, max));
        }

        public void ValidateDateOfBirth(DateTime from, DateTime to)
        {
            this.validators.Add(new DateOfBirthValidator(from, to));
        }

        public void ValidateProperty1(short minProperty1, short maxProperty1)
        {
            this.validators.Add(new Property1Validator(minProperty1, maxProperty1));
        }

        public void ValidateProperty2(decimal minProperty2, decimal maxProperty2)
        {
            this.validators.Add(new Property2Validator(minProperty2, maxProperty2));
        }

        public void ValidateProperty3(char[] banSymbols)
        {
            this.validators.Add(new Property3Validator(banSymbols));
        }

        public CompositeValidator Create()
        {
            return new CompositeValidator(this.validators);
        }

        public CompositeValidator CreateDefault()
        {
            DateTime minDate = new DateTime(1950, 6, 1);
            int minLength = 2;
            int maxLength = 60;
            DateTime from = new DateTime(1950, 6, 1);
            DateTime to = DateTime.Now;
            short minProperty1 = -100;
            short maxProperty1 = 100;
            short minProperty2 = -10;
            short maxProperty2 = 10;
            char[] banSymbols = new char[] { 'h', 'e', 'l', 'p' };

            this.validators.Clear();
            this.validators.Add(new FirstNameValidator(minLength, maxLength));
            this.validators.Add(new LastNameValidator(minLength, maxLength));
            this.validators.Add(new DateOfBirthValidator(from, to));
            this.validators.Add(new Property1Validator(minProperty1, maxProperty1));
            this.validators.Add(new Property2Validator(minProperty2, maxProperty2));
            this.validators.Add(new Property3Validator(banSymbols));
            return new CompositeValidator(this.validators);
        }

        public CompositeValidator CreateCustom()
        {
            DateTime minDate = new DateTime(1950, 6, 1);
            int minLength = 2;
            int maxLength = 60;
            DateTime from = new DateTime(1950, 6, 1);
            DateTime to = DateTime.Now;
            short minProperty1 = -200;
            short maxProperty1 = 200;
            short minProperty2 = -50;
            short maxProperty2 = 50;
            char[] banSymbols = new char[] { 's', 'c', 'p' };

            this.validators.Clear();
            this.validators.Add(new FirstNameValidator(minLength, maxLength));
            this.validators.Add(new LastNameValidator(minLength, maxLength));
            this.validators.Add(new DateOfBirthValidator(from, to));
            this.validators.Add(new Property1Validator(minProperty1, maxProperty1));
            this.validators.Add(new Property2Validator(minProperty2, maxProperty2));
            this.validators.Add(new Property3Validator(banSymbols));
            return new CompositeValidator(this.validators);
        }
    }
}
