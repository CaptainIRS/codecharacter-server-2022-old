using NodaTime;

#pragma warning disable CS8618

namespace CodeCharacter.Core.Entities;

public class AnnouncementEntity
{
    public int Id { get; set; }
    public string Message { get; set; }
    public Instant Timestamp { get; set; }
}