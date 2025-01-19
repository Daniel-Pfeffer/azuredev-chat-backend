using System.ComponentModel.DataAnnotations;

namespace BlazorChat.Server.Data;

public class Chat
{
    [Key] public int Id { get; set; } = 0;
    public string ExternalId { get; set; }
    public string Name { get; set; }
    public ICollection<string> UsersIds { get; set; }
    public ICollection<Message> Messages { get; set; }
}