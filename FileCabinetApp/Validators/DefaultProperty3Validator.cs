using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCabinetApp
{
    public class DefaultProperty3Validator : IRecordValidator
    {
        public bool ValidateParametrs(FileCabinetRecord record)
        {
            if (record.Property3 == 'l')
            {
               return false;
            }

            return true;
        }
    }
}
