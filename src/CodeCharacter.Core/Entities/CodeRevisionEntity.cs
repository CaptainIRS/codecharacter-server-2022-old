using System.ComponentModel.DataAnnotations;

namespace CodeCharacter.Core.Entities;

public class CodeRevisionEntity
{
    public UserEntity User { get; set; }
    [Key] public Guid Id { get; set; }
    public string Code { get; set; }
    public CodeRevisionEntity? ParentRevision { get; set; }
}