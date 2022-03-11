using System.Globalization;

namespace FileCabinetApp
{
    public static class FileCabinetService
    {
        private static readonly List<FileCabinetRecord> List = new List<FileCabinetRecord>();

        private static readonly Dictionary<string, List<FileCabinetRecord>> FirstNameDictionary = new Dictionary<string, List<FileCabinetRecord>>();
        private static readonly Dictionary<string, List<FileCabinetRecord>> LastNameDictionary = new Dictionary<string, List<FileCabinetRecord>>();
        private static readonly Dictionary<DateTime, List<FileCabinetRecord>> DateOfBirthDictionary = new Dictionary<DateTime, List<FileCabinetRecord>>();

        public static int CreateRecord(FileCabinetRecord newRecord)
        {
            if (string.IsNullOrEmpty(newRecord.FirstName) || newRecord.FirstName.Length < 2 || newRecord.FirstName.Length > 60)
            {
                throw new ArgumentException("incorrect format firstName", nameof(newRecord));
            }

            if (string.IsNullOrEmpty(newRecord.LastName) || newRecord.LastName.Length < 2 || newRecord.LastName.Length > 60)
            {
                throw new ArgumentException("incorrect format lastName", nameof(newRecord));
            }

            DateTime minDate = new DateTime(1950, 6, 1);
            if (minDate > newRecord.DateOfBirth || DateTime.Now < newRecord.DateOfBirth)
            {
                throw new ArgumentException("incorrect format dateOfBirth", nameof(newRecord));
            }

            if (newRecord.Property1 < 1 || newRecord.Property1 > 100)
            {
                throw new ArgumentException("50 <= property1 <= 100", nameof(newRecord));
            }

            if (newRecord.Property2 > 2)
            {
                throw new ArgumentException("incorrect format property2", nameof(newRecord));
            }

            if (newRecord.Property3 == 'l')
            {
                throw new ArgumentException("incorrect format property3", nameof(newRecord));
            }

            List.Add(newRecord);

            if (!FirstNameDictionary.ContainsKey(newRecord.FirstName))
            {
                FirstNameDictionary.Add(newRecord.FirstName, new List<FileCabinetRecord>());
            }

            FirstNameDictionary[newRecord.FirstName].Add(newRecord);

            if (!LastNameDictionary.ContainsKey(newRecord.LastName))
            {
                LastNameDictionary.Add(newRecord.LastName, new List<FileCabinetRecord>());
            }

            LastNameDictionary[newRecord.LastName].Add(newRecord);

            if (!DateOfBirthDictionary.ContainsKey(newRecord.DateOfBirth))
            {
                DateOfBirthDictionary.Add(newRecord.DateOfBirth, new List<FileCabinetRecord>());
            }

            DateOfBirthDictionary[newRecord.DateOfBirth].Add(newRecord);

            return newRecord.Id;
        }

        public static FileCabinetRecord[] GetRecords()
        {
            if (List.Count == 0)
            {
                return Array.Empty<FileCabinetRecord>();
            }
            else
            {
                var records = new FileCabinetRecord[List.Count];
                foreach (var record in List)
                {
                    records[record.Id - 1] = record;
                }

                return records;
            }
        }

        public static int GetStat()
        {
            return List.Count;
        }

        public static void EditRecord(int id, FileCabinetRecord recordEdit)
        {
            foreach (var record in List)
            {
                if (record.Id == id)
                {
                    CreateRecord(recordEdit);
                    recordEdit.Id = record.Id;
                    List.Insert(id - 1, recordEdit);
                    List.RemoveAt(List.Count - 1);
                    List.RemoveAt(id);
                    FirstNameDictionary[record.FirstName].Remove(record);
                    LastNameDictionary[record.LastName].Remove(record);
                    DateOfBirthDictionary[record.DateOfBirth].Remove(record);
                    Console.WriteLine($"Record #{id} is updated.");
                    return;
                }
            }

            throw new ArgumentException("index is not exsist.", nameof(id));
        }

        public static FileCabinetRecord[] FindByFirstName(string firstName)
        {
            if (firstName is null)
            {
                throw new ArgumentNullException(nameof(firstName));
            }

            return FirstNameDictionary[firstName].ToArray();
        }

        public static FileCabinetRecord[] FindByLastName(string lastName)
        {
            if (lastName is null)
            {
                throw new ArgumentNullException(nameof(lastName));
            }

            return LastNameDictionary[lastName].ToArray();
        }

        public static FileCabinetRecord[] FindByDateoOfBirth(string dateofbirth)
        {
            if (dateofbirth is null)
            {
                throw new ArgumentNullException(nameof(dateofbirth));
            }

            return DateOfBirthDictionary[DateTime.Parse(dateofbirth, CultureInfo.CurrentCulture)].ToArray();
        }
    }
}