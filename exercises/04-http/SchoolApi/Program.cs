using SchoolApi.Models;
using SchoolApi.Models.Requests;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) app.MapOpenApi();

app.UseHttpsRedirection();


// Data
List<Course> courses =
[
    new("C# 101", "Learn C#"),
    new(".NET 101", "Learn .NET")
];

List<Student> students =
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

List<CourseInstance> courseInstances =
[
    new(new DateTime(2026, 01, 01), new DateTime(2026, 03, 31), courses[0], students),
    new(new DateTime(2026, 02, 01), new DateTime(2026, 04, 30), courses[1], students.Slice(0, 1))
];

List<Grade> grades =
[
    new("A", courseInstances[0], students[0])
];

// Endpoints
app.MapGet("/students", () =>
{
    try
    {
        return Results.Ok(students ?? []);
    }
    catch (Exception e)
    {
        return Results.InternalServerError(e);
    }
});
app.MapGet("/students/{id}", (string id) =>
{
    try
    {
        var student = students.FirstOrDefault(s => s.Id.Equals(id));
        return student == null ? Results.NotFound($"Student {id} not found.") : Results.Ok(student);
    }
    catch (Exception e)
    {
        return Results.InternalServerError(e);
    }
});

app.MapPost("/students", (CreateStudentRequest request) =>
{
    try
    {
        if (string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.Email))
            return Results.BadRequest("Name and email are required.");

        Student newStudent = new(request.Name, request.Email);
        students.Add(newStudent);
        return Results.Created($"/students/{newStudent.Id}", newStudent);
    }
    catch (Exception e)
    {
        return Results.InternalServerError(e);
    }
});
app.MapPut("/students/{id}", (string id, UpdateStudentRequest request) =>
{
    try
    {
        var studentId = students.FindIndex(s => s.Id == id);
        if (studentId == -1) return Results.NotFound($"Student {id} not found.");
        if (request.Name != null) students[studentId].Name = request.Name;
        if (request.Email != null) students[studentId].Email = request.Email;
        return Results.Ok($"Student {id} updated successfully.");
    }
    catch (Exception e)
    {
        return Results.InternalServerError(e);
    }
});
app.MapDelete("/students/{id}", (string id) =>
{
    try
    {
        var studentId = students.FindIndex(s => s.Id == id);
        if (studentId == -1) return Results.NotFound($"Student {id} not found.");
        students.RemoveAt(studentId);
        return Results.NoContent();
    }
    catch (Exception e)
    {
        return Results.InternalServerError(e);
    }
});
app.MapGet("/students/{id}/courses", (string id) =>
{
    var student = courseInstances.FindAll(ci => ci.Students.Any(s => s.Id.Equals(id)));
    return student;
});
app.MapGet("/courses", () => courses);
app.MapGet("/courses/{id}", (string id) =>
{
    var course = courses.Find(x => x.Id.Equals(id));
    return course;
});
app.MapGet("/course-instances", () => courseInstances);
app.MapGet("/grades", () => grades);

app.Run();