using SchoolApi.Models;
using SchoolApi.Models.Requests;

namespace SchoolApi.Repositories;

public interface IStudentRepository
{
    public List<Student> GetAll();
    public Student? GetOne(string id);
    public Student Create(CreateStudentRequest request);
    public bool Update(string id, UpdateStudentRequest request);
    public bool Delete(string id);
}

public class InMemoryStudentRepository : IStudentRepository
{
    private readonly List<Student> _students =
    [
        new(
            "John Doe",
            "john.doe@example.com"),
        new(
            "Jane Doe",
            "jane.doe@example.com"),
        new(
            "Sarah Doe",
            "Sarah.doe@example.com")
    ];

    public List<Student> GetAll()
    {
        return _students;
    }

    public Student? GetOne(string id)
    {
        try
        {
            var student = _students.FirstOrDefault(s => s.Id == id);
            return student;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public bool Update(string id, UpdateStudentRequest request)
    {
        try
        {
            var studentIndex = _students.FindIndex(s => s.Id == id);
            if (studentIndex == -1) return false;
            if (request.Name != null) _students[studentIndex].Name = request.Name;
            if (request.Email != null) _students[studentIndex].Email = request.Email;
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public bool Delete(string id)
    {
        var index = _students.FindIndex(s => s.Id == id);
        _students.RemoveAt(index);
        return true;
    }

    public Student Create(CreateStudentRequest request)
    {
        try
        {
            Student newStudent = new(request.Name, request.Email);
            _students.Add(newStudent);
            return newStudent;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}