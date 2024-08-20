using System.Net;

namespace SmartMarket.Service.Common.Exceptions;

public class StatusCodeException : Exception
{
    public HttpStatusCode StatusCode { get; }

    public StatusCodeException(HttpStatusCode code, string message)
        : base(message)
    {
        StatusCode = code;
    }
}
