using ApplicationService.Projects;
using ApplicationService.ProjectStatuses;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;

namespace API.Controllers
{
    public class DepartmentLeadController : BaseAPIController
    {
        [HttpGet("GetAllProjects")]
        public async Task<IActionResult> GetAllProjects()
        {
            return HandleResult(await Mediator.Send(new List.Query()));
        }

        [HttpGet("GetAllProjectStatuses")]
        public async Task<IActionResult> GetAllProjectStatuses()
        {
            return HandleResult(await Mediator.Send(new StatusList.Query()));
        }

        [HttpPost]
        [Route("CreateProject")]
        public async Task<IActionResult> CreateProject([FromBody] ProjectDTO project)
        {
            return HandleResult(await Mediator.Send(new Create.Command(project)));
        }
    }
}
