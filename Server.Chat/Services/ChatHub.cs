using Microsoft.AspNetCore.SignalR;
using Server.Chat.Models;

namespace Server.Chat.Services;

internal class ChatHub(ChatService service, ILogger<ChatHub> logger) : Hub
{
    public Task BroadcastMessage(string name, string message) =>
        Clients.All.SendAsync("broadcastMessage", name, message);

    public Task JoinChat(string chatId)
    {
        logger.LogInformation("User {userId} joined chat {chatId}", Context.UserIdentifier, chatId);
        return Groups.AddToGroupAsync(Context.ConnectionId, chatId);
    }

    public Task LeaveChat(string chatId)
    {
        logger.LogInformation("User {userId} left chat {chatId}", Context.UserIdentifier, chatId);
        return Groups.RemoveFromGroupAsync(Context.ConnectionId, chatId);
    }

    public void SendMessage(string chatId, SendMessageDto message)
    {
        logger.LogInformation("Sending message to chat {chatId}", chatId);
        var msgId = service.SendMessage(chatId, message);

        Clients.Group(chatId)
            .SendAsync("message", msgId);
    }
}