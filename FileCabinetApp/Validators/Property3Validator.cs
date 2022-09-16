using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCabinetApp
{
    /// <summary>
    ///   Represents a validation block for a property3.
    /// </summary>
    public class Property3Validator : IRecordBlocksValidator
    {
        private readonly char[] banSymbols;

        /// <summary>Initializes a new instance of the <see cref="Property3Validator" /> class.</summary>
        /// <param name="banSymbols">The ban symbols for property3.</param>
        public Property3Validator(char[] banSymbols)
        {
            this.banSymbols = banSymbols;
        }

        /// <summary>Validates the property3 in a record.</summary>
        /// <param name="record">The record.</param>
        /// <returns>
        ///   <see langword="true"/> if the record passed validation, <see langword="false"/> otherwise.
        /// </returns>
        public bool ValidateParametrs(FileCabinetRecord record)
        {
            foreach (char symbol in this.banSymbols)
            {
                if (record.Property3 == symbol)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
