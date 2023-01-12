using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Persistence.Repo.Interfaces;

namespace Persistence.Repo.Repositories
{
    public class RoleRepository : Repository<IdentityRole>, IRoleRepository
    {
        public RoleRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
