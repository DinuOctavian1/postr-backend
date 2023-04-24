namespace Postr.Configurations
{
    public static class EmailServiceConfig
    {
        public const string emailAddress = "contact@postr.io";
        
        public const string confirmationEmailSubject = "Confirm your email";
        public static string GetEmailConfirmationMessage(string url) => $"<h1>Welcome to Postr</h1<p>Confirm your email <a href='{url}'>Click here</a></p>";
    }
}
