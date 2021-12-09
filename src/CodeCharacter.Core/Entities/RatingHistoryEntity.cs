using System.ComponentModel.DataAnnotations.Schema;
using NodaTime;

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

    public int Id { get; set; }

    [ForeignKey("User")] public int UserId { get; }

    public decimal Rating { get; set; }
    public decimal RatingDeviation { get; set; }
    public Instant ValidFrom { get; set; }
}