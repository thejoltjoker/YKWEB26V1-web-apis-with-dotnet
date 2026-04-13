using MySchoolWebApi.Models;
using MySchoolWebApi.Models.Requests;

namespace MySchoolWebApi.Repositories;

public interface ICourseInstancesRepository
{
    public List<CourseInstance> FindAll(DateTime? startDate, DateTime? endDate);
    public CourseInstance? FindOneById(string id);
    public bool Create(CourseInstance data);
    public CourseInstance? Update(string id, UpdateCourseInstanceRequest data);
    public bool Delete(string id);
}

public class InMemoryCourseInstancesRepository : ICourseInstancesRepository
{
    private List<CourseInstance> _courseInstances =
    [
        new(new DateTime(2026, 01, 01), new DateTime(2026, 03, 31), courses[0].Id, [students[0].Id]),
        new(new DateTime(2026, 02, 01), new DateTime(2026, 04, 30), courses[1].Id,
            [students[0].Id, students[1].Id, students[2].Id]),
        new(new DateTime(2026, 03, 01), new DateTime(2026, 05, 31), courses[2].Id, [students[2].Id, students[3].Id]),
        new(new DateTime(2026, 04, 01), new DateTime(2026, 06, 30), courses[3].Id,
            [students[0].Id, students[3].Id, students[4].Id]),
        new(new DateTime(2026, 05, 01), new DateTime(2026, 07, 31), courses[4].Id, [students[4].Id])
    ];

    public List<CourseInstance> FindAll(DateTime? startDate, DateTime? endDate)
    {
        List<CourseInstance> filteredCourseInstances = [];
        foreach (CourseInstance courseInstance in _courseInstances)
        {
            if (startDate != null && endDate != null)
            {
                if (courseInstance.StartDate <= endDate && courseInstance.EndDate >= startDate)
                {
                    filteredCourseInstances.Add(courseInstance);
                }
            }
            else if (startDate != null)
            {
                if (courseInstance.EndDate >= startDate)
                {
                    filteredCourseInstances.Add(courseInstance);
                }
            }
            else if (endDate != null)
            {
                if (courseInstance.StartDate <= endDate)
                {
                    filteredCourseInstances.Add(courseInstance);
                }
            }
            else
            {
                filteredCourseInstances.Add(courseInstance);
            }
        }

        return filteredCourseInstances;
    }

    public CourseInstance? FindOneById(string id)
    {
        CourseInstance? course = _courseInstances.FirstOrDefault(s => s.Id == id);
        return course;
    }

    public CourseInstance Create(CourseInstance data)
    {
        CourseInstance newCourseInstance =
            new(data.StartDate, data.EndDate, data.CourseId, data.Students);
        _courseInstances.Add(newCourseInstance);
        return newCourseInstance;
    }

    public CourseInstance? Update(string id, UpdateCourseInstanceRequest data)
    {
        CourseInstance? result = FindOneById(id);
        if (result == null) return null;
        var newStart = data.StartDate ?? result.StartDate;
        var newEnd = data.EndDate ?? result.EndDate;
        if (newStart > newEnd) return null;
        // TODO Validate related course and students
        // if (data.CourseId != null && courses.FirstOrDefault(c => c.Id == data.CourseId) == null)
        //     return NotFound($"Course {data.CourseId} not found");
        // if (data.Students != null)
        // {
        //     foreach (string studentId in data.Students)
        //     {
        //         if (students.FirstOrDefault(s => s.Id == studentId) == null)
        //             return NotFound($"Student {studentId} not found");
        //     }
        // }

        // Update data
        if (data.StartDate.HasValue) result.StartDate = data.StartDate.Value;
        if (data.EndDate.HasValue) result.EndDate = data.EndDate.Value;

        if (data.CourseId != null) result.CourseId = data.CourseId;
        if (data.Students != null) result.Students = data.Students;
        return result;
    }

    public bool Delete(string id)
    {
        CourseInstance? courseInstance = FindOneById(id);
        if (courseInstance == null) return false;
        _courseInstances.Remove(courseInstance);
        return true;
    }
}