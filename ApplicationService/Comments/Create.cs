using AutoMapper;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence;
using Shared;
using Shared.DTOs;

namespace ApplicationService.Comments
{
    public class Create
    {
        public record Command(string Body, string Username) : IRequest<Result<CommentDTO>>;

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Body).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Command, Result<CommentDTO>>
        {
            private readonly ApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly ILogger _logger;

            public Handler(ApplicationDbContext context, IMapper mapper, ILogger logger)
            {
                _context = context;
                _mapper = mapper;
                _logger = logger;
            }
            public async Task<Result<CommentDTO>> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    var comment = new Comment
                    {
                        Body = request.Body,
                    };

                    _context.Comments.Add(comment);

                    var success = await _context.SaveChangesAsync() > 0;

                    return new Result<CommentDTO> { IsSuccess = success, Value = _mapper.Map<CommentDTO>(comment) };
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    return new Result<CommentDTO> { IsSuccess = false, Error = "Failed to add comment" };
                }
            }
        }
    }
}
