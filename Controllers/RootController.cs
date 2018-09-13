using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.MicrosoftAccount;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DEMO_AzureSignalRService.Controllers
{
    [Route("/")]
    public class RootController : Controller
    {
        [HttpGet("login")]
        public async Task<IActionResult> Login()
        {
            // IF: User is not already authenticated.
            if (User.Identity.IsAuthenticated == false)
            {
                return Challenge(MicrosoftAccountDefaults.AuthenticationScheme);
            }

            // Append cookie to response that holds username information.
            HttpContext.Response.Cookies.Append("chat_user", User.Identity.Name);
            await HttpContext.SignInAsync(User);
            return Redirect("/");
        }
    }
}
