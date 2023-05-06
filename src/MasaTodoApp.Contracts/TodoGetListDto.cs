using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasaTodoApp.Contracts
{
    public class TodoGetListDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }=null!;
        public bool Done { get; set; }
    }
}