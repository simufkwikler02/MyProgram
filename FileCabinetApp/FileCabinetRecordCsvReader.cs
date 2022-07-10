using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCabinetApp
{
    public class FileCabinetRecordCsvReader
    {
        private StreamReader reader;

        public FileCabinetRecordCsvReader(StreamReader fstream)
        {
            this.reader = fstream;
        }

        public IList<FileCabinetRecord> ReadAll()
        {
            string? line;
            line = this.reader.ReadLine();
            int i = 1;
            IList<FileCabinetRecord> list = new List<FileCabinetRecord>();
            while ((line = this.reader.ReadLine()) != null)
            {
                i++;
                try
                {
                    string[] parts = line.Split(',');
                    var record = new FileCabinetRecord();
                    record.Id = Convert.ToInt32(parts[0], CultureInfo.CurrentCulture);
                    record.FirstName = parts[1];
                    record.LastName = parts[2];
                    record.DateOfBirth = Convert.ToDateTime(parts[3], CultureInfo.CurrentCulture);
                    record.Property1 = Convert.ToInt16(parts[4], CultureInfo.CurrentCulture);
                    record.Property2 = Convert.ToDecimal(parts[5], CultureInfo.CreateSpecificCulture("en-GB"));
                    record.Property3 = Convert.ToChar(parts[6], CultureInfo.CurrentCulture);
                    list.Add(record);
                }
                catch
                {
                    Console.WriteLine($"line number {i} read error,line skipped");
                    continue;
                }
            }

            return list;
        }
    }
}
