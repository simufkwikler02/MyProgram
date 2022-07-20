using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCabinetApp
{
    public class CustomProperty2Validator : IRecordValidator
    {
        public bool ValidateParametrs(FileCabinetRecord record)
        {
            return true;
        }
    }
}
