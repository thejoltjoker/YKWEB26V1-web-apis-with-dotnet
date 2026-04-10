namespace MySchoolWebApi.Models.Requests;

public struct CreateCourseRequest
{
    public string Title { get; set; }
    public string Description { get; set; }
}