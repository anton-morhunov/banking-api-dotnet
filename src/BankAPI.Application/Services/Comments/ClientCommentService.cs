using BankAPI.Application.DTOs.ClientCommentDto;
using BankAPI.Application.Exceptions;
using BankAPI.Application.Interfaces.RepositoryInterfaces.Comments;
using BankAPI.Application.Interfaces.ServiceInterfaces.Comments;
using BankAPI.Application.Mappers;
using Microsoft.Extensions.Logging;

namespace BankAPI.Application.Services.Comments;

public class ClientCommentService : IClientCommentService
{
   private readonly IClientCommentRepository _clientCommentRepository;
   private readonly ILogger<ClientCommentService> _logger;

   public ClientCommentService(
      IClientCommentRepository clientCommentRepository,
      ILogger<ClientCommentService> logger
      )
   {
      _clientCommentRepository = clientCommentRepository;
      _logger = logger;
   }

   public async Task<ClientCommentResponseDto> CreateCommentAsync(ClientCommentCreateDto clientCommentCreateDto)
   {
      _logger.LogInformation(
         "Creating comment for client {clientId} with content {commentContent}", 
         clientCommentCreateDto.ClientId, 
         clientCommentCreateDto.Text
         );
      
      var clientComment = ClientCommentsMapper.ToClientCommentModel(clientCommentCreateDto);
      clientComment.CreatedAt = DateTime.UtcNow;
      
      var createdComment = await _clientCommentRepository.CreateCommentAsync(clientComment);

      var response = ClientCommentsMapper.ToResponseDto(createdComment);
      
      _logger.LogInformation(
         "A new comment with {id} has been created",  
         createdComment.Id
         );

      return response;
   }

   public async Task<bool> DeleteCommentAsync(int commentId)
   {
      _logger.LogDebug(
         "Deleting a comment"
         );
      
      var comment = await _clientCommentRepository.GetCommentByIdAsync(commentId);

      _logger.LogInformation(
         "Looking for a comment with id {id}", 
         commentId
         );

      if (comment == null)
      {
         _logger.LogWarning(
            "Comment with id {id} was not found", 
            commentId
            );
         
         return false;
      }
      await _clientCommentRepository.DeleteCommentAsync(comment);
      
      _logger.LogInformation(
         "Comment with id {id} deleted", 
         commentId
         );
      
      return true;
   }

   public async Task<List<ClientCommentResponseDto>> GetCommentsByClientIdAsync(int clientId)
   {
      _logger.LogDebug(
         "Getting comments for a client with id {clientId}", 
         clientId
         );
      
      var comments = await _clientCommentRepository.GetCommentsByClientIdAsync(clientId);

      var response = new List<ClientCommentResponseDto>();

      foreach (var comment in comments)
      {
         var dto = ClientCommentsMapper.ToResponseDto(comment);
         
         response.Add(dto);
      }
      
      _logger.LogInformation(
         "Got  {count} comments for a client with id {clientId}", 
         comments.Count, 
         clientId
         );
      
      return response;
   }

   public async Task<ClientCommentResponseDto> UpdateCommentAsync(
      int commentId, 
      ClientCommentUpdateDto clientCommentUpdateDto
      )
   {
      
      _logger.LogInformation(
         "Getting a comment with id {id}", 
         commentId
         );
      
      var comment = await _clientCommentRepository.GetCommentByIdAsync(commentId);

      if (comment == null)
      {
         _logger.LogWarning(
            "Comment with id {id} was not found", 
            commentId
            );
         
         throw new NotFoundException($"Comment with id {commentId} not found");
      }
      
      comment.Text = clientCommentUpdateDto.Text ?? comment.Text;

      await _clientCommentRepository.SaveAsync();
      
      _logger.LogInformation(
         "Comment with id {id} updated", 
         commentId
         );

      return ClientCommentsMapper.ToResponseDto(comment);
   }
}