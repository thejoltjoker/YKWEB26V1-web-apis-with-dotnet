# 05 - Controllers, Services, Repositories

Denna README sammanfattar innehållet från:

- `1. Grundläggande kontrollers.pdf`
- `2. Services.pdf`
- `3. Repositories.pdf`
- `4. Reflektionsövningar.pdf`

## Del 1 - Grundläggande controllers

I dessa övningar använder ni en hårdkodad lista med studenter och tillhörande kurser och betyg. Det är medvetet för att ni senare ska refaktorera till en bättre struktur.

### Övning 1 - Skapa en egen controller

1. Skapa en ny controller `StudentController` (om du inte redan har en).
2. Lägg till en endpoint `GET /students` som returnerar en lista av hårdkodade studenter.
3. Lägg till en endpoint `GET /students/{id}` som returnerar en student baserat på `Id`.
   - Om ingen student finns: returnera `404 Not Found`.

### Övning 2 - CRUD i controllern

1. Lägg till följande endpoints i `StudentController`:
   - `POST /students` -> Skapa en ny student.
   - `PUT /students/{id}` -> Uppdatera en student.
   - `DELETE /students/{id}` -> Ta bort en student.
2. Testa varje endpoint i Postman.

### Övning 3 - Bygg fler controllers

1. Skapa `CourseController` och `CourseInstanceController`.
2. Implementera minst en `GET`-metod i vardera controller för att returnera hårdkodad data.
3. Testa alla tre controllers i Postman.

Poängen här är att se att controllers fungerar som bryggor mellan HTTP och kod.

## Del 2 - Refaktorering till Services

### Övning 4 - Flytta logik till en service

1. Skapa en mapp `Services`.
2. Skapa `StudentService` med metoderna:
   - `GetAll()`
   - `GetById(int id)`
   - `Add(Student student)`
   - `Update(int id, Student updated)`
   - `Delete(int id)`
3. Flytta all logik från `StudentController` till `StudentService`.
4. Ändra `StudentController` så att den bara anropar service-metoderna.

Målet är att göra controllern tunnare.

### Övning 5 - Dependency Injection

1. Registrera `StudentService` i `Program.cs`:
   - `builder.Services.AddSingleton<StudentService>();`
2. Ändra `StudentController` så att den tar emot `StudentService` via konstruktorn (injektion).
3. Testa alla endpoints igen i Postman.

Här ser ni hur DI fungerar i praktiken.

### Övning 6 - Applicera samma sak på Course och CourseInstance

1. Skapa `CourseService` och `CourseInstanceService`.
2. Flytta logik från respektive controller till sina services.
3. Registrera services i `Program.cs`.

## Del 3 - Refaktorering till Repositories

### Övning 7 - Skapa repository-interface

1. Skapa en mapp `Repositories`.
2. Skapa ett interface `IStudentRepository` med metoder för CRUD.
3. Skapa en implementation `InMemoryStudentRepository` som använder en lista i minnet.

### Övning 8 - Använd repository i service

1. Ändra `StudentService` så att den tar emot ett `IStudentRepository` i konstruktorn.
2. Ändra logiken i service-metoderna så att de anropar repository i stället för att hantera listan själva.
3. Registrera repository i `Program.cs`:
   - `builder.Services.AddSingleton<IStudentRepository, InMemoryStudentRepository>();`

Nu ser ni poängen med att kunna byta ut datakällan senare.

### Övning 9 - Applicera på Course och CourseInstance

1. Skapa `ICourseRepository` och `ICourseInstanceRepository` med in-memory-implementationer.
2. Koppla ihop `CourseService` och `CourseInstanceService` med respektive repository.
3. Testa endpoints i Postman igen.

## Del 4 - Reflektion

### Övning 10 - Diskussionsfrågor

1. Varför är det bättre att låta controllern vara tunn?
2. Vad vinner man på att använda services jämfört med att lägga all logik i controllern?
3. Vad vinner man på att använda repositories med interface jämfört med att bara skriva allting i service-klasserna?
4. Hur skulle du byta ut in-memory-lagring mot en databas (Entity Framework)?
