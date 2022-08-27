using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCabinetApp.CommandHandlers
{
    public class SelectCommandHandler : ServiceCommandHandlerBase
    {
        private const string HintMessageSelect = "";

        public SelectCommandHandler(IFileCabinetService? fileCabinetService)
            : base(fileCabinetService)
        {
        }

        public override void Handle(AppCommandRequest request)
        {
            if (request.Command.Equals("select", StringComparison.OrdinalIgnoreCase))
            {

                if (string.IsNullOrEmpty(request.Parameters))
                {
                    Console.WriteLine(HintMessageSelect);
                    return;
                }

                var data = request.Parameters.Replace("select", string.Empty, StringComparison.CurrentCulture).Replace("'", string.Empty, StringComparison.CurrentCulture).Split("where", StringSplitOptions.TrimEntries);

                var select = data[0].Split(new char[] { ',' }, StringSplitOptions.TrimEntries);
                var where = data[1].Split("and", StringSplitOptions.TrimEntries);

                List<List<long>> parameters = new List<List<long>>();
                foreach (var param in where)
                {
                    var index = this.service.FindIndex(param.Split('=')[0], param.Split('=')[1]);
                    parameters.Add(new List<long>(index));

                }

                for (int g = 1; g < parameters.Count; g++)
                {
                    var result = this.And(parameters[g - 1], parameters[g]);
                    parameters[g] = result;
                }

                var resultWhere = parameters[parameters.Count - 1];
                if (resultWhere.Count < 1)
                {
                    Console.WriteLine("This records does't exist");
                    return;
                }

            }
            else
            {
                base.Handle(request);
            }
        }

        private List<long> And(List<long> one, List<long> two)
        {
            var result = new List<long>();
            foreach (var item1 in one)
            {
                foreach (var item2 in two)
                {
                    if (item1 == item2 && !result.Contains(item2))
                    {
                        result.Add(item2);
                    }
                }
            }

            return result;
        }
    }
}
