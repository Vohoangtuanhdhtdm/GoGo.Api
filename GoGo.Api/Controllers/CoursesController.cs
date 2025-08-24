using GoGo.Api.Contracts;
using GoGo.Application.Features.Courses.Commands;
using GoGo.Application.Features.Courses.Module.Commands;
using GoGo.Application.Features.Courses.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GoGo.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")] 
    public class CoursesController : ControllerBase
    {
        private readonly ISender _mediator;

        public CoursesController(ISender mediator)
        {
            _mediator = mediator;
        }

        // POST /api/courses
        [HttpPost]
        public async Task<IActionResult> CreateCourse([FromBody] CreateCourseCommand command)
        {
            var courseId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetCourseById), new { id = courseId }, courseId);
        }

        // PUT /api/courses/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCourse(Guid id, [FromBody] UpdateCourseRequest request)
        {
           
            var command = new UpdateCourseCommand(
                id,
                request.Name,
                request.Description,
                request.SkillLevel,
                request.ThumbnailUrl,
                request.Price,
                request.PriceSale
            );
            await _mediator.Send(command);
            return NoContent(); // 204 No Content
        }

        // DELETE /api/courses/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(Guid id)
        {
            await _mediator.Send(new DeleteCourseCommand(id));
            return NoContent(); // 204 No Content
        }

        // GET /api/courses
        [HttpGet]
        public async Task<IActionResult> GetAllCourses()
        {
            var courses = await _mediator.Send(new GetAllCoursesQuery());
            return Ok(courses);
        }

        // GET /api/courses/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCourseById(Guid id)
        {
            var course = await _mediator.Send(new GetCourseByIdQuery(id));
            return course != null ? Ok(course) : NotFound();
        }

     
    }
}

