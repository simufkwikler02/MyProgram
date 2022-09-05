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
        private Action<string[]?, IEnumerable<FileCabinetRecord>?> printer;

        public SelectCommandHandler(IFileCabinetService? fileCabinetService, Action<string[]?, IEnumerable<FileCabinetRecord>?> printer)
            : base(fileCabinetService)
        {
            this.printer = printer;
        }

        public override void Handle(AppCommandRequest request)
        {
            if (request.Command?.Equals("select", StringComparison.OrdinalIgnoreCase) ?? false)
            {
                try
                {
                    if (string.IsNullOrEmpty(request.Parameters))
                    {
                        this.printer(new string[] { "id", "firstname", "lastname", "dateofbirth", "property1", "property2", "property3" }, this.Service?.GetRecords());
                        return;
                    }

                    if (!request.Parameters.Contains("where", StringComparison.OrdinalIgnoreCase))
                    {
                        var selectIf = request.Parameters?.Replace("'", string.Empty, StringComparison.CurrentCulture).Split(new char[] { ',' }, StringSplitOptions.TrimEntries);
                        this.printer(selectIf, this.Service?.GetRecords());
                        return;
                    }

                    var data = request.Parameters.Replace("'", string.Empty, StringComparison.CurrentCulture).Split("where", StringSplitOptions.TrimEntries);

                    var select = data[0].Split(new char[] { ',' }, StringSplitOptions.TrimEntries);
                    var where = data[1].Split(new string[] { "and", "or" }, StringSplitOptions.TrimEntries);

                    List<IEnumerable<FileCabinetRecord>> parameters = new List<IEnumerable<FileCabinetRecord>>();
                    foreach (var param in where)
                    {
                        var nameValue = param.Split('=', StringSplitOptions.TrimEntries);
                        if (nameValue.Length == 2)
                        {
                            var records = this.Service?.FindRecords(nameValue[0], nameValue[1]) ?? new List<FileCabinetRecord>();
                            parameters.Add(records);
                        }
                    }

                    if (data[1].Contains("and", StringComparison.CurrentCultureIgnoreCase))
                    {
                        for (int g = 1; g < parameters.Count; g++)
                        {
                            var result = And(parameters[g - 1], parameters[g]);
                            parameters[g] = result;
                        }
                    }
                    else if (data[1].Contains("or", StringComparison.CurrentCultureIgnoreCase))
                    {
                        for (int g = 1; g < parameters.Count; g++)
                        {
                            var result = Or(parameters[g - 1], parameters[g]);
                            parameters[g] = result;
                        }
                    }

                    IEnumerable<FileCabinetRecord> resultWhere = new List<FileCabinetRecord>();
                    if (parameters.Count > 0)
                    {
                        resultWhere = parameters[parameters.Count - 1];
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

        private static IEnumerable<FileCabinetRecord> And(IEnumerable<FileCabinetRecord> one, IEnumerable<FileCabinetRecord> two)
        {
            var result = new List<FileCabinetRecord>();
            foreach (var item1 in one)
            {
                foreach (var item2 in two)
                {
                    if (item1.Equals(item2) && !result.Contains(item2))
                    {
                        result.Add(item2);
                    }
                }
            }

            return result;
        }

        private static IEnumerable<FileCabinetRecord> Or(IEnumerable<FileCabinetRecord> one, IEnumerable<FileCabinetRecord> two)
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
