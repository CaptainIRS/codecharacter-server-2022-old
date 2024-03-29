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
        CreateMap<(UserEntity userEntity, PublicUserEntity publicUserEntity, UserStatsEntity userStatsEntity),
                CurrentUserProfileDto>()
            .ForMember(x => x.Id, opt => opt.MapFrom(s => s.userEntity.Id))
            .ForMember(x => x.Username, opt => opt.MapFrom(s => s.userEntity.UserName))
            .ForMember(x => x.Name, opt => opt.MapFrom(s => s.publicUserEntity.Name))
            .ForMember(x => x.Email, opt => opt.MapFrom(s => s.userEntity.Email))
            .ForMember(x => x.College, opt => opt.MapFrom(s => s.publicUserEntity.College))
            .ForMember(x => x.Country, opt => opt.MapFrom(s => s.publicUserEntity.Country))
            .ForMember(x => x.CurrentLevel, opt => opt.MapFrom(s => s.userStatsEntity.CurrentLevel))
            .ForMember(x => x.IsAdmin, opt => opt.MapFrom(s => false));
        CreateMap<PublicUserEntity, PublicUserDto>();
        CreateMap<UserStatsEntity, UserStatsDto>();
        CreateMap<(PublicUserEntity publicUserEntity, UserStatsEntity userStatsEntity), LeaderboardEntryDto>()
            .ForMember(x => x.User, opt => opt.MapFrom(s => s.publicUserEntity))
            .ForMember(x => x.Stats, opt => opt.MapFrom(s => s.userStatsEntity));
        CreateMap<GameEntity, GameDto>();
        CreateMap<(PublicUserEntity user1, PublicUserEntity user2, MatchEntity match), MatchDto>()
            .ForMember(x => x.Id, opt => opt.MapFrom(s => s.match.Id))
            .ForMember(x => x.Games, opt => opt.MapFrom(s => s.match.Games))
            .ForMember(x => x.MatchMode, opt => opt.MapFrom(s => s.match.MatchMode))
            .ForMember(x => x.MatchVerdict, opt => opt.MapFrom(s => s.match.MatchVerdict))
            .ForMember(x => x.CreatedAt, opt => opt.MapFrom(y => y.match.CreatedAt.ToDateTimeUtc()))
            .ForMember(x => x.User1, opt => opt.MapFrom(y => y.user1))
            .ForMember(x => x.User2, opt => opt.MapFrom(y => y.user2));
        CreateMap<NotificationEntity, NotificationDto>()
            .ForMember(x => x.CreatedAt, opt => opt.MapFrom(y => y.CreatedAt.ToDateTimeUtc()));
        CreateMap<RatingHistoryEntity, RatingHistoryDto>()
            .ForMember(x => x.ValidFrom, opt => opt.MapFrom(y => y.ValidFrom.ToDateTimeUtc()));
    }
}