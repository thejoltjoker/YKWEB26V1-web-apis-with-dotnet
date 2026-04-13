namespace MySchoolWebApi.Models;

public class Grade( string value, string courseInstanceId, string studentId)
{
    public string Id { get; } = Guid.NewGuid().ToString();
    public string Value { get; set; } = value;
    public string CourseInstanceId { get; set; } = courseInstanceId;
    public string StudentId { get; set; } = studentId;
}