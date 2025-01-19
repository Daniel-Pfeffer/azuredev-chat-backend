using BalzorApp.Shared.Models;

namespace BalzorApp.Shared.Services;

public interface IChatService
{
    IEnumerable<ChatDto> GetChats();

    void CreateChat(CreateChatDto chat);
    void InviteUserToChat(string chatId, string userId);
    void RemoveUserFromChat(string chatId, string userId);
    void DeleteChat(string chatId);
    void SendMessage(string chatId, string text);

    IEnumerable<MessageDto> GetMessages(string chatId);

    IEnumerable<UserDto> GetUsers(string chatId);
}