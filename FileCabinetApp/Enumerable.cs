using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCabinetApp
{
    /// <summary>
    ///   Represents the enumerator, which supports an iteration over a file.
    /// </summary>
    public class Enumerable : IEnumerable<FileCabinetRecord>
    {
        private readonly string data;

        private readonly string name;

        private readonly FileStream? fileStream;

        /// <summary>Initializes a new instance of the <see cref="Enumerable" /> class.</summary>
        /// <param name="fileStream">The file stream.</param>
        /// <param name="data">The value of property in the record, which wiil be found.</param>
        /// <param name="name">The name of property in the record.</param>
        public Enumerable(FileStream? fileStream, string data, string name)
        {
            this.fileStream = fileStream;
            this.data = data;
            this.name = name;
        }

        /// <summary>Gets the enumerator.</summary>
        /// <returns>
        ///   Returns an enumerator that iterates through a file.
        /// </returns>
        public IEnumerator<FileCabinetRecord> GetEnumerator()
        {
            if (this.fileStream is null)
            {
                yield break;
            }

            this.fileStream.Seek(0, SeekOrigin.Begin);
            while (this.fileStream.Position < this.fileStream.Length)
            {
                var record = this.ReadRecord();

                if (record is null)
                {
                    continue;
                }

                switch (this.name)
                {
                    case "id":
                        int id;
                        if (int.TryParse(this.data, out id) && record.Id == id)
                        {
                            yield return record;
                        }

                        break;
                    case "firstname":
                        if (record.FirstName == this.data)
                        {
                            yield return record;
                        }

                        break;
                    case "lastname":
                        if (record.LastName == this.data)
                        {
                            yield return record;
                        }

                        break;
                    case "dateofbirth":
                        var dateofbirth = default(DateTime);
                        if (DateTime.TryParse(this.data, out dateofbirth) && record.DateOfBirth == dateofbirth)
                        {
                            yield return record;
                        }

                        break;
                    case "property1":
                        short property1;
                        if (short.TryParse(this.data, out property1) && record.Property1 == property1)
                        {
                            yield return record;
                        }

                        break;
                    case "property2":
                        decimal property2;
                        if (decimal.TryParse(this.data, out property2) && record.Property2 == property2)
                        {
                            yield return record;
                        }

                        break;
                    case "property3":
                        char property3;
                        if (char.TryParse(this.data, out property3) && record.Property3 == property3)
                        {
                            yield return record;
                        }

                        break;
                }
            }

            yield break;
        }

        /// <summary>Returns an enumerator that iterates through a collection.</summary>
        /// <returns>  <see cref="IEnumerator" />. </returns>
        /// <exception cref="System.NotImplementedException">.</exception>
        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        /// <summary>Reads next record in the file.</summary>
        /// <returns>
        ///   The record <see cref="FileCabinetRecord" />.
        /// </returns>
        private FileCabinetRecord? ReadRecord()
        {
            FileCabinetRecord? record = new FileCabinetRecord();

            try
            {
                byte[] buffer = new byte[2];
                this.fileStream?.Read(buffer, 0, 2);

                short status = BitConverter.ToInt16(buffer, 0);

                buffer = new byte[4];
                this.fileStream?.Read(buffer, 0, 4);

                record.Id = BitConverter.ToInt32(buffer, 0);

                buffer = new byte[120];
                this.fileStream?.Read(buffer, 0, 120);
                int num = Array.IndexOf(buffer, (byte)0);
                if (num > 0)
                {
                    Array.Resize(ref buffer, num);
                }

                record.FirstName = Convert.ToString(Encoding.Default.GetString(buffer), CultureInfo.CurrentCulture);

                buffer = new byte[120];
                this.fileStream?.Read(buffer, 0, 120);
                num = Array.IndexOf(buffer, (byte)0);
                if (num > 0)
                {
                    Array.Resize(ref buffer, num);
                }

                record.LastName = Convert.ToString(Encoding.Default.GetString(buffer), CultureInfo.CurrentCulture);

                buffer = new byte[4];
                this.fileStream?.Read(buffer, 0, 4);
                num = Array.IndexOf(buffer, (byte)0);

                var year = BitConverter.ToInt32(buffer, 0);

                buffer = new byte[4];
                this.fileStream?.Read(buffer, 0, 4);
                num = Array.IndexOf(buffer, (byte)0);

                var month = BitConverter.ToInt32(buffer, 0);

                buffer = new byte[4];
                this.fileStream?.Read(buffer, 0, 4);
                num = Array.IndexOf(buffer, (byte)0);

                var day = BitConverter.ToInt32(buffer, 0);

                record.DateOfBirth = new DateTime(year, month, day);

                buffer = new byte[2];
                this.fileStream?.Read(buffer, 0, 2);
                num = Array.IndexOf(buffer, (byte)0);

                record.Property1 = BitConverter.ToInt16(buffer, 0);

                buffer = new byte[16];
                this.fileStream?.Read(buffer, 0, 16);
                num = Array.IndexOf(buffer, (byte)0);
                if (num > 0)
                {
                    Array.Resize(ref buffer, num);
                }

                record.Property2 = Convert.ToDecimal(Encoding.Default.GetString(buffer), CultureInfo.CurrentCulture);

                buffer = new byte[2];
                this.fileStream?.Read(buffer, 0, 2);
                num = Array.IndexOf(buffer, (byte)0);

                record.Property3 = BitConverter.ToChar(buffer, 0);
                record = status == 0 ? record : this.ReadRecord();
                return record;
            }
            catch
            {
                return null;
            }
        }
    }
}
