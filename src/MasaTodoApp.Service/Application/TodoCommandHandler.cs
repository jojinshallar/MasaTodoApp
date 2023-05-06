using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasaTodoApp.Service.Application.Commands;
using MasaTodoApp.Service.DataAccess.Entities;

namespace MasaTodoApp.Service.Application
{
    public class TodoCommandHandler
    {
        private readonly TodoAppDbContext _todoDbContext;
        public TodoCommandHandler(TodoAppDbContext todoAppDbContext){
            _todoDbContext=todoAppDbContext;
        }

        [EventHandler]
        public async Task CreateAsync(CreateTodoCommand command){
            await ValidateAsync(command.Dto.Title);
            var todo=command.Dto.Adapt<TodoEntity>();
            await _todoDbContext.Set<TodoEntity>().AddAsync(todo);
            await _todoDbContext.SaveChangesAsync();
        }

        [EventHandler]
        public async Task UpdateAsync(UpdateTodoCommand command){
            await ValidateAsync(command.Dto.Title,command.Id);
            var todo =await _todoDbContext.Set<TodoEntity>().AsNoTracking().FirstOrDefaultAsync(t=>t.Id==command.Id);
            if(todo==null){
                throw new UserFriendlyException("待办不存在");
            }
            command.Dto.Adapt(todo);
            _todoDbContext.Set<TodoEntity>().Update(todo);
            await _todoDbContext.SaveChangesAsync();
        }

        [EventHandler]
        public async Task DeleteAsync(DeleteTodoCommand command){
            var todo=await _todoDbContext.Set<TodoEntity>().AsNoTracking().FirstOrDefaultAsync(t=>t.Id==command.Id);
            if(todo==null){
                return ;
            }
            _todoDbContext.Set<TodoEntity>().Remove(todo);
            await _todoDbContext.SaveChangesAsync();
        }

        [EventHandler]
        public async Task DoneAsync(DoneTodoCommand command){
            var todo=await _todoDbContext.Set<TodoEntity>().AsNoTracking().FirstOrDefaultAsync(t=>t.Id==command.Id);
            if(todo==null){
                return;
            }
            todo.Done=command.Done;
            _todoDbContext.Set<TodoEntity>().Update(todo);
            await _todoDbContext.SaveChangesAsync();
        }

        private async Task ValidateAsync(string title,Guid? id=null){
            var todoExists=await _todoDbContext.Set<TodoEntity>().AnyAsync(t=>t.Title==title&&t.Id!=id);
            if(todoExists){
                throw new UserFriendlyException("待办已存在");
            }
        }


    }
}