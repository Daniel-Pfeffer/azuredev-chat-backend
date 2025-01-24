using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Chat.Models;
using Server.Chat.Services.Dto;

namespace Server.Chat.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChatController(IChatDtoService chatService)
{
    [HttpGet]
    public IEnumerable<ChatDto> GetChats()
    {
        return chatService.GetChats();
    }

    [HttpPost]
    public string CreateChatDto(CreateChatDto dto)
    {
        return chatService.CreateChatDto(dto);
    }

    [HttpPost("{chatId}/member/{userId}")]
    public void InviteUserToChat(string chatId, string userId)
    {
        chatService.InviteUserToChat(chatId, userId);
    }

    [HttpDelete("{chatId}/member/{userId}")]
    public void RemoveUserFromChat(string chatId, string userId)
    {
        chatService.RemoveUserFromChat(chatId, userId);
    }

    [HttpDelete("{chatId}")]
    public void DeleteChat(string chatId)
    {
        chatService.DeleteChat(chatId);
    }

    [HttpPut("{chatId}")]
    public void UpdateChat(string chatId, CreateChatDto dto)
    {
        chatService.UpdateChat(chatId, dto);
    }

    [HttpGet("{chatId}")]
    public ChatDto GetChat(string chatId)
    {
        return chatService.GetChat(chatId);
    }
    
    [HttpGet("{chatId}/messages")]
    public IEnumerable<MessageDto> GetMessages(string chatId)
    {
        return chatService.GetMessages(chatId);
    }

    [HttpGet("message/{messageId}")]
    public MessageDto GetMessage(string messageId)
    {
        return chatService.GetMessage(messageId);
    }
}