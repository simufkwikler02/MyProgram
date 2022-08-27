using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace FileCabinetApp
{
    public interface IFileCabinetService
    {
        public int CreateRecord(FileCabinetRecord newRecord);

        public string ServiceInfo();

        public int UpdateRecord(long position, FileCabinetRecord recordUpdate);

        public ReadOnlyCollection<FileCabinetRecord> GetRecords();

        public int GetStat();

        public bool IdExist(int id);

        public ReadOnlyCollection<long> FindIndex(string name, string value);

        public FileCabinetRecord GetRecord(long position);

        public int DeleteRecord(string name, string value);

        public int GetStatDelete();

        public void PurgeRecords();

        public IEnumerable<FileCabinetRecord> FindByFirstName(string firstName);

        public IEnumerable<FileCabinetRecord> FindByLastName(string lastName);

        public IEnumerable<FileCabinetRecord> FindByDateoOfBirth(string dateofbirth);

        public IEnumerable<FileCabinetRecord> FindByProperty1(string property1);

        public IEnumerable<FileCabinetRecord> FindByProperty2(string property2);

        public IEnumerable<FileCabinetRecord> FindByProperty3(string property3);

        public FileCabinetServiceSnapshot MakeSnapshot();

        public void Restore(FileCabinetServiceSnapshot snapshot);
    }
}
