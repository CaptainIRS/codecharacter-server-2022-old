using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeCharacter.Core.Entities;

public class GameLogEntity
{
    [Key] [ForeignKey("Game")] public int GameId { get; set; }
    public string GameLog { get; set; }
}