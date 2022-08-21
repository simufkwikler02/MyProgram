using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCabinetApp
{
    public class Enumerable : IEnumerable<FileCabinetRecord>
    {

        private string data;

        private string name;

        private FileStream? fileStream;

        public Enumerable(FileStream? fileStream, string data, string name)
        {
            this.fileStream = fileStream;
            this.data = data;
            this.name = name;
        }

        public IEnumerator<FileCabinetRecord> GetEnumerator()
        {
            return new Enumerator(this.fileStream, this.data, this.name);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
