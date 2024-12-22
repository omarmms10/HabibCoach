using HabibCoach.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace HabibCoach.Controllers
{
    public class ClientsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ClientsController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(string searchQuery = null, string level = null)
        {
            var clients = _userManager.Users.Include(u => u.Programe).AsQueryable();

            // Apply search filter by username if provided
            if (!string.IsNullOrEmpty(searchQuery))
            {
                clients = clients.Where(u => u.UserName.StartsWith(searchQuery));
            }

            // Apply filter by training level if provided
            if (!string.IsNullOrEmpty(level))
            {
                clients = clients.Where(u => u.Programe != null && u.Programe.Level == level);
            }

            ViewData["SearchQuery"] = searchQuery;
            ViewData["SelectedLevel"] = level;

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                // Return only the partial view if it's an AJAX request
                return PartialView("_ClientList", await clients.ToListAsync());
            }

            return View(await clients.ToListAsync());
        }
    }
}
