using System.Net;

namespace DogsApp_Core.Exceptions
{
    public class WrongModelException : HttpResponseException
    {
        public WrongModelException(string message) : base(HttpStatusCode.BadRequest, message)
        {}
    }
}
