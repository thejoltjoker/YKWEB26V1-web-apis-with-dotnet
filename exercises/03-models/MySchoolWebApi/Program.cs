using MySchoolWebApi.Models;

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

List<Student> students =
[
    new(1, "John Doe", "john.doe@example.com"),
    new(2, "Jane Smith", "jane.smith@example.com"),
    new(3, "Alice Johnson", "alice.johnson@example.com"),
    new(4, "Bob Lee", "bob.lee@example.com"),
    new(5, "Maria Svensson", "maria.svensson@example.com")
];

List<Course> courses =
[
    new(101, "Intro to C#", "Get started with C#."),
    new(102, "Advanced C#", "Deep dive into advanced C# concepts."),
    new(103, "Web Development", "Learn to build web applications with .NET."),
    new(104, "Database Fundamentals", "Introduction to SQL and relational databases."),
    new(105, "Software Architecture", "Explore software design and architecture patterns.")
];


List<CourseInstance> courseInstances =
[
    new(201, new DateTime(2026, 01, 01), new DateTime(2026, 03, 31), courses[0].Id, students.Select(s => s.Id)),
    new(202, new DateTime(2026, 02, 01), new DateTime(2026, 04, 30), courses[1].Id,
        students.Slice(0, 1).Select(s => s.Id).ToList()),
    new(203, new DateTime(2026, 03, 01), new DateTime(2026, 05, 31), courses[2].Id,
        students.Slice(0, 2).Select(s => s.Id)),
    new(204, new DateTime(2026, 04, 01), new DateTime(2026, 06, 30), courses[3].Id,
        students.Slice(0, 3).Select(s => s.Id)),
    new(205, new DateTime(2026, 05, 01), new DateTime(2026, 07, 31), courses[4].Id,
        students.Slice(0, 4).Select(s => s.Id))
];

List<Grade> grades =
[
    new(301, "A", courseInstances[0].Id, students[0].Id),
    new(302, "B", courseInstances[1].Id, students[0].Id),


    new(303, "B", courseInstances[0].Id, students[1].Id),
    new(304, "A", courseInstances[1].Id, students[1].Id),
    new(305, "C", courseInstances[2].Id, students[1].Id),


    new(306, "B", courseInstances[2].Id, students[2].Id),
    new(307, "A", courseInstances[3].Id, students[2].Id),


    new(308, "D", courseInstances[0].Id, students[3].Id),
    new(309, "C", courseInstances[3].Id, students[3].Id),
    new(310, "B", courseInstances[4].Id, students[3].Id),


    new(311, "A", courseInstances[4].Id, students[4].Id)
];

app.MapGet(("/students"), () => { return students; });

app.MapGet(("/students/{id}"), (int id) => { return students.FirstOrDefault(s => s.Id == id); });
app.MapGet(("/students/{id}/courses"), (int id) =>
{
    Student? student = students.FirstOrDefault(s => s.Id == id);
    if (student == null) return [];
    return courseInstances.FindAll(ci => ci.students.Contains(student.Id));
});
app.MapGet(("/students/{id}/grades"), (int id) =>
{
    Student? student = students.FirstOrDefault(s => s.Id == id);
    if (student == null) return null;
    return grades.FindAll(g => g.StudentId == student.Id);
});

app.MapGet(("/courses"), (DateTime? startDate, DateTime? endDate) =>
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
    return filteredCourses;
});

app.MapGet(("/courses/{id}"), (int id) => { return courses.FirstOrDefault(s => s.Id == id); });
app.MapGet(("/courses/{id}/instances"), (int id) => { return courseInstances.FindAll(ci => ci.CourseId == id); });

app.UseHttpsRedirection();


app.Run();