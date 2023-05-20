using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using VehicleManagement.Entities;
using VehicleManagement.Models;
using VehicleManagement.ViewModels;

namespace VehicleManagement.Controllers
{
    public class HomeController : BaseController
    {
        private readonly MyContext _context;

        public HomeController(MyContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            //return RedirectToAction("Index", "Login");
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
