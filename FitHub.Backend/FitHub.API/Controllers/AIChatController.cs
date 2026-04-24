using FitHub.Application.Modules.AIChat.Commands;
using Microsoft.AspNetCore.Mvc;

namespace FitHub.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AIChatController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AIChatController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> PostMessage([FromBody] SendMessageCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
