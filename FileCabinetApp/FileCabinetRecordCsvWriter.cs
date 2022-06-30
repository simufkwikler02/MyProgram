using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCabinetApp
{
    public class FileCabinetRecordCsvWriter
    {
        private readonly StreamWriter? writer;

        public FileCabinetRecordCsvWriter(StreamWriter fstream)
        {
            this.writer = fstream;
        }

        public void Write(List<FileCabinetRecord> records)
        {
            this.writer.WriteLine("Id,First Name,Last Name,Date of Birth,Property1,Property2,Property3");
            foreach (var record in records)
            {
                writer.WriteLine($"{record.Id},{record.FirstName},{record.LastName},{record.DateOfBirth},{record.Property1},{record.Property2},{record.Property3}");
            }
        }

    }
}
