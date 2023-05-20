using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using VehicleManagement.Entities;
using VehicleManagement.Helpers;
using VehicleManagement.Models;

namespace VehicleManagement.Controllers
{
    [Authorize]
    [Route("api/v1/auth")]
    [ApiController]
    public class AuthController : BaseController
    {
        // Just dummy text
        private readonly MyContext _context;
        private readonly Helpers.AppSettings _appSettings;

        public AuthController(MyContext context, IOptions<Helpers.AppSettings> appSettings)
        {
            _context = context;
            _appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Authenticate(LoginRequestModel requestModel)
        {
            string rtValue = "";
            //var user = _context.User.SingleOrDefault(x => x.Username == requestModel.email);
            var user = _context.User.Include(a => a.userRoles).SingleOrDefault(x => x.Username == requestModel.email);

            // check if username exists
            if (user == null)
            {
                return Ok(new
                {
                    Token = "",
                    login = false,
                    msg = "Username or password is incorrect"
                });
            }
              

            // check if password is correct
            if (!AuthenticationHelper.VerifyPasswordHash(requestModel.Password, user.PasswordHash, user.PasswordSalt))
            {
                return Ok(new
                {
                    Token = "",
                    login = false,
                    msg = "Username or password is incorrect"
                });
            }
               

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, user.Username.ToString()));
            //claims.Add(new Claim(ClaimTypes.Surname, user.employee.LastName.ToString()));
            foreach (var item in user.userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, _context.UserRole.SingleOrDefault(x => x.Id == item.RoleId).RoleName));
            }

            //var tokenDescriptor = new SecurityTokenDescriptor
            //{
            //    Subject = new ClaimsIdentity(new Claim[]
            //    {
            //        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            //        new Claim(ClaimTypes.Name, user.FirstName.ToString()),
            //        new Claim(ClaimTypes.Surname, user.LastName.ToString()),
            //        new Claim(ClaimTypes.Role, user.IsAdmin ? "Admin" : user.IsEmployee ? "Employee" : "User")
            //    }),
            //    Expires = DateTime.UtcNow.AddYears(1),
            //    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            //};

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddYears(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // SAVE FIREBASE TOKEN
            
            _context.User.Update(user);
            _context.SaveChanges();

            // return basic user info (without password) and token to store client side
            return Ok(new
            {
                Token = tokenString,
                login = true,
                msg="Success"
            });
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Create(RegisterRequestModel requestModel)
        {
            if (_context.User.Any(x => x.Username == requestModel.Username))
                return Error("Username \"" + requestModel.Username + "\" is already taken");

            if (_context.User.Any(x => x.Email == requestModel.Email))
                return Error("Email \"" + requestModel.Email + "\" is already taken");

            byte[] passwordHash, passwordSalt;
            AuthenticationHelper.CreatePasswordHash(requestModel.Password, out passwordHash, out passwordSalt);

            var user = new User
            {
                IsAdmin = false,
                //IsSubscribe = false,
                CreatedDate = DateTime.Now,

                Email = requestModel.Email,
                Username = requestModel.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,

            };

            _context.User.Add(user);
            await _context.SaveChangesAsync();

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Username.ToString()),
                    //new Claim(ClaimTypes.Surname, user.LastName.ToString()),
                    new Claim(ClaimTypes.Role, user.IsAdmin ? "Admin" : user.IsEmployee ? "Employee" : "User")
                }),
                Expires = DateTime.UtcNow.AddYears(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);



            return Ok(new
            {
                success = new
                {
                    user,
                    Token = tokenString

                }
            });
        }

    }
}