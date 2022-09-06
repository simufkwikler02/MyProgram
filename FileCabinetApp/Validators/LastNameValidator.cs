using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCabinetApp
{
    public class LastNameValidator : IRecordBlocksValidator
    {
        private readonly int minLength;
        private readonly int maxLength;

        public LastNameValidator(int minLength, int maxLength)
        {
            this.minLength = minLength;
            this.maxLength = maxLength;
        }

        public bool ValidateParametrs(FileCabinetRecord record)
        {
            if (string.IsNullOrEmpty(record.LastName) || record.LastName.Length < this.minLength || record.LastName.Length > this.maxLength)
            {
                return false;
            }

            return true;
        }
    }
}
