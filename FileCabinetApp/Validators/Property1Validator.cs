using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCabinetApp
{
    public class Property1Validator : IRecordValidator
    {
        private short minValue;
        private short maxValue;

        public Property1Validator(short minValue, short maxValue)
        {
            this.minValue = minValue;
            this.maxValue = maxValue;
        }

        public bool ValidateParametrs(FileCabinetRecord record)
        {
            return this.ValidateParametrs(record.Property1);
        }

        public bool ValidateParametrs(short input)
        {
            if (input < this.minValue || input > this.maxValue)
            {
                return false;
            }

            return true;
        }
    }
}
