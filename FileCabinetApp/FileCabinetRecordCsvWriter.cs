using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCabinetApp
{
    /// <summary>
    ///   Represents the CSV writer class with a set methods that can save records to a file.
    /// </summary>
    public class FileCabinetRecordCsvWriter
    {
        private readonly TextWriter writer;

        /// <summary>Initializes a new instance of the <see cref="FileCabinetRecordCsvWriter" /> class.</summary>
        /// <param name="fstream">The fstream.</param>
        public FileCabinetRecordCsvWriter(TextWriter fstream)
        {
            this.writer = fstream;
        }

        /// <summary>Writes the specified records to a CSV file.</summary>
        /// <param name="records">The records.</param>
        public void Write(List<FileCabinetRecord> records)
        {
            this.writer?.WriteLine("Id,First Name,Last Name,Date of Birth,Property1,Property2,Property3");
            foreach (var record in records)
            {
                this.writer?.WriteLine($"{record.Id},{record.FirstName},{record.LastName},{record.DateOfBirth},{record.Property1},{record.Property2.ToString(CultureInfo.CreateSpecificCulture("en-GB"))},{record.Property3}");
            }
        }
    }
}
