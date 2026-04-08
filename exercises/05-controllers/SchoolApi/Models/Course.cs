namespace SchoolApi.Models;

public class Course(string title, string description)
{
    public string Id { get; } = Guid.NewGuid().ToString();
    public string Title { get; set; } = title;
    public string Description { get; set; } = description;
}