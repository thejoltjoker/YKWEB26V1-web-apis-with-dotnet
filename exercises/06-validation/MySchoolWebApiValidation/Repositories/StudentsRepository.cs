using MySchoolWebApi.Models;
using MySchoolWebApi.Models.Requests;

namespace MySchoolWebApi.Repositories;

public interface IStudentsRepository
{
    
    public List<Student> FindAll();
    public Student? FindOneById(string id);
    public bool Create(Student data);
    public Student? Update(string id, UpdateStudentRequest data);
    public bool Delete(string id);
}

public class InMemoryStudentsRepository : IStudentsRepository
{
    private List<Student> _students =
    [
        new("John Doe", "john.doe@example.com"),
        new("Jane Smith", "jane.smith@example.com"),
        new("Alice Johnson", "alice.johnson@example.com"),
        new("Bob Lee", "bob.lee@example.com"),
        new("Maria Stevenson", "maria.stevenson@example.com")
    ];
    public List<Student> FindAll()
    {
        return _students;
    }

    
    public Student? FindOneById(string id)
    {
        Student? student = _students.FirstOrDefault(s => s.Id == id);
        return student;
    }
    
    public bool Create(Student data)
    {

        try
        {
            Student student = new(data.Name, data.Email);
            _students.Add(student);
            return true;
        }
        catch
        {
            throw;
        }
    }

    public Student? Update(string id, UpdateStudentRequest data)
    {
        var student = FindOneById(id);
        if (student == null) return null;
        if (!string.IsNullOrWhiteSpace(data.Email)) student.Email = data.Email;
        if (!string.IsNullOrWhiteSpace(data.Name)) student.Name = data.Name;
        return student;
    }

    public bool Delete(string id)
    {
        var student = FindOneById(id);
        if (student == null) return false;
        _students.Remove(student);
        return true;
    }
}