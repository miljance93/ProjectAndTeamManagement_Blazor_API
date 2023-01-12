using ApplicationService.Roles;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class RoleController : BaseAPIController
    {
        [HttpGet]
        public async Task<IActionResult> GetAllRoles()
        {
            return HandleResult(await Mediator.Send(new List.Query()));
        }
    }
}
