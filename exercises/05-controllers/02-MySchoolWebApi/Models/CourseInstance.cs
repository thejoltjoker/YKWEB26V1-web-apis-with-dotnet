namespace MySchoolWebApi.Models;

public class CourseInstance(DateTime startDate, DateTime endDate, string courseId, IEnumerable<string> students)
{
    public string Id { get; } = Guid.NewGuid().ToString();
    public DateTime StartDate { get; set; } = startDate;
    public DateTime EndDate { get; set; } = endDate;

    public string CourseId { get; set; } = courseId;
    public IEnumerable<string> Students { get; set; } = students;
}