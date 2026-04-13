# Del 3 - Refaktorering till Repositories

## Övning 7 - Skapa repository-interface

- [x] Skapa en mapp `Repositories`.
- [x] Skapa ett interface `IStudentRepository` med metoder för CRUD.
- [x] Skapa en implementation `InMemoryStudentRepository` som använder en lista i minnet.

## Övning 8 - Använd repository i service

- [x] Ändra `StudentService` så att den tar emot ett `IStudentRepository` i konstruktorn.
- [x] Ändra logiken i service-metoderna så att de anropar repository i stället för att hantera listan själva.
- [x] Registrera repository i `Program.cs`.
- [x] Lägg till:
      `builder.Services.AddSingleton<IStudentRepository, InMemoryStudentRepository>();`

Nu ser ni poängen med att kunna byta ut datakällan senare.

## Övning 9 - Applicera på Course och CourseInstance

- [x] Skapa `ICourseRepository` och `ICourseInstanceRepository` med in-memory-implementationer.
- [x] Koppla ihop `CourseService` och `CourseInstanceService` med respektive repository.
- [x] Testa endpoints i Postman igen.
