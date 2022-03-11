﻿namespace FileCabinetApp
{
    public class FileCabinetRecord
    {
        public FileCabinetRecord()
        {
            this.FirstName = string.Empty;
            this.LastName = string.Empty;
            this.DateOfBirth = DateTime.MinValue;
            this.Id = 0;
            this.Property1 = 0;
            this.Property2 = 0;
            this.Property3 = ' ';
        }

        public short Property1 { get; set; }

        public decimal Property2 { get; set; }

        public char Property3 { get; set; }

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }
    }
}