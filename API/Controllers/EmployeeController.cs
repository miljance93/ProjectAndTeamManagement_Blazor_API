using ApplicationService.Employees;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;
using System.Security.Claims;

namespace API.Controllers
{
    public class EmployeeController : BaseAPIController
    {

        [HttpPost]
        public async Task<IActionResult> CreateEmployee(EmployeeDTO employee)
        {
            return HandleResult(await Mediator.Send(new Create.Command(employee)));
        }
        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            return HandleResult(await Mediator.Send(new List.Query()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(string id)
        {
            return HandleResult(await Mediator.Send(new Search.Query(id)));
        }

        [HttpGet("email/{email}")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            return HandleResult(await Mediator.Send(new SearchByEmail.Query(email)));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEmployee(EmployeeDTO employee)
        {
            return HandleResult(await Mediator.Send(new Update.Command(employee)));
        }

        [HttpGet("currentuser")]
        public async Task<EmployeeDTO> GetCurrentEmployee()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var employeeClaims = identity.Claims;

                return new EmployeeDTO
                {
                    Email = employeeClaims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value,
                    Id = employeeClaims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value,
                    RoleId = employeeClaims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value,
                };
            }

            return null;
        }
    }
}
