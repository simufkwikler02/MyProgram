﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCabinetApp
{
    public interface IRecordValidator
    {
        public bool ValidateParametrs(FileCabinetRecord newRecord);
    }
}
