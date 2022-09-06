using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCabinetApp
{
    public class ValidatorBuilder
    {
        private readonly List<IRecordBlocksValidator> validators = new List<IRecordBlocksValidator>();
        private readonly ValidateParametrs parametrs = new ValidateParametrs();

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
