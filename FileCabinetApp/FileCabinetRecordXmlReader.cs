using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace FileCabinetApp
{
    /// <summary>
    ///   Represents the XML reader class with a set methods that can read saved records from a file.
    /// </summary>
    public class FileCabinetRecordXmlReader
    {
        private XmlReader reader;

        /// <summary>Initializes a new instance of the <see cref="FileCabinetRecordXmlReader" /> class.</summary>
        /// <param name="fstream">The fstream.</param>
        public FileCabinetRecordXmlReader(XmlReader fstream)
        {
            this.reader = fstream;
        }

        /// <summary>Reads all records from XML file.</summary>
        /// <returns>
        ///  The records <see langword="IList" />.
        /// </returns>
        public IList<FileCabinetRecord> ReadAll()
        {
            XmlSerializer formater = new XmlSerializer(typeof(FileCabinetRecord[]));
            FileCabinetRecord[] records = formater.Deserialize(this.reader) as FileCabinetRecord[] ?? Array.Empty<FileCabinetRecord>();
            return new List<FileCabinetRecord>(records);
        }
    }
}
