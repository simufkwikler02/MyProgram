using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCabinetApp
{
    /// <summary>
    ///   Represents a validation block for a first name.
    /// </summary>
    public class FirstNameValidator : IRecordBlocksValidator
    {
        private readonly int minLength;
        private readonly int maxLength;

        /// <summary>Initializes a new instance of the <see cref="FirstNameValidator" /> class.</summary>
        /// <param name="minLength">The minimum length first name.</param>
        /// <param name="maxLength">The maximum length first name.</param>
        public FirstNameValidator(int minLength, int maxLength)
        {
            this.minLength = minLength;
            this.maxLength = maxLength;
        }

        /// <summary>Validates the first name in a record.</summary>
        /// <param name="record">The record.</param>
        /// <returns>
        ///   <see langword="true"/> if the record passed validation, <see langword="false"/> otherwise  .
        /// </returns>
        public bool ValidateParametrs(FileCabinetRecord record)
        {
            if (string.IsNullOrEmpty(record.FirstName) || record.FirstName.Length < this.minLength || record.FirstName.Length > this.maxLength)
            {
                return false;
            }

            return true;
        }
    }
}
