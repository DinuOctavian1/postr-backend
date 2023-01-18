using Postr.Models;

namespace Postr.Services
{
    public interface IPostGeneratorService
    {
        Task<PostResponseModel> GeneratePost(string input);
    }
}
