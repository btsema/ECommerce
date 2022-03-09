namespace API.Errors
{
    public class ApiGlobalResponse
    {
        public ApiGlobalResponse(int statusCode, string errorMessage = null)
        {
            StatusCode = statusCode;
            Message = errorMessage ?? GetDefaultMessageForStatusCode(statusCode);
        }        

        public int StatusCode { get; set; }
        public string Message {  get; set; }

        private string GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "400: A bad request",
                401 => "401: Authorization error",
                404 => "404: File not found",
                _ => "Something went wrong"
            };
        }
    }
}
