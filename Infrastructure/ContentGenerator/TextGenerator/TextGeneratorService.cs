using Application.Common.Interfaces.ContentGenerator.TextGenerator;
using Domain.Model.ContentGenerator;
using ErrorOr;
using Infrastructure.ContentGenerator.Common;
using Microsoft.Extensions.Options;
using OpenAI.Interfaces;
using OpenAI.ObjectModels;
using OpenAI.ObjectModels.RequestModels;

namespace Infrastructure.ContentGenerator.TextGenerator
{
    public class TextGeneratorService : ITextGeneratorService
    {
        private readonly IOpenAIService _openAIService;
        private readonly PromtTemplate _promtTemplate;

        public TextGeneratorService(IOpenAIService openAIService, IOptions<PromtTemplate> promtTemplate)
        {
            _openAIService = openAIService;
            _promtTemplate = promtTemplate.Value;
        }

        public async Task<ErrorOr<string>> GenerateTextAsync(PostGeneratorModel model)
        {
            string prompt = _promtTemplate.GetSocialMediaPromt(model.PageCategories, model.PageName, model.SocialMediaPlatform, model.PostDescription);
            var completionResult = await _openAIService.Completions.CreateCompletion(new CompletionCreateRequest()
            {
                Prompt = prompt,
                Model = Models.TextDavinciV3,
                MaxTokens = 100
            });

            if (completionResult.Successful)
                return completionResult?.Choices.FirstOrDefault()?.Text;

            return Error.Failure(completionResult?.Error?.Code, completionResult?.Error?.Message);
        }
    }
}
