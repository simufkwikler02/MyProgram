using System.Collections.ObjectModel;
using System.Globalization;

namespace FileCabinetApp
{
    public class FileCabinetMemoryService : IFileCabinetService
    {
        private readonly List<FileCabinetRecord> list = new List<FileCabinetRecord>();
        private readonly IRecordValidator? validator;
        private readonly string serviceRules = "memory";
        private readonly int deleteRecords;

        private readonly Dictionary<int, List<FileCabinetRecord>> idDictionary = new Dictionary<int, List<FileCabinetRecord>>();
        private readonly Dictionary<string, List<FileCabinetRecord>> firstNameDictionary = new Dictionary<string, List<FileCabinetRecord>>();
        private readonly Dictionary<string, List<FileCabinetRecord>> lastNameDictionary = new Dictionary<string, List<FileCabinetRecord>>();
        private readonly Dictionary<DateTime, List<FileCabinetRecord>> dateOfBirthDictionary = new Dictionary<DateTime, List<FileCabinetRecord>>();
        private readonly Dictionary<short, List<FileCabinetRecord>> property1Dictionary = new Dictionary<short, List<FileCabinetRecord>>();
        private readonly Dictionary<decimal, List<FileCabinetRecord>> property2Dictionary = new Dictionary<decimal, List<FileCabinetRecord>>();
        private readonly Dictionary<char, List<FileCabinetRecord>> property3Dictionary = new Dictionary<char, List<FileCabinetRecord>>();

        public FileCabinetMemoryService(IRecordValidator? validator)
        {
            this.validator = validator;
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
                var id = 0;
                do
                {
                    if (!this.IdExist(id))
                    {
                        newRecord.Id = id;
                        break;
                    }

                    id++;
                }
                while (true);
            }

            this.list.Add(newRecord);
            this.AddToDictionary(newRecord);
            return newRecord.Id;
        }

        public ReadOnlyCollection<FileCabinetRecord> GetRecords()
        {
            if (this.list.Count == 0)
            {
                return new ReadOnlyCollection<FileCabinetRecord>(Array.Empty<FileCabinetRecord>());
            }
            else
            {
                var records = new List<FileCabinetRecord>();
                foreach (var record in this.list)
                {
                    records.Add(record);
                }

                return new ReadOnlyCollection<FileCabinetRecord>(records);
            }
        }

        public bool IdExist(int id)
        {
            var index = this.list.FindIndex(x => x.Id == id);
            if (index == -1)
            {
                return false;
            }

            return true;
        }

        public ReadOnlyCollection<long> FindIndex(string name, string value)
        {
            var recordFind = new List<FileCabinetRecord>();

            switch (name)
            {
                case "id":
                    recordFind = this.list.FindAll(x => x.Id == Convert.ToInt32(value, CultureInfo.CurrentCulture));
                    break;
                case "firstname":
                    recordFind = this.list.FindAll(x => x.FirstName == value);
                    break;
                case "lastname":
                    recordFind = this.list.FindAll(x => x.LastName == value);
                    break;
                case "dateofbirth":
                    recordFind = this.list.FindAll(x => x.DateOfBirth == Convert.ToDateTime(value, CultureInfo.CurrentCulture));
                    break;
                case "Property1":
                    recordFind = this.list.FindAll(x => x.Property1 == Convert.ToInt16(value, CultureInfo.CurrentCulture));
                    break;
                case "Property2":
                    recordFind = this.list.FindAll(x => x.Property2 == Convert.ToDecimal(value, CultureInfo.CurrentCulture));
                    break;
                case "Property3":
                    recordFind = this.list.FindAll(x => x.Property3 == Convert.ToChar(value, CultureInfo.CurrentCulture));
                    break;
            }

            var indexFind = new List<long>();
            foreach (var item in recordFind)
            {
                indexFind.Add(item.Id);
            }

            return new ReadOnlyCollection<long>(indexFind);
        }

        public FileCabinetRecord GetRecord(long position)
        {
            return this.list[(int)position];
        }

        public int GetStat()
        {
            return this.list.Count;
        }

        public int GetStatDelete()
        {
            return this.deleteRecords;
        }

        public int UpdateRecord(long position, FileCabinetRecord recordUpdate)
        {
            if (!this.validator.ValidateParametrs(recordUpdate))
            {
                return -1;
            }

            this.RemoveFromDictionary(this.list[(int)position]);
            this.list[(int)position] = recordUpdate;
            this.AddToDictionary(recordUpdate);
            return recordUpdate.Id;
        }

        public int DeleteRecord(string name, string value)
        {
            var index = this.FindIndex(name, value);

            if (!index.Any())
            {
                return -1;
            }

            var record = this.list[(int)index[0]];
            this.list.RemoveAt((int)index[0]);
            this.RemoveFromDictionary(record);
            return record.Id;
        }

        public void PurgeRecords()
        {
            Console.WriteLine("Error -> This command works only with 'file' type of service");
            return;
        }

        public IEnumerable<FileCabinetRecord> FindById(string id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return this.idDictionary[Convert.ToInt32(id, CultureInfo.CurrentCulture)];
        }

        public IEnumerable<FileCabinetRecord> FindByFirstName(string firstName)
        {
            if (firstName is null)
            {
                throw new ArgumentNullException(nameof(firstName));
            }

            return this.firstNameDictionary[firstName];
        }

        public IEnumerable<FileCabinetRecord> FindByLastName(string lastName)
        {
            if (lastName is null)
            {
                throw new ArgumentNullException(nameof(lastName));
            }

            return this.lastNameDictionary[lastName];
        }

        public IEnumerable<FileCabinetRecord> FindByDateoOfBirth(string dateofbirth)
        {
            if (dateofbirth is null)
            {
                throw new ArgumentNullException(nameof(dateofbirth));
            }

            return this.dateOfBirthDictionary[DateTime.Parse(dateofbirth, CultureInfo.CurrentCulture)];
        }

        public IEnumerable<FileCabinetRecord> FindByProperty1(string property1)
        {
            if (property1 is null)
            {
                throw new ArgumentNullException(nameof(property1));
            }

            return this.property1Dictionary[Convert.ToInt16(property1, CultureInfo.CurrentCulture)];
        }

        public IEnumerable<FileCabinetRecord> FindByProperty2(string property2)
        {
            if (property2 is null)
            {
                throw new ArgumentNullException(nameof(property2));
            }

            return this.property2Dictionary[Convert.ToDecimal(property2, CultureInfo.CurrentCulture)];
        }

        public IEnumerable<FileCabinetRecord> FindByProperty3(string property3)
        {
            if (property3 is null)
            {
                throw new ArgumentNullException(nameof(property3));
            }

            return this.property3Dictionary[Convert.ToChar(property3, CultureInfo.CurrentCulture)];
        }

        public FileCabinetServiceSnapshot MakeSnapshot()
        {
            return new FileCabinetServiceSnapshot(this.list);
        }

        public void Restore(FileCabinetServiceSnapshot snapshot)
        {
            var records = snapshot.GetRecords();
            var newlist = new List<FileCabinetRecord>(records);
            foreach (var record in newlist)
            {
                if (!this.validator.ValidateParametrs(record))
                {
                    Console.WriteLine($"Record validation error with id number {record.Id},record skipped");
                    newlist.Remove(record);
                }
            }

            var newlistconst = new List<FileCabinetRecord>(newlist);
            foreach (var record in newlistconst)
            {
                var index = this.list.FindIndex(x => x.Id == record.Id);
                if (index >= 0)
                {
                    newlist.Remove(record);
                    this.RemoveFromDictionary(this.list[index]);
                    this.list.Insert(index, record);
                    this.list.RemoveAt(index + 1);
                    continue;
                }
                else
                {
                    this.list.Add(record);
                }

                this.AddToDictionary(record);
            }

            Console.WriteLine($"{newlist.Count} records were imported");
        }

        private void AddToDictionary(FileCabinetRecord record)
        {
            if (!this.idDictionary.ContainsKey(record.Id))
            {
                this.idDictionary.Add(record.Id, new List<FileCabinetRecord>());
            }

            this.idDictionary[record.Id].Add(record);

            if (!this.firstNameDictionary.ContainsKey(record.FirstName))
            {
                this.firstNameDictionary.Add(record.FirstName, new List<FileCabinetRecord>());
            }

            this.firstNameDictionary[record.FirstName].Add(record);

            if (!this.lastNameDictionary.ContainsKey(record.LastName))
            {
                this.lastNameDictionary.Add(record.LastName, new List<FileCabinetRecord>());
            }

            this.lastNameDictionary[record.LastName].Add(record);

            if (!this.dateOfBirthDictionary.ContainsKey(record.DateOfBirth))
            {
                this.dateOfBirthDictionary.Add(record.DateOfBirth, new List<FileCabinetRecord>());
            }

            this.dateOfBirthDictionary[record.DateOfBirth].Add(record);

            if (!this.property1Dictionary.ContainsKey(record.Property1))
            {
                this.property1Dictionary.Add(record.Property1, new List<FileCabinetRecord>());
            }

            this.property1Dictionary[record.Property1].Add(record);

            if (!this.property2Dictionary.ContainsKey(record.Property2))
            {
                this.property2Dictionary.Add(record.Property2, new List<FileCabinetRecord>());
            }

            this.property2Dictionary[record.Property2].Add(record);

            if (!this.property3Dictionary.ContainsKey(record.Property3))
            {
                this.property3Dictionary.Add(record.Property3, new List<FileCabinetRecord>());
            }

            this.property3Dictionary[record.Property3].Add(record);
        }

        private void RemoveFromDictionary(FileCabinetRecord record)
        {
            this.idDictionary[record.Id].Remove(record);
            this.firstNameDictionary[record.FirstName].Remove(record);
            this.lastNameDictionary[record.LastName].Remove(record);
            this.dateOfBirthDictionary[record.DateOfBirth].Remove(record);
            this.property1Dictionary[record.Property1].Remove(record);
            this.property2Dictionary[record.Property2].Remove(record);
            this.property3Dictionary[record.Property3].Remove(record);
        }
    }
}