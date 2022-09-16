using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace FileCabinetApp
{
    /// <summary>
    ///    Represents the file cabinet service with a set of methods that can interact with records.
    /// </summary>
    public interface IFileCabinetService
    {
        /// <summary>Adds a new record to a collection.</summary>
        /// <param name="newRecord">The new record.</param>
        /// <returns>
        ///   New record id.
        /// </returns>
        /// <exception cref="System.ArgumentException"> Validation fail - newRecord. </exception>
        public int CreateRecord(FileCabinetRecord newRecord);

        /// <summary>Return the name of the validation rules.</summary>
        /// <returns>
        ///   The name of the validation rules <see cref="string" />.
        /// </returns>
        public string ServiceInfo();

        /// <summary>Updates the record.</summary>
        /// <param name="position">Position of the old record.</param>
        /// <param name="recordUpdate">A new record that will overwrite the old record.</param>
        /// <returns>
        ///   Updated record id.
        /// </returns>
        public int UpdateRecord(long position, FileCabinetRecord recordUpdate);

        /// <summary>Gets all records.</summary>
        /// <returns>
        ///   All records <see langword="ReadOnlyCollection" />.
        /// </returns>
        public ReadOnlyCollection<FileCabinetRecord> GetRecords();

        /// <summary>Gets count of records.</summary>
        /// <returns>
        ///   Count of records.
        /// </returns>
        public int GetStat();

        /// <summary>Identifiers the exist.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   <see langword="true"/> if the record with this id exists, <see langword="false"/> otherwise.
        /// </returns>
        public bool IdExist(int id);

        /// <summary>Finds the records by name and value.</summary>
        /// <param name="name">The name of property in the record.</param>
        /// <param name="value">The value of property in the record, which wiil be found.</param>
        /// <returns>
        ///   Returns an enumerator.
        /// </returns>
        public IEnumerable<FileCabinetRecord> FindRecords(string name, string value);

        /// <summary>Gets the record by position.</summary>
        /// <param name="position">The position.</param>
        /// <returns>
        ///   The record by position.
        /// </returns>
        public FileCabinetRecord? GetRecord(long position);

        /// <summary>Finds the record and return his position.</summary>
        /// <param name="record">The record.</param>
        /// <returns>
        ///   The position of the record.
        /// </returns>
        public long FindIndex(FileCabinetRecord record);

        /// <summary>Deletes the record.</summary>
        /// <param name="record">The record that should be deleted.</param>
        /// <returns>
        ///   Deleted record id.
        /// </returns>
        public int DeleteRecord(FileCabinetRecord record);

        /// <summary>Gets count of records deleted.</summary>
        /// <returns>
        ///   Count of records deleted.
        /// </returns>
        public int GetStatDelete();

        /// <summary>
        /// Purges deleted records.
        /// </summary>
        public void PurgeRecords();

        /// <summary>Finds the records by identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   Returns an enumerator.
        /// </returns>
        public IEnumerable<FileCabinetRecord> FindById(string id);

        /// <summary>Finds the records by first name.</summary>
        /// <param name="firstName">The first name.</param>
        /// <returns>
        ///   Returns an enumerator.
        /// </returns>
        public IEnumerable<FileCabinetRecord> FindByFirstName(string firstName);

        /// <summary>Finds the records by last name.</summary>
        /// <param name="lastName">The last name.</param>
        /// <returns>
        ///    Returns an enumerator.
        /// </returns>
        public IEnumerable<FileCabinetRecord> FindByLastName(string lastName);

        /// <summary>Finds the records by date of birth.</summary>
        /// <param name="dateofbirth">The date of birth.</param>
        /// <returns>
        ///    Returns an enumerator.
        /// </returns>
        public IEnumerable<FileCabinetRecord> FindByDateoOfBirth(string dateofbirth);

        /// <summary>Finds the records by property1.</summary>
        /// <param name="property1">The property1.</param>
        /// <returns>
        ///    Returns an enumerator.
        /// </returns>
        public IEnumerable<FileCabinetRecord> FindByProperty1(string property1);

        /// <summary>Finds the records by property2.</summary>
        /// <param name="property2">The property2.</param>
        /// <returns>
        ///   Returns an enumerator.
        /// </returns>
        public IEnumerable<FileCabinetRecord> FindByProperty2(string property2);

        /// <summary>Finds the records by property3.</summary>
        /// <param name="property3">The property3.</param>
        /// <returns>
        ///    Returns an enumerator.
        /// </returns>
        public IEnumerable<FileCabinetRecord> FindByProperty3(string property3);

        /// <summary>Makes the snapshot of file cabinet service.</summary>
        /// <returns>
        ///   The snapshot of file cabinet service <see cref="FileCabinetServiceSnapshot" />.
        /// </returns>
        public FileCabinetServiceSnapshot MakeSnapshot();

        /// <summary>Restores the specified snapshot.</summary>
        /// <param name="snapshot">The snapshot of file cabinet service.</param>
        public void Restore(FileCabinetServiceSnapshot snapshot);
    }
}
