using Postr.Services;

namespace Postr.Middelware
{
    public class JWTMiddleware : IMiddleware
    {
        private readonly IAuthService _authService;

        public JWTMiddleware(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var jwt = context.Request.Cookies["jwt"];

            if (jwt != null)
            {
                var user = await _authService.GetUserfromTokenAsync(jwt);
                if (user != null)
                {
                    context.Items["User"] = user;
                }
            }

            await next(context);
        }
    }

       
}
