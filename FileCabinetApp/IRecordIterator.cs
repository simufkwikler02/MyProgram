using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCabinetApp
{
    public interface IRecordIterator
    {
        public FileCabinetRecord? GetNext();

        public void Reset();

        public bool HasMore();
    }
}
