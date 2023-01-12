using AutoMapper;
using Domain;
using Domain.IdentityAuth;
using Microsoft.AspNetCore.Identity;
using Shared.DTOs;

namespace Shared.Mapp
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<RoleDTO, IdentityRole>().ReverseMap().ForMember(x => x.RoleId, y => y.MapFrom(z => z.Id));
            CreateMap<ApplicationUser, EmployeeDTO>().ReverseMap();
            CreateMap<Team, TeamDTO>().ReverseMap();
            CreateMap<Project, ProjectDTO>().ReverseMap();
            CreateMap<ProjectStatus, ProjectStatusDTO>().ReverseMap();
            CreateMap<Request, RequestDTO>().ReverseMap();
            CreateMap<RequestStatus , RequestStatusDTO>().ReverseMap();
            CreateMap<Comment, CommentDTO>().ReverseMap();
        }
    }
}
