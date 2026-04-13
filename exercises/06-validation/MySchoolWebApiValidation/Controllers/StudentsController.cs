using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MySchoolWebApi.Models;
using MySchoolWebApi.Models.Requests;
using MySchoolWebApi.Services;

namespace MySchoolWebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class StudentsController(IStudentsService studentsService) : ControllerBase
{
    private readonly IStudentsService _studentsService = studentsService;

    [HttpGet]
    public ActionResult<List<Student>> GetAllStudents()
    {
        List<Student> students = _studentsService.FindAll();
        return Ok(students);
    }

    [HttpGet]
    [Route("{id}")]
    public ActionResult<List<Student>> GetOneStudent(string id)
    {
        try
        {
            Student? student = _studentsService.FindOneById(id);
            if (student == null) return NotFound();
            return Ok(student);
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }

    [HttpPost]
    public ActionResult<List<Student>> CreateStudent(CreateStudentRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.Email))
            return BadRequest("All fields are required");
        if (!request.Email.Contains("@")) return BadRequest("Invalid email");
        try
        {
            Student student = _studentsService.Create(request);
            return Created($"/students/{student.Id}", student);
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }

    [HttpPut]
    [Route("{id}")]
    public ActionResult<Student> UpdateStudent(string id, UpdateStudentRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name) && string.IsNullOrWhiteSpace(request.Email))
            return BadRequest("No changes made");

        try
        {
            Student? student = _studentsService.Update(id, request);
            if (student == null) return NotFound($"Student {id} not found");

            return Ok(student);
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }

    [HttpDelete]
    [Route("{id}")]
    public ActionResult<List<Student>> DeleteStudent(string id)
    {
        try
        {
            bool result = _studentsService.Delete(id);
            if (!result) return NotFound($"Student {id} not found");
            return NoContent();
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }
}