using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCabinetApp.CommandHandlers
{
    public class InsertCommandHandler : ServiceCommandHandlerBase
    {
        private const string HintMessageInsert = "Use: insert (id, firstname, lastname, dateofbirth, Property1, Property2, Property3) values ('[value]', '[value]', ...)";

        public InsertCommandHandler(IFileCabinetService? fileCabinetService)
            : base(fileCabinetService)
        {
        }

        public override void Handle(AppCommandRequest request)
        {
            if (request.Command.Equals("insert", StringComparison.OrdinalIgnoreCase))
            {
                try
                {
                    if (string.IsNullOrEmpty(request.Parameters))
                    {
                        Console.WriteLine(HintMessageInsert);
                        return;
                    }

                    var data = request.Parameters.Replace(",", " ", StringComparison.CurrentCulture).Replace("(", string.Empty, StringComparison.CurrentCulture).Replace(")", string.Empty, StringComparison.CurrentCulture).Replace("'", string.Empty, StringComparison.CurrentCulture).Split("values");

                    var name = data[0].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    var value = data[1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    if (value.Length != name.Length)
                    {
                        Console.WriteLine(HintMessageInsert);
                        return;
                    }

                    var record = new FileCabinetRecord();
                    var ind = Array.FindIndex(name, i => i.Equals("id", StringComparison.OrdinalIgnoreCase));
                    record.Id = Convert.ToInt32(value[ind], CultureInfo.CurrentCulture);

                    ind = Array.FindIndex(name, i => i.Equals("firstname", StringComparison.OrdinalIgnoreCase));
                    record.FirstName = value[ind];

                    ind = Array.FindIndex(name, i => i.Equals("lastname", StringComparison.OrdinalIgnoreCase));
                    record.LastName = value[ind];

                    ind = Array.FindIndex(name, i => i.Equals("dateofbirth", StringComparison.OrdinalIgnoreCase));
                    record.DateOfBirth = DateTime.Parse(value[ind], CultureInfo.CurrentCulture);

                    ind = Array.FindIndex(name, i => i.Equals("Property1", StringComparison.OrdinalIgnoreCase));
                    record.Property1 = Convert.ToInt16(value[ind], CultureInfo.CurrentCulture);

                    ind = Array.FindIndex(name, i => i.Equals("Property2", StringComparison.OrdinalIgnoreCase));
                    record.Property2 = Convert.ToDecimal(value[ind], CultureInfo.CurrentCulture);

                    ind = Array.FindIndex(name, i => i.Equals("Property3", StringComparison.OrdinalIgnoreCase));
                    record.Property3 = Convert.ToChar(value[ind], CultureInfo.CurrentCulture);

                    var number = this.Service?.CreateRecord(record);
                    Console.WriteLine($"Record #{number} is created.");
                }
                catch
                {
                    Console.WriteLine("incorrect format");
                    return;
                }
            }
            else
            {
                base.Handle(request);
            }
        }
    }
}
