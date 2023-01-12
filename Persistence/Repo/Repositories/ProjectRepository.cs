using AutoMapper;
using Domain;
using Persistence.Repo.Interfaces;

namespace Persistence.Repo.Repositories
{
    public class ProjectRepository : Repository<Project>, IProjectRepository
    {
        public ProjectRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
