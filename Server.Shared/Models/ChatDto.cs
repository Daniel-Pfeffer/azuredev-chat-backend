namespace BalzorApp.Shared.Models;

public record ChatDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public IEnumerable<UserDto> Users { get; set; }
}

public record CreateChatDto
{
    public string Name { get; set; }
    public IEnumerable<string> Users { get; set; }
}