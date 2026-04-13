using Microsoft.AspNetCore.Http.HttpResults;
using MySchoolWebApi.Models;
using MySchoolWebApi.Models.Requests;
using MySchoolWebApi.Repositories;

namespace MySchoolWebApi.Services;

public interface ICoursesService
{
    public List<Course> FindAll();
    public Course? FindOneById(string id);
    public Course Create(CreateCourseRequest data);
    public Course? Update(string id, UpdateCourseRequest data);
    public bool Delete(string id);
}

public class CoursesService(ICoursesRepository coursesRepository) : ICoursesService
{
    private readonly ICoursesRepository _coursesRepository = coursesRepository;


    public List<Course> FindAll() => _coursesRepository.FindAll();


    public Course? FindOneById(string id) => _coursesRepository.FindOneById(id);


    public Course Create(CreateCourseRequest data)
    {
        Course newCourse = new(data.Title, data.Description);
        bool isCreated = _coursesRepository.Create(newCourse);
        if (!isCreated) throw new Exception("Couldn't create course"); 
        return newCourse;
    }

    public Course? Update(string id, UpdateCourseRequest data) => _coursesRepository.Update(id, data);

    public bool Delete(string id) => _coursesRepository.Delete(id);
}