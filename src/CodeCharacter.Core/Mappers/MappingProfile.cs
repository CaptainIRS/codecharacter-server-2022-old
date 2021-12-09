using AutoMapper;
using CodeCharacter.Core.Entities;
using CodeCharacter.CoreLibrary.Models;

namespace CodeCharacter.Core.Mappers;

/// <summary>
/// Mapping profile for the application
/// </summary>
public class MappingProfile : Profile
{
    /// <summary>
    /// Constructor
    /// </summary>
    public MappingProfile()
    {
        CreateMap<Exception, GenericErrorDto>(MemberList.Destination);
        CreateMap<RegisterUserRequestDto, PublicUserEntity>(MemberList.Destination).ForMember(x => x.UserId, opt => opt.Ignore());
        CreateMap<AnnouncementEntity, AnnouncementDto>(MemberList.Destination)
            .ForMember(x => x.Timestamp, opt => opt.MapFrom(y => y.Timestamp.ToDateTimeUtc()));
    }
}