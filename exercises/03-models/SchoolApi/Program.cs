using SchoolApi.Models;

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
    new("1", "C# 101", "Learn C#"),
    new("2", ".NET 101", "Learn .NET")
];

List<Student> students =
[
    new("1",
        "John Doe",
        "john.doe@example.com"),
    new("2",
        "Jane Doe",
        "jane.doe@example.com"),
    new("3",
        "Sarah Doe",
        "Sarah.doe@example.com")
];

List<CourseInstance> courseInstances =
[
    new("1", new DateTime(2026, 01, 01), new DateTime(2026, 03, 31), courses[0], students),
    new("2", new DateTime(2026, 02, 01), new DateTime(2026, 04, 30), courses[1], students.Slice(0, 1))
];


// TODO Skapa en ny endpoint som returnerar alla kurser mellan två givna datum

app.MapGet("/students/{id}", (string id) =>
{
    var student = students.Find(s => s.Id.Equals(id));
    return student;
});
app.MapGet("/students/{id}/courses", (string id) =>
{
    var student = courseInstances.FindAll(ci => ci.students.Any(s => s.Id.Equals(id)));
    return student;
});
app.MapGet("/students", () => students);
app.MapGet("/courses", () => courses);
app.MapGet("/courses/{id}", (string id) =>
{
    var course = courses.Find(x => x.Id.Equals(id));
    return course;
});
app.MapGet("/course-instances", () => courseInstances);

app.Run();