using System;
using System.Data.Common;
using CodeCharacter.Core.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using NUnit.Framework;

namespace CodeCharacter.Tests;

public class BaseServiceTests : IDisposable
{
    protected DbContextOptions<CodeCharacterDbContext> DbContextOptions = null!;
    private DbConnection _connection = null!;
    
    [SetUp]
    public void Setup()
    {
        DbContextOptions = new DbContextOptionsBuilder<CodeCharacterDbContext>().UseSqlite(CreateInMemoryDatabase(), x => x.UseNodaTime()).Options;
        _connection = RelationalOptionsExtension.Extract(DbContextOptions).Connection!;
    }

    private static DbConnection CreateInMemoryDatabase()
    {
        var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        return connection;
    }

    public void Dispose() => _connection.Dispose();
}