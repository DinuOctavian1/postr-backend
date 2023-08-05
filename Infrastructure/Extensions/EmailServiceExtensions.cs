using Application.Common.Interfaces.Email;
using Infrastructure.Mail;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions
{
    internal static class EmailServiceExtensions
    {
        public static IServiceCollection AddEmailServices(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddTransient<IEmailService, SendGridMailService>();
            services.Configure<EmailSettings>(configuration.GetSection(EmailSettings.SectionName));

            return services;

        }
    }
}
