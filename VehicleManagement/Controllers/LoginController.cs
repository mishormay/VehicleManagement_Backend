using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Threading.Tasks;
using VehicleManagement.Entities;
using VehicleManagement.Helpers;
using VehicleManagement.Models;
using VehicleManagement.ViewModels;

namespace VehicleManagement.Controllers
{
    public class LoginController : Controller
    {
        private readonly MyContext _context;
        private readonly IOptions<Helpers.AppSettings> _options;

        public LoginController(MyContext context, IOptions<Helpers.AppSettings> options)
        {
            _context = context;
            _options = options;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            var viewModel = new LoginViewModel();
            if (Request.Query.ContainsKey("message"))
            {
                viewModel.Message = Request.Query["message"];
            }
            return View(viewModel);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel viewModel)
        {
            return RedirectToAction("Index", "Home");
            //var user = _context.User.SingleOrDefault(x => x.Username == viewModel.Username);
            var user = _context.User.Include(a => a.userRoles).SingleOrDefault(x => x.Username == viewModel.Username);

            // check if username existsDp
            if (user == null ||
                !AuthenticationHelper.VerifyPasswordHash(viewModel.Password, user.PasswordHash, user.PasswordSalt))
            {
                viewModel.Message = "Username or password is incorrect";
                return View(viewModel);
            }

            //var claims = new List<Claim>
            //{
            //    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            //    new Claim(ClaimTypes.Name, user.Username.ToString()),
            //    new Claim(ClaimTypes.Surname, user.LastName.ToString()),
            //    new Claim(ClaimTypes.Role, user.IsAdmin ? "Admin" : user.IsEmployee? "Employee" : "User")
            //};

            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, user.Username.ToString()));
            //claims.Add(new Claim(ClaimTypes.Surname, user.employee.LastName.ToString()));
            foreach (var item in user.userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, _context.UserRole.SingleOrDefault(x => x.Id == item.RoleId).RoleName));
            }

            var authProperties = new AuthenticationProperties();

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            return RedirectToAction("Index", "Dashboard");
        }


        [AllowAnonymous]
        public IActionResult PasswordReset(string email, string token)
        {
            return View(new PasswordResetRequestModel
            {
                Email = email,
                Token = token
            });
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult PasswordReset(PasswordResetRequestModel requestModel)
        {
            if (string.IsNullOrEmpty(requestModel.Token))
            {
                ViewData["ErrorMessage"] = "Password Reset Token not found!";
                return View(requestModel);
            }

            var passwordReset = _context.PasswordReset.AsNoTracking().FirstOrDefault(a => a.Token == requestModel.Token);
            if (passwordReset == null || passwordReset.Email != requestModel.Email)
            {
                ViewData["ErrorMessage"] = "User not found!";
                return View(requestModel);
            }

            var user = _context.User.FirstOrDefault(a => a.Email == requestModel.Email);

            if (user == null)
            {
                ViewData["ErrorMessage"] = "Email not found!";
                return View(requestModel);
            }

            byte[] passwordHash, passwordSalt;
            AuthenticationHelper.CreatePasswordHash(requestModel.Password, out passwordHash, out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _context.PasswordReset.RemoveRange(_context.PasswordReset.Where(a => a.Email == requestModel.Email));
            _context.SaveChanges();

            return Redirect("~/Login");
        }

        [AllowAnonymous]
        public IActionResult PasswordForgot()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult PasswordForgot(string email)
        {
            if (!_context.User.Any(a => a.Email == email))
            {
                ViewData["Email"] = email;
                ViewData["ErrorMessage"] = "Email not found!";
                return View();
            }
            var token = Guid.NewGuid().ToString();

            var path = Url.Action("PasswordReset", "Login", new { email, token });
            string baseUrl = string.Format("{0}://{1}{2}", Request.Scheme, Request.Host, path);

            _context.PasswordReset.Add(new PasswordReset
            {
                Email = email,
                Token = token,
                CreatedDate = DateTime.Now
            });
            _context.SaveChanges();

            using (var message = new MailMessage())
            {
                message.To.Add(new MailAddress(email));
                message.From = new MailAddress(_options.Value.MailAddress);
                message.Subject = "Email Reset";
                message.Body = "<a href=\"" + baseUrl + "\">Reset Password Link</a>";
                message.IsBodyHtml = true;

                using (var client = new SmtpClient(_options.Value.MailHost))
                {
                    client.Port = _options.Value.MailPort;
                    client.Credentials = new NetworkCredential(_options.Value.MailAddress, _options.Value.MailPassword);
                    client.EnableSsl = true;
                    client.Send(message);
                }
            }
            ViewData["SuccessMessage"] = "successed send mail";
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> AccessDenied()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/Login?message=Access Denied");
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("~/Login");
        }
    }
}