using Microsoft.AspNetCore.Mvc;
using SchoolApi.Models;
using SchoolApi.Models.Requests;
using SchoolApi.Services;

namespace SchoolApi.Controllers;

[ApiController]
[Route("[controller]")]
public class StudentsController(IStudentsService service) : ControllerBase
{
    private readonly IStudentsService _service = service;

    [HttpGet]
    public ActionResult<List<Student>> GetAll()
    {
        var students = _service.GetAll();
        return Ok(students);
    }

    [HttpGet]
    [Route("{id}")]
    public ActionResult<Student?> GetOneById(string id)
    {
        var student = _service.GetOneById(id);
        if (student == null) return NotFound($"Student {id} not found");
        return student;
    }

    [HttpPost]
    public ActionResult<string> Create([FromBody] CreateStudentRequest request)
    {
        try
        {
            var studentId = _service.Create(request);
            return CreatedAtAction(nameof(GetOneById), new { id = studentId }, studentId);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpPut]
    [Route("{id}")]
    public ActionResult<string> Update(string id, [FromBody] UpdateStudentRequest request)
    {
        try
        {
            var result = _service.Update(id, request);
            if (!result) return NotFound($"Student {id} not found");
            return NoContent();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}