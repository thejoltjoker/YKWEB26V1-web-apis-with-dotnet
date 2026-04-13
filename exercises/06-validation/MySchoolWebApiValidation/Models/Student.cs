namespace MySchoolWebApi.Models;

public class Student(string name, string email)
{
    public string Id { get; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = name;
    public string Email { get; set; } = email;
}