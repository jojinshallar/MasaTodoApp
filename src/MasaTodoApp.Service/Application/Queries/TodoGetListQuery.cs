using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasaTodoApp.Service.Application.Queries
{
    public record TodoGetListQuery : Query<List<TodoGetListDto>>
    {
        public override List<TodoGetListDto> Result { get; set; } = null!;
    }
}