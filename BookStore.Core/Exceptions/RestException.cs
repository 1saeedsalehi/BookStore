using System.Net;

namespace BookStore.Core.Exceptions
{
    public class RestException : System.Exception
    {
        public RestException(HttpStatusCode code, string message = null)
        {
            Code = code;
            Message = message;
        }

        public HttpStatusCode Code { get; }

        public new string Message { get; }
    }
}
