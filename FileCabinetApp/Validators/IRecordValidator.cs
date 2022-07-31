using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCabinetApp
{
    public interface IRecordValidator
    {
        public bool ValidateParametrs(FileCabinetRecord record);

        public bool ValidateParametrFirstName(string input);

        public bool ValidateParametrLastName(string input);

        public bool ValidateParametrDateOfBirth(DateTime input);

        public bool ValidateParametrProperty1(short input);

        public bool ValidateParametrProperty2(decimal input);

        public bool ValidateParametrProperty3(char input);

        public string ValidateInfo();

        public ValidateParametrs ParametrsInfo();
    }
}
