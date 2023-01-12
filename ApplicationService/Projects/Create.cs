using FluentValidation;
using MediatR;
using Persistence.Repo.Interfaces;
using Shared;
using Shared.DTOs;

namespace ApplicationService.Projects
{
    public class Create
    {
        public record Command(ProjectDTO Project) : IRequest<Result<bool>>;

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Project).SetValidator(new ProjectsValidator());
            } 
        }

        public class Handler : IRequestHandler<Command, Result<bool>>
        {
            private readonly IProjectRepository _projectRepository;

            public Handler(IProjectRepository projectRepository)
            {
                _projectRepository = projectRepository;
            }
            public async Task<Result<bool>> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    var result = await _projectRepository.CreateAsync(request.Project);
                    return new Result<bool> { IsSuccess = true, Value = result };
                }
                catch (Exception)
                {
                    return new Result<bool> { IsSuccess = false, Error = "Project not created" };
                }
            }
        }
    }
}
