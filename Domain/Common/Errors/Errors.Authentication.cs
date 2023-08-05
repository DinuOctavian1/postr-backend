using ErrorOr;

namespace Domain.Common.Errors
{
    public static partial class Errors
    {
        public static class Authentication
        {
            public static Error InvalidCredentials = Error.Validation(code: "Auth.InvalidCred",
                                                                    description: "Invalid Credentials");

            public static Error EmailNotConfirmed = Error.Validation(code: "Auth.EmailNotConfirmed",
                                                                    description: "Your email is not confirmed");

            public static Error JwtTokenGenerationFailed = Error.Failure(code: "Auth.JwtTokenGenerationFailed",
                                                                         description: "Jwt token generation failed");

            public static Error InvalidEmailToken = Error.Validation(code: "Auth.InvalidEmailToken",
                                                                    description: "Invalid email token");

            public static Error EmailTokenNotGenerated = Error.Failure(code: "Auth.EmailTokenNotGenerated",
                                                                    description: "Email token not generated");

            public static Error UrlNotGenerated = Error.Failure(code: "Auth.UrlNotGenerated",
                                                                    description: "Url not generated");

            public static Error EmailNotSent = Error.Failure(code: "Auth.EmailNotSent",
                                                                    description: "Email not sent");

            public static Error UserNotFound = Error.NotFound(code: "Auth.UserNotFound",
                                                                    description: "User not found");

            public static Error TwoFactorAuthNotEnabled = Error.Failure(code:"Auth.TwoFactorAuthNotEnabled",
                                                                        description: "Two factor authentication not enabled");

            public static Error TwoFactorAuthGenerationFailure = Error.Failure(code: "Auth.TwoFactorAuthGenerationFailure",
                                                                               description: "Two factor authentication generation failure"); 
            public static Error TwoFactorAuthEmailFailure = Error.Failure(code: "Auth.TwoFactorAuthEmailFailure",
                                                                           description: "Two factor authentication email failure");

            public static Error TwoFactorAuthInvalidToken = Error.Validation(code: "Auth.TwoFactorAuthInvalidToken",
                                                                             description: "Two factor authentication invalid token");
        }
    }
}
