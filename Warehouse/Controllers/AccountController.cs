using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Warehouse.DB.Contexts;
using Warehouse.DB.Entities;
using Warehouse.Infrastructure;

namespace Warehouse.Controllers
{
    public class AccountController : Controller
    {
        private readonly WarehouseDbContext _context;

        public AccountController(WarehouseDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(User model)
        {
            if (!string.IsNullOrEmpty(model.UserName)
                && !string.IsNullOrEmpty(model.Password)
                )
            {
                User user = _context.User
                                   .Where(u => u.UserName == model.UserName && u.Password == model.Password)
                                   .FirstOrDefault();
                if (user != null)
                {
                    HttpContext.Session.Set<string>("FullName", user.FullName.Replace("\"", ""));
                    HttpContext.Session.Set<string>("UserName", user.UserName.Replace("\"", ""));
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData[key: "ErrorMsg"] =   "Invalid User Name or Password";
                    return View(new User());
                }
            }
            else
            {
                TempData[key: "ErrorMsg"] = "Kindly enter username and password";
                return View(model);
            }
        }
        public ActionResult LogOff()
        {
            HttpContext.Session.Set<string>("FullName", string.Empty);
            HttpContext.Session.Set<string>("UserName", string.Empty);
            return View("login");
        }
    }
}
