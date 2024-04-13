namespace PseuSM.Models.Errors
{
    public class ErrorWithMessage : Error
    {
        public ErrorWithMessage(int statusCode, string message) : base(statusCode)
        {
            Message = message;
        }

        public string Message { get; set; } = string.Empty;
    }
}
