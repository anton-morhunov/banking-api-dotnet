using BankAPI.Domain.Enums;

namespace BankAPI.Application.Exceptions;

public class ConflictException : ApiExceptions
{
    public ConflictException(string message) : base(message, ApiStatusCode.Conflict){}
}