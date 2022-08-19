﻿using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace FileCabinetApp
{
    public interface IFileCabinetService
    {
        public int CreateRecord(FileCabinetRecord newRecord);

        public string ServiceInfo();

        public void EditRecord(int id, FileCabinetRecord recordEdit);

        public ReadOnlyCollection<FileCabinetRecord> GetRecords();

        public int GetStat();

        public bool IdExist(int id);

        public void RemoveRecord(int id);

        public int GetStatDelete();

        public void PurgeRecords();

        public IRecordIterator FindByFirstName(string firstName);

        public IRecordIterator FindByLastName(string lastName);

        public IRecordIterator FindByDateoOfBirth(string dateofbirth);

        public FileCabinetServiceSnapshot MakeSnapshot();

        public void Restore(FileCabinetServiceSnapshot snapshot);
    }
}
