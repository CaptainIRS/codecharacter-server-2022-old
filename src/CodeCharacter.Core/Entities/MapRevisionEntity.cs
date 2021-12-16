using System.ComponentModel.DataAnnotations;

#pragma warning disable CS8618

namespace CodeCharacter.Core.Entities;

public class MapRevisionEntity
{
    public UserEntity User { get; init; }
    [Key] public Guid Id { get; init; }
    public string Map { get; init; }
    public MapRevisionEntity? ParentRevision { get; init; }
}