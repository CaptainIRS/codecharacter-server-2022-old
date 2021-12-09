using CodeCharacter.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CodeCharacter.Core.Services;

/// <inheritdoc />
public class MatchService : IMatchService
{
    /// <inheritdoc />
    public async Task<IActionResult> GetTopMatches()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public async Task<IActionResult> GetUserMatches()
    {
        throw new NotImplementedException();
    }
}