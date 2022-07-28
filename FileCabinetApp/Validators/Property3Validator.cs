using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCabinetApp
{
    public class Property3Validator : IRecordValidator
    {
        private char[] banSymbols;

        public Property3Validator(char[] banSymbols)
        {
            this.banSymbols = banSymbols;
        }

        public bool ValidateParametrs(FileCabinetRecord record)
        {
            return this.ValidateParametrs(record.Property3);
        }

        public bool ValidateParametrs(char input)
        {
            foreach (char symbol in this.banSymbols)
            {
                if (input == symbol)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
