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
        private readonly IRecordValidator? validator;
        private readonly int recordSize = 278;

        private int deleteRecords;
        private FileStream? fileStream;

        public FileCabinetFilesystemService(IRecordValidator? validator)
        {
            this.fileStream = new FileStream("cabinet-records.db", FileMode.OpenOrCreate);
            this.validator = validator;
            int i = 0;
            var poz = (int)this.fileStream.Seek(0, SeekOrigin.Begin);
            while (fileStream.Position < fileStream.Length)
            {
                var record = this.ReadRecord();
                if (record != null)
                {
                    i++;
                }
            }

            this.deleteRecords = this.GetStat() - i;
        }

        public string ServiceInfo()
        {
            return this.serviceRules;
        }

        public int CreateRecord(FileCabinetRecord newRecord)
        {
            if (!this.validator.ValidateParametrs(newRecord))
            {
                throw new ArgumentException("Incorrect format", nameof(newRecord));
            }

            if (this.IdExist(newRecord.Id))
            {
                throw new ArgumentException($"This Id exists.", nameof(newRecord));
            }

            var poz = (int)this.fileStream.Seek(0, SeekOrigin.End);
            short status = 0;

            this.WriteRecord(status, newRecord);
            return newRecord.Id;
        }

        public void PurgeRecords()
        {
            var records = this.GetRecords();
            var length = this.fileStream?.Length;

            this.fileStream?.Close();
            this.fileStream = new FileStream("cabinet-records.db", FileMode.Create);
            short status = 0;
            foreach (var record in records)
            {
                this.WriteRecord(status, record);
            }

            Console.WriteLine($"Data file processing is completed: {this.deleteRecords} of {length / this.recordSize} records were purged.");
            this.deleteRecords = 0;
        }

        public ReadOnlyCollection<FileCabinetRecord> GetRecords()
        {
            if (this.fileStream?.Length == 0)
            {
                return new ReadOnlyCollection<FileCabinetRecord>(Array.Empty<FileCabinetRecord>());
            }

            var poz = this.fileStream?.Seek(0, SeekOrigin.Begin);

            List<FileCabinetRecord> list = new List<FileCabinetRecord>();
            while (fileStream.Position < fileStream.Length)
            {
                var record = this.ReadRecord();
                if (record != null)
                {
                    list.Add(record);
                }
            }

            return new ReadOnlyCollection<FileCabinetRecord>(list);
        }

        public int UpdateRecord(long position, FileCabinetRecord recordUpdate)
        {
            if (!this.validator.ValidateParametrs(recordUpdate))
            {
               return -1;
            }

            this.fileStream?.Seek(position, SeekOrigin.Begin);
            short status = 0;
            this.WriteRecord(status, recordUpdate);
            return recordUpdate.Id;
        }

        public void RemoveRecord(int id)
        {
            this.fileStream?.Seek(0, SeekOrigin.Begin);
            while (this.fileStream.Position < this.fileStream.Length)
            {
                var record = this.ReadRecord();
                if (record.Id == id)
                {
                    var recordEdit = record;
                    short status = 1;
                    this.fileStream?.Seek(-this.recordSize, SeekOrigin.Current);
                    this.WriteRecord(status, recordEdit);
                    Console.WriteLine($"Record #{id} is removed.");
                    this.deleteRecords++;
                    return;
                }
            }
        }

        public int DeleteRecord(string name, string value)
        {
            var index = this.FindIndex(name, value);

            if (!index.Any())
            {
                return -1;
            }

            this.fileStream?.Seek(index[0], SeekOrigin.Begin);
            var record = this.ReadRecord();

            var recordEdit = record;
            short status = 1;
            this.fileStream?.Seek(-this.recordSize, SeekOrigin.Current);
            this.WriteRecord(status, recordEdit);
            this.deleteRecords++;
            return recordEdit.Id;
        }

        public int GetStatDelete()
        {
            return this.deleteRecords;
        }

        public int GetStat()
        {
            if (this.fileStream?.Length == 0)
            {
                return 0;
            }

            int numberRecords = (int)this.fileStream?.Length / this.recordSize;
            return numberRecords - this.deleteRecords;
        }

        public bool IdExist(int id)
        {
            var records = new List<FileCabinetRecord>(this.GetRecords());
            var index = records.FindIndex(x => x.Id == id);
            if (index == -1)
            {
                return false;
            }

            return true;
        }

        public ReadOnlyCollection<long> FindIndex(string name, string value)
        {
            this.fileStream.Seek(0, SeekOrigin.Begin);
            var indexFind = new List<long>();

            try
            {
                while (this.fileStream.Position < this.fileStream.Length)
                {
                    var record = this.ReadRecord();
                    if (record == null)
                    {
                        return new ReadOnlyCollection<long>(indexFind);
                    }

                    if (name.Equals("id", StringComparison.OrdinalIgnoreCase) && record.Id == Convert.ToInt32(value, CultureInfo.CurrentCulture))
                    {
                        indexFind.Add(this.fileStream.Position - this.recordSize);
                    }

                    if (name.Equals("firstname", StringComparison.OrdinalIgnoreCase) && record.FirstName == value)
                    {
                        indexFind.Add(this.fileStream.Position - this.recordSize);
                    }

                    if (name.Equals("lastname", StringComparison.OrdinalIgnoreCase) && record.LastName == value)
                    {
                        indexFind.Add(this.fileStream.Position - this.recordSize);
                    }

                    if (name.Equals("dateofbirth", StringComparison.OrdinalIgnoreCase) && record.DateOfBirth == Convert.ToDateTime(value, CultureInfo.CurrentCulture))
                    {
                        indexFind.Add(this.fileStream.Position - this.recordSize);
                    }

                    if (name.Equals("Property1", StringComparison.OrdinalIgnoreCase) && record.Property1 == Convert.ToInt16(value, CultureInfo.CurrentCulture))
                    {
                        indexFind.Add(this.fileStream.Position - this.recordSize);
                    }

                    if (name.Equals("Property2", StringComparison.OrdinalIgnoreCase) && record.Property2 == Convert.ToDecimal(value, CultureInfo.CurrentCulture))
                    {
                        indexFind.Add(this.fileStream.Position - this.recordSize);
                    }

                    if (name.Equals("Property3", StringComparison.OrdinalIgnoreCase) && record.Property3 == Convert.ToChar(value, CultureInfo.CurrentCulture))
                    {
                        indexFind.Add(this.fileStream.Position - this.recordSize);
                    }
                }
            }
            catch
            {
                return new ReadOnlyCollection<long>(indexFind);
            }

            return new ReadOnlyCollection<long>(indexFind);
        }

        public FileCabinetRecord GetRecord(long position)
        {
            this.fileStream.Seek(position, SeekOrigin.Begin);
            return this.ReadRecord();
        }

        public IEnumerable<FileCabinetRecord> FindByFirstName(string firstName)
        {
            return new Enumerable(this.fileStream, firstName, "firstname");
        }

        public IEnumerable<FileCabinetRecord> FindByLastName(string lastName)
        {

            return new Enumerable(this.fileStream, lastName, "lastname");
        }

        public IEnumerable<FileCabinetRecord> FindByDateoOfBirth(string dateofbirth)
        {
            return new Enumerable(this.fileStream, dateofbirth, "dateofbirth");
        }

        public FileCabinetServiceSnapshot MakeSnapshot()
        {
            var records = new List<FileCabinetRecord>(this.GetRecords());
            var snapshot = new FileCabinetServiceSnapshot(records);
            return snapshot;
        }

        public void Restore(FileCabinetServiceSnapshot snapshot)
        {
            var records = snapshot.GetRecords();
            var newlist = new List<FileCabinetRecord>(records);
            var newlistbuf = new List<FileCabinetRecord>(newlist);
            foreach (var record in newlistbuf)
            {
                if (!this.validator.ValidateParametrs(record))
                {
                    Console.WriteLine($"Record validation error with id number {record.Id},record skipped");
                    newlist.Remove(record);
                }
            }

            var oldlist = new List<FileCabinetRecord>(this.GetRecords());
            var newlistconst = new List<FileCabinetRecord>(newlist);
            foreach (var record in newlistconst)
            {
                var index = oldlist.FindIndex(x => x.Id == record.Id);
                if (index >= 0)
                {
                    newlist.Remove(record);
                    oldlist.Insert(index, record);

                    oldlist.RemoveAt(index + 1);
                    continue;
                }
                else
                {
                    oldlist.Add(record);
                }
            }

            var poz = this.fileStream?.Seek(0, SeekOrigin.Begin);
            foreach (var record in oldlist)
            {
                short status = 0;
                this.WriteRecord(status, record);
            }

            Console.WriteLine($"{newlist.Count} records were imported");
        }

        private void WriteRecord(short status, FileCabinetRecord newRecord)
        {
            byte[] buffer = Encoding.Default.GetBytes(status.ToString(CultureInfo.CurrentCulture));
            Array.Resize(ref buffer, 2);
            this.fileStream?.Write(buffer, 0, buffer.Length);

            buffer = Encoding.Default.GetBytes(newRecord.Id.ToString(CultureInfo.CurrentCulture));
            Array.Resize(ref buffer, 4);
            this.fileStream?.Write(buffer, 0, buffer.Length);

            buffer = Encoding.Default.GetBytes(newRecord.FirstName.ToString(CultureInfo.CurrentCulture));
            Array.Resize(ref buffer, 120);
            this.fileStream?.Write(buffer, 0, buffer.Length);

            buffer = Encoding.Default.GetBytes(newRecord.LastName.ToString(CultureInfo.CurrentCulture));
            Array.Resize(ref buffer, 120);
            this.fileStream?.Write(buffer, 0, buffer.Length);

            buffer = Encoding.Default.GetBytes(newRecord.DateOfBirth.Year.ToString(CultureInfo.CurrentCulture));
            Array.Resize(ref buffer, 4);
            this.fileStream?.Write(buffer, 0, buffer.Length);

            buffer = Encoding.Default.GetBytes(newRecord.DateOfBirth.Month.ToString(CultureInfo.CurrentCulture));
            Array.Resize(ref buffer, 4);
            this.fileStream?.Write(buffer, 0, buffer.Length);

            buffer = Encoding.Default.GetBytes(newRecord.DateOfBirth.Day.ToString(CultureInfo.CurrentCulture));
            Array.Resize(ref buffer, 4);
            this.fileStream?.Write(buffer, 0, buffer.Length);

            buffer = Encoding.Default.GetBytes(newRecord.Property1.ToString(CultureInfo.CurrentCulture));
            Array.Resize(ref buffer, 2);
            this.fileStream?.Write(buffer, 0, buffer.Length);

            buffer = Encoding.Default.GetBytes(newRecord.Property2.ToString(CultureInfo.CurrentCulture));
            Array.Resize(ref buffer, 16);
            this.fileStream?.Write(buffer, 0, buffer.Length);

            buffer = Encoding.Default.GetBytes(newRecord.Property3.ToString(CultureInfo.CurrentCulture));
            Array.Resize(ref buffer, 2);
            this.fileStream?.Write(buffer, 0, buffer.Length);
        }

        private FileCabinetRecord ReadRecord()
        {
            FileCabinetRecord record = new FileCabinetRecord();

            if (this.fileStream.Position == this.fileStream.Length)
            {
                return null;
            }

            byte[] buffer = new byte[2];
            this.fileStream?.Read(buffer, 0, 2);
            int num = Array.IndexOf(buffer, (byte)0);
            if (num > 0)
            {
                Array.Resize(ref buffer, num);
            }

            short status = Convert.ToInt16(Encoding.Default.GetString(buffer), CultureInfo.CurrentCulture);

            buffer = new byte[4];

            this.fileStream?.Read(buffer, 0, 4);
            num = Array.IndexOf(buffer, (byte)0);
            if (num > 0)
            {
                Array.Resize(ref buffer, num);
            }

            record.Id = Convert.ToInt32(Encoding.Default.GetString(buffer), CultureInfo.CurrentCulture);

            buffer = new byte[120];

            this.fileStream?.Read(buffer, 0, 120);
            num = Array.IndexOf(buffer, (byte)0);
            if (num > 0)
            {
                Array.Resize(ref buffer, num);
            }

            record.FirstName = Encoding.Default.GetString(buffer);

            buffer = new byte[120];

            this.fileStream?.Read(buffer, 0, 120);
            num = Array.IndexOf(buffer, (byte)0);
            if (num > 0)
            {
                Array.Resize(ref buffer, num);
            }

            record.LastName = Encoding.Default.GetString(buffer);

            buffer = new byte[4];

            this.fileStream?.Read(buffer, 0, 4);
            num = Array.IndexOf(buffer, (byte)0);
            if (num > 0)
            {
                Array.Resize(ref buffer, num);
            }

            var year = Convert.ToInt32(Encoding.Default.GetString(buffer), CultureInfo.CurrentCulture);

            buffer = new byte[4];

            this.fileStream?.Read(buffer, 0, 4);
            num = Array.IndexOf(buffer, (byte)0);
            if (num > 0)
            {
                Array.Resize(ref buffer, num);
            }

            var month = Convert.ToInt32(Encoding.Default.GetString(buffer), CultureInfo.CurrentCulture);

            buffer = new byte[4];

            this.fileStream?.Read(buffer, 0, 4);
            num = Array.IndexOf(buffer, (byte)0);
            if (num > 0)
            {
                Array.Resize(ref buffer, num);
            }

            var day = Convert.ToInt32(Encoding.Default.GetString(buffer), CultureInfo.CurrentCulture);

            record.DateOfBirth = new DateTime(year, month, day);

            buffer = new byte[2];

            this.fileStream?.Read(buffer, 0, 2);
            num = Array.IndexOf(buffer, (byte)0);
            if (num > 0)
            {
                Array.Resize(ref buffer, num);
            }

            record.Property1 = Convert.ToInt16(Encoding.Default.GetString(buffer), CultureInfo.CurrentCulture);

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
            if (num > 0)
            {
                Array.Resize(ref buffer, num);
            }

            record.Property3 = Convert.ToChar(Encoding.Default.GetString(buffer), CultureInfo.CurrentCulture);
            record = status == 0 ? record : this.ReadRecord();
            return record;
        }
    }
}
