using NodaTime;

#pragma warning disable CS8618

namespace CodeCharacter.Core.Entities;

public class NotificationEntity
{
    public Guid Id { get; init; }
    public string Title { get; init; }
    public string Content { get; init; }
    public Instant CreatedAt { get; init; }
    public bool Read { get; set; }
    public UserEntity User { get; init; }
}