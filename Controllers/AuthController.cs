using Microsoft.AspNetCore.Mvc;
using Postr.DTO;
using Postr.ResponseModels;
using Postr.Services.Implementation;

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

                return BadRequest(response.Error);
            }

            return BadRequest();
        }
    }
}
