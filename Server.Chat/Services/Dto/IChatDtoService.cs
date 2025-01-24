using Server.Chat.Models;

namespace Server.Chat.Services.Dto;

public interface IChatDtoService
{
    public IEnumerable<ChatDto> GetChats();

    public string CreateChatDto(CreateChatDto dto);

    public void InviteUserToChat(string chatId, string userId);

    public void RemoveUserFromChat(string chatId, string userId);

    public void DeleteChat(string chatId);

    public void UpdateChat(string chatId, CreateChatDto dto);

    public ChatDto GetChat(string chatId);
    
    public IEnumerable<MessageDto> GetMessages(string chatId);

    public MessageDto GetMessage(string messageId);
}