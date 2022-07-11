using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCabinetApp
{
    public interface IRecordValidator
    {
        public bool ValidateParametrs(FileCabinetRecord newRecord);

        public string ValidateInfo();

        public DateTime DateTimeMin();

        public bool ValidateParametrsFirstName(string input);

        public bool ValidateParametrsLastName(string input);

        public bool ValidateParametrsDateOfBirth(DateTime input);

        public bool ValidateParametrsProperty1(short input);

        public bool ValidateParametrsProperty2(decimal input);

        public bool ValidateParametrsProperty3(char input);
    }
}
