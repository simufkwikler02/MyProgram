﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCabinetApp
{
    public class DefaultValidator : IRecordValidator
    {
        private readonly string rules = "default";
        private readonly DateTime minDate = new DateTime(1950, 6, 1);

        public bool ValidateParametrsFirstName(string input)
        {
            if (string.IsNullOrEmpty(input) || input.Length < 2 || input.Length > 60)
            {
                return false;
            }

            return true;
        }

        public bool ValidateParametrsLastName(string input)
        {
            if (string.IsNullOrEmpty(input) || input.Length < 2 || input.Length > 60)
            {
                return false;
            }

            return true;
        }

        public bool ValidateParametrsDateOfBirth(DateTime input)
        {
            if (this.minDate > input || DateTime.Now < input)
            {
                return false;
            }

            return true;
        }

        public bool ValidateParametrsProperty1(short input)
        {
            if (input < 1 || input > 100)
            {
                return false;
            }

            return true;
        }

        public bool ValidateParametrsProperty2(decimal input)
        {
            if (input > 2)
            {
                return false;
            }

            return true;
        }

        public bool ValidateParametrsProperty3(char input)
        {
            if (input == 'l')
            {
                return false;
            }

            return true;
        }

        public bool ValidateParametrs(FileCabinetRecord newRecord)
        {
            if (string.IsNullOrEmpty(newRecord.FirstName) || newRecord.FirstName.Length < 2 || newRecord.FirstName.Length > 60)
            {
                return false;
            }

            if (string.IsNullOrEmpty(newRecord.LastName) || newRecord.LastName.Length < 2 || newRecord.LastName.Length > 60)
            {
                return false;
            }

            DateTime minDate = new DateTime(1950, 6, 1);
            if (minDate > newRecord.DateOfBirth || DateTime.Now < newRecord.DateOfBirth)
            {
                return false;
            }

            if (newRecord.Property1 < 1 || newRecord.Property1 > 100)
            {
                return false;
            }

            if (newRecord.Property2 > 2)
            {
                return false;
            }

            if (newRecord.Property3 == 'l')
            {
                return false;
            }

            return true;
        }

        public string ValidateInfo()
        {
            return this.rules;
        }

        public DateTime DateTimeMin()
        {
            return this.minDate;
        }
    }
}
