using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using aspnetcore_todo.Data;
using aspnetcore_todo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace aspnetcore_todo.Controllers
{
    [Authorize]
    public class TasksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserManager<IdentityUser> UserManager { get; }

        public TasksController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            UserManager = userManager;
        }

        // GET: Tasks
        public async Task<IActionResult> Index()
        {
            var id = UserManager.GetUserId(this.User);
            var vm = new TasksViewModel()
            {
                Tasks = await _context.TodoItem.Where(task => task.UserId == id).ToListAsync()
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Index(TasksViewModel todoItem)
        {
            if (ModelState.IsValid)
            {
                var newTodoItem = todoItem.NewTask;
                newTodoItem.Id = Guid.NewGuid();
                newTodoItem.UserId = UserManager.GetUserId(this.User);
                _context.Add(newTodoItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var id = UserManager.GetUserId(this.User);
            var vm = new TasksViewModel()
            {
                Tasks = await _context.TodoItem.Where(task => task.UserId == id).ToListAsync(),
                NewTask = todoItem.NewTask
            };
            return View(vm);
        }
        // GET: Tasks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tasks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TodoItem todoItem)
        {
            if (ModelState.IsValid)
            {
                todoItem.Id = Guid.NewGuid();
                todoItem.UserId = UserManager.GetUserId(this.User);
                _context.Add(todoItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(todoItem);
        }

        // POST: Tasks/Complete/5
        [HttpGet]
        public async Task<IActionResult> Complete(Guid id)
        {
            var todoItem = await _context.TodoItem.FindAsync(id);
            _context.TodoItem.Remove(todoItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TodoItemExists(Guid id)
        {
            return _context.TodoItem.Any(e => e.Id == id);
        }
    }
}
