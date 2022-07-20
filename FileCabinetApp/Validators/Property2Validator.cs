using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCabinetApp
{
    public class Property2Validator : IRecordValidator
    {
        private int minValue;
        private int maxValue;

        public Property2Validator(int minValue, int maxValue)
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
