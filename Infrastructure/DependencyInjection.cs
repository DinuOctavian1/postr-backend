using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Utils;
using Infrastructure.Authentication;
using Infrastructure.Extensions;
using Infrastructure.Persistance;
using Infrastructure.UploadMedia;
using Infrastructure.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthServices(configuration);

            services.AddContentGeneratorServices();

            services.AddEmailServices(configuration);

            services.AddAzureBlob(configuration);

            services.AddTransient<IUrlGeneratorService, UrlGeneratorService>();
            services.AddTransient<ISecurityTokenService, SecurityTokenService>();

            services.AddDbContext<DataContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            return services;
        }

        /*private static IServiceCollection AddContentGenerator(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOpenAIService();
            services.AddTransient<ITextGeneratorService, TextGeneratorService>();
            services.AddTransient<IContentGenerationService, ContentGenerationService>();

            return services;
        }

        private static IServiceCollection AddOpenAI(this IServiceCollection services, IConfiguration configuration)
        {


            *//* services.Configure<OpenAISettings>(configuration.GetSection(OpenAISettings.SectionName));*//*


            return services;
        }

        private static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = new JwtSettings();
            configuration.Bind(JwtSettings.SectionName, jwtSettings);

            services.AddSingleton(Options.Create(jwtSettings));
            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

            services.ConfigureIdentityFramework();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                            .AddCookie()
                                .AddJwtBearer(options =>
                                {
                                    options.TokenValidationParameters = new TokenValidationParameters()
                                    {
                                        ValidateIssuer = true,
                                        ValidateAudience = true,
                                        ValidateLifetime = true,
                                        ValidateIssuerSigningKey = true,
                                        ValidIssuer = jwtSettings.Issuer,
                                        ValidAudience = jwtSettings.Audience,
                                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
                                    };

                                    options.Events = new JwtBearerEvents
                                    {
                                        OnMessageReceived = context =>
                                        {
                                            context.Token = context.Request.Cookies[jwtSettings.Name];
                                            return Task.CompletedTask;
                                        }
                                    };
                                });

            return services;
        }

        private static IServiceCollection ConfigureIdentityFramework(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<DataContext>()
               .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.SignIn.RequireConfirmedEmail = true;
                options.User.RequireUniqueEmail = true;

                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 5;
                options.Password.RequiredUniqueChars = 0;
            });

            return services;
        }

        private static IServiceCollection ConfigureEmailService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IEmailService, SendGridMailService>();
            services.Configure<EmailSettings>(configuration.GetSection(EmailSettings.SectionName));

            return services;
        }*/
    }
}