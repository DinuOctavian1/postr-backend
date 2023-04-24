namespace Postr.DTO
{
    public class SignupDTO : LoginDTO
    {
        public string Username { get; set; }
        public string ConfirmedPassword { get; set; }
    }
}
