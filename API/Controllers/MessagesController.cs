using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class MessagesController(IUnitOfWork unitOfWork, IMapper mapper) : BaseApiController
    {
        public async Task<ActionResult<MessageDto>> CreateMessage(CreateMessageDto createMessageDto)
        {
            var username = User.GetUsername();

            if (username == createMessageDto.RecipientUsername.ToLower())
                return BadRequest("You cannot send messages to yourself");

            var sender = await unitOfWork.UserRepository.GetUserByUsernameAsync(username);
            var recipient = await unitOfWork.UserRepository.GetUserByUsernameAsync(createMessageDto.RecipientUsername);

            if( recipient == null ) return NotFound();

            var message = new Message
            {
                Sender = sender,
                Recipient = recipient,
                SenderUsername = sender.UserName,
                RecipientUsername = recipient.UserName,
                Content = createMessageDto.Content
            };
            
            unitOfWork.MessageRepository.AddMessage(message);

            if (await unitOfWork.Complete()) return Ok(mapper.Map<MessageDto>(message));

            return BadRequest("Faild to send message");
        }

        [HttpGet]
        public async Task<ActionResult<PagedList<MessageDto>>> GetMessagesForUser([FromQuery] MessageParams messageParams)
        {
            messageParams.Username = User.GetUsername();

            var messages = await unitOfWork.MessageRepository.GetMessagesForUser(messageParams);

            Response.AddPaginationHeader(new PaginationHeader(
                messages.CurrentPage, messages.PageSize, messages.TotalCount, messages.TotalPages));
            
            return messages;
        }

        [HttpGet("thread/{username}")]
        public async Task<ActionResult<IEnumerable<MessageDto>>> GetMessageThread(string username)
        {
            var currentUsername = User.GetUsername();

            return Ok(await unitOfWork.MessageRepository.GetMessagesThread(currentUsername,username));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMessage(int id)
        {
            var username = User.GetUsername();

            var message = await unitOfWork.MessageRepository.GetMessage(id);

            if (message.SenderUsername != username && message.RecipientUsername != username) 
                return Unauthorized();

            if (message.SenderUsername == username) message.SenderDeleted = true;
            if (message.RecipientUsername == username) message.RecipientDeleted = true;

            if(message.SenderDeleted && message.RecipientDeleted)
            {
                unitOfWork.MessageRepository.DeleteMessage(message);
            }

            if (await unitOfWork.Complete()) return Ok();

            return BadRequest("Problem delete=int the message");
        }
    }
}