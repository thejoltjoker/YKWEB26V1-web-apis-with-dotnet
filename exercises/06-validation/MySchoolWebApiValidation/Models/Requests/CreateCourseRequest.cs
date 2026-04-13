using System.ComponentModel.DataAnnotations;

namespace MySchoolWebApi.Models.Requests;

public struct CreateCourseRequest
{
    [MinLength(3)]
    public string Title { get; set; }
    public string Description { get; set; }
    [Required]
    public string Code { get; set; }
    
}