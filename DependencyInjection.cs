using Microsoft.EntityFrameworkCore;
using Postr.Data;
using Postr.Services.Implementation;
using Postr.Services;
using Postr.Middelware;

namespace Postr
{
    public static class DependencyInjection
    {
        public static IServiceCollection ConfigureDB(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<PostrDBContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            });

            return services;
        }

        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {
            services.AddTransient<IPostGeneratorService, OpenAIPostService>();
            services.AddTransient<IMailService, SendGridMailService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddTransient<ITokenService, JwtService>();
            services.AddScoped<JWTMiddleware>();

            return services;
        }
    }
}
