namespace Postr.ResponseModels
{
    public class PostResponseModel
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = "";
        public IEnumerable<string>? Errors { get; set; }

    }
}
