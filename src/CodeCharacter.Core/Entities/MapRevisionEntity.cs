using System.ComponentModel.DataAnnotations;

#pragma warning disable CS8618

namespace CodeCharacter.Core.Entities;

public class MapRevisionEntity
{
    public UserEntity User { get; set; }
    [Key] public Guid Id { get; set; }
    public string Map { get; set; }
    public MapRevisionEntity? ParentRevision { get; set; }
}