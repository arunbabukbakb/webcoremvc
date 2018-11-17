using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using webcoremvc.Models;
using Microsoft.Extensions.Configuration;

namespace webcoremvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration configuration;
        public HomeController(IConfiguration config)
        {
            this.configuration = config;
        }

        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult UserLogin(UsersModel usermodel)
        {
            UserMethods objhome = new UserMethods();
           
            //usermodel.username = usermodel.username;
            //usermodel.password = usermodel.password;
            bool result = objhome.ValidateLogin(usermodel);
            if (result == true)
            {                
                ViewBag.Title = "Eduxpert School";
                return View("Index");
            }
            else
            {
                ViewBag.message = "Invalid Credentials.";
                return View("Login");
            }

        }

        public ActionResult GetMenuList()
        {
            SiteMenuModel objmenu = new SiteMenuModel();
            List<sitemenulist> menulist = new List<sitemenulist>();
            menulist = objmenu.GetMenuList();
            return PartialView("_SiteMenuPartial", menulist);
        }

        public IActionResult Index()
        {
            string constring = configuration.GetConnectionString("schoolconnectionstring");
            return View();
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

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult GeneralForms()
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
