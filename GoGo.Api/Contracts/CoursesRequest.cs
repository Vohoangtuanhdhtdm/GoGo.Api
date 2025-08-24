namespace GoGo.Api.Contracts
{
    // Module
    public record CreateModuleRequest(string Title, string Description);
    public record AddModuleRequest(string Title, string? Description);
    public record UpdateModuleRequest(string Title, string Description);

    // Course
    public record UpdateCourseRequest(
        string Name,
        string Description,
        string SkillLevel,
        string ThumbnailUrl,
        decimal? Price,
        decimal? PriceSale
    );

    // Lesson
    public record CreateLessonRequest(
        string Title,
        string? Description,
        string VideoUrl,
        string? Content,
        int Duration,
        int DisplayOrder
     
    );

    public record AddLessonRequest(
        string Title,
        string? Description,
        string VideoUrl,
        string? Content,
        int Duration,
        int DisplayOrder
    );

    public record UpdateLessonRequest(
        string Title,
        string? Description,
        string VideoUrl,
        string? Content,
        int Duration,
        int DisplayOrder
    );
}
