namespace Server.Chat.Models;

public record ChatDto
{
    public string externalId { get; init; }
    public string Name { get; init; }
    public string CreatorId { get; init; }
    public DateTime CreatedAt { get; init; }
}

public record CreateChatDto
{
    public string Name { get; init; }
    public string CreatorId { get; init; }
}

public record MessageDto
{
    public string ExternalId { get; init; }
    public string ChatId { get; init; }
    public string SenderId { get; init; }
    public string Text { get; init; }
    public DateTime CreatedAt { get; init; }
}

public record SendMessageDto
{
    public string SenderId { get; init; }
    public string Text { get; init; }
}