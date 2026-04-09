namespace MySchoolWebApi.Models;

public class CourseInstance(int id, DateTime startDate, DateTime endDate, int courseId, IEnumerable<int> students)
{
    public int Id { get; set; } = id;
    public DateTime StartDate { get; set; } = startDate;
    public DateTime EndDate { get; set; } = endDate;

    public int CourseId { get; set; } = courseId;
    public IEnumerable<int> students { get; set; } = students;
}