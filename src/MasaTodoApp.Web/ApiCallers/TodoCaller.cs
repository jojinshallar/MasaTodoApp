using System.Security.Cryptography.X509Certificates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Masa.Contrib.Service.Caller.HttpClient;
using Microsoft.Extensions.Options;
using MasaTodoApp.Contracts;

namespace MasaTodoApp.Web.ApiCallers
{
    public class TodoCaller : HttpClientCallerBase
    {
        protected override string BaseAddress { get; set; }
        public TodoCaller(IOptions<TodoServiceOptions> options)
        {
            BaseAddress = options.Value.BaseAddress;
            Prefix = "/api/v1/todoes";
        }

        public async Task<List<TodoGetListDto>> GetListAsync()
        {
            var result = await Caller.GetAsync<List<TodoGetListDto>>($"list");
            return result ?? new();
        }

        public async Task CreateAsync(TodoCreateUpdateDto dto)
        {
            await Caller.PostAsync($"", dto);
        }

        public async Task UpdateAsync(Guid id, TodoCreateUpdateDto dto)
        {
            await Caller.PutAsync($"{id}", dto);
        }

        public async Task DeleteAsync(Guid id)
        {
            await Caller.DeleteAsync($"{id}", null);
        }

        public async Task DoneAsync(Guid id, bool done)
        {
            await Caller.PostAsync($"done?id={id}&done={done}", null);
        }
    }
}