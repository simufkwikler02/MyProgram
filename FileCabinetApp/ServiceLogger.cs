using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCabinetApp
{
    public class ServiceLogger : IFileCabinetService
    {
        private IFileCabinetService service;

        public ServiceLogger(IFileCabinetService service)
        {
            this.service = service;
            StreamWriter fstream = new StreamWriter("log.txt", false);
            fstream.Close();
        }

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

        public void EditRecord(int id, FileCabinetRecord recordEdit)
        {
            using (StreamWriter fstream = new StreamWriter("log.txt", true))
            {
                fstream.WriteLine($"{DateTime.Now} - Calling EditRecord() with id = '{id}', FirstName = '{recordEdit.FirstName}', LastName = '{recordEdit.LastName}', DateOfBirth = '{recordEdit.DateOfBirth.Date}',  Property1 = '{recordEdit.Property1}', Property2 = '{recordEdit.Property2}', Property3 = '{recordEdit.Property3}',");
                try
                {
                    this.service.EditRecord(id, recordEdit);
                }
                catch (Exception ex)
                {
                    fstream.WriteLine($"{DateTime.Now} - Catch Exception '{ex.Message}'");
                    throw new ArgumentException($"{ex.Message}");
                }
            }
        }

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

        public IEnumerable<FileCabinetRecord> FindByLastName(string lastName)
        {
            using (StreamWriter fstream = new StreamWriter("log.txt", true))
            {
                fstream.WriteLine($"{DateTime.Now} - Calling FindByLastName() with firstName = '{lastName}'");
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

        public void RemoveRecord(int id)
        {
            using (StreamWriter fstream = new StreamWriter("log.txt", true))
            {
                fstream.WriteLine($"{DateTime.Now} - Calling RemoveRecord() with id = '{id}'");
                try
                {
                    this.service.RemoveRecord(id);
                }
                catch (Exception ex)
                {
                    fstream.WriteLine($"{DateTime.Now} - Catch Exception '{ex.Message}'");
                    throw new ArgumentException($"{ex.Message}");
                }
            }
        }

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
    }
}
