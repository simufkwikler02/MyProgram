using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FileCabinetApp
{
    /// <summary>
    ///    Represents the file cabinet service meter which meter the running time of file cabinet service methods.
    /// </summary>
    public class ServiceMeter : IFileCabinetService
    {
        private readonly Stopwatch time = new Stopwatch();
        private readonly IFileCabinetService service;

        /// <summary>Initializes a new instance of the <see cref="ServiceMeter" /> class.</summary>
        /// <param name="service">The file cabinet service.</param>
        public ServiceMeter(IFileCabinetService service)
        {
            this.service = service;
        }

        /// <summary>Prints the running time of the CreateRecord method to the console.</summary>
        /// <param name="newRecord">The new record.</param>
        /// <returns>
        ///   New record id.
        /// </returns>
        /// <exception cref="System.ArgumentException"> Validation fail - newRecord. </exception>
        public int CreateRecord(FileCabinetRecord newRecord)
        {
            this.time.Reset();
            this.time.Start();
            var result = this.service.CreateRecord(newRecord);
            this.time.Stop();
            Console.WriteLine($"CreateRecord method execution duration is {this.time.ElapsedTicks} ticks.");
            return result;
        }

        /// <summary>Prints the running time of the DeleteRecord method to the console.</summary>
        /// <param name="record">The record that should be deleted.</param>
        /// <returns>
        ///   Deleted record id.
        /// </returns>
        public int DeleteRecord(FileCabinetRecord record)
        {
            this.time.Reset();
            this.time.Start();
            var result = this.service.DeleteRecord(record);
            this.time.Stop();
            Console.WriteLine($"DeleteRecord method execution duration is {this.time.ElapsedTicks} ticks.");
            return result;
        }

        /// <summary>Prints the running time of the UpdateRecord method to the console.</summary>
        /// <param name="position">Position of the old record.</param>
        /// <param name="recordUpdate">A new record that will overwrite the old record.</param>
        /// <returns>
        ///   Updated record id.
        /// </returns>
        public int UpdateRecord(long position, FileCabinetRecord recordUpdate)
        {
            this.time.Reset();
            this.time.Start();
            var result = this.service.UpdateRecord(position, recordUpdate);
            this.time.Stop();
            Console.WriteLine($"UpdateRecord method execution duration is {this.time.ElapsedTicks} ticks.");
            return result;
        }

        /// <summary>Prints the running time of the FindById method to the console.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   Returns an enumerator.
        /// </returns>
        public IEnumerable<FileCabinetRecord> FindById(string id)
        {
            this.time.Reset();
            this.time.Start();
            var result = this.service.FindById(id);
            this.time.Stop();
            Console.WriteLine($"FindById method execution duration is {this.time.ElapsedTicks} ticks.");
            return result;
        }

        /// <summary>Prints the running time of the FindByFirstName method to the console.</summary>
        /// <param name="firstName">The first name.</param>
        /// <returns>
        ///   Returns an enumerator.
        /// </returns>
        public IEnumerable<FileCabinetRecord> FindByFirstName(string firstName)
        {
            this.time.Reset();
            this.time.Start();
            var result = this.service.FindByFirstName(firstName);
            this.time.Stop();
            Console.WriteLine($"FindByFirstName method execution duration is {this.time.ElapsedTicks} ticks.");
            return result;
        }

        /// <summary>Prints the running time of the FindByLastName method to the console.</summary>
        /// <param name="lastName">The last name.</param>
        /// <returns>
        ///    Returns an enumerator.
        /// </returns>
        public IEnumerable<FileCabinetRecord> FindByLastName(string lastName)
        {
            this.time.Reset();
            this.time.Start();
            var result = this.service.FindByLastName(lastName);
            this.time.Stop();
            Console.WriteLine($"FindByLastName method execution duration is {this.time.ElapsedTicks} ticks.");
            return result;
        }

        /// <summary>Prints the running time of the FindByDateoOfBirth method to the console.</summary>
        /// <param name="dateofbirth">The date of birth.</param>
        /// <returns>
        ///    Returns an enumerator.
        /// </returns>
        public IEnumerable<FileCabinetRecord> FindByDateoOfBirth(string dateofbirth)
        {
            this.time.Reset();
            this.time.Start();
            var result = this.service.FindByDateoOfBirth(dateofbirth);
            this.time.Stop();
            Console.WriteLine($"FindByDateoOfBirth method execution duration is {this.time.ElapsedTicks} ticks.");
            return result;
        }

        /// <summary>Prints the running time of the FindByProperty1 method to the console.</summary>
        /// <param name="property1">The property1.</param>
        /// <returns>
        ///    Returns an enumerator.
        /// </returns>
        public IEnumerable<FileCabinetRecord> FindByProperty1(string property1)
        {
            this.time.Reset();
            this.time.Start();
            var result = this.service.FindByProperty1(property1);
            this.time.Stop();
            Console.WriteLine($"FindByProperty1 method execution duration is {this.time.ElapsedTicks} ticks.");
            return result;
        }

        /// <summary>Prints the running time of the FindByProperty2 method to the console.</summary>
        /// <param name="property2">The property2.</param>
        /// <returns>
        ///   Returns an enumerator.
        /// </returns>
        public IEnumerable<FileCabinetRecord> FindByProperty2(string property2)
        {
            this.time.Reset();
            this.time.Start();
            var result = this.service.FindByProperty2(property2);
            this.time.Stop();
            Console.WriteLine($"FindByProperty2 method execution duration is {this.time.ElapsedTicks} ticks.");
            return result;
        }

        /// <summary>Prints the running time of the FindByProperty3 method to the console.</summary>
        /// <param name="property3">The property3.</param>
        /// <returns>
        ///    Returns an enumerator.
        /// </returns>
        public IEnumerable<FileCabinetRecord> FindByProperty3(string property3)
        {
            this.time.Reset();
            this.time.Start();
            var result = this.service.FindByProperty3(property3);
            this.time.Stop();
            Console.WriteLine($"FindByProperty3 method execution duration is {this.time.ElapsedTicks} ticks.");
            return result;
        }

        /// <summary>Prints the running time of the FindIndex method to the console.</summary>
        /// <param name="record">The record.</param>
        /// <returns>
        ///   The position of the record.
        /// </returns>
        public long FindIndex(FileCabinetRecord record)
        {
            this.time.Reset();
            this.time.Start();
            var result = this.service.FindIndex(record);
            this.time.Stop();
            Console.WriteLine($"FindIndex method execution duration is {this.time.ElapsedTicks} ticks.");
            return result;
        }

        /// <summary>Prints the running time of the GetRecord method to the console.</summary>
        /// <param name="position">The position.</param>
        /// <returns>
        ///  The record by position.
        /// </returns>
        public FileCabinetRecord? GetRecord(long position)
        {
            this.time.Reset();
            this.time.Start();
            var result = this.service.GetRecord(position);
            this.time.Stop();
            Console.WriteLine($"GetRecord method execution duration is {this.time.ElapsedTicks} ticks.");
            return result;
        }

        /// <summary>Prints the running time of the GetRecords method to the console.</summary>
        /// <returns>
        ///   All records <see langword="ReadOnlyCollection" />.
        /// </returns>
        public ReadOnlyCollection<FileCabinetRecord> GetRecords()
        {
            this.time.Reset();
            this.time.Start();
            var result = this.service.GetRecords();
            this.time.Stop();
            Console.WriteLine($"GetRecords method execution duration is {this.time.ElapsedTicks} ticks.");
            return result;
        }

        /// <summary>Prints the running time of the GetStat method to the console.</summary>
        /// <returns>
        ///   Count of records.
        /// </returns>
        public int GetStat()
        {
            this.time.Reset();
            this.time.Start();
            var result = this.service.GetStat();
            this.time.Stop();
            Console.WriteLine($"GetStat method execution duration is {this.time.ElapsedTicks} ticks.");
            return result;
        }

        /// <summary>Prints the running time of the GetStatDelete method to the console.</summary>
        /// <returns>
        ///   Count of records deleted.
        /// </returns>
        public int GetStatDelete()
        {
            this.time.Reset();
            this.time.Start();
            var result = this.service.GetStatDelete();
            this.time.Stop();
            Console.WriteLine($"GetStatDelete method execution duration is {this.time.ElapsedTicks} ticks.");
            return result;
        }

        /// <summary>Prints the running time of the IdExist method to the console.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   <see langword="true"/> if the record with this id exists, <see langword="false"/> otherwise.
        /// </returns>
        public bool IdExist(int id)
        {
            this.time.Reset();
            this.time.Start();
            var result = this.service.IdExist(id);
            this.time.Stop();
            Console.WriteLine($"IdExist method execution duration is {this.time.ElapsedTicks} ticks.");
            return result;
        }

        /// <summary>Prints the running time of the MakeSnapshot method to the console.</summary>
        /// <returns>
        ///   The snapshot of file cabinet service <see cref="FileCabinetServiceSnapshot" />.
        /// </returns>
        public FileCabinetServiceSnapshot MakeSnapshot()
        {
            this.time.Reset();
            this.time.Start();
            var result = this.service.MakeSnapshot();
            this.time.Stop();
            Console.WriteLine($"MakeSnapshot method execution duration is {this.time.ElapsedTicks} ticks.");
            return result;
        }

        /// <summary>
        /// Prints the running time of the PurgeRecords method to the console.
        /// </summary>
        public void PurgeRecords()
        {
            this.time.Reset();
            this.time.Start();
            this.service.PurgeRecords();
            this.time.Stop();
            Console.WriteLine($"PurgeRecords method execution duration is {this.time.ElapsedTicks} ticks.");
        }

        /// <summary>Prints the running time of the Restore method to the console.</summary>
        /// <param name="snapshot">The snapshot of file cabinet service.</param>
        public void Restore(FileCabinetServiceSnapshot snapshot)
        {
            this.time.Reset();
            this.time.Start();
            this.service.Restore(snapshot);
            this.time.Stop();
            Console.WriteLine($"Restore method execution duration is {this.time.ElapsedTicks} ticks.");
        }

        /// <summary>Prints the running time of the ServiceInfo method to the console.</summary>
        /// <returns>
        ///   The name of the validation rules <see cref="string" />.
        /// </returns>
        public string ServiceInfo()
        {
            this.time.Reset();
            this.time.Start();
            var result = this.service.ServiceInfo();
            this.time.Stop();
            Console.WriteLine($"ServiceInfo method execution duration is {this.time.ElapsedTicks} ticks.");
            return result;
        }

        /// <summary>Prints the running time of the FindRecords method to the console.</summary>
        /// <param name="name">The name of property in the record.</param>
        /// <param name="value">The value of property in the record, which wiil be found.</param>
        /// <returns>
        ///   Returns an enumerator.
        /// </returns>
        public IEnumerable<FileCabinetRecord> FindRecords(string name, string value)
        {
            this.time.Reset();
            this.time.Start();
            var result = this.service.FindRecords(name, value);
            this.time.Stop();
            Console.WriteLine($"FindRecords method execution duration is {this.time.ElapsedTicks} ticks.");
            return result;
        }
    }
}
