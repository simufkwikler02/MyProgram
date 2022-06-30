using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCabinetApp
{
    public class FileCabinetServiceSnapshot
    {
        private readonly List<FileCabinetRecord>? records;

        public FileCabinetServiceSnapshot(List<FileCabinetRecord> records)
        {
            this.records = records;
        }

        public void SaveToCsv(StreamWriter fstream)
        {
            var scv = new FileCabinetRecordCsvWriter(fstream);
            scv.Write(this.records);
        }
    }
}
