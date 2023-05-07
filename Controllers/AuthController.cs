using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Postr.DTO;
using Postr.Models;
using Postr.RequestModels;
using Postr.ResponseModels;
using Postr.Services;

namespace Postr.Controllers
{
    public class AuthController : BaseApiController
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public AuthController(IAuthService authService, IMapper mapper)
        {
            _authService = authService;
            _mapper = mapper;
        }

        [HttpPost("signup")]
        public async Task<ActionResult> SignupUser([FromBody] SignupDTO model)
        {
            if (ModelState.IsValid)
            {
                AuthResponse response = await _authService.RegisterUserAsync(model);

                if (response.IsSuccess)
                {
                    return Ok(response);
                }

                return BadRequest(response.Message);
            }

            return BadRequest();
        }


        [HttpPost("confirm-email")]
        public async Task<ActionResult> ConfirmEmail([FromBody] EmailConfirmationRequestModel model)
        {
            if(ModelState.IsValid) 
            { 
                var result = await _authService.CofirmEmailAsync(model);
                if (result.IsSuccess)
                {
                    Response.Cookies.Append("jwt", result.Message, new CookieOptions
                    {
                        Secure = true,
                        HttpOnly = true,
                        SameSite = SameSiteMode.None,
                    });

                    result.Message = "Email confirmed successfully! You are now logged in!";
                    return Ok(result);
                }

                return BadRequest(result.Message);
            }

            return BadRequest(
                new AuthResponse
                {
                    IsSuccess = false,
                    Message = "Invalid payload"
                });
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginDTO model)
        {
            if(ModelState.IsValid)
            {
                AuthResponse response = await _authService.LoginAsync(model);
                if (response.IsSuccess)
                {
                    Response.Cookies.Append("jwt", response.Message, new CookieOptions
                    {
                        Secure = true,
                        HttpOnly = true,
                        SameSite = SameSiteMode.None,
                    });

                    response.Message = "Login successful";

                    return Ok(response);

                }
                return BadRequest(response.Message);
            };

            return BadRequest("Login data is not correct");


            
        }

        [HttpGet("logout")]
        public IActionResult Logout()
        {

            Response.Cookies.Delete("jwt", new CookieOptions
            {
                Secure = true,
                HttpOnly = true,
                SameSite = SameSiteMode.None
            });

            
            return Ok(new AuthResponse
            {
                IsSuccess = true,
                Message = "Logout successfull!",
            });
        }


        [HttpGet("user")]
        public async Task<ActionResult> GetCurrentUser()
        {
            User user = HttpContext.Items["User"] as User;

            if(user == null)
                return Unauthorized();

            UserDTO userDto = _mapper.Map<UserDTO>(user);

            AuthResponse response = new AuthResponse
            {
                IsSuccess = true,
                Message = "User found",
                Data = userDto
            };

            return Ok(response);
        }
    }
}
