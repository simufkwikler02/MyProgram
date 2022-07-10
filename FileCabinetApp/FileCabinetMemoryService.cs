﻿using System.Collections.ObjectModel;
using System.Globalization;

namespace FileCabinetApp
{
    public class FileCabinetMemoryService : IFileCabinetService
    {
        private readonly List<FileCabinetRecord> list = new List<FileCabinetRecord>();
        private readonly IRecordValidator validator;
        private readonly string serviceRules = "memory";

        private readonly Dictionary<string, List<FileCabinetRecord>> firstNameDictionary = new Dictionary<string, List<FileCabinetRecord>>();
        private readonly Dictionary<string, List<FileCabinetRecord>> lastNameDictionary = new Dictionary<string, List<FileCabinetRecord>>();
        private readonly Dictionary<DateTime, List<FileCabinetRecord>> dateOfBirthDictionary = new Dictionary<DateTime, List<FileCabinetRecord>>();

        public FileCabinetMemoryService(IRecordValidator validator)
        {
            this.validator = validator;
        }

        public string ValidateInfo()
        {
            return this.validator.ValidateInfo();
        }

        public string ServiceInfo()
        {
            return this.serviceRules;
        }

        public int CreateRecord(FileCabinetRecord newRecord)
        {
            if (!this.validator.ValidateParametrs(newRecord))
            {
                throw new ArgumentException("incorrect format", nameof(newRecord));
            }

            newRecord.Id = this.list.Count + 1;
            this.list.Add(newRecord);

            if (!this.firstNameDictionary.ContainsKey(newRecord.FirstName))
            {
                this.firstNameDictionary.Add(newRecord.FirstName, new List<FileCabinetRecord>());
            }

            this.firstNameDictionary[newRecord.FirstName].Add(newRecord);

            if (!this.lastNameDictionary.ContainsKey(newRecord.LastName))
            {
                this.lastNameDictionary.Add(newRecord.LastName, new List<FileCabinetRecord>());
            }

            this.lastNameDictionary[newRecord.LastName].Add(newRecord);

            if (!this.dateOfBirthDictionary.ContainsKey(newRecord.DateOfBirth))
            {
                this.dateOfBirthDictionary.Add(newRecord.DateOfBirth, new List<FileCabinetRecord>());
            }

            this.dateOfBirthDictionary[newRecord.DateOfBirth].Add(newRecord);

            return newRecord.Id;
        }

        public ReadOnlyCollection<FileCabinetRecord> GetRecords()
        {
            if (this.list.Count == 0)
            {
                return new ReadOnlyCollection<FileCabinetRecord>(Array.Empty<FileCabinetRecord>());
            }
            else
            {
                var records = new List<FileCabinetRecord>();
                foreach (var record in this.list)
                {
                    records.Add(record);
                }

                return new ReadOnlyCollection<FileCabinetRecord>(records);
            }
        }

        public int GetStat()
        {
            return this.list.Count;
        }

        public void EditRecord(int id, FileCabinetRecord recordEdit)
        {
            if (!this.validator.ValidateParametrs(recordEdit))
            {
                throw new ArgumentException("incorrect format", nameof(recordEdit));
            }

            foreach (var record in this.list)
            {
                if (record.Id == id)
                {
                    this.CreateRecord(recordEdit);
                    recordEdit.Id = record.Id;
                    this.list.Insert(id - 1, recordEdit);
                    this.list.RemoveAt(this.list.Count - 1);
                    this.list.RemoveAt(id);
                    this.firstNameDictionary[record.FirstName].Remove(record);
                    this.lastNameDictionary[record.LastName].Remove(record);
                    this.dateOfBirthDictionary[record.DateOfBirth].Remove(record);
                    Console.WriteLine($"Record #{id} is updated.");
                    return;
                }
            }

            throw new ArgumentException("index is not exsist.", nameof(id));
        }

        public ReadOnlyCollection<FileCabinetRecord> FindByFirstName(string firstName)
        {
            if (firstName is null)
            {
                throw new ArgumentNullException(nameof(firstName));
            }

            return new ReadOnlyCollection<FileCabinetRecord>(this.firstNameDictionary[firstName]);
        }

        public ReadOnlyCollection<FileCabinetRecord> FindByLastName(string lastName)
        {
            if (lastName is null)
            {
                throw new ArgumentNullException(nameof(lastName));
            }

            return new ReadOnlyCollection<FileCabinetRecord>(this.lastNameDictionary[lastName]);
        }

        public ReadOnlyCollection<FileCabinetRecord> FindByDateoOfBirth(string dateofbirth)
        {
            if (dateofbirth is null)
            {
                throw new ArgumentNullException(nameof(dateofbirth));
            }

            return new ReadOnlyCollection<FileCabinetRecord>(this.dateOfBirthDictionary[DateTime.Parse(dateofbirth, CultureInfo.CurrentCulture)]);
        }

        public FileCabinetServiceSnapshot MakeSnapshot()
        {
            return new FileCabinetServiceSnapshot(this.list);
        }

        public void Restore(FileCabinetServiceSnapshot snapshot)
        {
            var records = snapshot.GetRecords();
            var newlist = new List<FileCabinetRecord>(records);
            foreach (var record in newlist)
            {
                if (!this.validator.ValidateParametrs(record))
                {
                    Console.WriteLine($"Record validation error with id number {record.Id},record skipped");
                    newlist.Remove(record);
                }
            }

            foreach (var record in newlist)
            {
                var index = this.list.FindIndex(x => x.Id == record.Id);
                if (index >= 0)
                {
                    newlist.Remove(record);
                    this.list.Insert(index, record);
                    this.list.RemoveAt(index + 1);
                    continue;
                }

                this.list.Add(record);
            }

            Console.Write($"{newlist.Count} records were imported");
        }
    }
}