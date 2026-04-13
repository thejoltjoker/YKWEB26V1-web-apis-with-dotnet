## Del 2 – Refaktorering till Services

### Övning 4 – Flytta logik till en service

- [x] Skapa en mapp `Services`.
- [ ] Skapa `StudentService` med metoderna:
  - [x] `GetAll()`
  - [x] `GetById(int id)`
  - [x] `Add(Student student)`
  - [x] `Update(int id, Student updated)`
  - [x] `Delete(int id)`
- [x] Flytta all logik från `StudentController` till `StudentService`.
- [x] Ändra `StudentController` så att den bara anropar service-metoderna.

> Studenterna ser nu hur controllern blir tunnare.

### Övning 5 – Dependency Injection

- [x] Registrera `StudentService` i `Program.cs`:

  ```csharp
  builder.Services.AddSingleton<StudentService>();
  ```

- [x] Ändra `StudentController` så att den tar emot `StudentService` via konstruktorn (injektion).
- [x] Testa alla endpoints igen i Postman.

> Här ser studenterna hur DI fungerar i praktiken.

### Övning 6 – Applicera samma sak på Course och CourseInstance

- [ ] Skapa `CourseService` och `CourseInstanceService`.
- [ ] Flytta logik från respektive controller till sina services.
- [ ] Registrera services i `Program.cs`.
