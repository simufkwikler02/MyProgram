using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using ConsoleTables;

namespace FileCabinetApp
{
    /// <summary>
    ///    Represents the file cabinet service with a set of methods that can interact with records.
    /// </summary>
    /// <remarks> That class uses a file to store data.</remarks>
    public class FileCabinetFilesystemService : IFileCabinetService, IDisposable
    {
        private readonly string serviceRules = "file";
        private readonly IRecordValidator? validator;
        private readonly long recordSize = 278;

        private int deleteRecords;
        private FileStream? fileStream;
        private bool isDisposed;

        /// <summary>Initializes a new instance of the <see cref="FileCabinetFilesystemService" /> class.</summary>
        /// <param name="validator">The validator.</param>
        public FileCabinetFilesystemService(IRecordValidator? validator)
        {
            this.fileStream = new FileStream("cabinet-records.db", FileMode.OpenOrCreate);
            this.validator = validator;
            int i = 0;
            var poz = (int)this.fileStream.Seek(0, SeekOrigin.Begin);
            while (this.fileStream.Position < this.fileStream.Length)
            {
                var record = this.ReadRecord();
                if (record != null)
                {
                    i++;
                }
            }

            this.deleteRecords = this.GetStat() - i;
        }

        /// <summary>Finalizes an instance of the <see cref="FileCabinetFilesystemService" /> class.</summary>
        ~FileCabinetFilesystemService()
        {
            this.Dispose(false);
        }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>Return the name of the validation rules.</summary>
        /// <returns>
        ///   The name of the validation rules <see cref="string" />.
        /// </returns>
        public string ServiceInfo()
        {
            return this.serviceRules;
        }

        /// <summary>Adds a new record to a file.</summary>
        /// <param name="newRecord">The new record.</param>
        /// <returns>
        ///   New record id.
        /// </returns>
        /// <exception cref="System.ArgumentException"> Validation fail - newRecord. </exception>
        public int CreateRecord(FileCabinetRecord newRecord)
        {
            if (!(this.validator?.ValidateParametrs(newRecord) ?? false))
            {
                throw new ArgumentException("Validation fail", nameof(newRecord));
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

            this.fileStream?.Seek(0, SeekOrigin.End);
            short status = 0;

            this.WriteRecord(status, newRecord);
            return newRecord.Id;
        }

        /// <summary>
        /// Purges deleted records.
        /// </summary>
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

        /// <summary>Gets all records.</summary>
        /// <returns>
        ///   All records <see langword="ReadOnlyCollection" />.
        /// </returns>
        public ReadOnlyCollection<FileCabinetRecord> GetRecords()
        {
            if (this.fileStream?.Length == 0)
            {
                return new ReadOnlyCollection<FileCabinetRecord>(Array.Empty<FileCabinetRecord>());
            }

            var poz = this.fileStream?.Seek(0, SeekOrigin.Begin);

            List<FileCabinetRecord> list = new List<FileCabinetRecord>();
            while (this.fileStream?.Position < this.fileStream?.Length)
            {
                var record = this.ReadRecord();
                if (record != null)
                {
                    list.Add(record);
                }
            }

            return new ReadOnlyCollection<FileCabinetRecord>(list);
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

            this.fileStream?.Seek(position, SeekOrigin.Begin);
            short status = 0;
            this.WriteRecord(status, recordUpdate);
            return recordUpdate.Id;
        }

        /// <summary>Deletes the record.</summary>
        /// <param name="record">The record that should be deleted.</param>
        /// <returns>
        ///   Deleted record id.
        /// </returns>
        public int DeleteRecord(FileCabinetRecord record)
        {
            var index = this.FindIndex(record);

            if (index == -1)
            {
                return -1;
            }

            this.fileStream?.Seek(index, SeekOrigin.Begin);
            var recordEdit = record;
            short status = 1;

            this.WriteRecord(status, recordEdit);
            this.deleteRecords++;
            return recordEdit.Id;
        }

        /// <summary>Gets count of records deleted.</summary>
        /// <returns>
        ///   Count of records deleted.
        /// </returns>
        public int GetStatDelete()
        {
            return this.deleteRecords;
        }

        /// <summary>Gets count of records.</summary>
        /// <returns>
        ///   Count of records.
        /// </returns>
        public int GetStat()
        {
            if (this.fileStream?.Length == 0)
            {
                return 0;
            }

            var numberRecords = this.fileStream?.Length ?? 0 / this.recordSize;
            return (int)numberRecords - this.deleteRecords;
        }

        /// <summary>Identifiers the exist.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   <see langword="true"/> if the record with this id exists, <see langword="false"/> otherwise.
        /// </returns>
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

        /// <summary>Finds the record and return his position.</summary>
        /// <param name="record">The record.</param>
        /// <returns>
        ///   The position of the record.
        /// </returns>
        public long FindIndex(FileCabinetRecord record)
        {
            this.fileStream?.Seek(0, SeekOrigin.Begin);

            while (this.fileStream?.Position < this.fileStream?.Length)
            {
                var newRecord = this.ReadRecord();
                if (record != null && (newRecord?.Equals(record) ?? false))
                {
                    return this.fileStream?.Position ?? 0 - this.recordSize;
                }
            }

            return -1;
        }

        /// <summary>Finds the records by name and value.</summary>
        /// <param name="name">The name of property in the record.</param>
        /// <param name="value">The value of property in the record, which wiil be found.</param>
        /// <returns>
        ///   Returns an enumerator that iterates through a file.
        /// </returns>
        public IEnumerable<FileCabinetRecord> FindRecords(string name, string value)
        {
            if (name.Equals("id", StringComparison.OrdinalIgnoreCase))
            {
                return this.FindById(value);
            }

            if (name.Equals("firstname", StringComparison.OrdinalIgnoreCase))
            {
                return this.FindByFirstName(value);
            }

            if (name.Equals("lastname", StringComparison.OrdinalIgnoreCase))
            {
                return this.FindByLastName(value);
            }

            if (name.Equals("dateofbirth", StringComparison.OrdinalIgnoreCase))
            {
                return this.FindByDateoOfBirth(value);
            }

            if (name.Equals("Property1", StringComparison.OrdinalIgnoreCase))
            {
                return this.FindByProperty1(value);
            }

            if (name.Equals("Property2", StringComparison.OrdinalIgnoreCase))
            {
                return this.FindByProperty2(value);
            }

            if (name.Equals("Property3", StringComparison.OrdinalIgnoreCase))
            {
                return this.FindByProperty3(value);
            }

            return new List<FileCabinetRecord>();
        }

        /// <summary>Gets the record by position.</summary>
        /// <param name="position">The position.</param>
        /// <returns>
        ///  The record by position.
        /// </returns>
        public FileCabinetRecord? GetRecord(long position)
        {
            this.fileStream?.Seek(position, SeekOrigin.Begin);
            return this.ReadRecord();
        }

        /// <summary>Finds the records by identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   Returns an enumerator that iterates through a file.
        /// </returns>
        public IEnumerable<FileCabinetRecord> FindById(string id)
        {
            return new Enumerable(this.fileStream, id, "id");
        }

        /// <summary>Finds the records by first name.</summary>
        /// <param name="firstName">The first name.</param>
        /// <returns>
        ///   Returns an enumerator that iterates through a file.
        /// </returns>
        public IEnumerable<FileCabinetRecord> FindByFirstName(string firstName)
        {
            return new Enumerable(this.fileStream, firstName, "firstname");
        }

        /// <summary>Finds the records by last name.</summary>
        /// <param name="lastName">The last name.</param>
        /// <returns>
        ///    Returns an enumerator that iterates through a file.
        /// </returns>
        public IEnumerable<FileCabinetRecord> FindByLastName(string lastName)
        {
            return new Enumerable(this.fileStream, lastName, "lastname");
        }

        /// <summary>Finds the records by date of birth.</summary>
        /// <param name="dateofbirth">The date of birth.</param>
        /// <returns>
        ///    Returns an enumerator that iterates through a file.
        /// </returns>
        public IEnumerable<FileCabinetRecord> FindByDateoOfBirth(string dateofbirth)
        {
            return new Enumerable(this.fileStream, dateofbirth, "dateofbirth");
        }

        /// <summary>Finds the records by property1.</summary>
        /// <param name="property1">The property1.</param>
        /// <returns>
        ///    Returns an enumerator that iterates through a file.
        /// </returns>
        public IEnumerable<FileCabinetRecord> FindByProperty1(string property1)
        {
            return new Enumerable(this.fileStream, property1, "property1");
        }

        /// <summary>Finds the records by property2.</summary>
        /// <param name="property2">The property2.</param>
        /// <returns>
        ///   Returns an enumerator that iterates through a file.
        /// </returns>
        public IEnumerable<FileCabinetRecord> FindByProperty2(string property2)
        {
            return new Enumerable(this.fileStream, property2, "property2");
        }

        /// <summary>Finds the records by property3.</summary>
        /// <param name="property3">The property3.</param>
        /// <returns>
        ///    Returns an enumerator that iterates through a file.
        /// </returns>
        public IEnumerable<FileCabinetRecord> FindByProperty3(string property3)
        {
            return new Enumerable(this.fileStream, property3, "property3");
        }

        /// <summary>Makes the snapshot of file cabinet service.</summary>
        /// <returns>
        ///   The snapshot of file cabinet service <see cref="FileCabinetServiceSnapshot" />.
        /// </returns>
        public FileCabinetServiceSnapshot MakeSnapshot()
        {
            var records = new List<FileCabinetRecord>(this.GetRecords());
            var snapshot = new FileCabinetServiceSnapshot(records);
            return snapshot;
        }

        /// <summary>Restores the specified snapshot.</summary>
        /// <param name="snapshot">The snapshot of file cabinet service.</param>
        public void Restore(FileCabinetServiceSnapshot snapshot)
        {
            var records = snapshot.GetRecords();
            var newlist = new List<FileCabinetRecord>(records);
            var newlistbuf = new List<FileCabinetRecord>(newlist);
            foreach (var record in newlistbuf)
            {
                if (!(this.validator?.ValidateParametrs(record) ?? false))
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

        /// <summary>Releases unmanaged and - optionally - managed resources.</summary>
        /// <param name="disposing">
        ///   <see langword="true"/> to release both managed and unmanaged resources; <see langword="false"/> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (this.isDisposed)
            {
                return;
            }

            if (disposing)
            {
            }

            this.fileStream?.Close();
            this.isDisposed = true;
        }

        /// <summary>Writes the record to the file.</summary>
        /// <param name="status">The status.</param>
        /// <param name="newRecord">The new record.</param>
        /// <remarks> if status = 1, then the entry is marked as deleted, otherwise status = 0.</remarks>
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

        /// <summary>Reads next record in the file.</summary>
        /// <returns>
        ///   The record <see cref="FileCabinetRecord" />.
        /// </returns>
        private FileCabinetRecord? ReadRecord()
        {
            FileCabinetRecord? record = new FileCabinetRecord();

            if (this.fileStream?.Position == this.fileStream?.Length)
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