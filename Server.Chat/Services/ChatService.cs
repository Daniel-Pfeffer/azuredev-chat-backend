using Microsoft.EntityFrameworkCore;
using Server.Chat.Data;
using Server.Chat.Models;
using Server.Chat.Repositories;

namespace Server.Chat.Services;

internal class ChatService(ChatDbContext context)
{
    public IEnumerable<ChatEntity> GetChats()
    {
        return context.Chats;
    }

    public string CreateChat(CreateChatDto chat)
    {
        var newChat = new ChatEntity
        {
            CreatorId = chat.CreatorId,
            Name = chat.Name,
            ExternalUsersIds = [chat.CreatorId]
        };

        context.Chats.Add(newChat);
        context.SaveChanges();
        return newChat.ExternalId;
    }

    public void InviteUserToChat(string chatId, string userId)
    {
        ChatEntity chat = context.Chats.ByExternalId(chatId);

        chat.ExternalUsersIds.Add(userId);
        context.SaveChanges();
    }

    public void RemoveUserFromChat(string chatId, string userId)
    {
        var chat = context.Chats.ByExternalId(chatId);

        chat.ExternalUsersIds.Remove(userId);
        context.SaveChanges();
    }

    public void DeleteChat(string chatId)
    {
        var chat = context.Chats.ByExternalId(chatId);

        context.Chats.Remove(chat);
        context.SaveChanges();
    }

    public void UpdateChat(string chatId, CreateChatDto dto)
    {
        var chat = context.Chats.ByExternalId(chatId);

        chat.Name = dto.Name;
        context.SaveChanges();
    }

    public ChatEntity GetChat(string chatId)
    {
        return context.Chats.ByExternalId(chatId);
    }

    public string SendMessage(string chatId, SendMessageDto message)
    {
        var chat = context.Chats.ByExternalId(chatId);

        var newMessage = new MessageEntity
        {
            ChatId = chat.Id,
            Text = message.Text,
            SenderId = message.SenderId,
            CreatedAt = DateTime.Now.ToUniversalTime()
        };

        context.Messages.Add(newMessage);
        context.SaveChanges();

        return newMessage.ExternalId;
    }

    public IEnumerable<MessageEntity> GetMessages(string chatId)
    {
        var chat = context.Chats.ByExternalId(chatId);
        return context.Messages.Where(m => m.ChatId == chat.Id).Include(c => c.Chat);
    }

    public MessageEntity GetMessage(string messageId)
    {
        return context.Messages.Include(m => m.Chat).ByExternalId(messageId);
    }
}