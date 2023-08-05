using Application.Common.Interfaces.ContentGenerator;
using Contracts.ContentGeneration;
using Domain.Model.ContentGenerator;
using ErrorOr;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class GenerateController : BaseApiController
    {
        private readonly IContentGenerationService _contentGenerationService;
        private readonly IMapper _mapper;

        public GenerateController(IContentGenerationService contentGenerationService, IMapper mapper)
        {
            _contentGenerationService = contentGenerationService;
            _mapper = mapper;
        }

        [HttpPost("post")]
        public async Task<ActionResult> GenerateText(PostGenerationRequest postRequest)
        {
            PostGeneratorModel model = _mapper.Map<PostGeneratorModel>(postRequest);

            ErrorOr<string> result = await _contentGenerationService.GenerateTextAsync(model);

            return result.Match<ActionResult>(
                    result => Ok(new PostGenerationResponse(result)),
                    errors => Problem(errors)
            );
        }
    }
}
