using System.ComponentModel.DataAnnotations;

namespace MySchoolWebApi.Models.Requests;

public struct CreateStudentRequest
{
    [Required]
    public string Name { get; set; }
    [EmailAddress]
    public string Email { get; set; }
}