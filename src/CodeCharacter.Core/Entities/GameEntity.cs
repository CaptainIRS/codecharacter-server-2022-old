using CodeCharacter.CoreLibrary.Models;

#pragma warning disable CS8618

namespace CodeCharacter.Core.Entities;

public class GameEntity
{
    public Guid Id { get; init; }
    public string Map { get; init; }
    public int Points1 { get; init; }
    public int Points2 { get; init; }
    public GameDto.StatusEnum Status { get; init; }
    public GameDto.GameVerdictEnum GameVerdict { get; init; }
}