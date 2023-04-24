namespace Postr.ResponseModels
{
    public class AuthResponse
    {
        public bool IsSuccess { get; set; }
        public string Error { get; set; }
        public string Message { get; set; }
    }
}
