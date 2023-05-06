using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasaTodoApp.Contracts
{
    public class TodoCreateUpdateDto
    {
        public string Title { get; set; }=null!;
    }
}