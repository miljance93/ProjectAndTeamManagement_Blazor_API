using MediatR;
using Persistence.Repo.Interfaces;
using Shared;
using Shared.DTOs;

namespace ApplicationService.Roles
{
    public class List
    {
        public record Query : IRequest<Result<IEnumerable<RoleDTO>>>;

        public class Handler : IRequestHandler<Query, Result<IEnumerable<RoleDTO>>>
        {
            private readonly IRoleRepository _roleRepository;

            public Handler(IRoleRepository roleRepository)
            {
                _roleRepository = roleRepository;
            }
            public async Task<Result<IEnumerable<RoleDTO>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var roles = await _roleRepository.GetAllAsync<RoleDTO>();
                if (roles != null)
                {                    
                    return new Result<IEnumerable<RoleDTO>> { IsSuccess = true, Value = roles };
                }

                return new Result<IEnumerable<RoleDTO>> { IsSuccess = false, Error = "Roles not found!" };
            }
        }
    }
}
