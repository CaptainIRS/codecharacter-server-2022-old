using NodaTime;

#pragma warning disable CS8618

namespace CodeCharacter.Core.Entities;

public class NotificationEntity
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public Instant CreatedAt { get; set; }
    public bool Read { get; set; }
    public UserEntity User { get; set; }
}