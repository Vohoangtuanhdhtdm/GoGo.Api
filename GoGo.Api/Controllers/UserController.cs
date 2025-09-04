using Azure.Core;
using GoGo.Api.Contracts;
using GoGo.Application.Features.UserProfile.Commands;
using GoGo.Application.Features.UserProfile.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace GoGo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ISender _mediator;
        private readonly UserManager<IdentityUser<Guid>> _userManager;
        public UserController(ISender mediator , UserManager<IdentityUser<Guid>> userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _mediator.Send(new GetAllUserQuery());
            return Ok(users);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserProfileById(Guid userId)
        {
            var user = await _mediator.Send(new GetUserProfileByIdQuery(userId));
            if (user == null)
                return NotFound($"User with id {userId} not found");

            var userIdentity = await _userManager.FindByEmailAsync(user.Email);
            if (userIdentity == null)
                return NotFound("User identity not found");

            var role = await _userManager.GetRolesAsync(userIdentity);
            return Ok(new { user, role });
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
