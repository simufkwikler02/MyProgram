using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCabinetApp
{
    /// <summary>
    ///   Represents a validation block for a property2.
    /// </summary>
    public class Property2Validator : IRecordBlocksValidator
    {
        private readonly decimal minValue;
        private readonly decimal maxValue;

        /// <summary>Initializes a new instance of the <see cref="Property2Validator" /> class.</summary>
        /// <param name="minValue">The minimum value property2.</param>
        /// <param name="maxValue">The maximum value property2.</param>
        public Property2Validator(decimal minValue, decimal maxValue)
        {
            this.minValue = minValue;
            this.maxValue = maxValue;
        }

        /// <summary>Validates the property2 in a record.</summary>
        /// <param name="record">The record.</param>
        /// <returns>
        ///   <see langword="true"/> if the record passed validation, <see langword="false"/> otherwise  .
        /// </returns>
        public bool ValidateParametrs(FileCabinetRecord record)
        {
            if (record.Property2 < this.minValue || record.Property2 > this.maxValue)
            {
                return false;
            }

            return true;
        }
    }
}
