using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace FileCabinetApp
{
    /// <summary>
    ///   The class that contains parameters of the validation rules.
    /// </summary>
    public class ValidateParametrs
    {
        /// <summary>Initializes a new instance of the <see cref="ValidateParametrs" /> class with default settings.</summary>
        public ValidateParametrs()
        {
            this.ValidateInfo = "default";
            this.MinLengthFirstName = 2;
            this.MaxLengthFirstName = 60;
            this.MinLengthLastName = 2;
            this.MaxLengthLastName = 60;
            this.From = new DateTime(1950, 6, 1);
            this.To = DateTime.Now;
            this.MinProperty1 = -100;
            this.MaxProperty1 = 100;
            this.MinProperty2 = -10;
            this.MaxProperty2 = 10;
            this.BanSymbols = new char[] { 'h', 'e', 'l', 'p' };
        }

        /// <summary>Gets or sets the name of the validation rules.</summary>
        /// <value>The name of the validation rules <see cref="string"/>.</value>
        public string ValidateInfo { get; set; }

        /// <summary>Gets or sets the minimum length of the first name.</summary>
        /// <value>The minimum length of the first name <see cref="int"/>.</value>
        public int MinLengthFirstName { get; set; }

        /// <summary>Gets or sets the maximum length of the first name.</summary>
        /// <value>The maximum length of the first name <see cref="int"/>.</value>
        public int MaxLengthFirstName { get; set; }

        /// <summary>Gets or sets the minimum length of the last name.</summary>
        /// <value>The minimum length of the last name <see cref="int"/>.</value>
        public int MinLengthLastName { get; set; }

        /// <summary>Gets or sets the maximum length of the last name.</summary>
        /// <value>The maximum length of the last name <see cref="int"/>.</value>
        public int MaxLengthLastName { get; set; }

        /// <summary>Gets or sets the minimum value date of birth.</summary>
        /// <value>The minimum value date of birth <see cref="DateTime"/>.</value>
        public DateTime From { get; set; }

        /// <summary>Gets or sets the maximum value date of birth.</summary>
        /// <value>The maximum value date of birth <see cref="DateTime"/>.</value>
        public DateTime To { get; set; }

        /// <summary>Gets or sets the minimum value of the property1.</summary>
        /// <value>The minimum value of the property1 <see cref="short"/>.</value>
        public short MinProperty1 { get; set; }

        /// <summary>Gets or sets the maximum value of the property1.</summary>
        /// <value>The maximum value of the property1 <see cref="short"/>.</value>
        public short MaxProperty1 { get; set; }

        /// <summary>Gets or sets the minimum value of the property2.</summary>
        /// <value>The minimum value of the property2 <see cref="decimal"/>.</value>
        public decimal MinProperty2 { get; set; }

        /// <summary>Gets or sets the maximum value of the property2.</summary>
        /// <value>The maximum value of the property2 <see cref="decimal"/>.</value>
        public decimal MaxProperty2 { get; set; }

        /// <summary>Gets or sets the ban symbols for the property3.</summary>
        /// <value>The ban symbols for the property3 <see langword="char[]"/>.</value>
        public char[] BanSymbols { get; set; }

        /// <summary>Sets the default parameters of the validation rules.</summary>
        public void SetDefaultParametrs()
        {
            IConfiguration config = new ConfigurationBuilder().AddJsonFile("validation-rules.json").Build();
            var rules = config.GetSection("default:firstName");
            ConfigurationBinder.Bind(rules, this);
            rules = config.GetSection("default:lastName");
            ConfigurationBinder.Bind(rules, this);
            rules = config.GetSection("default:dateOfBirth");
            ConfigurationBinder.Bind(rules, this);
            rules = config.GetSection("default:Property1");
            ConfigurationBinder.Bind(rules, this);
            rules = config.GetSection("default:Property2");
            ConfigurationBinder.Bind(rules, this);
            rules = config.GetSection("default:Property3");
            this.BanSymbols = Array.Empty<char>();
            ConfigurationBinder.Bind(rules, this);
            this.ValidateInfo = "default";
        }

        /// <summary>Sets the custom parameters of the validation rules.</summary>
        public void SetCustomParametrs()
        {
            IConfiguration config = new ConfigurationBuilder().AddJsonFile("validation-rules.json").Build();
            var rules = config.GetSection("custom:firstName");
            ConfigurationBinder.Bind(rules, this);
            rules = config.GetSection("custom:lastName");
            ConfigurationBinder.Bind(rules, this);
            rules = config.GetSection("custom:dateOfBirth");
            ConfigurationBinder.Bind(rules, this);
            rules = config.GetSection("custom:Property1");
            ConfigurationBinder.Bind(rules, this);
            rules = config.GetSection("custom:Property2");
            ConfigurationBinder.Bind(rules, this);
            rules = config.GetSection("custom:Property3");
            this.BanSymbols = Array.Empty<char>();
            ConfigurationBinder.Bind(rules, this);
            this.ValidateInfo = "custom";
        }
    }
}
