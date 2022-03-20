using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCabinetApp
{
    public class FileCabinetDefaultService : FileCabinetService
    {
        private readonly string rules = "default";

        public override string GetRules()
        {
            return this.rules;
        }

        protected override IRecordValidator CreateValidator()
        {
            return new DefaultValidator();
        }
    }
}
