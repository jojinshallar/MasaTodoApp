using System;
using System.Linq;
using System.Threading.Tasks;
using MasaTodoApp.Contracts;
using MasaTodoApp.Web.ApiCallers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace MasaTodoApp.Web.Pages
{
    public partial class Index
    {
        [Inject]
        TodoCaller TodoCaller { get; set; }=null!;

        string _newTodo = string.Empty;
        string _updateTodo = string.Empty;

        List<TodoGetListDto> _tasks = new();

        private int CompletedTasks => _tasks.Count(t => t.Done);


        private float Progress => _tasks.Count <= 0 ? 0 : (CompletedTasks * 100f) / _tasks.Count;
        private int RemainingTasks => _tasks.Count - CompletedTasks;

        Guid? editorTodoId;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            await LoadListDataAsync();
        }

        async Task LoadListDataAsync(){
            var result=await TodoCaller.GetListAsync();
            _tasks=result;
        }

        async Task OnEnterKeyDown(KeyboardEventArgs eventArgs){
            if(eventArgs.Key=="Enter"){
                await Create();
            }
        }

        async Task Create(){
            await TodoCaller.CreateAsync(new TodoCreateUpdateDto(){Title=_newTodo});
            await LoadListDataAsync();
            _newTodo=string.Empty;
        }

        async Task Update(TodoGetListDto task){
            await TodoCaller.UpdateAsync(task.Id,new TodoCreateUpdateDto(){Title=_updateTodo});
            await LoadListDataAsync();
            //_updateTodo=string.Empty;
            editorTodoId=null;
        }

        async Task Delete(Guid id){
            await TodoCaller.DeleteAsync(id);
            await LoadListDataAsync();
            editorTodoId=null;
        }

        async Task Done(Guid id,bool done){
            await TodoCaller.DoneAsync(id,done);
            await LoadListDataAsync();
        }
    }
}