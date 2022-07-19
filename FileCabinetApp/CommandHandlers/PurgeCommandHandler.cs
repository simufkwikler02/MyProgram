﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCabinetApp.CommandHandlers
{
    public class PurgeCommandHandler : CommandHandlerBase
    {
        public PurgeCommandHandler(IFileCabinetService fileCabinetService, IRecordValidator validate)
            : base(fileCabinetService, validate)
        {
        }

        public override void Handle(AppCommandRequest request)
        {
            if (request.Command.Equals("purge", StringComparison.OrdinalIgnoreCase))
            {
                this.service.PurgeRecords();
            }
            else
            {
                base.Handle(request);
            }
        }
    }
}
