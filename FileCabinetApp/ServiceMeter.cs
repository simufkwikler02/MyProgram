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
    public class ServiceMeter : IFileCabinetService
    {
        private Stopwatch time = new Stopwatch();
        private IFileCabinetService service;

        public ServiceMeter(IFileCabinetService service)
        {
            this.service = service;
        }

        public int CreateRecord(FileCabinetRecord newRecord)
        {
            this.time.Reset();
            this.time.Start();
            var result = this.service.CreateRecord(newRecord);
            this.time.Stop();
            Console.WriteLine($"CreateRecord method execution duration is {this.time.ElapsedTicks} ticks.");
            return result;
        }

        public int DeleteRecord(string name, string value)
        {
            this.time.Reset();
            this.time.Start();
            var result = this.service.DeleteRecord(name, value);
            this.time.Stop();
            Console.WriteLine($"DeleteRecord method execution duration is {this.time.ElapsedTicks} ticks.");
            return result;
        }

        public void EditRecord(int id, FileCabinetRecord recordEdit)
        {
            this.time.Reset();
            this.time.Start();
            this.service.EditRecord(id, recordEdit);
            this.time.Stop();
            Console.WriteLine($"EditRecord method execution duration is {this.time.ElapsedTicks} ticks.");
        }

        public IEnumerable<FileCabinetRecord> FindByDateoOfBirth(string dateofbirth)
        {
            this.time.Reset();
            this.time.Start();
            var result = this.service.FindByDateoOfBirth(dateofbirth);
            this.time.Stop();
            Console.WriteLine($"FindByDateoOfBirth method execution duration is {this.time.ElapsedTicks} ticks.");
            return result;
        }

        public IEnumerable<FileCabinetRecord> FindByFirstName(string firstName)
        {
            this.time.Reset();
            this.time.Start();
            var result = this.service.FindByFirstName(firstName);
            this.time.Stop();
            Console.WriteLine($"FindByFirstName method execution duration is {this.time.ElapsedTicks} ticks.");
            return result;
        }

        public IEnumerable<FileCabinetRecord> FindByLastName(string lastName)
        {
            this.time.Reset();
            this.time.Start();
            var result = this.service.FindByLastName(lastName);
            this.time.Stop();
            Console.WriteLine($"FindByLastName method execution duration is {this.time.ElapsedTicks} ticks.");
            return result;
        }

        public long FindIndex(string name, string value)
        {
            this.time.Reset();
            this.time.Start();
            var result = this.service.FindIndex(name, value);
            this.time.Stop();
            Console.WriteLine($"FindIndex method execution duration is {this.time.ElapsedTicks} ticks.");
            return result;
        }

        public ReadOnlyCollection<FileCabinetRecord> GetRecords()
        {
            this.time.Reset();
            this.time.Start();
            var result = this.service.GetRecords();
            this.time.Stop();
            Console.WriteLine($"GetRecords method execution duration is {this.time.ElapsedTicks} ticks.");
            return result;
        }

        public int GetStat()
        {
            this.time.Reset();
            this.time.Start();
            var result = this.service.GetStat();
            this.time.Stop();
            Console.WriteLine($"GetStat method execution duration is {this.time.ElapsedTicks} ticks.");
            return result;
        }

        public int GetStatDelete()
        {
            this.time.Reset();
            this.time.Start();
            var result = this.service.GetStatDelete();
            this.time.Stop();
            Console.WriteLine($"GetStatDelete method execution duration is {this.time.ElapsedTicks} ticks.");
            return result;
        }

        public bool IdExist(int id)
        {
            this.time.Reset();
            this.time.Start();
            var result = this.service.IdExist(id);
            this.time.Stop();
            Console.WriteLine($"IdExist method execution duration is {this.time.ElapsedTicks} ticks.");
            return result;
        }

        public FileCabinetServiceSnapshot MakeSnapshot()
        {
            this.time.Reset();
            this.time.Start();
            var result = this.service.MakeSnapshot();
            this.time.Stop();
            Console.WriteLine($"MakeSnapshot method execution duration is {this.time.ElapsedTicks} ticks.");
            return result;
        }

        public void PurgeRecords()
        {
            this.time.Reset();
            this.time.Start();
            this.service.PurgeRecords();
            this.time.Stop();
            Console.WriteLine($"PurgeRecords method execution duration is {this.time.ElapsedTicks} ticks.");
        }

        public void RemoveRecord(int id)
        {
            this.time.Reset();
            this.time.Start();
            this.service.RemoveRecord(id);
            this.time.Stop();
            Console.WriteLine($"RemoveRecord method execution duration is {this.time.ElapsedTicks} ticks.");
        }

        public void Restore(FileCabinetServiceSnapshot snapshot)
        {
            this.time.Reset();
            this.time.Start();
            this.service.Restore(snapshot);
            this.time.Stop();
            Console.WriteLine($"Restore method execution duration is {this.time.ElapsedTicks} ticks.");
        }

        public string ServiceInfo()
        {
            this.time.Reset();
            this.time.Start();
            var result = this.service.ServiceInfo();
            this.time.Stop();
            Console.WriteLine($"ServiceInfo method execution duration is {this.time.ElapsedTicks} ticks.");
            return result;
        }
    }
}
