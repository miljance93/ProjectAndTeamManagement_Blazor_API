using AutoMapper;
using Domain;
using Persistence.Repo.Interfaces;

namespace Persistence.Repo.Repositories
{
    public class RequestRepository : Repository<Request>, IRequestRepository
    {
        public RequestRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
