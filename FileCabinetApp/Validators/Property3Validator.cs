using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCabinetApp
{
    public class Property3Validator : IRecordBlocksValidator
    {
        private readonly char[] banSymbols;

        public Property3Validator(char[] banSymbols)
        {
            this.banSymbols = banSymbols;
        }

        public bool ValidateParametrs(FileCabinetRecord record)
        {
            foreach (char symbol in this.banSymbols)
            {
                if (record.Property3 == symbol)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
