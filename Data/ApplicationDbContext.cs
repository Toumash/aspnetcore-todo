using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using aspnetcore_todo.Models;
using Microsoft.AspNetCore.Identity;

namespace aspnetcore_todo.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<aspnetcore_todo.Models.TodoItem> TodoItem { get; set; }
    }
}
