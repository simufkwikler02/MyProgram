﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
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
            this.fileStream.Seek(0, SeekOrigin.Begin);
            while (this.fileStream.Position < this.fileStream.Length)
            {
                var record = this.ReadRecord();
                switch (this.name)
                {
                    case "firstname":
                        if (record.FirstName == this.data)
                        {
                            yield return record;
                        }

                        break;
                    case "lastname":
                        if (record.LastName == this.data)
                        {
                            yield return record;
                        }

                        break;
                    case "dateofbirth":
                        if (record.DateOfBirth == DateTime.Parse(this.data, CultureInfo.CurrentCulture))
                        {
                            yield return record;
                        }

                        break;
                }
            }

            yield break;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        private FileCabinetRecord ReadRecord()
        {
            FileCabinetRecord record = new FileCabinetRecord();
            byte[] buffer = new byte[2];
            this.fileStream?.Read(buffer, 0, 2);
            int num = Array.IndexOf(buffer, (byte)0);
            if (num > 0)
            {
                Array.Resize(ref buffer, num);
            }

            short status = Convert.ToInt16(Encoding.Default.GetString(buffer), CultureInfo.CurrentCulture);

            buffer = new byte[4];

            this.fileStream?.Read(buffer, 0, 4);
            num = Array.IndexOf(buffer, (byte)0);
            if (num > 0)
            {
                Array.Resize(ref buffer, num);
            }

            record.Id = Convert.ToInt32(Encoding.Default.GetString(buffer), CultureInfo.CurrentCulture);

            buffer = new byte[120];

            this.fileStream?.Read(buffer, 0, 120);
            num = Array.IndexOf(buffer, (byte)0);
            if (num > 0)
            {
                Array.Resize(ref buffer, num);
            }

            record.FirstName = Encoding.Default.GetString(buffer);

            buffer = new byte[120];

            this.fileStream?.Read(buffer, 0, 120);
            num = Array.IndexOf(buffer, (byte)0);
            if (num > 0)
            {
                Array.Resize(ref buffer, num);
            }

            record.LastName = Encoding.Default.GetString(buffer);

            buffer = new byte[4];

            this.fileStream?.Read(buffer, 0, 4);
            num = Array.IndexOf(buffer, (byte)0);
            if (num > 0)
            {
                Array.Resize(ref buffer, num);
            }

            var year = Convert.ToInt32(Encoding.Default.GetString(buffer), CultureInfo.CurrentCulture);

            buffer = new byte[4];

            this.fileStream?.Read(buffer, 0, 4);
            num = Array.IndexOf(buffer, (byte)0);
            if (num > 0)
            {
                Array.Resize(ref buffer, num);
            }

            var month = Convert.ToInt32(Encoding.Default.GetString(buffer), CultureInfo.CurrentCulture);

            buffer = new byte[4];

            this.fileStream?.Read(buffer, 0, 4);
            num = Array.IndexOf(buffer, (byte)0);
            if (num > 0)
            {
                Array.Resize(ref buffer, num);
            }

            var day = Convert.ToInt32(Encoding.Default.GetString(buffer), CultureInfo.CurrentCulture);

            record.DateOfBirth = new DateTime(year, month, day);

            buffer = new byte[2];

            this.fileStream?.Read(buffer, 0, 2);
            num = Array.IndexOf(buffer, (byte)0);
            if (num > 0)
            {
                Array.Resize(ref buffer, num);
            }

            record.Property1 = Convert.ToInt16(Encoding.Default.GetString(buffer), CultureInfo.CurrentCulture);

            buffer = new byte[16];

            this.fileStream?.Read(buffer, 0, 16);
            num = Array.IndexOf(buffer, (byte)0);
            if (num > 0)
            {
                Array.Resize(ref buffer, num);
            }

            record.Property2 = Convert.ToDecimal(Encoding.Default.GetString(buffer), CultureInfo.CurrentCulture);

            buffer = new byte[2];

            this.fileStream?.Read(buffer, 0, 2);
            num = Array.IndexOf(buffer, (byte)0);
            if (num > 0)
            {
                Array.Resize(ref buffer, num);
            }

            record.Property3 = Convert.ToChar(Encoding.Default.GetString(buffer), CultureInfo.CurrentCulture);
            record = status == 0 ? record : this.ReadRecord();
            return record;
        }
    }
}
