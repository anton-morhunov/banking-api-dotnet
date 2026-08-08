using BankAPI.Application.DTOs.AuthDto;
using BankAPI.Domain.Entities;

namespace BankAPI.Application.Mappers;

public static class UserMapper
{
    public static UserResponse ToResponseDto(UserModel userModel)
    {
        return new UserResponse
        {
            Id = userModel.Id,
            Email = userModel.Email,
            UserRole = userModel.Role
        };
    }

    public static UserModel ToUserModel(CreateUserRequest createUserRequest)
    {
        return new UserModel
        {
            Email = createUserRequest.Email,
            Role = createUserRequest.UserRole
        };
    }
}