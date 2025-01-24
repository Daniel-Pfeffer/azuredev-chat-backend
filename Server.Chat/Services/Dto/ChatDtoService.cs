using Server.Chat.Models;

namespace Server.Chat.Services.Dto;

internal class ChatDtoService(ChatService chatService) : IChatDtoService
{
    public IEnumerable<ChatDto> GetChats()
    {
        return chatService.GetChats().Select(c =>
            new ChatDto
            {
                externalId = c.ExternalId,
                Name = c.Name,
                CreatorId = c.CreatorId,
                CreatedAt = c.CreatedAt
            }
        );
    }

    public string CreateChatDto(CreateChatDto dto)
    {
        return chatService.CreateChat(dto);
    }

    public void InviteUserToChat(string chatId, string userId)
    {
        chatService.InviteUserToChat(chatId, userId);
    }

    public void RemoveUserFromChat(string chatId, string userId)
    {
        chatService.RemoveUserFromChat(chatId, userId);
    }

    public void DeleteChat(string chatId)
    {
        chatService.DeleteChat(chatId);
    }

    public void UpdateChat(string chatId, CreateChatDto dto)
    {
        chatService.UpdateChat(chatId, dto);
    }

    public ChatDto GetChat(string chatId)
    {
        var chat = chatService.GetChat(chatId);
        return new ChatDto
        {
            externalId = chat.ExternalId,
            Name = chat.Name,
            CreatorId = chat.CreatorId,
            CreatedAt = chat.CreatedAt
        };
    }

    public IEnumerable<MessageDto> GetMessages(string chatId)
    {
        return chatService.GetMessages(chatId).Select(m =>
            new MessageDto
            {
                ExternalId = m.ExternalId,
                ChatId = m.Chat.ExternalId,
                SenderId = m.SenderId,
                Text = m.Text,
                CreatedAt = m.CreatedAt
            }
        );
    }

    public MessageDto GetMessage(string messageId)
    {
        var message = chatService.GetMessage(messageId);
        return new MessageDto
        {
            ExternalId = message.ExternalId,
            ChatId = message.Chat.ExternalId,
            SenderId = message.SenderId,
            Text = message.Text,
            CreatedAt = message.CreatedAt
        };
    }
}