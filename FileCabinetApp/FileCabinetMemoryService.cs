using System.Collections.ObjectModel;
using System.Globalization;

namespace FileCabinetApp
{
    /// <summary>
    ///    Represents the file cabinet service with a set of methods that can interact with records.
    /// </summary>
    /// <remarks> That class uses a memory to store data.</remarks>
    public class FileCabinetMemoryService : IFileCabinetService
    {
        private readonly List<FileCabinetRecord> list = new List<FileCabinetRecord>();
        private readonly IRecordValidator? validator;
        private readonly string serviceRules = "memory";
        private readonly int deleteRecords;

        private readonly Dictionary<int, List<FileCabinetRecord>> idrecordDictionary = new Dictionary<int, List<FileCabinetRecord>>();
        private readonly Dictionary<string, List<FileCabinetRecord>> firstNameDictionary = new Dictionary<string, List<FileCabinetRecord>>();
        private readonly Dictionary<string, List<FileCabinetRecord>> lastNameDictionary = new Dictionary<string, List<FileCabinetRecord>>();
        private readonly Dictionary<DateTime, List<FileCabinetRecord>> dateOfBirthDictionary = new Dictionary<DateTime, List<FileCabinetRecord>>();
        private readonly Dictionary<short, List<FileCabinetRecord>> property1Dictionary = new Dictionary<short, List<FileCabinetRecord>>();
        private readonly Dictionary<decimal, List<FileCabinetRecord>> property2Dictionary = new Dictionary<decimal, List<FileCabinetRecord>>();
        private readonly Dictionary<char, List<FileCabinetRecord>> property3Dictionary = new Dictionary<char, List<FileCabinetRecord>>();

        private readonly Dictionary<string, IEnumerable<FileCabinetRecord>> findRecordsDictionary = new Dictionary<string, IEnumerable<FileCabinetRecord>>();
        private readonly Dictionary<FileCabinetRecord, long> findIndexDictionary = new Dictionary<FileCabinetRecord, long>();

        /// <summary>Initializes a new instance of the <see cref="FileCabinetMemoryService" /> class.</summary>
        /// <param name="validator">The validator.</param>
        public FileCabinetMemoryService(IRecordValidator? validator)
        {
            this.validator = validator;
        }

        /// <summary>Return the name of the validation rules.</summary>
        /// <returns>
        ///   The name of the validation rules <see cref="string" />.
        /// </returns>
        public string ServiceInfo()
        {
            return this.serviceRules;
        }

        /// <summary>Adds a new record to the list.</summary>
        /// <param name="newRecord">The new record.</param>
        /// <returns>
        ///   New record id.
        /// </returns>
        /// <exception cref="System.ArgumentException"> Validation fail - newRecord. </exception>
        public int CreateRecord(FileCabinetRecord newRecord)
        {
            if (!this.validator?.ValidateParametrs(newRecord) ?? false)
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

            this.findIndexDictionary.Clear();
            this.findRecordsDictionary.Clear();
            return newRecord.Id;
        }

        /// <summary>Gets all records.</summary>
        /// <returns>
        ///   All records <see langword="ReadOnlyCollection" />.
        /// </returns>
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

        /// <summary>Identifiers the exist.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   The position of the record.
        /// </returns>
        public bool IdExist(int id)
        {
            var index = this.list.FindIndex(x => x.Id == id);
            if (index == -1)
            {
                return false;
            }

            return true;
        }

        /// <summary>Finds the records by name and value.</summary>
        /// <param name="name">The name of property in the record.</param>
        /// <param name="value">The value of property in the record, which wiil be found.</param>
        /// <returns>
        ///   Returns an enumerator.
        /// </returns>
        public IEnumerable<FileCabinetRecord> FindRecords(string name, string value)
        {
            if (this.findRecordsDictionary.ContainsKey(name + value))
            {
                return this.findRecordsDictionary[name + value];
            }

            if (name.Equals("id", StringComparison.OrdinalIgnoreCase))
            {
                this.findRecordsDictionary.Add(name + value, this.FindById(value));
                return this.FindById(value);
            }

            if (name.Equals("firstname", StringComparison.OrdinalIgnoreCase))
            {
                this.findRecordsDictionary.Add(name + value, this.FindByFirstName(value));
                return this.FindByFirstName(value);
            }

            if (name.Equals("lastname", StringComparison.OrdinalIgnoreCase))
            {
                this.findRecordsDictionary.Add(name + value, this.FindByLastName(value));
                return this.FindByLastName(value);
            }

            if (name.Equals("dateofbirth", StringComparison.OrdinalIgnoreCase))
            {
                this.findRecordsDictionary.Add(name + value, this.FindByDateoOfBirth(value));
                return this.FindByDateoOfBirth(value);
            }

            if (name.Equals("Property1", StringComparison.OrdinalIgnoreCase))
            {
                this.findRecordsDictionary.Add(name + value, this.FindByProperty1(value));
                return this.FindByProperty1(value);
            }

            if (name.Equals("Property2", StringComparison.OrdinalIgnoreCase))
            {
                this.findRecordsDictionary.Add(name + value, this.FindByProperty2(value));
                return this.FindByProperty2(value);
            }

            if (name.Equals("Property3", StringComparison.OrdinalIgnoreCase))
            {
                this.findRecordsDictionary.Add(name + value, this.FindByProperty3(value));
                return this.FindByProperty3(value);
            }

            this.findRecordsDictionary.Add(name + value, new List<FileCabinetRecord>());
            return new List<FileCabinetRecord>();
        }

        /// <summary>Finds the record and return his position.</summary>
        /// <param name="record">The record.</param>
        /// <returns>
        ///   The position of the record.
        /// </returns>
        public long FindIndex(FileCabinetRecord record)
        {
            if (this.findIndexDictionary.ContainsKey(record))
            {
                return this.findIndexDictionary[record];
            }

            this.findIndexDictionary.Add(record, this.list.FindIndex(x => x == record));
            return this.list.FindIndex(x => x == record);
        }

        /// <summary>Gets the record by position.</summary>
        /// <param name="position">The position.</param>
        /// <returns>
        ///  The record by position.
        /// </returns>
        public FileCabinetRecord GetRecord(long position)
        {
            return this.list[(int)position];
        }

        /// <summary>Gets count of records.</summary>
        /// <returns>
        ///   Count of records.
        /// </returns>
        public int GetStat()
        {
            return this.list.Count;
        }

        /// <summary>Gets count of records deleted.</summary>
        /// <returns>
        ///   Count of records deleted.
        /// </returns>
        public int GetStatDelete()
        {
            return this.deleteRecords;
        }

        /// <summary>Updates the record.</summary>
        /// <param name="position">Position of the old record.</param>
        /// <param name="recordUpdate">A new record that will overwrite the old record.</param>
        /// <returns>
        ///   Updated record id.
        /// </returns>
        public int UpdateRecord(long position, FileCabinetRecord recordUpdate)
        {
            if (!(this.validator?.ValidateParametrs(recordUpdate) ?? false))
            {
                return -1;
            }

            this.RemoveFromDictionary(this.list[(int)position]);
            this.list[(int)position] = recordUpdate;
            this.AddToDictionary(recordUpdate);

            this.findIndexDictionary.Clear();
            this.findRecordsDictionary.Clear();
            return recordUpdate.Id;
        }

        /// <summary>Deletes the record.</summary>
        /// <param name="record">The record that should be deleted.</param>
        /// <returns>
        ///   Deleted record id.
        /// </returns>
        public int DeleteRecord(FileCabinetRecord record)
        {
            if (this.FindIndex(record) == -1)
            {
                return -1;
            }

            this.list.Remove(record);
            this.RemoveFromDictionary(record);

            this.findIndexDictionary.Clear();
            this.findRecordsDictionary.Clear();
            return record.Id;
        }

        /// <summary>Purges deleted records.</summary>
        public void PurgeRecords()
        {
            Console.WriteLine("Error -> This command works only with 'file' type of service");
            return;
        }

        /// <summary>Finds the records by identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   Returns an enumerator.
        /// </returns>
        public IEnumerable<FileCabinetRecord> FindById(string id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return this.idrecordDictionary[Convert.ToInt32(id, CultureInfo.CurrentCulture)];
        }

        /// <summary>Finds the records by first name.</summary>
        /// <param name="firstName">The first name.</param>
        /// <returns>
        ///   Returns an enumerator.
        /// </returns>
        public IEnumerable<FileCabinetRecord> FindByFirstName(string firstName)
        {
            if (firstName is null)
            {
                throw new ArgumentNullException(nameof(firstName));
            }

            return this.firstNameDictionary[firstName];
        }

        /// <summary>Finds the records by last name.</summary>
        /// <param name="lastName">The last name.</param>
        /// <returns>
        ///    Returns an enumerator.
        /// </returns>
        public IEnumerable<FileCabinetRecord> FindByLastName(string lastName)
        {
            if (lastName is null)
            {
                throw new ArgumentNullException(nameof(lastName));
            }

            return this.lastNameDictionary[lastName];
        }

        /// <summary>Finds the records by date of birth.</summary>
        /// <param name="dateofbirth">The date of birth.</param>
        /// <returns>
        ///    Returns an enumerator.
        /// </returns>
        public IEnumerable<FileCabinetRecord> FindByDateoOfBirth(string dateofbirth)
        {
            if (dateofbirth is null)
            {
                throw new ArgumentNullException(nameof(dateofbirth));
            }

            return this.dateOfBirthDictionary[DateTime.Parse(dateofbirth, CultureInfo.CurrentCulture)];
        }

        /// <summary>Finds the records by property1.</summary>
        /// <param name="property1">The property1.</param>
        /// <returns>
        ///    Returns an enumerator.
        /// </returns>
        public IEnumerable<FileCabinetRecord> FindByProperty1(string property1)
        {
            if (property1 is null)
            {
                throw new ArgumentNullException(nameof(property1));
            }

            return this.property1Dictionary[Convert.ToInt16(property1, CultureInfo.CurrentCulture)];
        }

        /// <summary>Finds the records by property2.</summary>
        /// <param name="property2">The property2.</param>
        /// <returns>
        ///   Returns an enumerator.
        /// </returns>
        public IEnumerable<FileCabinetRecord> FindByProperty2(string property2)
        {
            if (property2 is null)
            {
                throw new ArgumentNullException(nameof(property2));
            }

            return this.property2Dictionary[Convert.ToDecimal(property2, CultureInfo.CurrentCulture)];
        }

        /// <summary>Finds the records by property3.</summary>
        /// <param name="property3">The property3.</param>
        /// <returns>
        ///    Returns an enumerator.
        /// </returns>
        public IEnumerable<FileCabinetRecord> FindByProperty3(string property3)
        {
            if (property3 is null)
            {
                throw new ArgumentNullException(nameof(property3));
            }

            return this.property3Dictionary[Convert.ToChar(property3, CultureInfo.CurrentCulture)];
        }

        /// <summary>Makes the snapshot of file cabinet service.</summary>
        /// <returns>
        ///   The snapshot of file cabinet service <see cref="FileCabinetServiceSnapshot" />.
        /// </returns>
        public FileCabinetServiceSnapshot MakeSnapshot()
        {
            return new FileCabinetServiceSnapshot(this.list);
        }

        /// <summary>Restores the specified snapshot.</summary>
        /// <param name="snapshot">The snapshot of file cabinet service.</param>
        public void Restore(FileCabinetServiceSnapshot snapshot)
        {
            var records = snapshot.GetRecords();
            var newlist = new List<FileCabinetRecord>(records);
            foreach (var record in newlist)
            {
                if (!(this.validator?.ValidateParametrs(record) ?? false))
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

        /// <summary>Adds record to search dictionaries.</summary>
        /// <param name="record">The record.</param>
        private void AddToDictionary(FileCabinetRecord record)
        {
            if (!this.idrecordDictionary.ContainsKey(record.Id))
            {
                this.idrecordDictionary.Add(record.Id, new List<FileCabinetRecord>());
            }

            this.idrecordDictionary[record.Id].Add(record);

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

        /// <summary>Removes record from search dictionaries.</summary>
        /// <param name="record">The record.</param>
        private void RemoveFromDictionary(FileCabinetRecord record)
        {
            this.idrecordDictionary[record.Id].Remove(record);
            this.firstNameDictionary[record.FirstName].Remove(record);
            this.lastNameDictionary[record.LastName].Remove(record);
            this.dateOfBirthDictionary[record.DateOfBirth].Remove(record);
            this.property1Dictionary[record.Property1].Remove(record);
            this.property2Dictionary[record.Property2].Remove(record);
            this.property3Dictionary[record.Property3].Remove(record);
        }
    }
}