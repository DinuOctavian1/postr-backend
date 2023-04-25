namespace Postr.ResponseModels
{
    public class EmailConfrimationRequestModel
    {
        public string UserId { get; set; }
        public string Token { get; set; }
    }
}
