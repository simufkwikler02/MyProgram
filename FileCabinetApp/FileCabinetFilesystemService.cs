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
        private readonly Dictionary<string, List<int>> firstNameDictionary = new Dictionary<string, List<int>>();
        private readonly Dictionary<string, List<int>> lastNameDictionary = new Dictionary<string, List<int>>();
        private readonly Dictionary<DateTime, List<int>> dateOfBirthDictionary = new Dictionary<DateTime, List<int>>();

        private int deleteRecords;
        private FileStream? fileStream;

        public FileCabinetFilesystemService(IRecordValidator? validator)
        {
            this.fileStream = new FileStream("cabinet-records.db", FileMode.OpenOrCreate);
            this.validator = validator;
            var records = this.GetRecords();
            this.ClearAndSaveInDictionary(records);
            int i = 0;
            var poz = (int)this.fileStream.Seek(0, SeekOrigin.Begin);
            while (fileStream.Position < fileStream.Length)
            {
                var record = this.ReadRecord();
                i++;
            }

            this.deleteRecords = this.GetStat() - i;
        }

        public void ClearAndSaveInDictionary(ReadOnlyCollection<FileCabinetRecord> records)
        {
            this.firstNameDictionary.Clear();
            this.lastNameDictionary.Clear();
            this.dateOfBirthDictionary.Clear();

            for (int i = 0; i < records.Count; i++)
            {
                if (!this.firstNameDictionary.ContainsKey(records[i].FirstName))
                {
                    this.firstNameDictionary.Add(records[i].FirstName, new List<int>());
                }

                this.firstNameDictionary[records[i].FirstName].Add(i * this.recordSize);

                if (!this.lastNameDictionary.ContainsKey(records[i].LastName))
                {
                    this.lastNameDictionary.Add(records[i].LastName, new List<int>());
                }

                this.lastNameDictionary[records[i].LastName].Add(i * this.recordSize);

                if (!this.dateOfBirthDictionary.ContainsKey(records[i].DateOfBirth))
                {
                    this.dateOfBirthDictionary.Add(records[i].DateOfBirth, new List<int>());
                }

                this.dateOfBirthDictionary[records[i].DateOfBirth].Add(i * this.recordSize);
            }
        }

        public string ServiceInfo()
        {
            return this.serviceRules;
        }

        public int CreateRecord(FileCabinetRecord newRecord)
        {
            var poz = (int)this.fileStream.Seek(0, SeekOrigin.End);
            newRecord.Id = (Convert.ToInt32(poz, CultureInfo.CurrentCulture) / this.recordSize) + 1;
            short status = 0;
            if (!this.firstNameDictionary.ContainsKey(newRecord.FirstName))
            {
                this.firstNameDictionary.Add(newRecord.FirstName, new List<int>());
            }

            this.firstNameDictionary[newRecord.FirstName].Add(poz);

            if (!this.lastNameDictionary.ContainsKey(newRecord.LastName))
            {
                this.lastNameDictionary.Add(newRecord.LastName, new List<int>());
            }

            this.lastNameDictionary[newRecord.LastName].Add(poz);

            if (!this.dateOfBirthDictionary.ContainsKey(newRecord.DateOfBirth))
            {
                this.dateOfBirthDictionary.Add(newRecord.DateOfBirth, new List<int>());
            }

            this.dateOfBirthDictionary[newRecord.DateOfBirth].Add(poz);

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

            this.ClearAndSaveInDictionary(records);
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
                list.Add(this.ReadRecord());
            }

            return new ReadOnlyCollection<FileCabinetRecord>(list);
        }

        public void EditRecord(int id, FileCabinetRecord recordEdit)
        {
            if (!this.validator.ValidateParametrs(recordEdit))
            {
                throw new ArgumentException("incorrect format", nameof(recordEdit));
            }

            var nuumber = this.GetStat();
            var poz = (int)this.fileStream?.Seek(0, SeekOrigin.Begin);

            while (fileStream.Position < fileStream.Length)
            {
                var record = this.ReadRecord();
                if (record.Id == id)
                {
                    short status = 0;
                    recordEdit.Id = id;
                    poz = (int)this.fileStream?.Seek(-this.recordSize, SeekOrigin.Current);
                    this.firstNameDictionary[record.FirstName].Remove(poz);
                    this.lastNameDictionary[record.LastName].Remove(poz);
                    this.dateOfBirthDictionary[record.DateOfBirth].Remove(poz);

                    if (!this.firstNameDictionary.ContainsKey(recordEdit.FirstName))
                    {
                        this.firstNameDictionary.Add(recordEdit.FirstName, new List<int>());
                    }

                    this.firstNameDictionary[recordEdit.FirstName].Add(poz);

                    if (!this.lastNameDictionary.ContainsKey(recordEdit.LastName))
                    {
                        this.lastNameDictionary.Add(recordEdit.LastName, new List<int>());
                    }

                    this.lastNameDictionary[recordEdit.LastName].Add(poz);

                    if (!this.dateOfBirthDictionary.ContainsKey(recordEdit.DateOfBirth))
                    {
                        this.dateOfBirthDictionary.Add(recordEdit.DateOfBirth, new List<int>());
                    }

                    this.dateOfBirthDictionary[recordEdit.DateOfBirth].Add(poz);

                    this.WriteRecord(status, recordEdit);

                    Console.WriteLine($"Record #{id} is updated.");
                    return;
                }
            }

            Console.WriteLine($"Record #{id} is not found");
        }

        public void RemoveRecord(int id)
        {
            var nuumber = this.GetStat();
            var poz = (int)this.fileStream?.Seek(0, SeekOrigin.Begin);

            while (fileStream.Position < fileStream.Length)
            {
                var record = this.ReadRecord();
                if (record.Id == id)
                {
                    var recordEdit = record;
                    short status = 1;
                    poz = (int)this.fileStream?.Seek(-this.recordSize, SeekOrigin.Current);
                    this.firstNameDictionary[record.FirstName].Remove(poz);
                    this.lastNameDictionary[record.LastName].Remove(poz);
                    this.dateOfBirthDictionary[record.DateOfBirth].Remove(poz);
                    this.WriteRecord(status, recordEdit);
                    Console.WriteLine($"Record #{id} is removed.");
                    this.deleteRecords++;
                    return;
                }
            }
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

        public ReadOnlyCollection<FileCabinetRecord> FindByFirstName(string firstName)
        {
            var nuumber = this.GetStat();
            var poz = this.fileStream?.Seek(0, SeekOrigin.Begin);
            List<FileCabinetRecord> list = new List<FileCabinetRecord>();
            if (!this.firstNameDictionary.ContainsKey(firstName))
            {
                return new ReadOnlyCollection<FileCabinetRecord>(list);
            }

            foreach (var position in this.firstNameDictionary[firstName])
            {
                poz = this.fileStream?.Seek(position, SeekOrigin.Begin);
                var record = this.ReadRecord();
                list.Add(record);
            }

            return new ReadOnlyCollection<FileCabinetRecord>(list);
        }

        public ReadOnlyCollection<FileCabinetRecord> FindByLastName(string lastName)
        {
            var nuumber = this.GetStat();
            var poz = this.fileStream?.Seek(0, SeekOrigin.Begin);
            List<FileCabinetRecord> list = new List<FileCabinetRecord>();

            if (!this.lastNameDictionary.ContainsKey(lastName))
            {
                return new ReadOnlyCollection<FileCabinetRecord>(list);
            }

            foreach (var position in this.lastNameDictionary[lastName])
            {
                poz = this.fileStream?.Seek(position, SeekOrigin.Begin);
                var record = this.ReadRecord();
                list.Add(record);
            }

            return new ReadOnlyCollection<FileCabinetRecord>(list);
        }

        public ReadOnlyCollection<FileCabinetRecord> FindByDateoOfBirth(string dateofbirth)
        {
            var nuumber = this.GetStat();
            var poz = this.fileStream?.Seek(0, SeekOrigin.Begin);
            List<FileCabinetRecord> list = new List<FileCabinetRecord>();
            DateTime date;
            if (!DateTime.TryParse(dateofbirth, out date))
            {
                return new ReadOnlyCollection<FileCabinetRecord>(list);
            }

            if (!this.dateOfBirthDictionary.ContainsKey(date))
            {
                return new ReadOnlyCollection<FileCabinetRecord>(list);
            }

            foreach (var position in this.dateOfBirthDictionary[date])
            {
                poz = this.fileStream?.Seek(position, SeekOrigin.Begin);
                var record = this.ReadRecord();
                list.Add(record);
            }

            return new ReadOnlyCollection<FileCabinetRecord>(list);
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

            this.ClearAndSaveInDictionary(new ReadOnlyCollection<FileCabinetRecord>(oldlist));

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
