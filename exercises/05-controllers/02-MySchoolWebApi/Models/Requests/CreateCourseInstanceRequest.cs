using System.ComponentModel.DataAnnotations;

namespace MySchoolWebApi.Models.Requests;

public struct CreateCourseInstanceRequest
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; } 

    public string CourseId { get; set; } 
    public IEnumerable<string> Students { get; set; } 
}