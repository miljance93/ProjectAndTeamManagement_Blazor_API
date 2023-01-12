using FluentValidation;
using Shared.DTOs;

namespace ApplicationService.Employees
{
    public class EmployeesValidator : AbstractValidator<EmployeeDTO>
    {
        public EmployeesValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();    
            RuleFor(x => x.Email).NotEmpty();
        }
    }
}
