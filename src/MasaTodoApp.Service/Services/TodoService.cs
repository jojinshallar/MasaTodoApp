using System.Security.Cryptography.X509Certificates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasaTodoApp.Contracts;
using MasaTodoApp.Service.Application.Queries;
using MasaTodoApp.Service.Application.Commands;

namespace MasaTodoApp.Service.Services
{
    public class TodoService : ServiceBase
    {
        private IEventBus EventBus => GetRequiredService<IEventBus>();

        public async Task<List<TodoGetListDto>> GetListAsync()
        {
            var todoQuery = new TodoGetListQuery();
            await EventBus.PublishAsync(todoQuery);
            return todoQuery.Result;
        }

        public async Task CreateAsync(TodoCreateUpdateDto dto){
            var createCmd=new CreateTodoCommand(dto);
            await EventBus.PublishAsync(createCmd);
        }

        public async Task UpdateAsync(Guid id,TodoCreateUpdateDto dto){
            var updateCmd=new UpdateTodoCommand(id,dto);
            await EventBus.PublishAsync(updateCmd);
        }

        public async Task DeleteAsync(Guid id){
            var deleteCmd=new DeleteTodoCommand(id);
            await EventBus.PublishAsync(deleteCmd);
        }

        public async Task DoneAsync([FromQuery]Guid id,[FromQuery]bool done){
            var doneCmd=new DoneTodoCommand(id,done);
            await EventBus.PublishAsync(doneCmd);
        }
    }
}