using FluentValidation;
using Shared.DTOs;

namespace ApplicationService.Projects
{
    public class ProjectsValidator : AbstractValidator<ProjectDTO>
    {
        public ProjectsValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.StartDate).NotEmpty();
            RuleFor(x => x.EndDate).NotEmpty();
        }
    }
}
