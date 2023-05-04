namespace Postr.Configurations
{
    public class EmailConfig
    {
        public string EmailAddress { get; set; }
        public string ConfirmationEmailSubj { get; set; }
        public string ConfirmationEmailMess { get; set; }
        public string ResetPasswordEmailSubj { get; set; }
        public string ResetPasswordEmailMess { get; set; }
    }
}
