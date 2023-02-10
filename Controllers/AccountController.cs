using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Postr.Models;
using System.Security.Claims;

namespace Postr.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public AccountController(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet("facebook-login")]
        public IActionResult FacebookLogin()
        {
            var redirect = Url.Action("FacebookCallback", "Account");
            var state = Guid.NewGuid().ToString();
            var authenticationProperties = new AuthenticationProperties
            {
                RedirectUri = redirect,
                Items = { { "state", state } }
            };
            return Challenge(authenticationProperties, "Facebook");
        }

        [HttpGet("facebook-callback")]
        public async Task<IActionResult> FacebookCallback()
        {


            var authenticateResult = await HttpContext.AuthenticateAsync("Facebook");
            var user = authenticateResult.Principal;
            var externalUserId = user.FindFirstValue(ClaimTypes.NameIdentifier);
            var email = user.FindFirstValue(ClaimTypes.Email);
            var name = user.FindFirstValue(ClaimTypes.Name);

            var identityUser = await _userManager.FindByLoginAsync("facebook", externalUserId);
            if (identityUser == null)
            {
                // Create a new user if one does not exist
                identityUser = new User
                {
                    Email = email,
                    UserName = email
                };
                var identityResult = await _userManager.CreateAsync(identityUser);
                if (!identityResult.Succeeded)
                {
                    // Handle errors
                    return BadRequest(identityResult.Errors);
                }

                // Add the external login information to the user
                identityResult = await _userManager.AddLoginAsync(identityUser, new UserLoginInfo("facebook", externalUserId, name));
                if (!identityResult.Succeeded)
                {
                    // Handle errors
                    return BadRequest(identityResult.Errors);
                }
            }
            return Ok(new { access_token = "" });
        }

    }
}
