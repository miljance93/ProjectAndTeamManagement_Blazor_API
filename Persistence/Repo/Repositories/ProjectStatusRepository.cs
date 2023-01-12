using AutoMapper;
using Domain;
using Persistence.Repo.Interfaces;

namespace Persistence.Repo.Repositories
{
    public class ProjectStatusRepository : Repository<ProjectStatus>, IProjectStatusRepository
    {
        public ProjectStatusRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
