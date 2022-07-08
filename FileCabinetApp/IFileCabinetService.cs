using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace FileCabinetApp
{
    public interface IFileCabinetService
    {
        public int CreateRecord(FileCabinetRecord newRecord);

        public string ValidateInfo();

        public string ServiceInfo();

        public void EditRecord(int id, FileCabinetRecord recordEdit);

        public ReadOnlyCollection<FileCabinetRecord> GetRecords();

        public int GetStat();

        public ReadOnlyCollection<FileCabinetRecord> FindByFirstName(string firstName);

        public ReadOnlyCollection<FileCabinetRecord> FindByLastName(string lastName);

        public ReadOnlyCollection<FileCabinetRecord> FindByDateoOfBirth(string dateofbirth);

        public FileCabinetServiceSnapshot MakeSnapshot();
    }
}
