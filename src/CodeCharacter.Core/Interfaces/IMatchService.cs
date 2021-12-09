using Microsoft.AspNetCore.Mvc;

namespace CodeCharacter.Core.Interfaces;

/// <summary>
///     Service for match operations
/// </summary>
public interface IMatchService
{
    /// <summary>
    ///     Get top matches
    /// </summary>
    /// <returns></returns>
    Task<IActionResult> GetTopMatches();

    /// <summary>
    ///     Get matches for user
    /// </summary>
    /// <returns></returns>
    Task<IActionResult> GetUserMatches();
}