using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCabinetApp
{
    public class FileCabinetDefaultService : FileCabinetService
    {
        public string validator { get; set; }

        protected override bool ValidateParameters(FileCabinetRecord newRecord)
        {
            if (string.IsNullOrEmpty(newRecord.FirstName) || newRecord.FirstName.Length < 2 || newRecord.FirstName.Length > 60)
            {
                return false;
            }

            if (string.IsNullOrEmpty(newRecord.LastName) || newRecord.LastName.Length < 2 || newRecord.LastName.Length > 60)
            {
                return false;
            }

            DateTime minDate = new DateTime(1950, 6, 1);
            if (minDate > newRecord.DateOfBirth || DateTime.Now < newRecord.DateOfBirth)
            {
                return false;
            }

            if (newRecord.Property1 < 1 || newRecord.Property1 > 100)
            {
                return false;
            }

            if (newRecord.Property2 > 2)
            {
                return false;
            }

            if (newRecord.Property3 == 'l')
            {
                return false;
            }

            return true;
        }
    }
}
