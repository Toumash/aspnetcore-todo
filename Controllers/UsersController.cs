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
    [Authorize(Roles = "admin")]
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserManager<IdentityUser> UserManager { get; }

        public UsersController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            UserManager = userManager;
        }

        // GET: Tasks
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }

        public IActionResult ResetPassword(string id)
        {
            return View(new ResetPasswordViewModel() { UserId = id });
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel vm, [FromServices] UserManager<IdentityUser> userManager)
        {
            if (this.ModelState.IsValid)
            {
                var user = await userManager.FindByIdAsync(vm.UserId);
                var result = await userManager.ChangePasswordAsync(user, vm.oldPassword, vm.newPassword);
                if (result.Succeeded)
                    return RedirectToAction(nameof(Index));
                else
                    ModelState.AddModelError("", string.Join(",", result.Errors.Select(e => e.Description)));
                return View(vm);
            }
            return View(vm);
        }

        // GET: Tasks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await UserManager.FindByIdAsync(id);
            await UserManager.DeleteAsync(user);
            return RedirectToAction(nameof(Index));
        }
    }
}
