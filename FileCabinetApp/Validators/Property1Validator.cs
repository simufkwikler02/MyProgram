using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCabinetApp
{
    public class Property1Validator : IRecordValidator
    {
        private int minValue;
        private int maxValue;

        public Property1Validator(int minValue, int maxValue)
        {
            this.minValue = minValue;
            this.maxValue = maxValue;
        }

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
