using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Warehouse.DB.Contexts;
using Warehouse.Infrastructure;
using Warehouse.Models;

namespace Warehouse.Controllers
{
    [SessionExpireFilter]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly WarehouseDbContext _context;

        public HomeController(ILogger<HomeController> logger, WarehouseDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            //_context.Employee.Add(new DB.Entities.Employee
            //{
            //    Email = "ssss",
            //    EmployeeName = "ssss",
            //    Skill = "ssss"
            //});
            //_context.SaveChanges();

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
    }
}
