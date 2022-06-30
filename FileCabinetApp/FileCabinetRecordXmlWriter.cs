using System;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace FileCabinetApp
{
    public class FileCabinetRecordXmlWriter
    {
        private readonly XmlWriter? writer;

        public FileCabinetRecordXmlWriter(XmlWriter fstream)
        {
            this.writer = fstream;
        }

        public void Write(List<FileCabinetRecord> records)
        {
            writer.WriteStartDocument();
            writer.WriteStartElement("records");

            foreach (FileCabinetRecord record in records)
            {
                writer.WriteStartElement("record");
                writer.WriteAttributeString("Id", $"{record.Id}");

                writer.WriteStartElement("name");
                writer.WriteAttributeString("first", $"{record.FirstName}");
                writer.WriteAttributeString("last", $"{record.LastName}");
                writer.WriteEndElement();

                writer.WriteStartElement("dateOfBirth");
                writer.WriteString(record.DateOfBirth.ToString(CultureInfo.CurrentCulture));
                writer.WriteEndElement();

                writer.WriteStartElement("property1");
                writer.WriteString(record.Property1.ToString(CultureInfo.CurrentCulture));
                writer.WriteEndElement();

                writer.WriteStartElement("property2");
                writer.WriteString(record.Property2.ToString(CultureInfo.CurrentCulture));
                writer.WriteEndElement();

                writer.WriteStartElement("property3");
                writer.WriteString(record.Property3.ToString(CultureInfo.CurrentCulture));
                writer.WriteEndElement();

                writer.WriteEndElement();

            }
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Close();
        }

    }
}
