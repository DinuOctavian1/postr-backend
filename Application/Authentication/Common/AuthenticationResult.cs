using Domain.Entities;

namespace Application.Authentication.Common
{
    public record AuthenticationResult
    {
        public string Message { get; set; }
        public AppUser User { get; set; }
        public string Token { get; set; }
    }
}
