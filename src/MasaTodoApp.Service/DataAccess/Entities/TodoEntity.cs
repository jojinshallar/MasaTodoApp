using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasaTodoApp.Service.DataAccess.Entities
{
    public class TodoEntity : Entity<Guid>
    {
        public string Title { get; set; } = null!;
        public bool Done { get; set; }
    }
}