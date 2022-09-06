using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCabinetApp
{
    public class Property2Validator : IRecordBlocksValidator
    {
        private readonly decimal minValue;
        private readonly decimal maxValue;

        public Property2Validator(decimal minValue, decimal maxValue)
        {
            this.minValue = minValue;
            this.maxValue = maxValue;
        }

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
