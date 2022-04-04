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

        public bool ValidateParametrString(string input);

        public bool ValidateParametrDate(DateTime input);

        public bool ValidateParametrProperty1(short input);

        public bool ValidateParametrProperty2(decimal input);

        public bool ValidateParametrProperty3(char input);
    }
}
