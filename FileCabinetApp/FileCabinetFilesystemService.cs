using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace FileCabinetApp
{
    public class FileCabinetFilesystemService : IFileCabinetService
    {
        private readonly string serviceRules = "file";
        private readonly IRecordValidator validator;
        private readonly int recordSize = 278;
        private FileStream? fileStream;

        public FileCabinetFilesystemService(IRecordValidator validator)
        {
            this.fileStream = new FileStream("cabinet-records.db", FileMode.Create);
            this.validator = validator;
        }

        public string ValidateInfo()
        {
            return this.validator.ValidateInfo();
        }

        public string ServiceInfo()
        {
            return this.serviceRules;
        }

        public int CreateRecord(FileCabinetRecord newRecord)
        {
            var poz = fileStream.Seek(0, SeekOrigin.End);
            newRecord.Id = (Convert.ToInt32(poz) / this.recordSize) + 1;
            short status = 0;

            byte[] buffer = Encoding.Default.GetBytes(status.ToString(CultureInfo.CurrentCulture));
            Array.Resize(ref buffer, 2);
            this.fileStream.Write(buffer, 0, buffer.Length);

            buffer = Encoding.Default.GetBytes(newRecord.Id.ToString(CultureInfo.CurrentCulture));
            Array.Resize(ref buffer, 4);
            this.fileStream.Write(buffer, 0, buffer.Length);

            buffer = Encoding.Default.GetBytes(newRecord.FirstName.ToString(CultureInfo.CurrentCulture));
            Array.Resize(ref buffer, 120);
            this.fileStream.Write(buffer, 0, buffer.Length);

            buffer = Encoding.Default.GetBytes(newRecord.LastName.ToString(CultureInfo.CurrentCulture));
            Array.Resize(ref buffer, 120);
            this.fileStream.Write(buffer, 0, buffer.Length);

            buffer = Encoding.Default.GetBytes(newRecord.DateOfBirth.Year.ToString(CultureInfo.CurrentCulture));
            Array.Resize(ref buffer, 4);
            this.fileStream.Write(buffer, 0, buffer.Length);

            buffer = Encoding.Default.GetBytes(newRecord.DateOfBirth.Month.ToString(CultureInfo.CurrentCulture));
            Array.Resize(ref buffer, 4);
            this.fileStream.Write(buffer, 0, buffer.Length);

            buffer = Encoding.Default.GetBytes(newRecord.DateOfBirth.Day.ToString(CultureInfo.CurrentCulture));
            Array.Resize(ref buffer, 4);
            this.fileStream.Write(buffer, 0, buffer.Length);

            buffer = Encoding.Default.GetBytes(newRecord.Property1.ToString(CultureInfo.CurrentCulture));
            Array.Resize(ref buffer, 2);
            this.fileStream.Write(buffer, 0, buffer.Length);

            buffer = Encoding.Default.GetBytes(newRecord.Property2.ToString(CultureInfo.CurrentCulture));
            Array.Resize(ref buffer, 16);
            this.fileStream.Write(buffer, 0, buffer.Length);

            buffer = Encoding.Default.GetBytes(newRecord.Property3.ToString(CultureInfo.CurrentCulture));
            Array.Resize(ref buffer, 2);
            this.fileStream.Write(buffer, 0, buffer.Length);

            return newRecord.Id;
        }

        public ReadOnlyCollection<FileCabinetRecord> GetRecords()
        {
            if (this.fileStream.Length == 0)
            {
                return new ReadOnlyCollection<FileCabinetRecord>(Array.Empty<FileCabinetRecord>());
            }

            var poz = this.fileStream.Seek(0, SeekOrigin.Begin);
            var numberRecords = this.fileStream.Length / this.recordSize;

            FileCabinetRecord[] records = new FileCabinetRecord[numberRecords];
            for (int i = 0; i < numberRecords; i++)
            {
                FileCabinetRecord record = new FileCabinetRecord();
                byte[] buffer = new byte[2];

                this.fileStream.Read(buffer, 0, 2);
                int num = Array.IndexOf(buffer, (byte)0);
                if (num > 0)
                {
                    Array.Resize(ref buffer, num);
                }

                short status = Convert.ToInt16(Encoding.Default.GetString(buffer), CultureInfo.CurrentCulture);

                buffer = new byte[4];

                this.fileStream.Read(buffer, 0, 4);
                num = Array.IndexOf(buffer, (byte)0);
                if (num > 0)
                {
                    Array.Resize(ref buffer, num);
                }

                record.Id = Convert.ToInt32(Encoding.Default.GetString(buffer), CultureInfo.CurrentCulture);

                buffer = new byte[120];

                this.fileStream.Read(buffer, 0, 120);
                num = Array.IndexOf(buffer, (byte)0);
                if (num > 0)
                {
                    Array.Resize(ref buffer, num);
                }

                record.FirstName = Encoding.Default.GetString(buffer);

                buffer = new byte[120];

                this.fileStream.Read(buffer, 0, 120);
                num = Array.IndexOf(buffer, (byte)0);
                if (num > 0)
                {
                    Array.Resize(ref buffer, num);
                }

                record.LastName = Encoding.Default.GetString(buffer);

                buffer = new byte[4];

                this.fileStream.Read(buffer, 0, 4);
                num = Array.IndexOf(buffer, (byte)0);
                if (num > 0)
                {
                    Array.Resize(ref buffer, num);
                }

                var year = Convert.ToInt32(Encoding.Default.GetString(buffer), CultureInfo.CurrentCulture);

                buffer = new byte[4];

                this.fileStream.Read(buffer, 0, 4);
                num = Array.IndexOf(buffer, (byte)0);
                if (num > 0)
                {
                    Array.Resize(ref buffer, num);
                }

                var month = Convert.ToInt32(Encoding.Default.GetString(buffer), CultureInfo.CurrentCulture);

                buffer = new byte[4];

                this.fileStream.Read(buffer, 0, 4);
                num = Array.IndexOf(buffer, (byte)0);
                if (num > 0)
                {
                    Array.Resize(ref buffer, num);
                }

                var day = Convert.ToInt32(Encoding.Default.GetString(buffer), CultureInfo.CurrentCulture);

                record.DateOfBirth = new DateTime(year, month, day);

                buffer = new byte[2];

                this.fileStream.Read(buffer, 0, 2);
                num = Array.IndexOf(buffer, (byte)0);
                if (num > 0)
                {
                    Array.Resize(ref buffer, num);
                }

                record.Property1 = Convert.ToInt16(Encoding.Default.GetString(buffer), CultureInfo.CurrentCulture);

                buffer = new byte[16];

                this.fileStream.Read(buffer, 0, 16);
                num = Array.IndexOf(buffer, (byte)0);
                if (num > 0)
                {
                    Array.Resize(ref buffer, num);
                }

                record.Property2 = Convert.ToDecimal(Encoding.Default.GetString(buffer), CultureInfo.CurrentCulture);

                buffer = new byte[2];

                this.fileStream.Read(buffer, 0, 2);
                num = Array.IndexOf(buffer, (byte)0);
                if (num > 0)
                {
                    Array.Resize(ref buffer, num);
                }

                record.Property3 = Convert.ToChar(Encoding.Default.GetString(buffer), CultureInfo.CurrentCulture);

                records[i] = record;
            }

            return new ReadOnlyCollection<FileCabinetRecord>(records);
        }

        public void EditRecord(int id, FileCabinetRecord recordEdit)
        {
            throw new NotImplementedException();
        }

        public int GetStat()
        {
            throw new NotImplementedException();
        }

        public ReadOnlyCollection<FileCabinetRecord> FindByFirstName(string firstName)
        {
            throw new NotImplementedException();
        }

        public ReadOnlyCollection<FileCabinetRecord> FindByLastName(string lastName)
        {
            throw new NotImplementedException();
        }

        public ReadOnlyCollection<FileCabinetRecord> FindByDateoOfBirth(string dateofbirth)
        {
            throw new NotImplementedException();
        }

        public FileCabinetServiceSnapshot MakeSnapshot()
        {
            throw new NotImplementedException();
        }
    }
}
