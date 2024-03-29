﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCabinetApp
{
    /// <summary>
    ///    Represents the file cabinet service logger which saves information about service method calls and passed parameters to a text file.
    /// </summary>
    public class ServiceLogger : IFileCabinetService
    {
        private readonly IFileCabinetService service;

        /// <summary>Initializes a new instance of the <see cref="ServiceLogger" /> class.</summary>
        /// <param name="service">The file cabinet service.</param>
        public ServiceLogger(IFileCabinetService service)
        {
            this.service = service;
            StreamWriter fstream = new StreamWriter("log.txt", false);
            fstream.Close();
        }

        /// <summary>Saves information about CreateRecord method to a text file.</summary>
        /// <param name="newRecord">The new record.</param>
        /// <returns>
        ///   New record id.
        /// </returns>
        /// <exception cref="System.ArgumentException"> Validation fail - newRecord. </exception>
        public int CreateRecord(FileCabinetRecord newRecord)
        {
            using (StreamWriter fstream = new StreamWriter("log.txt", true))
            {
                fstream.WriteLine($"{DateTime.Now} - Calling CreateRecord() with FirstName = '{newRecord.FirstName}', LastName = '{newRecord.LastName}', DateOfBirth = '{newRecord.DateOfBirth.Date}',  Property1 = '{newRecord.Property1}', Property2 = '{newRecord.Property2}', Property3 = '{newRecord.Property3}',");
                try
                {
                    var result = this.service.CreateRecord(newRecord);
                    fstream.WriteLine($"{DateTime.Now} - CreateRecord() returned '{result}'");
                    return result;
                }
                catch (Exception ex)
                {
                    fstream.WriteLine($"{DateTime.Now} - Catch Exception '{ex.Message}'");
                    throw new ArgumentException($"{ex.Message}");
                }
            }
        }

        /// <summary>Saves information about DeleteRecord method to a text file.</summary>
        /// <param name="record">The record that should be deleted.</param>
        /// <returns>
        ///   Deleted record id.
        /// </returns>
        public int DeleteRecord(FileCabinetRecord record)
        {
            using (StreamWriter fstream = new StreamWriter("log.txt", true))
            {
                fstream.WriteLine($"{DateTime.Now} - Calling DeleteRecord() with record = '{record}'");
                try
                {
                    var result = this.service.DeleteRecord(record);
                    fstream.WriteLine($"{DateTime.Now} - DeleteRecord() returned '{result}'");
                    return result;
                }
                catch (Exception ex)
                {
                    fstream.WriteLine($"{DateTime.Now} - Catch Exception '{ex.Message}'");
                    throw new ArgumentException($"{ex.Message}");
                }
            }
        }

        /// <summary>Saves information about UpdateRecord method to a text file.</summary>
        /// <param name="position">Position of the old record.</param>
        /// <param name="recordUpdate">A new record that will overwrite the old record.</param>
        /// <returns>
        ///   Updated record id.
        /// </returns>
        public int UpdateRecord(long position, FileCabinetRecord recordUpdate)
        {
            using (StreamWriter fstream = new StreamWriter("log.txt", true))
            {
                fstream.WriteLine($"{DateTime.Now} - Calling UpdateRecord() with position = '{position}', recordUpdate = '{recordUpdate}'");
                try
                {
                    var result = this.service.UpdateRecord(position, recordUpdate);
                    fstream.WriteLine($"{DateTime.Now} - UpdateRecord() returned '{result}'");
                    return result;
                }
                catch (Exception ex)
                {
                    fstream.WriteLine($"{DateTime.Now} - Catch Exception '{ex.Message}'");
                    throw new ArgumentException($"{ex.Message}");
                }
            }
        }

        /// <summary>Saves information about FindById method to a text file.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   Returns an enumerator.
        /// </returns>
        public IEnumerable<FileCabinetRecord> FindById(string id)
        {
            using (StreamWriter fstream = new StreamWriter("log.txt", true))
            {
                fstream.WriteLine($"{DateTime.Now} - Calling FindById() with id = '{id}'");
                try
                {
                    var result = this.service.FindById(id);
                    fstream.WriteLine($"{DateTime.Now} - FindById() returned '{result}'");
                    return result;
                }
                catch (Exception ex)
                {
                    fstream.WriteLine($"{DateTime.Now} - Catch Exception '{ex.Message}'");
                    throw new ArgumentException($"{ex.Message}");
                }
            }
        }

        /// <summary>Saves information about FindByFirstName method to a text file.</summary>
        /// <param name="firstName">The first name.</param>
        /// <returns>
        ///   Returns an enumerator.
        /// </returns>
        public IEnumerable<FileCabinetRecord> FindByFirstName(string firstName)
        {
            using (StreamWriter fstream = new StreamWriter("log.txt", true))
            {
                fstream.WriteLine($"{DateTime.Now} - Calling FindByFirstName() with firstName = '{firstName}'");
                try
                {
                    var result = this.service.FindByFirstName(firstName);
                    fstream.WriteLine($"{DateTime.Now} - FindByFirstName() returned '{result}'");
                    return result;
                }
                catch (Exception ex)
                {
                    fstream.WriteLine($"{DateTime.Now} - Catch Exception '{ex.Message}'");
                    throw new ArgumentException($"{ex.Message}");
                }
            }
        }

        /// <summary>Saves information about FindByLastName method to a text file.</summary>
        /// <param name="lastName">The last name.</param>
        /// <returns>
        ///    Returns an enumerator.
        /// </returns>
        public IEnumerable<FileCabinetRecord> FindByLastName(string lastName)
        {
            using (StreamWriter fstream = new StreamWriter("log.txt", true))
            {
                fstream.WriteLine($"{DateTime.Now} - Calling FindByLastName() with lastName = '{lastName}'");
                try
                {
                    var result = this.service.FindByLastName(lastName);
                    fstream.WriteLine($"{DateTime.Now} - FindByLastName() returned '{result}'");
                    return result;
                }
                catch (Exception ex)
                {
                    fstream.WriteLine($"{DateTime.Now} - Catch Exception '{ex.Message}'");
                    throw new ArgumentException($"{ex.Message}");
                }
            }
        }

        /// <summary>Saves information about FindByDateoOfBirth method to a text file.</summary>
        /// <param name="dateofbirth">The date of birth.</param>
        /// <returns>
        ///    Returns an enumerator.
        /// </returns>
        public IEnumerable<FileCabinetRecord> FindByDateoOfBirth(string dateofbirth)
        {
            using (StreamWriter fstream = new StreamWriter("log.txt", true))
            {
                fstream.WriteLine($"{DateTime.Now} - Calling FindByDateoOfBirth() with dateofbirth = '{dateofbirth}'");
                try
                {
                    var result = this.service.FindByDateoOfBirth(dateofbirth);
                    fstream.WriteLine($"{DateTime.Now} - FindByDateoOfBirth() returned '{result}'");
                    return result;
                }
                catch (Exception ex)
                {
                    fstream.WriteLine($"{DateTime.Now} - Catch Exception '{ex.Message}'");
                    throw new ArgumentException($"{ex.Message}");
                }
            }
        }

        /// <summary>Saves information about FindByProperty1 method to a text file.</summary>
        /// <param name="property1">The property1.</param>
        /// <returns>
        ///    Returns an enumerator.
        /// </returns>
        public IEnumerable<FileCabinetRecord> FindByProperty1(string property1)
        {
            using (StreamWriter fstream = new StreamWriter("log.txt", true))
            {
                fstream.WriteLine($"{DateTime.Now} - Calling FindByProperty1() with property1 = '{property1}'");
                try
                {
                    var result = this.service.FindByProperty1(property1);
                    fstream.WriteLine($"{DateTime.Now} - FindByProperty1() returned '{result}'");
                    return result;
                }
                catch (Exception ex)
                {
                    fstream.WriteLine($"{DateTime.Now} - Catch Exception '{ex.Message}'");
                    throw new ArgumentException($"{ex.Message}");
                }
            }
        }

        /// <summary>Saves information about FindByProperty2 method to a text file.</summary>
        /// <param name="property2">The property2.</param>
        /// <returns>
        ///   Returns an enumerator.
        /// </returns>
        public IEnumerable<FileCabinetRecord> FindByProperty2(string property2)
        {
            using (StreamWriter fstream = new StreamWriter("log.txt", true))
            {
                fstream.WriteLine($"{DateTime.Now} - Calling FindByProperty2() with property2 = '{property2}'");
                try
                {
                    var result = this.service.FindByProperty2(property2);
                    fstream.WriteLine($"{DateTime.Now} - FindByProperty2() returned '{result}'");
                    return result;
                }
                catch (Exception ex)
                {
                    fstream.WriteLine($"{DateTime.Now} - Catch Exception '{ex.Message}'");
                    throw new ArgumentException($"{ex.Message}");
                }
            }
        }

        /// <summary>Saves information about FindByProperty3 method to a text file.</summary>
        /// <param name="property3">The property3.</param>
        /// <returns>
        ///    Returns an enumerator.
        /// </returns>
        public IEnumerable<FileCabinetRecord> FindByProperty3(string property3)
        {
            using (StreamWriter fstream = new StreamWriter("log.txt", true))
            {
                fstream.WriteLine($"{DateTime.Now} - Calling FindByProperty3() with property3 = '{property3}'");
                try
                {
                    var result = this.service.FindByProperty2(property3);
                    fstream.WriteLine($"{DateTime.Now} - FindByProperty3() returned '{result}'");
                    return result;
                }
                catch (Exception ex)
                {
                    fstream.WriteLine($"{DateTime.Now} - Catch Exception '{ex.Message}'");
                    throw new ArgumentException($"{ex.Message}");
                }
            }
        }

        /// <summary>Saves information about FindRecords method to a text file.</summary>
        /// <param name="name">The name of property in the record.</param>
        /// <param name="value">The value of property in the record, which wiil be found.</param>
        /// <returns>
        ///   Returns an enumerator.
        /// </returns>
        public IEnumerable<FileCabinetRecord> FindRecords(string name, string value)
        {
            using (StreamWriter fstream = new StreamWriter("log.txt", true))
            {
                fstream.WriteLine($"{DateTime.Now} - Calling FindRecords() with name = '{name}', value = '{value}'");
                try
                {
                    var result = this.service.FindRecords(name, value);
                    fstream.WriteLine($"{DateTime.Now} - FindRecords() returned '{result}'");
                    return result;
                }
                catch (Exception ex)
                {
                    fstream.WriteLine($"{DateTime.Now} - Catch Exception '{ex.Message}'");
                    throw new ArgumentException($"{ex.Message}");
                }
            }
        }

        /// <summary>Saves information about GetRecord method to a text file.</summary>
        /// <param name="position">The position.</param>
        /// <returns>
        ///  The record by position.
        /// </returns>
        public FileCabinetRecord? GetRecord(long position)
        {
            using (StreamWriter fstream = new StreamWriter("log.txt", true))
            {
                fstream.WriteLine($"{DateTime.Now} - Calling GetRecord() with position = '{position}'");
                try
                {
                    var result = this.service.GetRecord(position);
                    fstream.WriteLine($"{DateTime.Now} - GetRecord() returned '{result}'");
                    return result;
                }
                catch (Exception ex)
                {
                    fstream.WriteLine($"{DateTime.Now} - Catch Exception '{ex.Message}'");
                    throw new ArgumentException($"{ex.Message}");
                }
            }
        }

        /// <summary>Saves information about GetRecords method to a text file.</summary>
        /// <returns>
        ///   All records <see langword="ReadOnlyCollection" />.
        /// </returns>
        public ReadOnlyCollection<FileCabinetRecord> GetRecords()
        {
            using (StreamWriter fstream = new StreamWriter("log.txt", true))
            {
                fstream.WriteLine($"{DateTime.Now} - Calling GetRecords()");
                try
                {
                    var result = this.service.GetRecords();
                    fstream.WriteLine($"{DateTime.Now} - GetRecords() returned '{result}'");
                    return result;
                }
                catch (Exception ex)
                {
                    fstream.WriteLine($"{DateTime.Now} - Catch Exception '{ex.Message}'");
                    throw new ArgumentException($"{ex.Message}");
                }
            }
        }

        /// <summary>Saves information about GetStat method to a text file.</summary>
        /// <returns>
        ///   Count of records.
        /// </returns>
        public int GetStat()
        {
            using (StreamWriter fstream = new StreamWriter("log.txt", true))
            {
                fstream.WriteLine($"{DateTime.Now} - Calling GetStat()");
                try
                {
                    var result = this.service.GetStat();
                    fstream.WriteLine($"{DateTime.Now} - GetStat() returned '{result}'");
                    return result;
                }
                catch (Exception ex)
                {
                    fstream.WriteLine($"{DateTime.Now} - Catch Exception '{ex.Message}'");
                    throw new ArgumentException($"{ex.Message}");
                }
            }
        }

        /// <summary>Saves information about GetStatDelete method to a text file.</summary>
        /// <returns>
        ///   Count of records deleted.
        /// </returns>
        public int GetStatDelete()
        {
            using (StreamWriter fstream = new StreamWriter("log.txt", true))
            {
                fstream.WriteLine($"{DateTime.Now} - Calling GetStatDelete()");
                try
                {
                    var result = this.service.GetStatDelete();
                    fstream.WriteLine($"{DateTime.Now} - GetStatDelete() returned '{result}'");
                    return result;
                }
                catch (Exception ex)
                {
                    fstream.WriteLine($"{DateTime.Now} - Catch Exception '{ex.Message}'");
                    throw new ArgumentException($"{ex.Message}");
                }
            }
        }

        /// <summary>Saves information about IdExist method to a text file.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   <see langword="true"/> if the record with this id exists, <see langword="false"/> otherwise.
        /// </returns>
        public bool IdExist(int id)
        {
            using (StreamWriter fstream = new StreamWriter("log.txt", true))
            {
                fstream.WriteLine($"{DateTime.Now} - Calling IdExist() with id = '{id}'");
                try
                {
                    var result = this.service.IdExist(id);
                    fstream.WriteLine($"{DateTime.Now} - IdExist() returned '{result}'");
                    return result;
                }
                catch (Exception ex)
                {
                    fstream.WriteLine($"{DateTime.Now} - Catch Exception '{ex.Message}'");
                    throw new ArgumentException($"{ex.Message}");
                }
            }
        }

        /// <summary>Saves information about MakeSnapshot method to a text file.</summary>
        /// <returns>
        ///   The snapshot of file cabinet service <see cref="FileCabinetServiceSnapshot" />.
        /// </returns>
        public FileCabinetServiceSnapshot MakeSnapshot()
        {
            using (StreamWriter fstream = new StreamWriter("log.txt", true))
            {
                fstream.WriteLine($"{DateTime.Now} - Calling MakeSnapshot()");
                try
                {
                    var result = this.service.MakeSnapshot();
                    fstream.WriteLine($"{DateTime.Now} - MakeSnapshot() returned '{result}'");
                    return result;
                }
                catch (Exception ex)
                {
                    fstream.WriteLine($"{DateTime.Now} - Catch Exception '{ex.Message}'");
                    throw new ArgumentException($"{ex.Message}");
                }
            }
        }

        /// <summary>
        /// Saves information about PurgeRecords method to a text file.
        /// </summary>
        public void PurgeRecords()
        {
            using (StreamWriter fstream = new StreamWriter("log.txt", true))
            {
                fstream.WriteLine($"{DateTime.Now} - Calling PurgeRecords()");
                try
                {
                    this.service.PurgeRecords();
                }
                catch (Exception ex)
                {
                    fstream.WriteLine($"{DateTime.Now} - Catch Exception '{ex.Message}'");
                    throw new ArgumentException($"{ex.Message}");
                }
            }
        }

        /// <summary>Saves information about Restore method to a text file.</summary>
        /// <param name="snapshot">The snapshot of file cabinet service.</param>
        public void Restore(FileCabinetServiceSnapshot snapshot)
        {
            using (StreamWriter fstream = new StreamWriter("log.txt", true))
            {
                fstream.WriteLine($"{DateTime.Now} - Calling Restore() with snapshot = '{snapshot}'");
                try
                {
                    this.service.Restore(snapshot);
                }
                catch (Exception ex)
                {
                    fstream.WriteLine($"{DateTime.Now} - Catch Exception '{ex.Message}'");
                    throw new ArgumentException($"{ex.Message}");
                }
            }
        }

        /// <summary>Saves information about ServiceInfo method to a text file.</summary>
        /// <returns>
        ///   The name of the validation rules <see cref="string" />.
        /// </returns>
        public string ServiceInfo()
        {
            using (StreamWriter fstream = new StreamWriter("log.txt", true))
            {
                fstream.WriteLine($"{DateTime.Now} - Calling ServiceInfo()");
                try
                {
                    var result = this.service.ServiceInfo();
                    fstream.WriteLine($"{DateTime.Now} - ServiceInfo() returned '{result}'");
                    return result;
                }
                catch (Exception ex)
                {
                    fstream.WriteLine($"{DateTime.Now} - Catch Exception '{ex.Message}'");
                    throw new ArgumentException($"{ex.Message}");
                }
            }
        }

        /// <summary>Saves information about FindIndex method to a text file.</summary>
        /// <param name="record">The record.</param>
        /// <returns>
        ///   The position of the record.
        /// </returns>
        public long FindIndex(FileCabinetRecord record)
        {
            using (StreamWriter fstream = new StreamWriter("log.txt", true))
            {
                fstream.WriteLine($"{DateTime.Now} - Calling FindIndex() with record = '{record}'");
                try
                {
                    var result = this.service.FindIndex(record);
                    fstream.WriteLine($"{DateTime.Now} - FindIndex() returned '{result}'");
                    return result;
                }
                catch (Exception ex)
                {
                    fstream.WriteLine($"{DateTime.Now} - Catch Exception '{ex.Message}'");
                    throw new ArgumentException($"{ex.Message}");
                }
            }
        }
    }
}
