using MySchoolWebApi.Models;
using MySchoolWebApi.Models.Requests;
using System.Text.RegularExpressions;
using MySchoolWebApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();
// TODO Replace with AddScoped
builder.Services.AddSingleton<IStudentsService, StudentsService>();
builder.Services.AddSingleton<ICoursesService, CoursesService>();
builder.Services.AddSingleton<ICourseInstancesService, CourseInstancesService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

List<Student> students =
[
    new("John Doe", "john.doe@example.com"),
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
    new(new DateTime(2026, 01, 01), new DateTime(2026, 03, 31), courses[0].Id, [students[0].Id]),
    // new(new DateTime(2026, 02, 01), new DateTime(2026, 04, 30), courses[1].Id,
    //     [students[0].Id, students[1].Id, students[2].Id]),
    // new(new DateTime(2026, 03, 01), new DateTime(2026, 05, 31), courses[2].Id, [students[2].Id, students[3].Id]),
    // new(new DateTime(2026, 04, 01), new DateTime(2026, 06, 30), courses[3].Id,
    //     [students[0].Id, students[3].Id, students[4].Id]),
    // new(new DateTime(2026, 05, 01), new DateTime(2026, 07, 31), courses[4].Id, [students[4].Id])
];

List<Grade> grades =
[
    new("A", courseInstances[0].Id, students[0].Id),
    // new("B", courseInstances[0].Id, students[1].Id),
    //
    //
    // new("B", courseInstances[1].Id, students[0].Id),
    // new("A", courseInstances[1].Id, students[1].Id),
    // new("C", courseInstances[1].Id, students[2].Id),
    //
    //
    // new("B", courseInstances[2].Id, students[2].Id),
    // new("A", courseInstances[2].Id, students[3].Id),
    //
    //
    // new("D", courseInstances[3].Id, students[0].Id),
    // new("C", courseInstances[3].Id, students[3].Id),
    // new("B", courseInstances[3].Id, students[4].Id),
    //
    //
    // new("A", courseInstances[4].Id, students[4].Id)
];


app.UseHttpsRedirection();

app.MapControllers();

// app.MapGet(("/students/{id}/courses"), (string id) =>
// {
//     Student? student = students.FirstOrDefault(s => s.Id == id);
//     if (student == null) return [];
//     return courseInstances.FindAll(ci => ci.Students.Contains(student.Id));
// });
// app.MapGet(("/students/{id}/grades"), (string id) =>
// {
//     Student? student = students.FirstOrDefault(s => s.Id == id);
//     if (student == null) return null;
//     return grades.FindAll(g => g.StudentId == student.Id);
// });



// app.MapGet(("/courses/{id}/instances"), (string id) =>
// {
//     try
//     {
//         List<CourseInstance> result = courseInstances.FindAll(ci => ci.CourseId == id);
//         return Results.Ok(result);
//     }
//     catch (Exception e)
//     {
//         Console.WriteLine(e);
//         return Results.InternalServerError();
//     }
// });




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