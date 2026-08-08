using BankAPI.Domain.Enums;

namespace BankAPI.Application.Exceptions;

public abstract class ApiExceptions : Exception
{
    public ApiStatusCode StatusCode { get; }

    protected ApiExceptions(string message, ApiStatusCode statusCode) : base(message)
    {
        StatusCode = statusCode;
    }
}