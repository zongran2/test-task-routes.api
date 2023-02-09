using System.Net;

namespace TestTask.Exceptions
{
    public class ApiException : Exception
    {
        private readonly HttpStatusCode statusCode;

        public ApiException(HttpStatusCode statusCode, string message, Exception ex)
            : base($"{statusCode}: {message}", ex)
        {
            this.statusCode = statusCode;
        }

        public ApiException(HttpStatusCode statusCode, string message)
            : base($"{statusCode}: {message}")
        {
            this.statusCode = statusCode;
        }

        public ApiException(HttpStatusCode statusCode)
        {
            this.statusCode = statusCode;
        }

        public HttpStatusCode StatusCode
        {
            get { return this.statusCode; }
        }
    }
}
