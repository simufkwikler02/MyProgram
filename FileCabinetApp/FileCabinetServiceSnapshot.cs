using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml;

namespace FileCabinetApp
{
    public class FileCabinetServiceSnapshot
    {
        private readonly List<FileCabinetRecord>? records;

        private List<FileCabinetRecord>? Records;

        public FileCabinetServiceSnapshot(List<FileCabinetRecord> records)
        {
            this.records = records;
        }

        public FileCabinetServiceSnapshot()
        {
        }

        public ReadOnlyCollection<FileCabinetRecord> GetRecords()
        {
            return new ReadOnlyCollection<FileCabinetRecord>(this.Records);
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

        public void LoadFromCsv(StreamReader fstream)
        {
            var scv = new FileCabinetRecordCsvReader(fstream);
            var list = scv.ReadAll();

            this.Records = (List<FileCabinetRecord>)list;
        }

        public void LoadFromXml(StreamReader fstream)
        {
            XmlReaderSettings settings = new XmlReaderSettings();

            var xml = new FileCabinetRecordXmlReader(XmlReader.Create(fstream, settings));
            var list = xml.ReadAll();

            this.Records = (List<FileCabinetRecord>)list;
        }
    }
}
