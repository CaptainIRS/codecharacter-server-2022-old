using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NodaTime;

#pragma warning disable CS8618

namespace CodeCharacter.Core.Entities;

public class MapEntity
{
    [Key] [ForeignKey("User")] public int UserId { get; set; }
    public string Map { get; set; }
    public Instant LastSavedAt { get; set; }
}