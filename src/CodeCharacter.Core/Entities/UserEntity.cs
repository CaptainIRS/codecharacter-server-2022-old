using Microsoft.AspNetCore.Identity;

namespace CodeCharacter.Core.Entities;

public class UserEntity : IdentityUser<int>
{
    public UserEntity(string userName, string email) : base(userName)
    {
        Email = email;
        EmailConfirmed = false;
        LockoutEnabled = true;
    }
}