using Microsoft.AspNetCore.Mvc;
using MySchoolWebApi.Models;
using MySchoolWebApi.Models.Requests;
using MySchoolWebApi.Services;

namespace MySchoolWebApi.Controllers;

[ApiController]
public class CourseInstancesController(ICourseInstancesService courseInstancesService) : ControllerBase
{
    private readonly ICourseInstancesService _courseInstancesService = courseInstancesService;

    [HttpGet]
    public ActionResult<IEnumerable<CourseInstance>> GetAllCourses(
        [FromQuery] DateTime? startDate,
        [FromQuery] DateTime? endDate)
    {
        try
        {
            List<CourseInstance> courseInstances = _courseInstancesService.FindAll(startDate, endDate);

            return Ok(courseInstances);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Problem(e.Message);
        }
    }

    [HttpGet]
    public ActionResult<IEnumerable<CourseInstance>> GetOneCourseInstance(string id)
    {
        try
        {
            CourseInstance? course = _courseInstancesService.FindOneById(id);
            if (course == null) return NotFound($"Course instance {id} not found");
            return Ok(course);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Problem(e.Message);
        }
    }

    [HttpPost]
    public ActionResult CreateCourseInstance(CreateCourseInstanceRequest request)
    {
        if (request.StartDate == default || request.EndDate == default)
            return BadRequest("Date fields are required");
        if (request.CourseId == null || request.Students == null)
            return BadRequest("All fields are required");
        if (courses.FirstOrDefault(c => c.Id == request.CourseId) == null)
            return NotFound($"Course {request.CourseId} not found");
        foreach (string studentId in request.Students)
        {
            if (students.FirstOrDefault(s => s.Id == studentId) == null)
                return NotFound($"Student {studentId} not found");
        }

        if (request.StartDate > request.EndDate) return BadRequest("Start date must be before end date");
        try
        {
            CourseInstance newCourseInstance = _courseInstancesService.Create(request);
            return Created($"/courseinstances/{newCourseInstance.Id}", newCourseInstance);
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }

    [HttpPut]
    public ActionResult UpdateCourseInstance(string id, UpdateCourseInstanceRequest request)
    {
        try
        {
            // Validate data
            var newStart = request.StartDate;
            var newEnd = request.EndDate;
            if (newStart > newEnd) return BadRequest("Start date must be before end date");
            CourseInstance? result = _courseInstancesService.Update(id, request);
            if (result == null) return NotFound($"CourseInstance {id} not found");
            // if (request.CourseId != null && courses.FirstOrDefault(c => c.Id == request.CourseId) == null)
            //     return NotFound($"Course {request.CourseId} not found");
            // if (request.Students != null)
            // {
            //     foreach (string studentId in request.Students)
            //     {
            //         if (students.FirstOrDefault(s => s.Id == studentId) == null)
            //             return NotFound($"Student {studentId} not found");
            //     }
            // }
            return Ok(result);
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }

    
    [HttpDelete]
    public ActionResult DeleteCourseInstance(string id)
    {
        try
        {
            bool isDeleted = _courseInstancesService.Delete(id);
            if (!isDeleted ) return NotFound();
            return NoContent();
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }
}