using CodeCharacter.CoreLibrary.Models;

namespace CodeCharacter.Core.Entities;

public class GameEntity
{
    public Guid Id { get; set; }
    public string? Map { get; set; }
    public int Points1 { get; set; }
    public int Points2 { get; set; }
    public GameDto.StatusEnum Status { get; set; }
    public GameDto.GameVerdictEnum GameVerdict { get; set; }
}