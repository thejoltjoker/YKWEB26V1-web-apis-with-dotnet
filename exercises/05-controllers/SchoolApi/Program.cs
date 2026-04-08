using SchoolApi.Repositories;
using SchoolApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddScoped<IStudentsService, StudentsService>();
builder.Services.AddSingleton<IStudentRepository, InMemoryStudentRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) app.MapOpenApi();

app.UseHttpsRedirection();
app.MapControllers();

// Data
// List<Course> courses =
// [
//     new("C# 101", "Learn C#"),
//     new(".NET 101", "Learn .NET")
// ];
//
//
// List<CourseInstance> courseInstances =
// [
//     new(new DateTime(2026, 01, 01), new DateTime(2026, 03, 31), courses[0], students),
//     new(new DateTime(2026, 02, 01), new DateTime(2026, 04, 30), courses[1], students.Slice(0, 1))
// ];
//
// List<Grade> grades =
// [
//     new("A", courseInstances[0], students[0])
// ];


// app.MapGet("/students/{id}/courses", (string id) =>
// {
//     var student = courseInstances.FindAll(ci => ci.Students.Any(s => s.Id.Equals(id)));
//     return student;
// });
// app.MapGet("/courses", () => courses);
// app.MapGet("/courses/{id}", (string id) =>
// {
//     var course = courses.Find(x => x.Id.Equals(id));
//     return course;
// });
// app.MapGet("/course-instances", () => courseInstances);
// app.MapGet("/grades", () => grades);

app.Run();