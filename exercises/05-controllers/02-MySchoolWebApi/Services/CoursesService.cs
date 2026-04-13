using MySchoolWebApi.Models;
using MySchoolWebApi.Models.Requests;

namespace MySchoolWebApi.Services;

public interface ICoursesService
{
    public List<Course> FindAll();
    public Course? FindOneById(string id);
    public Course Create(CreateCourseRequest data);
    public Course? Update(string id, UpdateCourseRequest data);
    public bool Delete(string id);
}

public class CoursesService : ICoursesService
{
    private List<Course> _courses =
    [
        new("Intro to C#", "Get started with C#."),
        new("Advanced C#", "Deep dive into advanced C# concepts."),
        new("Web Development", "Learn to build web applications with .NET."),
        new("Database Fundamentals", "Introduction to SQL and relational databases."),
        new("Software Architecture", "Explore software design and architecture patterns.")
    ];

    public List<Course> FindAll() => _courses;


    public Course? FindOneById(string id) => _courses.FirstOrDefault(c => c.Id == id);


    public Course Create(CreateCourseRequest data)
    {
        Course newCourse = new(data.Title, data.Description);
        _courses.Add(newCourse);
        return newCourse;
    }

    public Course? Update(string id, UpdateCourseRequest data)
    {
        Course? course = FindOneById(id);
        if (course == null) return null;
        if (!string.IsNullOrWhiteSpace(data.Title)) course.Title = data.Title;
        if (!string.IsNullOrWhiteSpace(data.Description)) course.Description = data.Description;
        return course;
    }

    public bool Delete(string id)
    {
        Course? course = FindOneById(id);
        if (course == null) return false;
        _courses.Remove(course);
        return true;
    }
}