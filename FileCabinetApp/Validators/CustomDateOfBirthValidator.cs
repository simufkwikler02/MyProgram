using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCabinetApp
{
    public class CustomDateOfBirthValidator : IRecordValidator
    {
        private readonly DateTime minDate = new DateTime(1950, 6, 1);

        public bool ValidateParametrs(FileCabinetRecord record)
        {
            if (this.minDate > record.DateOfBirth || DateTime.Now < record.DateOfBirth)
            {
                return false;
            }

            return true;
        }
    }
}
