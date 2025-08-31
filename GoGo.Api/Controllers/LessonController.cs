using GoGo.Api.Contracts;
using GoGo.Application.Features.Courses.Lesson.Commands;
using GoGo.Application.Features.Courses.Lesson.Queries;
using GoGo.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoGo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonController : ControllerBase
    {
        private readonly ISender _mediator;

        public LessonController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("{moduleId}/Lesson")]
        public async Task<IActionResult> CreateLesson(Guid moduleId, [FromBody] CreateLessonRequest request)
        {
            var command = new CreateLessonCommand(
                request.Title,
                request.Description,
                request.VideoUrl,
                request.Content,
                request.Duration,
                request.DisplayOrder,
                moduleId
            );
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("{moduleId}/Lesson")]
        public async Task<IActionResult> GetLessonOfModule(Guid moduleId)
        {
            var result = await _mediator.Send(new GetLessonOfModuleQuery(moduleId));
            return Ok(result);
        }
            
        [HttpGet("{lessonId}")]
        public async Task<IActionResult> GetDetailLesson(Guid lessonId)
        {
            var result = await _mediator.Send(new GetDetailLessonByIdQuery(lessonId));
            return Ok(result);
        }

        [HttpPut("{lessonId}")]
        public async Task<IActionResult> UpdateLesson(Guid lessonId, [FromBody] UpdateLessonRequest request)
        {
            var command = new UpdateLessonCommand
            (
                lessonId,
                request.Title,
                request.Description,
                request.VideoUrl,
                request.Content,
                request.Duration,
                request.DisplayOrder
            );
            await _mediator.Send(command);
            return Ok();
        }

        [HttpDelete("{lessonId}")]
        public async Task<IActionResult> DeleteLesson(Guid lessonId)
        {
            await _mediator.Send(new DeleteLessonCommand(lessonId));
            return Ok();
        }

     
    }
}
