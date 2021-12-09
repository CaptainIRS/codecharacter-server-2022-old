using CodeCharacter.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CodeCharacter.Core.Services;

/// <inheritdoc />
public class GameService : IGameService
{
    /// <inheritdoc />
    public async Task<IActionResult> GetGameLogsByGameId(Guid gameId)
    {
        throw new NotImplementedException();
    }
}