using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReversiMvcApp.Models;
using System.Diagnostics;
using System.Security.Claims;
using ReversiMvcApp.Data;

namespace ReversiMvcApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ReversiDbContext _context;

        public HomeController(ILogger<HomeController> logger, ReversiDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [Authorize]
        public IActionResult Index()
        {
            ClaimsPrincipal currentUser = this.User;
            var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;

            var speler = _context.Spelers.Find(currentUserID);

            if (speler == null)
            {
                speler = new Speler
                {
                    Guid = currentUserID,
                    Naam = currentUser.Identity.Name,
                    AantalGewonnen = 0,
                    AantalVerloren = 0,
                    AantalGelijk = 0
                };
                _context.Spelers.Add(speler);
                _context.SaveChanges();
            }

            return View();
        }

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}