using Microsoft.AspNetCore.Identity;

namespace CodeCharacter.Core.Entities;

public class UserEntity : IdentityUser<int>
{
    public UserEntity(string email) : base(email)
    {
        Email = email;
        EmailConfirmed = false;
        LockoutEnabled = true;
    }
}