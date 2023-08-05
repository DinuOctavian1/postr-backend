using Domain.Model.ContentGenerator;
using ErrorOr;

namespace Application.Common.Interfaces.ContentGenerator.TextGenerator
{
    public interface ITextGeneratorService
    {
        Task<ErrorOr<string>> GenerateTextAsync(PostGeneratorModel model);
    }
}
