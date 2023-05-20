using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleManagement.Entities;
using VehicleManagement.Helpers;
using VehicleManagement.Models;
using VehicleManagement.ViewModels;

namespace VehicleManagement.Controllers
{
    [Authorize(Roles = "Admin", AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class UsersController : Controller
    {
        private readonly MyContext _context;

        public UsersController(MyContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.User);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, User requestModel)
        {
            if (id != requestModel.Id)
            {
                return NotFound();
            }

            if (!User.IsInRole("Admin"))
            {
                return Redirect(Url.Action("Index"));
            }

            var user = _context.User.FirstOrDefault(a => a.Id == id);


            if (user.Username != requestModel.Username)
            {
                if (_context.User.Any(x => x.Username == requestModel.Username))
                {
                    ModelState.AddModelError("Username", "This username is already taken");
                    return View(requestModel);
                }
                user.Username = requestModel.Username;
            }

            if (user.Email != requestModel.Email)
            {
                if (_context.User.Any(x => x.Email == requestModel.Email))
                {
                    ModelState.AddModelError("Email", "This email is already taken");
                    return View(requestModel);
                }
                user.Email = requestModel.Email;
            }

            await _context.SaveChangesAsync();

            return Redirect(Url.Action("Index"));
        }
        public IActionResult Create()
        {




            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUserViewModel requestModel)
        {


            requestModel.user.Email = requestModel.user.Username;



            byte[] passwordHash, passwordSalt;
            AuthenticationHelper.CreatePasswordHash(requestModel.Password, out passwordHash, out passwordSalt);

            Entities.User user = new User();

            user.FirstName = requestModel.user.FirstName;
            user.LastName = requestModel.user.LastName;
            user.Email = requestModel.user.Email;
            user.Address = requestModel.user.Address;
            user.PhoneNumber = requestModel.user.PhoneNumber;

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.IsAdmin = requestModel.user.IsAdmin;

            user.CreatedDate = DateTime.Now;
            user.Username = requestModel.user.Username;
            user.Email = user.Username;

            _context.User.Add(user);
            await _context.SaveChangesAsync();

            return Redirect(Url.Action("Index"));
        }
        public async Task<IActionResult> ChangeUserPassword(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            ChangeUserPasswordModel model = new ChangeUserPasswordModel();
            model.user = user;
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeUserPassword(int id, ChangeUserPasswordModel requestModel)
        {
            if (id != requestModel.user.Id)
            {
                return NotFound();
            }

            if (!User.IsInRole("Admin"))
            {
                return Redirect(Url.Action("Index"));
            }

            var user = _context.User.FirstOrDefault(a => a.Id == id);

            byte[] passwordHash, passwordSalt;
            AuthenticationHelper.CreatePasswordHash(requestModel.NewPassword, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _context.SaveChangesAsync();

            return Redirect(Url.Action("Index"));
        }

        [HttpPost, ActionName("UpdateRoleForAdmin")]
        public IActionResult UpdateRoleForAdmin(UpdateRoleRequestModel model)
        {
            var user = _context.User.FirstOrDefault(a => a.Id == model.UserId);
            user.IsAdmin = model.IsChecked;
            _context.SaveChanges();
            return Ok();
        }

        [HttpPost, ActionName("UpdateRoleForAgent")]
        public IActionResult UpdateRoleForAgent(UpdateRoleRequestModel model)
        {
            var user = _context.User.FirstOrDefault(a => a.Id == model.UserId);
            user.IsEmployee = model.IsChecked;
            _context.SaveChanges();
            return Ok();
        }
    }
}