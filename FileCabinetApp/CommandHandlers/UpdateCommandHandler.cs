using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCabinetApp.CommandHandlers
{
    /// <summary>
    ///   Represents the command handler "update".
    /// </summary>
    public class UpdateCommandHandler : ServiceCommandHandlerBase
    {
        private const string HintMessageUpdate = "Use: update set [name] = '[value]', [name] = '[value]' where [name] = '[value]'";

        /// <summary>Initializes a new instance of the <see cref="UpdateCommandHandler" /> class.</summary>
        /// <param name="fileCabinetService">The file cabinet service.</param>
        public UpdateCommandHandler(IFileCabinetService? fileCabinetService)
            : base(fileCabinetService)
        {
        }

        /// <summary>Handles the specified request.</summary>
        /// <param name="request">The request.</param>
        public override void Handle(AppCommandRequest request)
        {
            if (request.Command.Equals("update", StringComparison.OrdinalIgnoreCase))
            {
                try
                {
                    if (string.IsNullOrEmpty(request.Parameters))
                    {
                        Console.WriteLine(HintMessageUpdate);
                        return;
                    }

                    var data = request.Parameters.Replace("set", string.Empty, StringComparison.CurrentCulture).Replace("'", string.Empty, StringComparison.CurrentCulture).Split("where", StringSplitOptions.TrimEntries);

                    var set = data[0].Split(new char[] { ',' }, StringSplitOptions.TrimEntries);
                    var where = data[1].Split(new string[] { "and", "or" }, StringSplitOptions.TrimEntries);

                    List<IEnumerable<FileCabinetRecord>> parameters = new List<IEnumerable<FileCabinetRecord>>();
                    foreach (var param in where)
                    {
                        var records = this.Service?.FindRecords(param.Split('=')[0], param.Split('=')[1]) ?? new List<FileCabinetRecord>();
                        parameters.Add(records);
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

                    var resultWhere = parameters[parameters.Count - 1];
                    if (!resultWhere.Any())
                    {
                        Console.WriteLine("This records does't exist");
                        return;
                    }

                    var name = new List<string>();
                    var value = new List<string>();
                    Dictionary<string, string> nameValue = new Dictionary<string, string>();

                    foreach (var param in set)
                    {
                        name.Add(param.Split('=', StringSplitOptions.TrimEntries)[0]);
                        value.Add(param.Split('=', StringSplitOptions.TrimEntries)[1]);
                    }

                    var idupdate = new List<long>();
                    foreach (var record in resultWhere)
                    {
                        var ind = name.FindIndex(i => i.Equals("id", StringComparison.OrdinalIgnoreCase));
                        record.Id = ind == -1 ? record.Id : Convert.ToInt32(value[ind], CultureInfo.CurrentCulture);

                        ind = name.FindIndex(i => i.Equals("firstname", StringComparison.OrdinalIgnoreCase));
                        record.FirstName = ind == -1 ? record.FirstName : value[ind];

                        ind = name.FindIndex(i => i.Equals("lastname", StringComparison.OrdinalIgnoreCase));
                        record.LastName = ind == -1 ? record.LastName : value[ind];

                        ind = name.FindIndex(i => i.Equals("dateofbirth", StringComparison.OrdinalIgnoreCase));
                        record.DateOfBirth = ind == -1 ? record.DateOfBirth : DateTime.Parse(value[ind], CultureInfo.CurrentCulture);

                        ind = name.FindIndex(i => i.Equals("Property1", StringComparison.OrdinalIgnoreCase));
                        record.Property1 = ind == -1 ? record.Property1 : Convert.ToInt16(value[ind], CultureInfo.CurrentCulture);

                        ind = name.FindIndex(i => i.Equals("Property2", StringComparison.OrdinalIgnoreCase));
                        record.Property2 = ind == -1 ? record.Property2 : Convert.ToDecimal(value[ind], CultureInfo.CurrentCulture);

                        ind = name.FindIndex(i => i.Equals("Property3", StringComparison.OrdinalIgnoreCase));
                        record.Property3 = ind == -1 ? record.Property3 : Convert.ToChar(value[ind], CultureInfo.CurrentCulture);
                        var indexUpdate = this.Service?.UpdateRecord(this.Service.FindIndex(record), record) ?? -1;
                        if (indexUpdate != -1)
                        {
                            idupdate.Add(indexUpdate);
                        }
                    }

                    if (idupdate.Count > 1)
                    {
                        Console.Write("Records ");
                        Console.Write($"#{idupdate[0]}");
                        for (int g = 1; g < idupdate.Count; g++)
                        {
                            Console.Write($", #{idupdate[g]}");
                        }

                        Console.WriteLine(" are updated.");
                    }
                    else if (idupdate.Count == 1)
                    {
                        Console.WriteLine($"Record #{idupdate[0]} is updated");
                    }
                    else
                    {
                        Console.WriteLine("incorrect format");
                    }
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
