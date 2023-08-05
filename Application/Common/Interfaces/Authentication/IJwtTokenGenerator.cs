using Domain.Entities;

namespace Application.Common.Interfaces.Authentication
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(AppUser user);

        string GetTokenName { get; }

    }
}
