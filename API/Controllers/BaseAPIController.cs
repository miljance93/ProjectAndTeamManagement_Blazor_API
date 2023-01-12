using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class BaseAPIController : ControllerBase
    {
        private IMediator _mediator;

        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        protected IActionResult HandleResult<T>(Result<T> result)
        {
            if (result == null) return NotFound(result);
            if (result.Value != null && result.IsSuccess == true)
            {
                return Ok(result);
            }
            if (result.Value == null && result.IsSuccess == true)
            {
                return NotFound(result);
            }
            return BadRequest(result.Error);
        }
    }
}
