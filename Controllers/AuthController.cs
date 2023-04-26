using Microsoft.AspNetCore.Mvc;
using Postr.DTO;
using Postr.RequestModels;
using Postr.ResponseModels;
using Postr.Services;

namespace Postr.Controllers
{
    public class AuthController : BaseApiController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("signup")]
        public async Task<ActionResult> SignupUserAsync([FromBody] SignupDTO model)
        {
            if (ModelState.IsValid)
            {
                AuthResponse response = await _authService.RegisterUserAsync(model);

                if (response.IsSuccess)
                {
                    return Ok(response.Message);
                }

                return BadRequest(response.Message);
            }

            return BadRequest();
        }


        [HttpPost("confirm-email")]
        public async Task<ActionResult> ConfirmEmail([FromBody] EmailConfrimationRequestModel model)
        {
            if(ModelState.IsValid) 
            { 
                var result = await _authService.CofirmEmailAsync(model);
                if (result.IsSuccess)
                {
                    return Ok(result.Message);
                }

                return BadRequest(result.Message);
            }

            return BadRequest("Invalid payload");
        }

    }
}
