using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Shared;
using Shared.DTOs;

namespace ApplicationService.Comments
{
    public class List
    {
        public record Query : IRequest<Result<List<CommentDTO>>>;
        public class Handler : IRequestHandler<Query, Result<List<CommentDTO>>>
        {
            private readonly ApplicationDbContext _context;
            private readonly IMapper _mapper;

            public Handler(ApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<Result<List<CommentDTO>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var comments = await _context.Comments
                    .OrderBy(x => x.CreatedAt)
                    .ProjectTo<CommentDTO>(_mapper.ConfigurationProvider)
                    .ToListAsync();

                return new Result<List<CommentDTO>> { IsSuccess = true, Value = comments };
            }
        }
    }
}
