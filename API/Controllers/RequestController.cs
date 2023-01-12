using ApplicationService.Requests;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;

namespace API.Controllers
{
    public class RequestController : BaseAPIController
    {
        [HttpGet]
        public async Task<IActionResult> GetAllRequests()
        {
            return HandleResult(await Mediator.Send(new List.Query()));
        }

        [HttpPost]
        public async Task<IActionResult> CreateRequest(RequestDTO request)
        {
            return HandleResult(await Mediator.Send(new Create.Command(request)));
        } 

        [HttpGet("getrequeststatuses")]
        public async Task<IActionResult> GetRequestStatuses()
        {
            return HandleResult(await Mediator.Send(new RequestStatusesList.Query()));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRequest(RequestDTO request)
        {
            return HandleResult(await Mediator.Send(new Update.Command(request)));
        }
    }
}
