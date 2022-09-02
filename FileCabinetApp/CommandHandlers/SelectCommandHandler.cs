using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCabinetApp.CommandHandlers
{
    public class SelectCommandHandler : ServiceCommandHandlerBase
    {
        private const string HintMessageSelect = "Use: select id, firstname, lastname, dateofbirth, Property1 ... Property3 where firstname = '[value]' [and/or] lastname = '[value]'";
        private Action<string[], IEnumerable<FileCabinetRecord>> printer;

        public SelectCommandHandler(IFileCabinetService? fileCabinetService, Action<string[], IEnumerable<FileCabinetRecord>> printer)
            : base(fileCabinetService)
        {
            this.printer = printer;
        }

        public override void Handle(AppCommandRequest request)
        {
            if (request.Command.Equals("select", StringComparison.OrdinalIgnoreCase))
            {
                var data = request.Parameters.Replace("select", string.Empty, StringComparison.CurrentCulture).Replace("'", string.Empty, StringComparison.CurrentCulture).Split("where", StringSplitOptions.TrimEntries);

                var select = data[0].Split(new char[] { ',' }, StringSplitOptions.TrimEntries);
                string where;
                if (data.Length == 1)
                {
                    where = string.Empty;
                }
                else
                {
                    where = data[1];
                }

                var whereParametrs = where.Split(new string[] { "and", "or" }, StringSplitOptions.TrimEntries);
                List<IEnumerable<FileCabinetRecord>> parameters = new List<IEnumerable<FileCabinetRecord>>();
                foreach (var param in whereParametrs)
                {
                    var nameValue = param.Split('=', StringSplitOptions.TrimEntries);
                    if (nameValue.Length == 2)
                    {
                        var records = this.service.FindRecords(nameValue[0], nameValue[1]);
                        parameters.Add(records);
                    }
                }

                if (where.Contains("and", StringComparison.CurrentCultureIgnoreCase))
                {
                    for (int g = 1; g < parameters.Count; g++)
                    {
                        var result = this.And(parameters[g - 1], parameters[g]);
                        parameters[g] = result;
                    }
                }
                else if (where.Contains("or", StringComparison.CurrentCultureIgnoreCase))
                {
                    for (int g = 1; g < parameters.Count; g++)
                    {
                        var result = this.Or(parameters[g - 1], parameters[g]);
                        parameters[g] = result;
                    }
                }

                IEnumerable<FileCabinetRecord> resultWhere = new List<FileCabinetRecord>();
                if (parameters.Count > 0)
                {
                    resultWhere = parameters[parameters.Count - 1];
                }


                if (string.IsNullOrEmpty(where))
                {
                    resultWhere = this.service.GetRecords();
                }

                if (!resultWhere.Any())
                {
                    Console.WriteLine("records are not found.");
                    return;
                }

                if (select.Length == 1 && string.IsNullOrEmpty(select[0]))
                {
                    select = new string[] { "id", "firstname", "lastname", "dateofbirth", "property1", "property2", "property3" };
                }

                try
                {
                    this.printer(select, resultWhere);
                }
                catch
                {
                    Console.WriteLine("incorrect format");
                    Console.WriteLine(HintMessageSelect);
                }
            }
            else
            {
                base.Handle(request);
            }
        }

        private IEnumerable<FileCabinetRecord> And(IEnumerable<FileCabinetRecord> one, IEnumerable<FileCabinetRecord> two)
        {
            var result = new List<FileCabinetRecord>();
            foreach (var item1 in one)
            {
                foreach (var item2 in two)
                {
                    if (item1.Id == item2.Id && !result.Contains(item2))
                    {
                        result.Add(item2);
                    }
                }
            }

            return result;
        }

        private IEnumerable<FileCabinetRecord> Or(IEnumerable<FileCabinetRecord> one, IEnumerable<FileCabinetRecord> two)
        {
            var result = new List<FileCabinetRecord>();
            foreach (var item1 in one)
            {
                if (!result.Contains(item1))
                {
                    result.Add(item1);
                }
            }

            foreach (var item2 in two)
            {
                if (!result.Contains(item2))
                {
                    result.Add(item2);
                }
            }

            return result;
        }
    }
}
