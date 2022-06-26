using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FileCabinetApp
{
    internal interface IRecordValidator
    {
        public abstract bool ValidateParameters(FileCabinetRecord newRecord);
    }
}
