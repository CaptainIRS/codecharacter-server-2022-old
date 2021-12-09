using AutoMapper;
using CodeCharacter.Core.Mappers;
using NUnit.Framework;

namespace CodeCharacter.Tests;

[TestFixture]
public class MappingProfileTest
{
    [Test]
    public void MappingProfile_Maps_Correctly()
    {
        var config = new MapperConfiguration(cfg => { cfg.AddProfile<MappingProfile>(); });
        config.AssertConfigurationIsValid();
    }
}