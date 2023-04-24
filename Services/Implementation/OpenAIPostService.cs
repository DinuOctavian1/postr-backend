using OpenAI.GPT3;
using OpenAI.GPT3.Interfaces;
using OpenAI.GPT3.Managers;
using OpenAI.GPT3.ObjectModels.RequestModels;
using Postr.Models;

namespace Postr.Services.Implementation
{
    public class OpenAIPostService: IPostGeneratorService
    {
        private readonly IConfiguration _configuration;
        private readonly IOpenAIService _openAIService;
        private const string OpenAIModelID = "text-davinci-003";
        //private const string OpenAIModelID = "gpt-3.5-turbo";
        public OpenAIPostService(IConfiguration configuration)
        {
            _configuration = configuration;
            _openAIService = new OpenAIService(new OpenAiOptions()
            {
                ApiKey = _configuration["OpenAIApiKey"]
            });
            _openAIService.SetDefaultModelId(OpenAIModelID);
        }

        public async Task<PostResponseModel> GeneratePost(string input)
        {
            var completionResult = await _openAIService.Completions.CreateCompletion(new CompletionCreateRequest()
            {
                Prompt = input,
                MaxTokens = 20
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
                    Errors = new List<string>() { completionResult?.Error?.Message ?? "Unknown error!" }
                };
                //Console.WriteLine($"{completionResult.Error.Code}: {completionResult.Error.Message}");
            }
        }

       
    }
}
