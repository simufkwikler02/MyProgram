using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCabinetApp
{
    public class ValidatorBuilder
    {
        private List<IRecordBlocksValidator> validators = new List<IRecordBlocksValidator>();
        private ValidateParametrs parametrs = new ValidateParametrs();

        public void ValidateFirstName(int min, int max)
        {
            this.parametrs.MinLengthFirstName = min;
            this.parametrs.MaxLengthFirstName = max;
            this.validators.Add(new FirstNameValidator(min, max));
        }

        public void ValidateLastName(int min, int max)
        {
            this.parametrs.MinLengthLastName = min;
            this.parametrs.MaxLengthLastName = max;
            this.validators.Add(new LastNameValidator(min, max));
        }

        public void ValidateDateOfBirth(DateTime from, DateTime to)
        {
            this.parametrs.From = from;
            this.parametrs.To = to;
            this.validators.Add(new DateOfBirthValidator(from, to));
        }

        public void ValidateProperty1(short minProperty1, short maxProperty1)
        {
            this.parametrs.MinProperty1 = minProperty1;
            this.parametrs.MaxProperty1 = maxProperty1;
            this.validators.Add(new Property1Validator(minProperty1, maxProperty1));
        }

        public void ValidateProperty2(decimal minProperty2, decimal maxProperty2)
        {
            this.parametrs.MinProperty2 = minProperty2;
            this.parametrs.MaxProperty2 = maxProperty2;
            this.validators.Add(new Property2Validator(minProperty2, maxProperty2));
        }

        public void ValidateProperty3(char[] banSymbols)
        {
            this.parametrs.BanSymbols = banSymbols;
            this.validators.Add(new Property3Validator(banSymbols));
        }

        public IRecordValidator Create()
        {
            this.parametrs.ValidateInfo = "personal";
            return new CompositeValidator(this.validators, this.parametrs);
        }

        public IRecordValidator CreateDefault()
        {
            this.parametrs.MinLengthFirstName = 2;
            this.parametrs.MaxLengthFirstName = 60;
            this.parametrs.MinLengthLastName = 2;
            this.parametrs.MaxLengthLastName = 60;
            this.parametrs.From = new DateTime(1950, 6, 1);
            this.parametrs.To = DateTime.Now;
            this.parametrs.MinProperty1 = -100;
            this.parametrs.MaxProperty1 = 100;
            this.parametrs.MinProperty2 = -10;
            this.parametrs.MaxProperty2 = 10;
            this.parametrs.BanSymbols = new char[] { 'h', 'e', 'l', 'p' };

            this.validators.Clear();
            this.validators.Add(new FirstNameValidator(this.parametrs.MinLengthFirstName, this.parametrs.MaxLengthFirstName));
            this.validators.Add(new LastNameValidator(this.parametrs.MinLengthLastName, this.parametrs.MaxLengthLastName));
            this.validators.Add(new DateOfBirthValidator(this.parametrs.From, this.parametrs.To));
            this.validators.Add(new Property1Validator(this.parametrs.MinProperty1, this.parametrs.MaxProperty1));
            this.validators.Add(new Property2Validator(this.parametrs.MinProperty2, this.parametrs.MaxProperty2));
            this.validators.Add(new Property3Validator(this.parametrs.BanSymbols));
            this.parametrs.ValidateInfo = "default";
            return new CompositeValidator(this.validators, this.parametrs);
        }

        public IRecordValidator CreateCustom()
        {
            this.parametrs.MinLengthFirstName = 2;
            this.parametrs.MaxLengthFirstName = 60;
            this.parametrs.MinLengthLastName = 2;
            this.parametrs.MaxLengthLastName = 60;
            this.parametrs.From = new DateTime(1950, 6, 1);
            this.parametrs.To = DateTime.Now;
            this.parametrs.MinProperty1 = -200;
            this.parametrs.MaxProperty1 = 200;
            this.parametrs.MinProperty2 = -50;
            this.parametrs.MaxProperty2 = 50;
            this.parametrs.BanSymbols = new char[] { 's', 'c', 'p' };

            this.validators.Clear();
            this.validators.Add(new FirstNameValidator(this.parametrs.MinLengthFirstName, this.parametrs.MaxLengthFirstName));
            this.validators.Add(new LastNameValidator(this.parametrs.MinLengthLastName, this.parametrs.MaxLengthLastName));
            this.validators.Add(new DateOfBirthValidator(this.parametrs.From, this.parametrs.To));
            this.validators.Add(new Property1Validator(this.parametrs.MinProperty1, this.parametrs.MaxProperty1));
            this.validators.Add(new Property2Validator(this.parametrs.MinProperty2, this.parametrs.MaxProperty2));
            this.validators.Add(new Property3Validator(this.parametrs.BanSymbols));
            this.parametrs.ValidateInfo = "custom";
            return new CompositeValidator(this.validators, this.parametrs);
        }
    }
}
