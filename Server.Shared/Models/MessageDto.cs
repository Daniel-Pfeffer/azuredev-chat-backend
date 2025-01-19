namespace BalzorApp.Shared.Models;

public record MessageDto
{
    public string Id { get; set; }
    public string SenderId { get; set; }
    public string Text { get; set; }
    public DateTime Time { get; set; }
    public string ChatId { get; set; }
}

public record SendMessageDto
{
    public string ChatId { get; set; }
    public string Text { get; set; }
}