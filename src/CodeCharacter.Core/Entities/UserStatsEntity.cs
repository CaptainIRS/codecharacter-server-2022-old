using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeCharacter.Core.Entities;

public class UserStatsEntity
{
    public UserStatsEntity()
    {
    }

    public UserStatsEntity(int userId)
    {
        UserId = userId;
        CurrrentLevel = 1;
        Rating = 0;
        Wins = 0;
        Losses = 0;
        Ties = 0;
    }

    [Key] [ForeignKey("User")] public int UserId { get; set; }

    public int CurrrentLevel { get; set; }
    public decimal Rating { get; set; }
    public int Wins { get; set; }
    public int Losses { get; set; }
    public int Ties { get; set; }
}