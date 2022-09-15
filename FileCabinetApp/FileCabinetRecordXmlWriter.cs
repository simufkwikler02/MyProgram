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
    /// <summary>
    ///   Represents the XML writer class with a set methods that can save records to a file.
    /// </summary>
    public class FileCabinetRecordXmlWriter
    {
        private readonly XmlWriter writer;

        /// <summary>Initializes a new instance of the <see cref="FileCabinetRecordXmlWriter" /> class.</summary>
        /// <param name="fstream">The fstream.</param>
        public FileCabinetRecordXmlWriter(XmlWriter fstream)
        {
            this.writer = fstream;
        }

        /// <summary>Writes the specified records to a XML file.</summary>
        /// <param name="records">The records.</param>
        public void Write(List<FileCabinetRecord> records)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(List<FileCabinetRecord>));
            formatter.Serialize(this.writer, records);
        }
    }
}
