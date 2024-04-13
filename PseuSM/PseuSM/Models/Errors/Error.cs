namespace PseuSM.Models.Errors
{
    public class Error
    {
        public Error(int statusCode)
        {
            StatusCode = statusCode;
        }

        public int StatusCode { get; set; }
    }
}
