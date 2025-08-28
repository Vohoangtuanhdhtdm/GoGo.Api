using GoGo.Api.Contracts;
using GoGo.Application.Features.UserProfile.Commands;
using GoGo.Application.Features.UserProfile.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoGo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ISender _mediator;
        public UserController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserProfileById(Guid userId)
        {
            var user = await _mediator.Send(new GetUserProfileByIdQuery(userId));
            return Ok(user);
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUserProfile(Guid userId, [FromBody] UserRequest userUpdate)
        {
            var command = new UpdateUserProfileCommand(userId,userUpdate.fullName, userUpdate.AvatarUrl);

            await _mediator.Send(command);
            return Ok();
        }
    }
}
