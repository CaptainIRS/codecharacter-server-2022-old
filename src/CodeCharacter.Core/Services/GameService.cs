using CodeCharacter.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CodeCharacter.Core.Services;

/// <inheritdoc />
public class GameService : IGameService
{
    /// <inheritdoc />
    public Task<IActionResult> GetGameLogsByGameId(Guid gameId)
    {
        throw new NotImplementedException();
    }
}