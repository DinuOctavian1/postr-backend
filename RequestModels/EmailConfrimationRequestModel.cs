namespace Postr.RequestModels
{
    public class EmailConfirmationRequestModel
    {
        public string UserId { get; set; }
        public string Token { get; set; }
    }
}
