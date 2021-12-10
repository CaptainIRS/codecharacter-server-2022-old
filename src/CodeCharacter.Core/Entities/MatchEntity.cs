using CodeCharacter.CoreLibrary.Models;
using NodaTime;

namespace CodeCharacter.Core.Entities;

public class MatchEntity
{
    public Guid Id { get; set; }
    public List<GameEntity>? Games { get; set; }
    public MatchDto.MatchModeEnum MatchMode { get; set; }
    public MatchDto.MatchVerdictEnum MatchVerdict { get; set; }
    public Instant CreatedAt { get; set; }
    public UserEntity? User1 { get; set; }
    public UserEntity? User2 { get; set; }
}