using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace FileCabinetApp
{
    internal interface IFileCabinetService
    {
        public int CreateRecord(FileCabinetRecord newRecord);

        public void EditRecord(int id, FileCabinetRecord recordEdit);

        public ReadOnlyCollection<FileCabinetRecord> GetRecords();

        public int GetStat();
    }
}
