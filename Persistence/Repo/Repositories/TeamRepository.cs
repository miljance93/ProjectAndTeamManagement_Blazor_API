using AutoMapper;
using Domain;
using Persistence.Repo.Interfaces;

namespace Persistence.Repo.Repositories
{
    public class TeamRepository : Repository<Team>, ITeamRepository
    {
        public TeamRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
