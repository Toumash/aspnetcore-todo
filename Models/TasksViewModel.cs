using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aspnetcore_todo.Models
{
    public class TasksViewModel
    {
        public IEnumerable<TodoItem> Tasks { get; set; }
        public TodoItem NewTask { get; set; }
    }
}
