using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using VehicleManagement.Entities;
using VehicleManagement.Helpers;
using VehicleManagement.ViewModels;

namespace VehicleManagement.Controllers
{
    public class DashboardController : BaseController
    {
        //[Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        public IActionResult Index()
        {
            return View();
        }
    }
}