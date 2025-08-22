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
            var command = new UpdateCourseCommand(id, request.Name, request.Description, request.SkillLevel);
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

        // --- ENDPOINT MỚI ĐỂ TẠO MODULE ---
        // POST /api/courses/{courseId}/modules
        [HttpPost("{courseId}/modules")]
        public async Task<IActionResult> CreateModule(Guid courseId, [FromBody] CreateModuleRequest request)
        {
            // 1. Tạo command từ dữ liệu của route và request body
            var command = new CreateModuleCommand(
                request.Title,
                request.Description,
                courseId // Lấy courseId từ URL
            );

            // 2. Gửi command cho MediatR và chờ kết quả
            var result = await _mediator.Send(command);

            // 3. Trả về response 201 Created
            // Giả sử bạn sẽ có một ModulesController với endpoint GetModuleById
            return CreatedAtAction("GetModuleById", "Modules", new { id = result.id }, result);
        }
    }
}

// DTO cho request Update, để tách biệt với Command
public record UpdateCourseRequest(string Name, string Description, string SkillLevel);
// DTO cho request tạo Module
public record CreateModuleRequest(string Title, string Description);