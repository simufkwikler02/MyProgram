using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCabinetApp
{
    /// <summary>
    ///   Represents the validator interface.
    /// </summary>
    public interface IRecordValidator
    {
        /// <summary>Validates the record.</summary>
        /// <param name="record">The record.</param>
        /// <returns>
        ///   <see langword="true"/> if the record passed validation, <see langword="false"/> otherwise  .
        /// </returns>
        public bool ValidateParametrs(FileCabinetRecord record);

        /// <summary>Validates the first name.</summary>
        /// <param name="input">The first name.</param>
        /// <returns>
        ///   <see langword="true"/> if the input passed validation, <see langword="false"/> otherwise  .
        /// </returns>
        public bool ValidateParametrFirstName(string input);

        /// <summary>Validates the last name.</summary>
        /// <param name="input">The last name.</param>
        /// <returns>
        ///   <see langword="true"/> if the input passed validation, <see langword="false"/> otherwise  .
        /// </returns>
        public bool ValidateParametrLastName(string input);

        /// <summary>Validates the date of birth.</summary>
        /// <param name="input">The date of birth.</param>
        /// <returns>
        ///   <see langword="true"/> if the input passed validation, <see langword="false"/> otherwise  .
        /// </returns>
        public bool ValidateParametrDateOfBirth(DateTime input);

        /// <summary>Validates the property1.</summary>
        /// <param name="input">The property1.</param>
        /// <returns>
        ///   <see langword="true"/> if the input passed validation, <see langword="false"/> otherwise  .
        /// </returns>
        public bool ValidateParametrProperty1(short input);

        /// <summary>Validates the property2.</summary>
        /// <param name="input">The property2.</param>
        /// <returns>
        ///   <see langword="true"/> if the input passed validation, <see langword="false"/> otherwise  .
        /// </returns>
        public bool ValidateParametrProperty2(decimal input);

        /// <summary>Validates the property3.</summary>
        /// <param name="input">The property3.</param>
        /// <returns>
        ///   <see langword="true"/> if the input passed validation, <see langword="false"/> otherwise  .
        /// </returns>
        public bool ValidateParametrProperty3(char input);

        /// <summary>Return the name of the validation rules.</summary>
        /// <returns>
        ///   The name of the validation rules <see cref="string"/>.
        /// </returns>
        public string ValidateInfo();

        /// <summary>Return instance that contains validation rules.</summary>
        /// <returns>
        ///   The instance <see cref="FileCabinetApp.ValidateParametrs"/>.
        /// </returns>
        public ValidateParametrs ParametrsInfo();
    }
}
