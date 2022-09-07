using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace FileCabinetApp
{
    public class FileCabinetRecordXmlWriter
    {
        private readonly XmlWriter writer;

        public FileCabinetRecordXmlWriter(XmlWriter fstream)
        {
            this.writer = fstream;
        }

        public void Write(List<FileCabinetRecord> records)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(List<FileCabinetRecord>));
            formatter.Serialize(this.writer, records);
        }
    }
}
