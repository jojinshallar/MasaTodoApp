using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasaTodoApp.Service.Application.Queries;
using MasaTodoApp.Service.DataAccess.Entities;

namespace MasaTodoApp.Service.Application
{
    public class TodoQueryHandler
    {
        private readonly TodoAppDbContext _todoDbContext;

        public TodoQueryHandler(TodoAppDbContext todoAppDbContext)
        {
            _todoDbContext=todoAppDbContext;
        }

      [EventHandler]
      public async Task GetListAsync(TodoGetListQuery query){
        var todoDbQuery=_todoDbContext.Set<TodoEntity>().AsNoTracking();
        query.Result=await todoDbQuery.Select(p=>p.Adapt<TodoGetListDto>()).ToListAsync();
      }
    }
}