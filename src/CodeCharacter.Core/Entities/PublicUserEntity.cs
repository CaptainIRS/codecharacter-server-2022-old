using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#pragma warning disable CS8618

namespace CodeCharacter.Core.Entities;

public class PublicUserEntity
{
    public int AvatarId { get; set; }
    public string College { get; set; }
    public string Country { get; set; }
    public string Name { get; set; }

    [Key] [ForeignKey("User")] public int UserId { get; set; }
}