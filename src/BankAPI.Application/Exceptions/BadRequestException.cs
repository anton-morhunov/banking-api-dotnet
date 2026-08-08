using BankAPI.Domain.Enums;

namespace BankAPI.Application.Exceptions;

public class BadRequestException : ApiExceptions
{
    public BadRequestException(string message) : base(message, ApiStatusCode.BadRequest){}
}