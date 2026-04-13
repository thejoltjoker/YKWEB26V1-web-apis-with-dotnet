namespace MySchoolWebApi.Models.Requests;

public struct UpdateCourseRequest
{
    public string? Title { get; set; }
    public string? Description { get; set; }
}