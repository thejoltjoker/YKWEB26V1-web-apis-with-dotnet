namespace MySchoolWebApi.Models;

public struct GetCoursesQueryParams
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}