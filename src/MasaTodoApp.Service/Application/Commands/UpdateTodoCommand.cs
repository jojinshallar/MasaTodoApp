using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasaTodoApp.Service.Application.Commands
{
    public record UpdateTodoCommand(Guid Id, TodoCreateUpdateDto Dto) : Command
    {

    }
}