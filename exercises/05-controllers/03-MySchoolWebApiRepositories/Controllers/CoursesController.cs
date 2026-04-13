using Microsoft.AspNetCore.Mvc;
using MySchoolWebApi.Models;
using MySchoolWebApi.Models.Requests;
using MySchoolWebApi.Services;

namespace MySchoolWebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CoursesController(ICoursesService coursesService) : ControllerBase
{
    private readonly ICoursesService _coursesService = coursesService;


    [HttpGet]
    public ActionResult<IEnumerable<Course>> GetAllCourses()
    {
        try
        {
            var courses = _coursesService.FindAll();
            return Ok(courses);
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }


    [HttpGet]
    [Route("{id}")]
    public ActionResult<Course> GetOneCourse(string id)
    {
        try
        {
            Course? course = _coursesService.FindOneById(id);
            if (course == null) return NotFound($"Course {id} not found");
            return Ok(course);
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }


    [HttpPost]
    public ActionResult CreateCourse(CreateCourseRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Title) || string.IsNullOrWhiteSpace(request.Description))
            return BadRequest("All fields are required");
        try
        {
            Course created = _coursesService.Create(request);
            return Created($"/courses/{created.Id}", created);
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
            Course? result = _coursesService.Update(id, request);
            if (result == null) return NotFound($"Course {id} not found");
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
            bool isDeleted = _coursesService.Delete(id);
            if (!isDeleted) return NotFound($"Course {id} not found");
            return NoContent();
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }
}