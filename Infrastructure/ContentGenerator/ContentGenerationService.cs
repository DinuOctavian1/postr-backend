using Application.Common.Interfaces.ContentGenerator;
using Application.Common.Interfaces.ContentGenerator.TextGenerator;
using Domain.Model.ContentGenerator;
using ErrorOr;

namespace Infrastructure.ContentGenerator
{
    public class ContentGenerationService : IContentGenerationService
    {
        private readonly ITextGeneratorService _textGeneratorService;

        public ContentGenerationService(ITextGeneratorService textGeneratorService)
        {
            _textGeneratorService = textGeneratorService;
        }

        public async Task<ErrorOr<string>> GenerateTextAsync(PostGeneratorModel model)
        {
            return await _textGeneratorService.GenerateTextAsync(model);
        }



    }
}
