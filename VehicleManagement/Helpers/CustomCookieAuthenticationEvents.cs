using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using VehicleManagement.Entities;

namespace VehicleManagement.Helpers
{
    public class CustomCookieAuthenticationEvents : CookieAuthenticationEvents
    {
        private MyContext dbContext;

        public CustomCookieAuthenticationEvents(MyContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public override async Task ValidatePrincipal(CookieValidatePrincipalContext context)
        {
            var userPrincipal = context.Principal;
            var userId = userPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                context.RejectPrincipal();
                await context.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            }

            var user = dbContext.User.FirstOrDefault(a => a.Id == int.Parse(userId));

            if (user == null)
            {
                context.RejectPrincipal();
                await context.HttpContext.SignOutAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme);
            }
        }
    }
}
