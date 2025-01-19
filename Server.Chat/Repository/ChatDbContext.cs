using BlazorChat.Server.Data;
using Microsoft.EntityFrameworkCore;

namespace BlazorChat.Server.Repository;

public class ChatDbContext : DbContext
{
    public ChatDbContext(DbContextOptions<ChatDbContext> options) : base(options)
    {
    }

    public DbSet<Chat> Chats { get; set; }

    public DbSet<Message> Messages { get; set; }
}