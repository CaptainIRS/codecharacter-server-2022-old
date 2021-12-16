using System.ComponentModel.DataAnnotations;

#pragma warning disable CS8618

namespace CodeCharacter.Core.Entities;

public class CodeRevisionEntity
{
    public UserEntity User { get; init; }
    [Key] public Guid Id { get; init; }
    public string Code { get; init; }
    public CodeRevisionEntity? ParentRevision { get; init; }
}