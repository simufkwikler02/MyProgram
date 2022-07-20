using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCabinetApp
{
    public class CustomLastNameValidator : IRecordValidator
    {
        public bool ValidateParametrs(FileCabinetRecord record)
        {
            if (string.IsNullOrEmpty(record.LastName) || record.LastName.Length < 2 || record.LastName.Length > 60)
            {
                return false;
            }

            return true;
        }
    }
}
