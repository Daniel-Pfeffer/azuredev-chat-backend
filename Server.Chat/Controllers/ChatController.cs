using BalzorApp.Shared.Models;
using BalzorApp.Shared.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlazorChat.Server.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ChatController : ControllerBase
{
    private readonly IChatService _chatService;

    public ChatController(IChatService chatService)
    {
        _chatService = chatService;
    }

    [HttpGet]
    public IEnumerable<ChatDto> GetChats()
    {
        return _chatService.GetChats();
    }

    [HttpPost]
    public void CreateChat(CreateChatDto chat)
    {
        _chatService.CreateChat(chat);
    }

    [HttpPost("{chatId}/invite/{userId}")]
    public void InviteUserToChat(string chatId, string userId)
    {
        _chatService.InviteUserToChat(chatId, userId);
    }

    [HttpPost("{chatId}/remove/{userId}")]
    public void RemoveUserFromChat(string chatId, string userId)
    {
        _chatService.RemoveUserFromChat(chatId, userId);
    }

    [HttpDelete("{chatId}")]
    public void DeleteChat(string chatId)
    {
        _chatService.DeleteChat(chatId);
    }

    [HttpPost("{chatId}/message")]
    public void SendMessage(string chatId, string text)
    {
        _chatService.SendMessage(chatId, text);
    }

    [HttpGet("{chatId}/messages")]
    public IEnumerable<MessageDto> GetMessages(string chatId)
    {
        return _chatService.GetMessages(chatId);
    }

    [HttpGet("{chatId}/users")]
    public IEnumerable<UserDto> GetUsers(string chatId)
    {
        return _chatService.GetUsers(chatId);
    }
}