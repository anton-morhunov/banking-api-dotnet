using BankAPI.Application.DTOs.ClientDto;
using BankAPI.Domain.Entities;

namespace BankAPI.Application.Mappers;

public static class ClientMapper
{
    public static ClientResponseDTO ToResponseDto(ClientModel client)
    {
        return new ClientResponseDTO
        {
            Id = client.Id,
            Name = client.Name,
            Email = client.Email,
            PhoneNumber = client.PhoneNumber,
            Created = client.CreateDate,
            Status = client.Status,
        };
    }
}