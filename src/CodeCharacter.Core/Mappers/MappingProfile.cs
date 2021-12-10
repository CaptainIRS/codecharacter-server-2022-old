using AutoMapper;
using CodeCharacter.Core.Entities;
using CodeCharacter.CoreLibrary.Models;

namespace CodeCharacter.Core.Mappers;

/// <summary>
///     Mapping profile for the application
/// </summary>
public class MappingProfile : Profile
{
    /// <summary>
    ///     Constructor
    /// </summary>
    public MappingProfile()
    {
        CreateMap<Exception, GenericErrorDto>(MemberList.Destination);
        CreateMap<RegisterUserRequestDto, PublicUserEntity>(MemberList.Destination)
            .ForMember(x => x.UserId, opt => opt.Ignore());
        CreateMap<AnnouncementEntity, AnnouncementDto>(MemberList.Destination)
            .ForMember(x => x.Timestamp, opt => opt.MapFrom(y => y.Timestamp.ToDateTimeUtc()));
        CreateMap<CodeRevisionEntity, CodeRevisionDto>()
            .ForMember(x => x.ParentRevision,
                opt => opt.MapFrom(y => y.ParentRevision == null ? Guid.Empty : y.ParentRevision.Id));
        CreateMap<CodeEntity, CodeDto>()
            .ForMember(x => x.LastSavedAt, opt => opt.MapFrom(y => y.LastSavedAt.ToDateTimeUtc()));
        CreateMap<MapRevisionEntity, MapRevisionDto>()
            .ForMember(x => x.ParentRevision,
                opt => opt.MapFrom(y => y.ParentRevision == null ? Guid.Empty : y.ParentRevision.Id));
        CreateMap<MapEntity, MapDto>()
            .ForMember(x => x.LastSavedAt, opt => opt.MapFrom(y => y.LastSavedAt.ToDateTimeUtc()));
    }
}