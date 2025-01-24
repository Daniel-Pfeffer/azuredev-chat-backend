using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Chat.Data;

public interface ExternalIdEntity
{
    public string ExternalId { get; init; }
}

public class ChatEntity : ExternalIdEntity
{
    [Key] public int Id { get; init; }
    public string ExternalId { get; init; } = Guid.NewGuid().ToString();

    public string Name { get; set; }
    public string CreatorId { get; init; }
    public DateTime CreatedAt { get; init; } = DateTime.Now.ToUniversalTime();

    public List<string> ExternalUsersIds { get; set; } = new();
}

public class MessageEntity : ExternalIdEntity
{
    [Key] public int Id { get; init; }
    public string ExternalId { get; init; } = Guid.NewGuid().ToString();

    public int ChatId { get; init; }
    [ForeignKey("ChatId")] public ChatEntity Chat { get; set; }
    public string SenderId { get; init; }
    public string Text { get; init; }
    public DateTime CreatedAt { get; init; } = DateTime.Now.ToUniversalTime();
}