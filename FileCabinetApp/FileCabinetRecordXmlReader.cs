using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace FileCabinetApp
{
    public class FileCabinetRecordXmlReader
    {
        private XmlReader reader;

        public FileCabinetRecordXmlReader(XmlReader fstream)
        {
            this.reader = fstream;
        }

        public IList<FileCabinetRecord> ReadAll()
        {
            XmlSerializer formater = new XmlSerializer(typeof(FileCabinetRecord[]));
            FileCabinetRecord[] records = formater.Deserialize(this.reader) as FileCabinetRecord[] ?? Array.Empty<FileCabinetRecord>();
            return new List<FileCabinetRecord>(records);
        }
    }
}
