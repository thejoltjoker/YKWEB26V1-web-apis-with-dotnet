namespace SchoolApi.Models;

public class Course(string id, string title, string description)
{
    public string Id { get; set; } = id;
    public string Title { get; set; } = title;
    public string Description { get; set; } = description;
}