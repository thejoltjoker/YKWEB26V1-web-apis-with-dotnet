using Microsoft.AspNetCore.Mvc;
using MySchoolWebApi.Models;
using MySchoolWebApi.Models.Requests;

namespace MySchoolWebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CoursesController : ControllerBase
{
    private List<Course> _courses =
    [
        new("Intro to C#", "Get started with C#."),
        new("Advanced C#", "Deep dive into advanced C# concepts."),
        new("Web Development", "Learn to build web applications with .NET."),
        new("Database Fundamentals", "Introduction to SQL and relational databases."),
        new("Software Architecture", "Explore software design and architecture patterns.")
    ];

    [HttpGet]
    public ActionResult<IEnumerable<Course>> GetAllCourses()
    {
        try
        {
            return Ok(_courses);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Problem();
        }
    }


    [HttpGet]
    [Route("{id}")]
    public ActionResult<Course> GetOneCourse(string id)
    {
        try
        {
            Course? course = _courses.FirstOrDefault(s => s.Id == id);
            if (course == null) return NotFound($"Course {id} not found");
            return Ok(course);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Problem();
        }
    }


    [HttpPost]
    public ActionResult CreateCourse(CreateCourseRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Title) || string.IsNullOrWhiteSpace(request.Description))
            return BadRequest("All fields are required");
        try
        {
            Course newCourse = new(request.Title, request.Description);
            _courses.Add(newCourse);
            return Created($"/courses/{newCourse.Id}", newCourse);
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }

    [HttpPut]
    [Route("{id}")]
    public ActionResult UpdateCourse(string id, UpdateCourseRequest request)
    {
        try
        {
            Course? result = _courses.FirstOrDefault(c => c.Id == id);
            if (result == null) return NotFound($"Course {id} not found");
            if (!string.IsNullOrWhiteSpace(request.Title)) result.Title = request.Title;
            if (!string.IsNullOrWhiteSpace(request.Description)) result.Description = request.Description;
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Problem();
        }
    }

    [HttpDelete]
    [Route("{id}")]
    public ActionResult DeleteCourse(string id)

    {
        try
        {
            Course? course = _courses.FirstOrDefault(c => c.Id == id);
            if (course == null) return NotFound();
            _courses.Remove(course);
            return NoContent();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Problem();
        }
    }
}