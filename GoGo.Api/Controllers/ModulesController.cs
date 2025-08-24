using GoGo.Api.Contracts;
using GoGo.Application.Features.Courses.Module.Commands;
using GoGo.Application.Features.Courses.Module.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoGo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModulesController : ControllerBase
    {
        private readonly ISender _mediator;

        public ModulesController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("{courseId}/modules")]
        public async Task<IActionResult> CreateModule(Guid courseId, [FromBody] CreateModuleRequest request)
        {

            var command = new CreateModuleCommand(
                request.Title,
                request.Description,
                courseId // Lấy courseId từ URL
            );


            var result = await _mediator.Send(command);

            // 3. Trả về response 201 Created
            // Giả sử bạn sẽ có một ModulesController với endpoint GetModuleById
            return Ok(result);
        }
        [HttpGet("{courseId}/modules")]
        public async Task<IActionResult> GetAllModuleByCourseId(Guid courseId)
        {

            var command = new GetAllModuleByCourseIdQuery(
                courseId
            );

            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("{moduleId}")]
        public async Task<IActionResult> UpdateModuleById(Guid moduleId, [FromBody] UpdateModuleRequest request)
        {
            var command = new UpdateModuleCommand(
               moduleId,
               request.Title,
               request.Description
            );
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{moduleId}")]
        public async Task<IActionResult> DeleteModule(Guid moduleId)
        {
            await _mediator.Send(new DeleteModuleCommand(moduleId));
            return NoContent();
        }
    }
}
