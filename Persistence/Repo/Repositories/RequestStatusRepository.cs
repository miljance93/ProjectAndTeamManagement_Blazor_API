using AutoMapper;
using Domain;
using Persistence.Repo.Interfaces;

namespace Persistence.Repo.Repositories
{
    public class RequestStatusRepository : Repository<RequestStatus>, IRequestStatusRepository
    {
        public RequestStatusRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
