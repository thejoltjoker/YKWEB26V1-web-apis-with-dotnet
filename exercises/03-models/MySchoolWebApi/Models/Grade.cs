namespace MySchoolWebApi.Models;

public class Grade(int id, string value, int courseInstanceId, int studentId)
{
    public int Id { get; set; } = id;
    public string Value { get; set; } = value;
    public int CourseInstanceId { get; set; } = courseInstanceId;
    public int StudentId { get; set; } = studentId;
}