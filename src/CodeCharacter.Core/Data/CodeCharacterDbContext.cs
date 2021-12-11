using CodeCharacter.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CodeCharacter.Core.Data;

/// <inheritdoc />
public class CodeCharacterDbContext : IdentityDbContext<UserEntity, RoleEntity, int>
{
    /// <summary>
    ///     CodeCharacter Database Context
    /// </summary>
    /// <param name="options"></param>
    public CodeCharacterDbContext(DbContextOptions options) : base(options)
    {
    }

    /// <summary>
    ///     Public users table
    /// </summary>
    public DbSet<PublicUserEntity> PublicUsers { get; set; } = null!;

    /// <summary>
    ///     User stats table
    /// </summary>
    public DbSet<UserStatsEntity> UserStats { get; set; } = null!;

    /// <summary>
    ///     Rating histories table
    /// </summary>
    public DbSet<RatingHistoryEntity> RatingHistories { get; set; } = null!;

    /// <summary>
    ///     Announcements table
    /// </summary>
    public DbSet<AnnouncementEntity> Announcements { get; set; } = null!;

    /// <summary>
    ///     Codes table
    /// </summary>
    public DbSet<CodeEntity> Codes { get; set; } = null!;

    /// <summary>
    ///     Code revisions table
    /// </summary>
    public DbSet<CodeRevisionEntity> CodeRevisions { get; set; } = null!;

    /// <summary>
    ///     Games table
    /// </summary>
    public DbSet<GameEntity> Games { get; set; } = null!;

    /// <summary>
    ///     Game logs table
    /// </summary>
    public DbSet<GameLogEntity> GameLogs { get; set; } = null!;

    /// <summary>
    ///     Maps table
    /// </summary>
    public DbSet<MapEntity> Maps { get; set; } = null!;

    /// <summary>
    ///     Map revisions table
    /// </summary>
    public DbSet<MapRevisionEntity> MapRevisions { get; set; } = null!;

    /// <summary>
    ///     Matches table
    /// </summary>
    public DbSet<MatchEntity> Matches { get; set; } = null!;

    /// <summary>
    ///     Notifications table
    /// </summary>
    public DbSet<NotificationEntity> Notifications { get; set; } = null!;

    /// <summary>
    ///     Model builder
    /// </summary>
    /// <param name="builder"></param>
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<UserEntity>(entity => entity.ToTable("Users"));
        builder.Entity<RoleEntity>(entity => entity.ToTable("Roles"));
        builder.Entity<IdentityUserRole<int>>(entity => entity.ToTable("UserRoles"));
        builder.Entity<IdentityUserClaim<int>>(entity => entity.ToTable("UserClaims"));
        builder.Entity<IdentityUserLogin<int>>(entity => entity.ToTable("UserLogins"));
        builder.Entity<IdentityUserToken<int>>(entity => entity.ToTable("UserTokens"));
        builder.Entity<IdentityRoleClaim<int>>(entity => entity.ToTable("RoleClaims"));


        builder.Entity<PublicUserEntity>(entity =>
        {
            entity.ToTable("PublicUsers");
            entity.HasIndex(e => e.UserName).IsUnique();
        });
        builder.Entity<UserStatsEntity>(entity => entity.ToTable("UserStats"));
        builder.Entity<RatingHistoryEntity>(entity => entity.ToTable("RatingHistories"));
        builder.Entity<AnnouncementEntity>(entity => entity.ToTable("Announcements"));
        builder.Entity<CodeEntity>(entity => entity.ToTable("Codes"));
        builder.Entity<CodeRevisionEntity>(entity => entity.ToTable("CodeRevisions"));
        builder.Entity<GameEntity>(entity => entity.ToTable("Games"));
        builder.Entity<GameLogEntity>(entity => entity.ToTable("GameLogs"));
        builder.Entity<MapEntity>(entity => entity.ToTable("Maps"));
        builder.Entity<MapRevisionEntity>(entity => entity.ToTable("MapRevisions"));
        builder.Entity<MatchEntity>(entity => entity.ToTable("Matches"));
        builder.Entity<NotificationEntity>(entity => entity.ToTable("Notifications"));
    }
}