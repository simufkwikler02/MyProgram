using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCabinetApp
{
    public class DefaultProperty1Validator : IRecordValidator
    {
        public bool ValidateParametrs(FileCabinetRecord record)
        {
            if (record.Property1 < 1 || record.Property1 > 100)
            {
                return false;
            }

            return true;
        }
    }
}
