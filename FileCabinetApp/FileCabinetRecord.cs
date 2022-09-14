namespace FileCabinetApp
{
    /// <summary>
    ///   Represents the record for file cabinet service with a set of properties.
    /// </summary>
    public class FileCabinetRecord
    {
        /// <summary>Initializes a new instance of the <see cref="FileCabinetRecord" /> class.</summary>
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

        /// <summary>Initializes a new instance of the <see cref="FileCabinetRecord" /> class.</summary>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="dateOfBirth">The date of birth.</param>
        /// <param name="property1">The property1.</param>
        /// <param name="property2">The property2.</param>
        /// <param name="property3">The property3.</param>
        public FileCabinetRecord(string firstName, string lastName, DateTime dateOfBirth, short property1, decimal property2, char property3)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.DateOfBirth = dateOfBirth;
            this.Id = 0;
            this.Property1 = property1;
            this.Property2 = property2;
            this.Property3 = property3;
        }

        /// <summary>Gets or sets the property1.</summary>
        /// <value>The property1.</value>
        public short Property1 { get; set; }

        /// <summary>Gets or sets the property2.</summary>
        /// <value>The property2.</value>
        public decimal Property2 { get; set; }

        /// <summary>Gets or sets the property3.</summary>
        /// <value>The property3.</value>
        public char Property3 { get; set; }

        /// <summary>Gets or sets the identifier.</summary>
        /// <value>The identifier.</value>
        public int Id { get; set; }

        /// <summary>Gets or sets the first name.</summary>
        /// <value>The first name.</value>
        public string FirstName { get; set; }

        /// <summary>Gets or sets the last name.</summary>
        /// <value>The last name.</value>
        public string LastName { get; set; }

        /// <summary>Gets or sets the date of birth.</summary>
        /// <value>The date of birth.</value>
        public DateTime DateOfBirth { get; set; }

        /// <summary>Equalses the specified record.</summary>
        /// <param name="record">The record.</param>
        /// <returns>
        ///   <see langword="true"/> if the records are equals, <see langword="false"/> otherwise.
        /// </returns>
        public bool Equals(FileCabinetRecord? record)
        {
            if (record == null)
            {
                return false;
            }

            if (record.Id != this.Id || record.FirstName != this.FirstName || record.LastName != this.LastName || record.DateOfBirth != this.DateOfBirth || record.Property1 != this.Property1 || record.Property2 != this.Property2 || record.Property3 != this.Property3)
            {
                return false;
            }

            return true;
        }
    }
}