using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCabinetApp
{
    public class MemoryIterator : IRecordIterator
    {
        private int position = -1;

        private List<FileCabinetRecord> records;

        public MemoryIterator(IEnumerable<FileCabinetRecord> records)
        {
            this.records = new List<FileCabinetRecord>(records);
        }

        public FileCabinetRecord? GetNext()
        {
            if (!this.HasMore())
            {
                return null;
            }

            this.position++;

            return this.records[this.position];
        }

        public bool HasMore()
        {
            return this.position + 1 < this.records.Count;
        }

        public void Reset()
        {
            this.position = -1;
        }
    }
}
