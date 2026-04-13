namespace MySchoolWebApi.Models;

public class Course( string name, string description)
{
    public string Id { get; } = Guid.NewGuid().ToString();
    public string Title { get; set; } = name;
    public string Description { get; set; } = description;
}