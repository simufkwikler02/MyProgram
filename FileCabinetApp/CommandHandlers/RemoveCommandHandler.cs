using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCabinetApp.CommandHandlers
{
    public class RemoveCommandHandler : ServiceCommandHandlerBase
    {
        private const string HintMessageRemove = "Use: remove [number]";

        public RemoveCommandHandler(IFileCabinetService? fileCabinetService)
            : base(fileCabinetService)
        {
        }

        public override void Handle(AppCommandRequest request)
        {
            if (request.Command.Equals("remove", StringComparison.OrdinalIgnoreCase))
            {
                var command = request.Parameters != null ? request.Parameters : string.Empty;

                if (string.IsNullOrEmpty(command))
                {
                    Console.WriteLine(HintMessageRemove);
                    return;
                }

                try
                {
                    int id = Convert.ToInt32(command, CultureInfo.CurrentCulture);
                    if (!this.service.IdExist(id))
                    {
                        Console.WriteLine($"record with number {id} is not exist.");
                        return;
                    }

                    this.service.RemoveRecord(id);
                }
                catch
                {
                    Console.WriteLine("incorrect format");
                }
            }
            else
            {
                base.Handle(request);
            }
        }
    }
}
