using System.ComponentModel.DataAnnotations;

#pragma warning disable CS8618

namespace CodeCharacter.Core.Entities;

public class CodeRevisionEntity
{
    public UserEntity User { get; set; }
    [Key] public Guid Id { get; set; }
    public string Code { get; set; }
    public CodeRevisionEntity? ParentRevision { get; set; }
}