namespace PseuSM.Models.Errors
{
    public class ErrorWithInnerException : ErrorWithMessage
    {
        public ErrorWithInnerException(int statusCode, string message, string innerExceptionMessage) : base(statusCode, message)
        {
            InnerExceptionMessage = innerExceptionMessage;
        }

        public string InnerExceptionMessage { get; set; } = string.Empty;
    }
}
