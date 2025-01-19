using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorChat.Server.Data;

public class Message
{
    [Key] public int Id { get; set; } = 0;
    public string ExternalId { get; set; }
    public string Text { get; set; }
    public DateTime Time { get; set; }
    public string SenderId { get; set; }
    public int ChatId { get; set; }
    [ForeignKey("ChatId")] public Chat Chat { get; set; }
}