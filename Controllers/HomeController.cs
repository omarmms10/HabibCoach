using HabibCoach.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace HabibCoach.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public HomeController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public async Task<IActionResult> Search(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                return Json(new List<object>()); // Return empty list if query is empty
            }

            var clients = await _userManager.Users
                .Where(u => u.UserName.Contains(query) || u.Email.Contains(query))
                .Select(u => new {
                    type = "client",
                    id = u.Id,
                    name = u.UserName,
                    email = u.Email
                })
                .ToListAsync();

            var programs = await _context.Programes
                .Where(p => p.WorkoutProgramTitle.Contains(query))
                .Select(p => new {
                    type = "program",
                    id = p.ProgramId,
                    title = p.WorkoutProgramTitle
                })
                .ToListAsync();

            var results = clients.Concat<object>(programs); // Combine both results

            return Json(results); // Return combined results as JSON
        }
    }
}

