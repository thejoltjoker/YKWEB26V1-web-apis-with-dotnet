using SchoolApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

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
        "Sarah.doe@example.com"),
];

app.MapGet("/students/{id}", (string id) =>
{
    var student = students.Find(s => s.Id.Equals(id));
    return student;
});
app.MapGet("/students", () => students);
app.MapGet("/courses", () => courses);
app.MapGet("/courses/{id}", (string id) =>
{
    var course = courses.Find(x => x.Id.Equals(id));
    return course;
});

app.Run();