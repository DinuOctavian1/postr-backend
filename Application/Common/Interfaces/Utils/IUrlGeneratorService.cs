using Domain.Entities;

namespace Application.Common.Interfaces.Utils
{
    public interface IUrlGeneratorService
    {
        string GenerateConfirmationUrl(AppUser user, string emailToken);

        string GenerateResetPasswordUrl(string email, string token);
    }
}
