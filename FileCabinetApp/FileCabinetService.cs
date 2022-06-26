using System.Globalization;

namespace FileCabinetApp
{
    public abstract class FileCabinetService
    {
        private readonly List<FileCabinetRecord> list = new List<FileCabinetRecord>();

        private readonly Dictionary<string, List<FileCabinetRecord>> FirstNameDictionary = new Dictionary<string, List<FileCabinetRecord>>();
        private readonly Dictionary<string, List<FileCabinetRecord>> LastNameDictionary = new Dictionary<string, List<FileCabinetRecord>>();
        private readonly Dictionary<DateTime, List<FileCabinetRecord>> DateOfBirthDictionary = new Dictionary<DateTime, List<FileCabinetRecord>>();

        public abstract string ValidateInfo();

        public int CreateRecord(FileCabinetRecord newRecord)
        {
            if (!this.ValidateParameters(newRecord))
            {
                throw new ArgumentException("incorrect format", nameof(newRecord));
            }

            newRecord.Id = this.list.Count + 1;
            this.list.Add(newRecord);

            if (!this.FirstNameDictionary.ContainsKey(newRecord.FirstName))
            {
                this.FirstNameDictionary.Add(newRecord.FirstName, new List<FileCabinetRecord>());
            }

            this.FirstNameDictionary[newRecord.FirstName].Add(newRecord);

            if (!this.LastNameDictionary.ContainsKey(newRecord.LastName))
            {
                this.LastNameDictionary.Add(newRecord.LastName, new List<FileCabinetRecord>());
            }

            this.LastNameDictionary[newRecord.LastName].Add(newRecord);

            if (!this.DateOfBirthDictionary.ContainsKey(newRecord.DateOfBirth))
            {
                this.DateOfBirthDictionary.Add(newRecord.DateOfBirth, new List<FileCabinetRecord>());
            }

            this.DateOfBirthDictionary[newRecord.DateOfBirth].Add(newRecord);

            return newRecord.Id;
        }

        public FileCabinetRecord[] GetRecords()
        {
            if (this.list.Count == 0)
            {
                return Array.Empty<FileCabinetRecord>();
            }
            else
            {
                var records = new FileCabinetRecord[this.list.Count];
                foreach (var record in this.list)
                {
                    records[record.Id - 1] = record;
                }

                return records;
            }
        }

        public int GetStat()
        {
            return this.list.Count;
        }

        public void EditRecord(int id, FileCabinetRecord recordEdit)
        {
            if (!this.ValidateParameters(recordEdit))
            {
                throw new ArgumentException("incorrect format", nameof(recordEdit));
            }

            foreach (var record in this.list)
            {
                if (record.Id == id)
                {
                    this.CreateRecord(recordEdit);
                    recordEdit.Id = record.Id;
                    this.list.Insert(id - 1, recordEdit);
                    this.list.RemoveAt(this.list.Count - 1);
                    this.list.RemoveAt(id);
                    this.FirstNameDictionary[record.FirstName].Remove(record);
                    this.LastNameDictionary[record.LastName].Remove(record);
                    this.DateOfBirthDictionary[record.DateOfBirth].Remove(record);
                    Console.WriteLine($"Record #{id} is updated.");
                    return;
                }
            }

            throw new ArgumentException("index is not exsist.", nameof(id));
        }

        public FileCabinetRecord[] FindByFirstName(string firstName)
        {
            if (firstName is null)
            {
                throw new ArgumentNullException(nameof(firstName));
            }

            return this.FirstNameDictionary[firstName].ToArray();
        }

        public FileCabinetRecord[] FindByLastName(string lastName)
        {
            if (lastName is null)
            {
                throw new ArgumentNullException(nameof(lastName));
            }

            return this.LastNameDictionary[lastName].ToArray();
        }

        public FileCabinetRecord[] FindByDateoOfBirth(string dateofbirth)
        {
            if (dateofbirth is null)
            {
                throw new ArgumentNullException(nameof(dateofbirth));
            }

            return this.DateOfBirthDictionary[DateTime.Parse(dateofbirth, CultureInfo.CurrentCulture)].ToArray();
        }
    }
}