using Application.Activities;
using Application.Bookmakers;
using Application.Comments;
using Application.Config;
using Application.Events;
using Application.Products;
using Application.Profiles;
using Application.Project;
using Application.ProjectWeight;
using Application.Register;
using Application.Sales;
using Application.SalesPerformanceTeam;
using Application.Seller;
using Application.TivoGame;
using Domain;

namespace Application.Core
{
    public class MappingProfiles : AutoMapper.Profile
    {
        public MappingProfiles()
        {
            string currentUsername = null;
            CreateMap<Activity, Activity>();
            CreateMap<Activity, ActivityDto>()
                .ForMember(d => d.HostUsername, o => o.MapFrom(s => s.Attendees
                    .FirstOrDefault(x => x.IsHost).AppUser.UserName));
            CreateMap<ActivityAttendee, AttendeeDto>()
                .ForMember(d => d.DisplayName, o => o.MapFrom(s => s.AppUser.DisplayName))
                .ForMember(d => d.Username, o => o.MapFrom(s => s.AppUser.UserName))
                .ForMember(d => d.Bio, o => o.MapFrom(s => s.AppUser.Bio))
                .ForMember(d => d.Image, o => o.MapFrom(s => s.AppUser.Photos.FirstOrDefault(x => x.IsMain).Url))
                .ForMember(d => d.FollowersCount, o => o.MapFrom(s => s.AppUser.Followers.Count))
                .ForMember(d => d.FollowingCount, o => o.MapFrom(s => s.AppUser.Followings.Count))
                .ForMember(d => d.Following,
                    o => o.MapFrom(s => s.AppUser.Followers.Any(x => x.Observer.UserName == currentUsername)));
            CreateMap<AppUser, Profile>()
                .ForMember(d => d.Image, s => s.MapFrom(o => o.Photos.FirstOrDefault(x => x.IsMain).Url))
                .ForMember(d => d.FollowersCount, o => o.MapFrom(s => s.Followers.Count))
                .ForMember(d => d.FollowingCount, o => o.MapFrom(s => s.Followings.Count))
                .ForMember(d => d.Following,
                    o => o.MapFrom(s => s.Followers.Any(x => x.Observer.UserName == currentUsername)));
            CreateMap<Comment, CommentDto>()
                .ForMember(d => d.Username, o => o.MapFrom(s => s.Author.UserName))
                .ForMember(d => d.DisplayName, o => o.MapFrom(s => s.Author.DisplayName))
                .ForMember(d => d.Image, o => o.MapFrom(s => s.Author.Photos.FirstOrDefault(x => x.IsMain).Url));
            CreateMap<ActivityAttendee, UserActivityDto>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Activity.Id))
                .ForMember(d => d.Date, o => o.MapFrom(s => s.Activity.Date))
                .ForMember(d => d.Title, o => o.MapFrom(s => s.Activity.Title))
                .ForMember(d => d.Category, o => o.MapFrom(s => s.Activity.Category))
                .ForMember(d => d.HostUsername, o => o.MapFrom(s =>
                    s.Activity.Attendees.FirstOrDefault(x => x.IsHost).AppUser.UserName));
            CreateMap<Configuration, ConfigurationDto>();
            CreateMap<ConfigurationDto, Configuration>();
            CreateMap<Sale, SaleDto>();
            CreateMap<SaleDto, Sale>();
            CreateMap<SaleDto, SaleDto>();
            CreateMap<Domain.Project, ProjectDto>().ReverseMap();
            CreateMap<Domain.Seller, SellerDto>();
            CreateMap<SellerDto, Domain.Seller>();
            CreateMap<SellerDto, SellerDto>().ReverseMap();
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Bookmaker, BookmakerDto>().ReverseMap();
            CreateMap<Domain.Register, RegisterDto>()
                .ForMember(d => d.EventsId, o => o.MapFrom(s => s.Events.EventsId))
                .ForMember(d => d.BookmakerId, o => o.MapFrom(s => s.Bookmaker.BookmakerId))
                .ForMember(d => d.SellerId, o => o.MapFrom(s => s.Seller.SellerId))
                .ReverseMap();
            CreateMap<Domain.Events, EventDto>().ReverseMap();
            CreateMap<RegisterDto, RegisterDto>();

            CreateMap<Domain.SalesPerformanceTeam, SalesPerformaceTeamDto>()
                .ForMember(d => d.SPTSellerId, o => o.MapFrom(s => s.SPTSeller.SellerId))
                .ForMember(d => d.SPTProjectId, o => o.MapFrom(s => s.SPTProject.ProjectId))
                .ForMember(d => d.SPTEventId, o => o.MapFrom(s => s.SPTEvent.EventsId))
                .ReverseMap();
            CreateMap<SalesPerformaceTeamDto, SalesPerformaceTeamDto>();
            CreateMap<Domain.ProjectWeight, ProjectWeightDto>()
                .ForMember(p => p.ProjectName, o => o.MapFrom(s => s.Project.ProjectName))
                .ReverseMap();
            CreateMap<Domain.TivoGame, TivoGameDto>();
        }
    }
}