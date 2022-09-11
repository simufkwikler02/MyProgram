using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCabinetApp
{
    /// <summary>
    ///   Represents a validation block for a property1.
    /// </summary>
    public class Property1Validator : IRecordBlocksValidator
    {
        private readonly short minValue;
        private readonly short maxValue;

        /// <summary>Initializes a new instance of the <see cref="Property1Validator" /> class.</summary>
        /// <param name="minValue">The minimum value property1.</param>
        /// <param name="maxValue">The maximum value property1.</param>
        public Property1Validator(short minValue, short maxValue)
        {
            this.minValue = minValue;
            this.maxValue = maxValue;
        }

        /// <summary>Validates the property1 in a record.</summary>
        /// <param name="record">The record.</param>
        /// <returns>
        ///   <see langword="true"/> if the record passed validation, <see langword="false"/> otherwise  .
        /// </returns>
        public bool ValidateParametrs(FileCabinetRecord record)
        {
            if (record.Property1 < this.minValue || record.Property1 > this.maxValue)
            {
                return false;
            }

            return true;
        }
    }
}
