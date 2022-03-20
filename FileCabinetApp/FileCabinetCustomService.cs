using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCabinetApp
{
    public class FileCabinetCustomService : FileCabinetService
    {
        private readonly string rules = "custom";

        public override string GetRules()
        {
            return this.rules;
        }

        protected override IRecordValidator CreateValidator()
        {
            return new CustomValidator();
        }

    }
}
