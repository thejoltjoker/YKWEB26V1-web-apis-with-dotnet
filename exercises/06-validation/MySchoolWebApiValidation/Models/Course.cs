namespace MySchoolWebApi.Models;

public class Course( string name, string description, string code)
{
    public string Id { get; } = Guid.NewGuid().ToString();
    public string Title { get; set; } = name;
    public string Description { get; set; } = description;
    public string Code { get; set; } = code;
}