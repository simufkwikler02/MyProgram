using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCabinetApp
{
    public class CustomFirstNameValidator : IRecordValidator
    {
        public bool ValidateParametrs(FileCabinetRecord record)
        {
            if (string.IsNullOrEmpty(record.FirstName) || record.FirstName.Length < 2 || record.FirstName.Length > 60)
            {
                return false;
            }

            return true;
        }
    }
}
