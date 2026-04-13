using MySchoolWebApi.Models;
using MySchoolWebApi.Models.Requests;
using MySchoolWebApi.Repositories;

namespace MySchoolWebApi.Services;

public interface ICourseInstancesService
{
    public List<CourseInstance> FindAll(DateTime? startDate, DateTime? endDate);
    public CourseInstance? FindOneById(string id);
    public CourseInstance Create(CreateCourseInstanceRequest data);
    public CourseInstance? Update(string id, UpdateCourseInstanceRequest data);
    public bool Delete(string id);
}

public class CourseInstancesService(ICourseInstancesRepository courseInstancesRepository) : ICourseInstancesService
{
    private readonly ICourseInstancesRepository _courseInstancesRepository = courseInstancesRepository;

    public List<CourseInstance> FindAll(DateTime? startDate, DateTime? endDate)
    {
        return _courseInstancesRepository.FindAll(startDate, endDate);
    }

    public CourseInstance? FindOneById(string id)
    {
        return _courseInstancesRepository.FindOneById(id);
    }

    public CourseInstance Create(CreateCourseInstanceRequest data)
    {
        CourseInstance newCourseInstance =
            new(data.StartDate, data.EndDate, data.CourseId, data.Students);
        _courseInstancesRepository.Create(newCourseInstance);
        return newCourseInstance;
    }

    public CourseInstance? Update(string id, UpdateCourseInstanceRequest data)
    {
        return _courseInstancesRepository.Update(id, data);
    }

    public bool Delete(string id)
    {
        return _courseInstancesRepository.Delete(id);
    }
}