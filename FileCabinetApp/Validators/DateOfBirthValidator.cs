using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCabinetApp
{
    public class DateOfBirthValidator : IRecordValidator
    {
        private DateTime from;
        private DateTime to;

        public DateOfBirthValidator(DateTime from, DateTime to)
        {
            this.to = to;
            this.from = from;
        }

        public bool ValidateParametrs(FileCabinetRecord record)
        {
            if (this.from > record.DateOfBirth || this.to < record.DateOfBirth)
            {
                return false;
            }

            return true;
        }
    }
}
