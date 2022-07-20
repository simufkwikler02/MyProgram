using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCabinetApp
{
    public class DefaultProperty2Validator : IRecordValidator
    {
        public bool ValidateParametrs(FileCabinetRecord record)
        {
            if (record.Property2 > 2)
            {
                return false;
            }

            return true;
        }
    }
}
