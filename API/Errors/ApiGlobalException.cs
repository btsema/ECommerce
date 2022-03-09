namespace API.Errors
{
    public class ApiGlobalException : ApiGlobalResponse
    {
        public ApiGlobalException(int statusCode, string errorMessage = null, string errordDetails = null) : base(statusCode, errorMessage)
        {
            ErrorDetails = errordDetails;
        }

        public string ErrorDetails { get; set; }
    }
}