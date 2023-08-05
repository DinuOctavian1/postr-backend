namespace Contracts.ContentGeneration
{
    public record PostGenerationRequest(
        string PostDescription,
        string SocialMediaPlatform,
        string PageName,
        string PageCategories
        );
}
