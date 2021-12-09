using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NodaTime;

namespace CodeCharacter.Core.Entities;

public class CodeEntity
{
    [Key] [ForeignKey("User")] public int UserId { get; set; }
    public string Code { get; set; }
    public Instant LastSavedAt { get; set; }
}