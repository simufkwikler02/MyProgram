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
        private bool result;

        public bool ValidateParametrsFirstName(string input)
        {
            if (string.IsNullOrEmpty(input) || input.Length < 2 || input.Length > 60)
            {
                this.result = false;
                return false;
            }

            return true;
        }

        public bool ValidateParametrsLastName(string input)
        {
            if (string.IsNullOrEmpty(input) || input.Length < 2 || input.Length > 60)
            {
                this.result = false;
                return false;
            }

            return true;
        }

        public bool ValidateParametrsDateOfBirth(DateTime input)
        {
            if (this.minDate > input || DateTime.Now < input)
            {
                this.result = false;
                return false;
            }

            return true;
        }

        public bool ValidateParametrsProperty1(short input)
        {
            if (input < 1 || input > 100)
            {
                this.result = false;
                return false;
            }

            return true;
        }

        public bool ValidateParametrsProperty2(decimal input)
        {
            if (input > 2)
            {
                this.result = false;
                return false;
            }

            return true;
        }

        public bool ValidateParametrsProperty3(char input)
        {
            if (input == 'l')
            {
                this.result = false;
                return false;
            }

            return true;
        }

        public bool ValidateParametrs(FileCabinetRecord newRecord)
        {
            this.result = true;
            this.ValidateParametrsFirstName(newRecord.FirstName);
            this.ValidateParametrsLastName(newRecord.LastName);
            this.ValidateParametrsDateOfBirth(newRecord.DateOfBirth);
            this.ValidateParametrsProperty1(newRecord.Property1);
            this.ValidateParametrsProperty2(newRecord.Property2);
            this.ValidateParametrsProperty3(newRecord.Property3);

            return this.result;
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
