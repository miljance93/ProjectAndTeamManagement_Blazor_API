using ApplicationService.Comments;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace API.SignalR
{
    public class ChatHub : Hub
    {
        private readonly IMediator _mediator;

        public ChatHub(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task SendMessage(string userInput, string messageInput)
        {
        //    var command = new Create.Command(messageInput, userInput);
        //    var comment = await _mediator.Send(command);

            await Clients.All.SendAsync("ReceiveMessage", userInput, messageInput);
        }
    }
}
