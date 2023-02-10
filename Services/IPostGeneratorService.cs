using Postr.RequestModels;
using Postr.ResponseModels;

namespace Postr.Services
{
    public interface IPostGeneratorService
    {
        Task<PostResponseModel> GeneratePostAsync(PostRequestModel model);
    }
}
