using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoWithAuth.Data;
using ToDoWithAuth.Models;

namespace ToDoWithAuth.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ApplicationDbContext context, UserManager<ApplicationUser> um)
        {
            _context = context;
            _userManager = um;
        }

        [HttpPost]
        public async Task<IActionResult> Index(string newToDoName)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var currentToDo = new ToDoModel{
                TaskName = newToDoName
            };

            currentToDo.UserID = user.Id;
            
            _context.ToDos.Add(currentToDo);
            _context.SaveChanges();

            return View(_context.ToDos.Where(w => w.UserID == user.Id).ToList());
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var applicationDbContext = _context.ToDos.Include(p => p.User).Where(w => w.UserID == user.Id);
            return View(await applicationDbContext.ToListAsync());
        }

        [HttpPost]
         public IActionResult Complete(int id)
        {
            var current = _context.ToDos.SingleOrDefault(m => m.ID == id);

            current.CompleteTask();
            _context.SaveChanges();

            return Redirect("Index");
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}