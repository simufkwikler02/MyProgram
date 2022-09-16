using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCabinetApp
{
    /// <summary>
    ///   Represents a validation block for a last name.
    /// </summary>
    public class LastNameValidator : IRecordBlocksValidator
    {
        private readonly int minLength;
        private readonly int maxLength;

        /// <summary>Initializes a new instance of the <see cref="LastNameValidator" /> class.</summary>
        /// <param name="minLength">The minimum length last name.</param>
        /// <param name="maxLength">The maximum length last name.</param>
        public LastNameValidator(int minLength, int maxLength)
        {
            this.minLength = minLength;
            this.maxLength = maxLength;
        }

        /// <summary>Validates the last name in a record.</summary>
        /// <param name="record">The record.</param>
        /// <returns>
        ///   <see langword="true"/> if the record passed validation, <see langword="false"/> otherwise  .
        /// </returns>
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
