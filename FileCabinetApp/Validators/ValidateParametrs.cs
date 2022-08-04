using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace FileCabinetApp
{
    public class ValidateParametrs
    {
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

        public string ValidateInfo { get; set; }

        public int MinLengthFirstName { get; set; }

        public int MaxLengthFirstName { get; set; }

        public int MinLengthLastName { get; set; }

        public int MaxLengthLastName { get; set; }

        public DateTime From { get; set; }

        public DateTime To { get; set; }

        public short MinProperty1 { get; set; }

        public short MaxProperty1 { get; set; }

        public decimal MinProperty2 { get; set; }

        public decimal MaxProperty2 { get; set; }

        public char[] BanSymbols { get; set; }

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
