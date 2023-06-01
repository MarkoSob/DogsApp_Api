using System.Net;

namespace DogsApp_Core.Exceptions
{
    public class HttpResponseException : Exception
    {
        public int StatusCode { get; }
        public object Object { get; }

        protected HttpResponseException(HttpStatusCode statusCode, string message = "", object _object = null!)
            : base(message)
        {
            StatusCode = (int)statusCode;
            Object = _object;
        }

        public override string Message
            => base.Message + $"\nError code: {StatusCode}";
    }
}
