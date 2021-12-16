using System.ComponentModel.DataAnnotations.Schema;
using NodaTime;

#pragma warning disable CS8618

namespace CodeCharacter.Core.Entities;

public class RatingHistoryEntity
{
    public RatingHistoryEntity()
    {
    }

    public RatingHistoryEntity(int userId)
    {
        UserId = userId;
        Rating = 0;
        RatingDeviation = 0;
        ValidFrom = Instant.FromDateTimeUtc(DateTime.UtcNow);
    }

    public int Id { get; init; }

    [ForeignKey("User")] public int UserId { get; init; }

    public decimal Rating { get; init; }
    public decimal RatingDeviation { get; init; }
    public Instant ValidFrom { get; init; }
}