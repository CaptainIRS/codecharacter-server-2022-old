using CodeCharacter.CoreLibrary.Models;
using NodaTime;

#pragma warning disable CS8618

namespace CodeCharacter.Core.Entities;

public class MatchEntity
{
    public Guid Id { get; init; }
    public List<GameEntity> Games { get; init; }
    public MatchDto.MatchModeEnum MatchMode { get; init; }
    public MatchDto.MatchVerdictEnum MatchVerdict { get; init; }
    public Instant CreatedAt { get; init; }
    public UserEntity User1 { get; init; }
    public UserEntity User2 { get; init; }
}