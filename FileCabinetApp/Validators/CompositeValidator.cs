using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCabinetApp
{
    public abstract class CompositeValidator : IRecordValidator
    {
        private List<IRecordValidator> validators;

        protected CompositeValidator(IEnumerable<IRecordValidator> validators)
        {
            this.validators = new List<IRecordValidator>(validators);
        }

        public bool ValidateParametrs(FileCabinetRecord record)
        {
            var result = true;
            foreach (var validator in this.validators)
            {
                result &= validator.ValidateParametrs(record);
            }

            return result;
        }
    }
}
