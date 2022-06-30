using System;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCabinetApp
{
    public class FileCabinetServiceSnapshot
    {
        private readonly List<FileCabinetRecord>? records;

        public FileCabinetServiceSnapshot(List<FileCabinetRecord> records)
        {
            this.records = records;
        }

        public void SaveToCsv(StreamWriter fstream)
        {
            var scv = new FileCabinetRecordCsvWriter(fstream);
            scv.Write(this.records);
        }

        public void SaveToXml(StreamWriter fstream)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = "    ";
            var scv = new FileCabinetRecordXmlWriter(XmlWriter.Create(fstream, settings));
            scv.Write(this.records);
        }
    }
}
