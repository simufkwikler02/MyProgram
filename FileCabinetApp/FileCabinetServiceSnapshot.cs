using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml;

namespace FileCabinetApp
{
    /// <summary>
    ///   Represents the file cabinet service snapshot class with a set methods that can save file cabinet state .
    /// </summary>
    public class FileCabinetServiceSnapshot
    {
        private readonly List<FileCabinetRecord> records;

        private List<FileCabinetRecord> recordsToRestore;

        /// <summary>Initializes a new instance of the <see cref="FileCabinetServiceSnapshot" /> class.</summary>
        /// <param name="records">The snapshot of records.</param>
        public FileCabinetServiceSnapshot(List<FileCabinetRecord> records)
        {
            this.records = records;
            this.recordsToRestore = new List<FileCabinetRecord>();
        }

        /// <summary>Initializes a new instance of the <see cref="FileCabinetServiceSnapshot" /> class.</summary>
        public FileCabinetServiceSnapshot()
        {
            this.records = new List<FileCabinetRecord>();
            this.recordsToRestore = new List<FileCabinetRecord>();
        }

        /// <summary>Gets the save records for restore.</summary>
        /// <returns>
        ///   The records <see langword="ReadOnlyCollection" />.
        /// </returns>
        public ReadOnlyCollection<FileCabinetRecord> GetRecords()
        {
            return new ReadOnlyCollection<FileCabinetRecord>(this.recordsToRestore);
        }

        /// <summary>Saves to CSV file snapshot of records.</summary>
        /// <param name="fstream">The fstream.</param>
        public void SaveToCsv(StreamWriter fstream)
        {
            var scv = new FileCabinetRecordCsvWriter(fstream);
            scv.Write(this.records);
        }

        /// <summary>Saves to XML file snapshot of records.</summary>
        /// <param name="fstream">The fstream.</param>
        public void SaveToXml(StreamWriter fstream)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = "    ";
            var scv = new FileCabinetRecordXmlWriter(XmlWriter.Create(fstream, settings));
            scv.Write(this.records);
        }

        /// <summary>Loads from CSV file records and saves them for restore.</summary>
        /// <param name="fstream">The fstream.</param>
        public void LoadFromCsv(StreamReader fstream)
        {
            var scv = new FileCabinetRecordCsvReader(fstream);
            var list = scv.ReadAll();

            this.recordsToRestore = (List<FileCabinetRecord>)list;
        }

        /// <summary>Loads from XML file records and saves them for restore.</summary>
        /// <param name="fstream">The fstream.</param>
        public void LoadFromXml(StreamReader fstream)
        {
            XmlReaderSettings settings = new XmlReaderSettings();

            var xml = new FileCabinetRecordXmlReader(XmlReader.Create(fstream, settings));
            var list = xml.ReadAll();

            this.recordsToRestore = (List<FileCabinetRecord>)list;
        }
    }
}
