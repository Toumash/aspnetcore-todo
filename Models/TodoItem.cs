using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace aspnetcore_todo.Models
{
    public class TodoItem
    {
        public Guid Id { get; set; }
        [Required]
        [Display(Name = "Tytuł")]
        public string Title { get; set; }
        public bool Done { get; set; }
        public DateTime Created { get; set; }
        public string UserId { get; internal set; }
    }
}