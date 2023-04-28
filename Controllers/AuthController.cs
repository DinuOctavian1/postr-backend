using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OpenAI.GPT3.ObjectModels.ResponseModels;
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
        public async Task<ActionResult> SignupUserAsync([FromBody] SignupDTO model)
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

                    result.Message = "Email confirmed successfully";
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


        [HttpGet("user")]
        public async Task<ActionResult> GetCurrentUser()
        {
            User user = HttpContext.Items["User"] as User;

            if(user == null)
                return Unauthorized();

            UserDTO userDto = _mapper.Map<UserDTO>(user);

            return Ok(userDto);
        }
    }
}
