namespace Infrastructure.ContentGenerator.Common
{
    public class PromtTemplate
    {
        public string GetSocialMediaPromt(string pageCategories,
                                          string pageName,
                                          string socialMediaPlatform,
                                          string postDescription) => $"As a social media manager, generate a post for {pageCategories} page categories on {pageName} page name"
                                                                     + $"using {socialMediaPlatform} social platform. The focus of the post should be to highlight {postDescription}.";
    }
}
