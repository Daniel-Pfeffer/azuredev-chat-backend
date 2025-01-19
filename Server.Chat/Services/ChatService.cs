using BalzorApp.Shared.Models;
using BalzorApp.Shared.Services;
using BlazorChat.Server.Data;
using BlazorChat.Server.Repository;
using Microsoft.EntityFrameworkCore;

namespace BlazorChat.Server.Services;

public class ChatService(ChatDbContext context) : IChatService
{
    private readonly ChatDbContext _context = context;

    public IEnumerable<ChatDto> GetChats()
    {
        return _context.Chats.ToList()
            .Select(c =>
                new ChatDto
                {
                    Id = c.ExternalId,
                    Name = c.Name,
                    Users = c.UsersIds.Select(userId => new UserDto { Id = userId }),
                }
            );
    }

    public void CreateChat(CreateChatDto chat)
    {
        var newChat = new Chat
        {
            ExternalId = Guid.NewGuid().ToString(),
            Name = chat.Name,
            UsersIds = chat.Users.ToList(),
        };

        _context.Chats.Add(newChat);
        _context.SaveChanges();
    }

    public void InviteUserToChat(string chatId, string userId)
    {
        var chat = _context.Chats.FirstOrDefault(c => c.ExternalId == chatId);
        chat?.UsersIds.Add(userId);
        _context.SaveChanges();
    }

    public void RemoveUserFromChat(string chatId, string userId)
    {
        var chat = _context.Chats.FirstOrDefault(c => c.ExternalId == chatId);
        chat?.UsersIds.Remove(userId);
        _context.SaveChanges();
    }

    public void DeleteChat(string chatId)
    {
        var chat = _context.Chats.FirstOrDefault(c => c.ExternalId == chatId);
        if (chat != null) _context.Chats.Remove(chat);
        _context.SaveChanges();
    }

    public void SendMessage(string chatId, string text)
    {
        var chat = _context.Chats.FirstOrDefault(c => c.ExternalId == chatId);

        if (chat == null) return;

        var message = new Message
        {
            ExternalId = Guid.NewGuid().ToString(),
            ChatId = chat.Id,
            Text = text,
        };

        _context.Messages.Add(message);
        _context.SaveChanges();
    }

    public IEnumerable<MessageDto> GetMessages(string chatId)
    {
        var chat = _context.Chats.Include(c => c.Messages).FirstOrDefault(c => c.ExternalId == chatId);

        if (chat == null) return new List<MessageDto>();

        return chat.Messages
            .Select(m =>
                new MessageDto
                {
                    Id = m.ExternalId,
                    Text = m.Text,
                    Time = m.Time,
                    SenderId = m.SenderId,
                }
            );
    }

    public IEnumerable<UserDto> GetUsers(string chatId)
    {
        var chat = _context.Chats.FirstOrDefault(c => c.ExternalId == chatId);
        return chat?.UsersIds.Select(userId => new UserDto { Id = userId }) ?? new List<UserDto>();
    }
}