using CodeCharacter.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CodeCharacter.Core.Services;

public class LeaderboardService : ILeaderboardService
{
    public Task<IActionResult> GetLeaderboard()
    {
        throw new NotImplementedException();
    }
}