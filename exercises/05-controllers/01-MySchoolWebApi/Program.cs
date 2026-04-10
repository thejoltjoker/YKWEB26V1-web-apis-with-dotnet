using MySchoolWebApi.Models;
using MySchoolWebApi.Models.Requests;
using System.Text.RegularExpressions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

List<Student> students =
[
    new("John Doe", "john.doe@example.com"),
    new("Jane Smith", "jane.smith@example.com"),
    new("Alice Johnson", "alice.johnson@example.com"),
    new("Bob Lee", "bob.lee@example.com"),
    new("Maria Svensson", "maria.svensson@example.com")
];

List<Course> courses =
[
    new("Intro to C#", "Get started with C#."),
    new("Advanced C#", "Deep dive into advanced C# concepts."),
    new("Web Development", "Learn to build web applications with .NET."),
    new("Database Fundamentals", "Introduction to SQL and relational databases."),
    new("Software Architecture", "Explore software design and architecture patterns.")
];


List<CourseInstance> courseInstances =
[
    new(new DateTime(2026, 01, 01), new DateTime(2026, 03, 31), courses[0].Id, [students[0].Id, students[1].Id]),
    new(new DateTime(2026, 02, 01), new DateTime(2026, 04, 30), courses[1].Id,
        [students[0].Id, students[1].Id, students[2].Id]),
    new(new DateTime(2026, 03, 01), new DateTime(2026, 05, 31), courses[2].Id, [students[2].Id, students[3].Id]),
    new(new DateTime(2026, 04, 01), new DateTime(2026, 06, 30), courses[3].Id,
        [students[0].Id, students[3].Id, students[4].Id]),
    new(new DateTime(2026, 05, 01), new DateTime(2026, 07, 31), courses[4].Id, [students[4].Id])
];

List<Grade> grades =
[
    new("A", courseInstances[0].Id, students[0].Id),
    new("B", courseInstances[0].Id, students[1].Id),


    new("B", courseInstances[1].Id, students[0].Id),
    new("A", courseInstances[1].Id, students[1].Id),
    new("C", courseInstances[1].Id, students[2].Id),


    new("B", courseInstances[2].Id, students[2].Id),
    new("A", courseInstances[2].Id, students[3].Id),


    new("D", courseInstances[3].Id, students[0].Id),
    new("C", courseInstances[3].Id, students[3].Id),
    new("B", courseInstances[3].Id, students[4].Id),


    new("A", courseInstances[4].Id, students[4].Id)
];


app.UseHttpsRedirection();

app.MapControllers();

app.MapPost(("/students"), (CreateStudentRequest request) =>
{
    if (string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.Email))
        return Results.BadRequest("All fields are required");

    try
    {
        Student student = new(request.Name, request.Email);
        students.Add(student);
        return Results.Created($"/students/{student.Id}", student);
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
        return Results.InternalServerError();
    }
});
app.MapPut(("/students/{id}"), (string id, UpdateStudentRequest request) =>
{
    if (string.IsNullOrWhiteSpace(request.Name) && string.IsNullOrWhiteSpace(request.Email))
        return Results.BadRequest("No changes made");

    try
    {
        var student = students.FirstOrDefault(x => x.Id == id);

        if (student == null) return Results.NotFound("Student not found");
        if (!string.IsNullOrWhiteSpace(request.Email)) student.Email = request.Email;
        if (!string.IsNullOrWhiteSpace(request.Name)) student.Name = request.Name;

        return Results.Ok();
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
        return Results.InternalServerError();
    }
});


app.MapDelete(("/students/{id}"), (string id) =>
{
    try
    {
        Student? student = students.FirstOrDefault(s => s.Id == id);
        if (student == null) return Results.NotFound();
        students.Remove(student);
        return Results.NoContent();
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
        return Results.InternalServerError();
    }
});
app.MapGet(("/students/{id}/courses"), (string id) =>
{
    Student? student = students.FirstOrDefault(s => s.Id == id);
    if (student == null) return [];
    return courseInstances.FindAll(ci => ci.Students.Contains(student.Id));
});
app.MapGet(("/students/{id}/grades"), (string id) =>
{
    Student? student = students.FirstOrDefault(s => s.Id == id);
    if (student == null) return null;
    return grades.FindAll(g => g.StudentId == student.Id);
});

app.MapGet(("/courses"), (DateTime? startDate, DateTime? endDate) =>
{
    try
    {
        List<CourseInstance> filteredCourseInstances = [];
        foreach (CourseInstance courseInstance in courseInstances)
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


        var courseIds = filteredCourseInstances.Select(ci => ci.CourseId).Distinct();
        List<Course> filteredCourses = courses.Where(c => courseIds.Contains(c.Id)).ToList();
        return Results.Ok(filteredCourses);
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
        return Results.InternalServerError();
    }
});
app.MapGet(("/courses/{id}"), (string id) =>
{
    try
    {
        Course? course = courses.FirstOrDefault(s => s.Id == id);
        if (course == null) return Results.NotFound($"Course {id} not found");
        return Results.Ok(course);
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
        return Results.InternalServerError();
    }
});
app.MapPost(("/courses"), (CreateCourseRequest request) =>
{
    if (string.IsNullOrWhiteSpace(request.Title) || string.IsNullOrWhiteSpace(request.Description))
        return Results.BadRequest("All fields are required");
    try
    {
        Course newCourse = new(request.Title, request.Description);
        courses.Add(newCourse);
        return Results.Created($"/courses/{newCourse.Id}", newCourse);
    }
    catch (Exception e)
    {
        return Results.InternalServerError(e);
    }
});
app.MapPut(("/courses/{id}"), (string id, UpdateCourseRequest request) =>
{
    try
    {
        Course? result = courses.FirstOrDefault(c => c.Id == id);
        if (result == null) return Results.NotFound($"Course {id} not found");
        if (!string.IsNullOrWhiteSpace(request.Title)) result.Title = request.Title;
        if (!string.IsNullOrWhiteSpace(request.Description)) result.Description = request.Description;
        return Results.Ok();
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
        return Results.InternalServerError();
    }
});
app.MapDelete(("/courses/{id}"), (string id) =>
{
    try
    {
        Course? course = courses.FirstOrDefault(c => c.Id == id);
        if (course == null) return Results.NotFound();
        courses.Remove(course);
        return Results.NoContent();
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
        return Results.InternalServerError();
    }
});
app.MapGet(("/courses/{id}/instances"), (string id) =>
{
    try
    {
        List<CourseInstance> result = courseInstances.FindAll(ci => ci.CourseId == id);
        return Results.Ok(result);
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
        return Results.InternalServerError();
    }
});

app.MapGet(("/courseinstances"), (DateTime? startDate, DateTime? endDate) =>
{
    try
    {
        List<CourseInstance> filteredCourseInstances = [];
        foreach (CourseInstance courseInstance in courseInstances)
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

        return Results.Ok(filteredCourseInstances);
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
        return Results.InternalServerError();
    }
});
app.MapGet(("/courseinstances/{id}"), (string id) =>
{
    try
    {
        CourseInstance? course = courseInstances.FirstOrDefault(s => s.Id == id);
        if (course == null) return Results.NotFound($"Course instance {id} not found");
        return Results.Ok(course);
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
        return Results.InternalServerError();
    }
});
app.MapPost(("/courseinstances"), (CreateCourseInstanceRequest request) =>
{
    if (request.StartDate == default || request.EndDate == default)
        return Results.BadRequest("Date fields are required");
    if (request.CourseId == null || request.Students == null)
        return Results.BadRequest("All fields are required");
    if (courses.FirstOrDefault(c => c.Id == request.CourseId) == null)
        return Results.NotFound($"Course {request.CourseId} not found");
    foreach (string studentId in request.Students)
    {
        if (students.FirstOrDefault(s => s.Id == studentId) == null)
            return Results.NotFound($"Student {studentId} not found");
    }

    if (request.StartDate > request.EndDate) return Results.BadRequest("Start date must be before end date");
    try
    {
        CourseInstance newCourseInstance = new(request.StartDate, request.EndDate, request.CourseId, request.Students);
        courseInstances.Add(newCourseInstance);
        return Results.Created($"/courseinstances/{newCourseInstance.Id}", newCourseInstance);
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
        return Results.InternalServerError();
    }
});
app.MapPut(("/courseinstances/{id}"), (string id, UpdateCourseInstanceRequest request) =>
{
    try
    {
        // Validate data
        CourseInstance? result = courseInstances.FirstOrDefault(c => c.Id == id);
        if (result == null) return Results.NotFound($"CourseInstance {id} not found");
        var newStart = request.StartDate ?? result.StartDate;
        var newEnd = request.EndDate ?? result.EndDate;
        if (newStart > newEnd) return Results.BadRequest("Start date must be before end date");
        if (request.CourseId != null && courses.FirstOrDefault(c => c.Id == request.CourseId) == null)
            return Results.NotFound($"Course {request.CourseId} not found");
        if (request.Students != null)
        {
            foreach (string studentId in request.Students)
            {
                if (students.FirstOrDefault(s => s.Id == studentId) == null)
                    return Results.NotFound($"Student {studentId} not found");
            }
        }

        // Update data
        if (request.StartDate.HasValue) result.StartDate = request.StartDate.Value;
        if (request.EndDate.HasValue) result.EndDate = request.EndDate.Value;

        if (request.CourseId != null) result.CourseId = request.CourseId;
        if (request.Students != null) result.Students = request.Students;

        return Results.Ok();
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
        return Results.InternalServerError();
    }
});
app.MapDelete(("/courseinstances/{id}"), (string id) =>
{
    try
    {
        CourseInstance? courseInstance = courseInstances.FirstOrDefault(c => c.Id == id);
        if (courseInstance == null) return Results.NotFound();
        courseInstances.Remove(courseInstance);
        return Results.NoContent();
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
        return Results.InternalServerError();
    }
});

app.MapPost(("/grades"), (CreateGradeRequest request) =>
{
    if (string.IsNullOrWhiteSpace(request.StudentId) || string.IsNullOrWhiteSpace(request.CourseInstanceId) ||
        string.IsNullOrWhiteSpace(request.Value))
        return Results.BadRequest("All fields are required");
    if (!Regex.IsMatch(request.Value.ToLower(), @"^[a-f]$"))
        return Results.BadRequest("Grade should be single letter, A-F");

    Student? student = students.FirstOrDefault(s => s.Id == request.StudentId);
    if (student == null) return Results.NotFound($"Student {request.StudentId} not found");
    CourseInstance? courseInstance = courseInstances.FirstOrDefault(ci => ci.Id == request.CourseInstanceId);
    if (courseInstance == null) return Results.NotFound($"Course instance {request.CourseInstanceId} not found");

    try
    {
        Grade newGrade = new(request.Value, request.CourseInstanceId, request.StudentId);
        grades.Add(newGrade);
        return Results.Created();
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
        return Results.InternalServerError();
    }
});


app.Run();