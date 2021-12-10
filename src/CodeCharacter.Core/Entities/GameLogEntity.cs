using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#pragma warning disable CS8618

namespace CodeCharacter.Core.Entities;

public class GameLogEntity
{
    [Key] [ForeignKey("Game")] public Guid GameId { get; set; }
    public string GameLog { get; set; }
}