namespace Infrastructure.Mail
{
    public class EmailTemplates
    {
        public string TwoFactorAuthSubject => "Two Factor Authentication Code";
        public  string RegistrationConfirmationSubject => "Registration Confirmation";
        public  string ForgotPasswordSubject => "Forgot Password";

        public string GetRegistrationConfirmationBody(string username, string activationLink)
        {
            return $"<h2>Dear {username}<h2>" +
                $"<p>Thank you for registering. Please click the following link to activate your account: " +
                $"<a href={activationLink}>Click here</a></p>";
        }

        public string GetForgotPasswordBody(string url)
        {
            return $"<h1>Follow the instruction to reset your password</h1>" +
                $"<p>Tap to reset your password <a href='{url}'>Click here</a></p>";
        }

        public string Get2FABody(string token)
        {
            return $"<h1>Two Factor Authentication Code</h1>" +
                $"<p>Your two factor authentication code is: {token}</p>";
        }
    }
}
