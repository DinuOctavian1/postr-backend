using Application.Common.Interfaces.ContentGenerator.TextGenerator;
using Application.Common.Interfaces.ContentGenerator;
using Infrastructure.ContentGenerator.TextGenerator;
using Microsoft.Extensions.DependencyInjection;
using OpenAI.Extensions;
using Infrastructure.ContentGenerator;

namespace Infrastructure.Extensions
{
    internal static class ContentGeneratorExtensions
    {
        public static IServiceCollection AddContentGeneratorServices(this IServiceCollection services) 
        {
            services.AddOpenAIService();
            services.AddTransient<ITextGeneratorService, TextGeneratorService>();
            services.AddTransient<IContentGenerationService, ContentGenerationService>();

            return services;
        }
    }
}
