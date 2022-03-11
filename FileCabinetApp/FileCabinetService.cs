using System.Globalization;

namespace FileCabinetApp
{
    public static class FileCabinetService
    {
        private static readonly List<FileCabinetRecord> List = new List<FileCabinetRecord>();

        private static readonly Dictionary<string, List<FileCabinetRecord>> FirstNameDictionary = new Dictionary<string, List<FileCabinetRecord>>();
        private static readonly Dictionary<string, List<FileCabinetRecord>> LastNameDictionary = new Dictionary<string, List<FileCabinetRecord>>();
        private static readonly Dictionary<DateTime, List<FileCabinetRecord>> DateOfBirthDictionary = new Dictionary<DateTime, List<FileCabinetRecord>>();

        public static int CreateRecord(string firstName, string lastName, DateTime dateOfBirth, short property1, decimal property2, char property3)
        {
            if (string.IsNullOrEmpty(firstName) || firstName.Length < 2 || firstName.Length > 60)
            {
                throw new ArgumentException("incorrect format firstName", nameof(firstName));
            }

            if (string.IsNullOrEmpty(lastName) || lastName.Length < 2 || lastName.Length > 60)
            {
                throw new ArgumentException("incorrect format lastName", nameof(lastName));
            }

            DateTime minDate = new DateTime(1950, 6, 1);
            if (minDate > dateOfBirth || DateTime.Now < dateOfBirth)
            {
                throw new ArgumentException("incorrect format dateOfBirth", nameof(dateOfBirth));
            }

            if (property1 < 1 || property1 > 100)
            {
                throw new ArgumentException("50 <= property1 <= 100", nameof(property1));
            }

            if (property2 > 2)
            {
                throw new ArgumentException("incorrect format property2", nameof(property2));
            }

            if (property3 == 'l')
            {
                throw new ArgumentException("incorrect format property3", nameof(property3));
            }

            var record = new FileCabinetRecord
            {
                Id = List.Count + 1,
                FirstName = firstName,
                LastName = lastName,
                DateOfBirth = dateOfBirth,
                Property1 = property1,
                Property2 = property2,
                Property3 = property3,
            };

            List.Add(record);

            if (!FirstNameDictionary.ContainsKey(firstName))
            {
                FirstNameDictionary.Add(firstName, new List<FileCabinetRecord>());
            }

            FirstNameDictionary[firstName].Add(record);

            if (!LastNameDictionary.ContainsKey(lastName))
            {
                LastNameDictionary.Add(lastName, new List<FileCabinetRecord>());
            }

            LastNameDictionary[lastName].Add(record);

            if (!DateOfBirthDictionary.ContainsKey(dateOfBirth))
            {
                DateOfBirthDictionary.Add(dateOfBirth, new List<FileCabinetRecord>());
            }

            DateOfBirthDictionary[dateOfBirth].Add(record);

            return record.Id;
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

        public static void EditRecord(int id, string firstName, string lastName, DateTime dateOfBirth, short property1, decimal property2, char property3)
        {
            foreach (var record in List)
            {
                if (record.Id == id)
                {
                    CreateRecord(firstName, lastName, dateOfBirth, property1, property2, property3);
                    var newrecord = List[^1];
                    newrecord.Id = record.Id;
                    List.Insert(id - 1, newrecord);
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