using Microsoft.AspNetCore.Mvc;
using MySchoolWebApi.Models;
using MySchoolWebApi.Models.Requests;

namespace MySchoolWebApi.Controllers;

[ApiController]
public class CourseInstancesController : ControllerBase

{
    private List<CourseInstance> courseInstances =
    [
        new(new DateTime(2026, 01, 01), new DateTime(2026, 03, 31), courses[0].Id, [students[0].Id]),
        // new(new DateTime(2026, 02, 01), new DateTime(2026, 04, 30), courses[1].Id,
        //     [students[0].Id, students[1].Id, students[2].Id]),
        // new(new DateTime(2026, 03, 01), new DateTime(2026, 05, 31), courses[2].Id, [students[2].Id, students[3].Id]),
        // new(new DateTime(2026, 04, 01), new DateTime(2026, 06, 30), courses[3].Id,
        //     [students[0].Id, students[3].Id, students[4].Id]),
        // new(new DateTime(2026, 05, 01), new DateTime(2026, 07, 31), courses[4].Id, [students[4].Id])
    ];

    [HttpGet]
    public ActionResult<IEnumerable<CourseInstance>> GetAllCourses(
        [FromQuery] DateTime? startDate,
        [FromQuery] DateTime? endDate)
    {
        try
        {
            List<CourseInstance> filteredCourseInstances = [];
            foreach (CourseInstance courseInstance in courseInstances)
            {
                if (startDate != null && endDate != null)
                {
                    if (courseInstance.StartDate <= endDate && courseInstance.EndDate >= startDate)
                    {
                        filteredCourseInstances.Add(courseInstance);
                    }
                }
                else if (startDate != null)
                {
                    if (courseInstance.EndDate >= startDate)
                    {
                        filteredCourseInstances.Add(courseInstance);
                    }
                }
                else if (endDate != null)
                {
                    if (courseInstance.StartDate <= endDate)
                    {
                        filteredCourseInstances.Add(courseInstance);
                    }
                }
                else
                {
                    filteredCourseInstances.Add(courseInstance);
                }
            }

            return Ok(filteredCourseInstances);
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
            CourseInstance? course = courseInstances.FirstOrDefault(s => s.Id == id);
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
            CourseInstance newCourseInstance =
                new(request.StartDate, request.EndDate, request.CourseId, request.Students);
            courseInstances.Add(newCourseInstance);
            return Created($"/courseinstances/{newCourseInstance.Id}", newCourseInstance);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Problem(e.Message);
        }
    }

    [HttpPut]
    public ActionResult UpdateCourseInstance(string id, UpdateCourseInstanceRequest request)
    {
        try
        {
            // Validate data
            CourseInstance? result = courseInstances.FirstOrDefault(c => c.Id == id);
            if (result == null) return NotFound($"CourseInstance {id} not found");
            var newStart = request.StartDate ?? result.StartDate;
            var newEnd = request.EndDate ?? result.EndDate;
            if (newStart > newEnd) return BadRequest("Start date must be before end date");
            if (request.CourseId != null && courses.FirstOrDefault(c => c.Id == request.CourseId) == null)
                return NotFound($"Course {request.CourseId} not found");
            if (request.Students != null)
            {
                foreach (string studentId in request.Students)
                {
                    if (students.FirstOrDefault(s => s.Id == studentId) == null)
                        return NotFound($"Student {studentId} not found");
                }
            }

            // Update data
            if (request.StartDate.HasValue) result.StartDate = request.StartDate.Value;
            if (request.EndDate.HasValue) result.EndDate = request.EndDate.Value;

            if (request.CourseId != null) result.CourseId = request.CourseId;
            if (request.Students != null) result.Students = request.Students;

            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Problem(e.Message);
        }
    }

    []
    [HttpDelete]
    public ActionResult DeleteCourseInstance(string id)
    {
        try
        {
            CourseInstance? courseInstance = courseInstances.FirstOrDefault(c => c.Id == id);
            if (courseInstance == null) return NotFound();
            courseInstances.Remove(courseInstance);
            return NoContent();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Problem(e.Message);
        }
    }
}