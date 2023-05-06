using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasaTodoApp.Service.Application.Commands
{
    public record DoneTodoCommand(Guid Id, bool Done) : Command
    {

    }
}