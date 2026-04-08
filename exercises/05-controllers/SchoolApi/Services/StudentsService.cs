using SchoolApi.Models;
using SchoolApi.Models.Requests;
using SchoolApi.Repositories;

namespace SchoolApi.Services;

public interface IStudentsService
{
    public List<Student> GetAll();
    public Student? GetOneById(string id);
    public string Create(CreateStudentRequest request);
    public bool Update(string id, UpdateStudentRequest request);
    public bool Delete(string id);
}

public class StudentsService : IStudentsService
{
    private readonly IStudentRepository _repository;

    public StudentsService(IStudentRepository repository)
    {
        _repository = repository;
    }


    public List<Student> GetAll()
    {
        try
        {
            return _repository.GetAll();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Student? GetOneById(string id)
    {
        try
        {
            return _repository.GetOne(id);
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
            return _repository.Update(id, request);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public bool Delete(string id)
    {
        try
        {
            return _repository.Delete(id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public string Create(CreateStudentRequest request)
    {
        try
        {
            var newStudent = _repository.Create(request);
            return newStudent.Id;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}