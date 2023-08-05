using Application.Common.Interfaces.UploadMedia;
using Infrastructure.UploadMedia.Azure;
using Infrastructure.UploadMedia.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.UploadMedia
{
    internal static class DependencyInjection
    {
        public static IServiceCollection AddAzureBlob(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AzureSettings>(configuration.GetSection(AzureSettings.SectionName));
            services.AddTransient<IAzureMediaService, AzureMediaService>();
            services.AddTransient<IUploadMediaService, UploadMediaService>();

            return services;
        }

    }
}
