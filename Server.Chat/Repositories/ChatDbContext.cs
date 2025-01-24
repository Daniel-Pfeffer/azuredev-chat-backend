using Microsoft.EntityFrameworkCore;
using Server.Chat.Data;
using Server.Shared.Exceptions;

namespace Server.Chat.Repositories;

public class ChatDbContext(DbContextOptions<ChatDbContext> options) : DbContext(options)
{
    public DbSet<ChatEntity> Chats { get; set; }

    public DbSet<MessageEntity> Messages { get; set; }
}

public static class Extension
{
    public static T ByExternalId<T>(this IQueryable<T> set, string externalId) where T : class, ExternalIdEntity
    {
        return set.FirstOrDefault(c => c.ExternalId == externalId) ??
               throw new NotFoundException($"Chat with id ({externalId}) not found");
    }
}