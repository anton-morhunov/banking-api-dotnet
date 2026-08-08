
using BankAPI.Domain.Enums;

namespace BankAPI.Application.Exceptions;

public class NotFoundException : ApiExceptions
{
    public NotFoundException(string message) : base(message, ApiStatusCode.NotFound)
    {
    }
}