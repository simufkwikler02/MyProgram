using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCabinetApp
{
    /// <summary>
    ///   Represents a validation block for a date of birth.
    /// </summary>
    public class DateOfBirthValidator : IRecordBlocksValidator
    {
        private readonly DateTime from;
        private readonly DateTime to;

        /// <summary>Initializes a new instance of the <see cref="DateOfBirthValidator" /> class.</summary>
        /// <param name="from">The minimum value date.</param>
        /// <param name="to">The maximum value date.</param>
        public DateOfBirthValidator(DateTime from, DateTime to)
        {
            this.to = to;
            this.from = from;
        }

        /// <summary>Validates the date of birth in a record.</summary>
        /// <param name="record">The record.</param>
        /// <returns>
        ///   <see langword="true"/> if the record passed validation, <see langword="false"/> otherwise  .
        /// </returns>
        public bool ValidateParametrs(FileCabinetRecord record)
        {
            if (this.from > record.DateOfBirth || this.to < record.DateOfBirth)
            {
                return false;
            }

            return true;
        }
    }
}
