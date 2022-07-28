using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCabinetApp
{
    public class Property2Validator : IRecordValidator
    {
        private decimal minValue;
        private decimal maxValue;

        public Property2Validator(decimal minValue, decimal maxValue)
        {
            this.minValue = minValue;
            this.maxValue = maxValue;
        }

        public bool ValidateParametrs(FileCabinetRecord record)
        {
            return this.ValidateParametrs(record.Property2);
        }

        public bool ValidateParametrs(decimal input)
        {
            if (input < this.minValue || input > this.maxValue)
            {
                return false;
            }

            return true;
        }
    }
}
