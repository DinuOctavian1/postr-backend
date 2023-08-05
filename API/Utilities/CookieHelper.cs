namespace API.Utilities
{
    public static class CookieHelper
    {
        public static CookieOptions GetSecureCookieOptions()
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict
            };

            return cookieOptions;
        }
    }
}
