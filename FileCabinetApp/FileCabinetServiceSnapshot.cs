using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml;

namespace FileCabinetApp
{
    public class FileCabinetServiceSnapshot
    {
        private readonly List<FileCabinetRecord> records;

        private List<FileCabinetRecord> recordsToRestore;

        public FileCabinetServiceSnapshot(List<FileCabinetRecord> records)
        {
            this.records = records;
            this.recordsToRestore = new List<FileCabinetRecord>();
        }

        public FileCabinetServiceSnapshot()
        {
            this.records = new List<FileCabinetRecord>();
            this.recordsToRestore = new List<FileCabinetRecord>();
        }

        public ReadOnlyCollection<FileCabinetRecord> GetRecords()
        {
            return new ReadOnlyCollection<FileCabinetRecord>(this.recordsToRestore);
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

            this.recordsToRestore = (List<FileCabinetRecord>)list;
        }

        public void LoadFromXml(StreamReader fstream)
        {
            XmlReaderSettings settings = new XmlReaderSettings();

            var xml = new FileCabinetRecordXmlReader(XmlReader.Create(fstream, settings));
            var list = xml.ReadAll();

            this.recordsToRestore = (List<FileCabinetRecord>)list;
        }
    }
}
