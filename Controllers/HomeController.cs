using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToDoWithAuth.Data;
using ToDoWithAuth.Models;

namespace ToDoWithAuth.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Index(string newToDoName)
        {
            var currentToDo = new ToDoModel{
                TaskName = newToDoName
            };
            
            _context.ToDos.Add(currentToDo);
            _context.SaveChanges();

            return View(_context.ToDos.ToList());
        }


        public IActionResult Index()
        {
            return View(_context.ToDos.ToList());
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