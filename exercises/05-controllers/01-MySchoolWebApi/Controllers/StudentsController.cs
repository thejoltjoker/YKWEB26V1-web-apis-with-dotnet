using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MySchoolWebApi.Models;

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
            return InternalServerError();
        }
    }
    
    [HttpPost]
    public ActionResult<IEnumerable<Student>> GetAllStudents()
    {
        return Ok(_students);
    }
}