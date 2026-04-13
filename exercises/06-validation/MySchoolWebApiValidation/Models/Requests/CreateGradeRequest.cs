namespace MySchoolWebApi.Models.Requests;

public struct CreateGradeRequest
{
    public string Value { get; set; }
    public string CourseInstanceId { get; set; }
    public string StudentId { get; set; }
}