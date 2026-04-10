using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MySchoolWebApi.Models;
using MySchoolWebApi.Models.Requests;

namespace MySchoolWebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class StudentsController : ControllerBase
{
    private List<Student> _students =
    [
        new("John Doe", "john.doe@example.com"),
        new("Jane Smith", "jane.smith@example.com"),
        new("Alice Johnson", "alice.johnson@example.com"),
        new("Bob Lee", "bob.lee@example.com"),
        new("Maria Stevenson", "maria.stevenson@example.com")
    ];

    [HttpGet]
    public ActionResult<IEnumerable<Student>> GetAllStudents()
    {
        return Ok(_students);
    }

    [HttpGet]
    [Route("{id}")]
    public ActionResult<IEnumerable<Student>> GetOneStudent(string id)
    {
        try
        {
            Student? student = _students.FirstOrDefault(s => s.Id == id);
            if (student == null) return NotFound();
            return Ok(student);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Problem(e.Message);
        }
    }
    
    [HttpPost]
    public ActionResult<IEnumerable<Student>> CreateStudent(CreateStudentRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.Email))
            return BadRequest("All fields are required");

        try
        {
            Student student = new(request.Name, request.Email);
            _students.Add(student);
            return Created($"/students/{student.Id}", student);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Problem(e.Message);
        }
    }
    
    [HttpPut]
    [Route("{id}")]
    public ActionResult<IEnumerable<Student>> UpdateStudent(string id, UpdateStudentRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name) && string.IsNullOrWhiteSpace(request.Email))
            return BadRequest("No changes made");

        try
        {
            var student = _students.FirstOrDefault(s => s.Id == id);

            if (student == null) return NotFound("Student not found");
            if (!string.IsNullOrWhiteSpace(request.Email)) student.Email = request.Email;
            if (!string.IsNullOrWhiteSpace(request.Name)) student.Name = request.Name;

            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Problem(e.Message);
        }
    }
    
    [HttpDelete]
    [Route("{id}")]
    public ActionResult<IEnumerable<Student>> DeleteStudent(string id)
    {
        try
        {
            Student? student = _students.FirstOrDefault(s => s.Id == id);
            if (student == null) return NotFound();
            _students.Remove(student);
            return NoContent();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Problem(e.Message);
        }
    }
}