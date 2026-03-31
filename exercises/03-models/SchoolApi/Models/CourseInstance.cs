namespace SchoolApi.Models;

public class CourseInstance(string id, DateTime startDate, DateTime endDate, Course course, List<Student> students)
{
    public string Id { get; set; } = id;
    public DateTime StartDate { get; set; } = startDate;
    public DateTime EndDate { get; set; } = endDate;
    public Course Course { get; } = course;
    public List<Student> Students { get; } = students;
}