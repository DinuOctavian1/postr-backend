using OpenAI.GPT3;
using OpenAI.GPT3.Interfaces;
using OpenAI.GPT3.Managers;
using OpenAI.GPT3.ObjectModels.RequestModels;
using Postr.RequestModels;
using Postr.ResponseModels;

namespace Postr.Services.Implementation
{
    public class OpenAIPostService: IPostGeneratorService
    {
        private readonly IConfiguration _configuration;
        private readonly IOpenAIService _openAIService;
        private const string OpenAIModelID = "text-babbage-001";

        public OpenAIPostService(IConfiguration configuration)
        {
            _configuration = configuration;
            _openAIService = new OpenAIService(new OpenAiOptions()
            {
                ApiKey = _configuration["OpenAIApiKey"]
            });
            _openAIService.SetDefaultModelId(OpenAIModelID);
        }

        public async Task<PostResponseModel> GeneratePostAsync(PostRequestModel model)
        {
            var completionResult = await _openAIService.Completions.CreateCompletion(new CompletionCreateRequest()
            {
                Prompt = "Generate a post on the" + model.SocialPlatform + " about " + model.PostDescription,
                MaxTokens = 100,
            });

            if (completionResult.Successful)
            {
                return new PostResponseModel
                {
                    IsSuccess = true,
                    Message = completionResult?.Choices.FirstOrDefault()?.Text
                };
                //Console.WriteLine(completionResult.Choices.FirstOrDefault());
            }
            else
            {
                return new PostResponseModel
                {
                    IsSuccess = false,
                    Errors = new List<string>() { completionResult?.Error?.Message ?? "Unknown error!"}
                };
                //Console.WriteLine($"{completionResult.Error.Code}: {completionResult.Error.Message}");
            }
        }
    }
}
