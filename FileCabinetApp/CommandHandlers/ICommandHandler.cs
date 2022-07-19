﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCabinetApp.CommandHandlers
{
    public interface ICommandHandler
    {
        public ICommandHandler SetNext(ICommandHandler a);

        public void Handle(AppCommandRequest a);
    }
}
