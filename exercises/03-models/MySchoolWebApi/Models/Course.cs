namespace MySchoolWebApi.Models;

public class Course(int id, string name, string description)
{
    public int Id { get; set; } = id;
    public string Title { get; set; } = name;
    public string Description { get; set; } = description;
}