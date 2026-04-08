namespace SchoolApi.Models;

public class Grade(string value, CourseInstance courseInstance, Student student)
{
    public int Id { get; } = new Random().Next(1, 9999);
    public string Value { get; set; } = value;
    public CourseInstance CourseInstance { get; } = courseInstance;
    public Student Student { get; } = student;
}