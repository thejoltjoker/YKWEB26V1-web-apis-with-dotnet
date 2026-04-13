using MySchoolWebApi.Models;
using MySchoolWebApi.Models.Requests;
using MySchoolWebApi.Repositories;

namespace MySchoolWebApi.Services;

public interface IStudentsService
{
    public List<Student> FindAll();
    public Student? FindOneById(string id);
    public Student Create(CreateStudentRequest data);
    public Student? Update(string id, UpdateStudentRequest data);
    public bool Delete(string id);
}

public class StudentsService(IStudentsRepository studentsRepository) : IStudentsService
{
    private readonly IStudentsRepository _studentsRepository = studentsRepository;


    public List<Student> FindAll() => _studentsRepository.FindAll();


    public Student? FindOneById(string id) => _studentsRepository.FindOneById(id);


    public Student Create(CreateStudentRequest data)
    {
        Student student = new(data.Name, data.Email);
        bool isCreated = _studentsRepository.Create(student);
        if (isCreated) return student;
        throw new Exception("Something went wrong when trying to create student");
    }

    public Student? Update(string id, UpdateStudentRequest data) => _studentsRepository.Update(id, data);


    public bool Delete(string id) => _studentsRepository.Delete(id);
}