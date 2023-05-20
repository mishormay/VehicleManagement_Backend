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
    [Authorize(Roles = "Admin,Agent", AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class SettingsController : BaseController
    {
        public static readonly int DEFAULT_RECORD_ID = 1;
        private readonly MyContext _context;

        public SettingsController(MyContext context)
        {
            _context = context;
        }

        // GET: Settings
        public async Task<IActionResult> Index()
        {
            var appSettings = _context.AppSettings.SingleOrDefault(a => a.Id == DEFAULT_RECORD_ID);
            if (appSettings == null)
            {
                appSettings = new Entities.AppSettings
                {
                    Id = 1,
                    UpdatedDate = DateTime.Now,
                    CreatedDate = DateTime.Now
                };
                _context.AppSettings.Add(appSettings);
                await _context.SaveChangesAsync();
            }

            var user = _context.User.FirstOrDefault(a => a.Id == UserId);

            return View(new SettingsViewModel
            {
                AppSettings = appSettings,
                //FirstName = user.employee.FirstName,
                //LastName = user.employee.LastName,
                //PhoneNumber = user.employee.CellNo,
                Email = user.Email,
                //Address = user.employee.AddressLine1 + " " + user.employee.AddressLine2,
                Latitude = 0,
                Longitude = 0,
                Username = user.Username
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(SettingsViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var appSettings = viewModel.AppSettings;
                if (appSettings != null)
                {
                    appSettings.Id = DEFAULT_RECORD_ID;
                    appSettings.UpdatedDate = DateTime.Now;
                    _context.Update(viewModel.AppSettings);
                }

                var user = _context.User.FirstOrDefault(a => a.Id == UserId);


                if (user.Username != viewModel.Username)
                {
                    if (_context.User.Any(x => x.Username == viewModel.Username))
                    {
                        ModelState.AddModelError("Username", "This username is already taken");
                        return View(viewModel);
                    }
                    user.Username = viewModel.Username;
                }

                if (user.Email != viewModel.Email)
                {
                    if (_context.User.Any(x => x.Email == viewModel.Email))
                    {
                        ModelState.AddModelError("Email", "This email is already taken");
                        return View(viewModel);
                    }
                    user.Email = viewModel.Email;
                }

                if (!string.IsNullOrEmpty(viewModel.UserNewPassword) && !string.IsNullOrEmpty(viewModel.UserNewPassword))
                {
                    byte[] passwordHash, passwordSalt;
                    AuthenticationHelper.CreatePasswordHash(viewModel.UserNewPassword, out passwordHash, out passwordSalt);
                    user.PasswordHash = passwordHash;
                    user.PasswordSalt = passwordSalt;
                }

                await _context.SaveChangesAsync();

                if (appSettings != null && !string.IsNullOrEmpty(appSettings.HeaderImages))
                    appSettings.HeaderImages.Split(",").ToList().ForEach(a => Helpers.FileHelper.moveCacheToImages(a));
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
