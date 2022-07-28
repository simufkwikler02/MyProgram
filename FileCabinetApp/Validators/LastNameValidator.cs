using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCabinetApp
{
    public class LastNameValidator : IRecordValidator
    {
        private int minLength;
        private int maxLength;

        public LastNameValidator(int minLength, int maxLength)
        {
            this.minLength = minLength;
            this.maxLength = maxLength;
        }

        public bool ValidateParametrs(FileCabinetRecord record)
        {
            return this.ValidateParametrs(record.LastName);
        }

        public bool ValidateParametrs(string input)
        {
            if (string.IsNullOrEmpty(input) || input.Length < this.minLength || input.Length > this.maxLength)
            {
                return false;
            }

            return true;
        }
    }
}
