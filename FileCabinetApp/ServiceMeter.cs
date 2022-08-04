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
        private IFileCabinetService service;

        public ServiceMeter(IFileCabinetService service)
        {
            this.service = service;
        }

        public int CreateRecord(FileCabinetRecord newRecord)
        {
            var time = new Stopwatch();
            time.Start();
            var result = this.service.CreateRecord(newRecord);
            time.Stop();
            Console.WriteLine($"CreateRecord method execution duration is {time.ElapsedTicks} ticks.");
            return result;
        }

        public void EditRecord(int id, FileCabinetRecord recordEdit)
        {
            var time = new Stopwatch();
            time.Start();
            this.service.EditRecord(id, recordEdit);
            time.Stop();
            Console.WriteLine($"EditRecord method execution duration is {time.ElapsedTicks} ticks.");
        }

        public ReadOnlyCollection<FileCabinetRecord> FindByDateoOfBirth(string dateofbirth)
        {
            var time = new Stopwatch();
            time.Start();
            var result = this.service.FindByDateoOfBirth(dateofbirth);
            time.Stop();
            Console.WriteLine($"FindByDateoOfBirth method execution duration is {time.ElapsedTicks} ticks.");
            return result;
        }

        public ReadOnlyCollection<FileCabinetRecord> FindByFirstName(string firstName)
        {
            var time = new Stopwatch();
            time.Start();
            var result = this.service.FindByFirstName(firstName);
            time.Stop();
            Console.WriteLine($"FindByFirstName method execution duration is {time.ElapsedTicks} ticks.");
            return result;
        }

        public ReadOnlyCollection<FileCabinetRecord> FindByLastName(string lastName)
        {
            var time = new Stopwatch();
            time.Start();
            var result = this.service.FindByLastName(lastName);
            time.Stop();
            Console.WriteLine($"FindByLastName method execution duration is {time.ElapsedTicks} ticks.");
            return result;
        }

        public ReadOnlyCollection<FileCabinetRecord> GetRecords()
        {
            var time = new Stopwatch();
            time.Start();
            var result = this.service.GetRecords();
            time.Stop();
            Console.WriteLine($"GetRecords method execution duration is {time.ElapsedTicks} ticks.");
            return result;
        }

        public int GetStat()
        {
            var time = new Stopwatch();
            time.Start();
            var result = this.service.GetStat();
            time.Stop();
            Console.WriteLine($"GetStat method execution duration is {time.ElapsedTicks} ticks.");
            return result;
        }

        public int GetStatDelete()
        {
            var time = new Stopwatch();
            time.Start();
            var result = this.service.GetStatDelete();
            time.Stop();
            Console.WriteLine($"GetStatDelete method execution duration is {time.ElapsedTicks} ticks.");
            return result;
        }

        public bool IdExist(int id)
        {
            var time = new Stopwatch();
            time.Start();
            var result = this.service.IdExist(id);
            time.Stop();
            Console.WriteLine($"IdExist method execution duration is {time.ElapsedTicks} ticks.");
            return result;
        }

        public FileCabinetServiceSnapshot MakeSnapshot()
        {
            var time = new Stopwatch();
            time.Start();
            var result = this.service.MakeSnapshot();
            time.Stop();
            Console.WriteLine($"MakeSnapshot method execution duration is {time.ElapsedTicks} ticks.");
            return result;
        }

        public void PurgeRecords()
        {
            var time = new Stopwatch();
            time.Start();
            this.service.PurgeRecords();
            time.Stop();
            Console.WriteLine($"PurgeRecords method execution duration is {time.ElapsedTicks} ticks.");
        }

        public void RemoveRecord(int id)
        {
            var time = new Stopwatch();
            time.Start();
            this.service.RemoveRecord(id);
            time.Stop();
            Console.WriteLine($"RemoveRecord method execution duration is {time.ElapsedTicks} ticks.");
        }

        public void Restore(FileCabinetServiceSnapshot snapshot)
        {
            var time = new Stopwatch();
            time.Start();
            this.service.Restore(snapshot);
            time.Stop();
            Console.WriteLine($"Restore method execution duration is {time.ElapsedTicks} ticks.");
        }

        public string ServiceInfo()
        {
            var time = new Stopwatch();
            time.Start();
            var result = this.service.ServiceInfo();
            time.Stop();
            Console.WriteLine($"ServiceInfo method execution duration is {time.ElapsedTicks} ticks.");
            return result;
        }
    }
}
