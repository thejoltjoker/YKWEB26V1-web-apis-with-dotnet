# Övningar – HTTP

## Övning 1 – Utforska HTTP-svarskoder

- Skriv ner en kort förklaring av följande koder och när de används i API:et:
  - `200 OK`
  - `201 Created`
  - `204 No Content`
  - `400 Bad Request`
  - `404 Not Found`
  - `500 Internal Server Error`
- Här sätter vi ramarna för vad som är "korrekt" svar i olika situationer.
- För mer information angående svarskoder kan ni kika på följande länk: [HTTP Status Codes explained](https://developer.mozilla.org/en-US/docs/Web/HTTP/Status)

## Övning 2 – CRUD för Student med rätt statuskoder

Skapa följande endpoint:

1. `GET /students`
   - [x] Returnera alla studenter -> `200 OK`.
   - [x] Om inga studenter finns -> returnera en tom lista men fortfarande `200 OK`.
2. `GET /students/{id}`
   - [x] Om studenten finns -> returnera `200 OK` + student.
   - [x] Om studenten inte finns -> returnera `404 Not Found`.
3. `POST /students`
   - [x] Om all data är giltig -> skapa studenten och returnera `201 Created`.
   - [x] Om något fält saknas eller är ogiltigt -> returnera `400 Bad Request`.
4. `PUT /students/{id}`
   - [ ] Om studenten finns och uppdateringen lyckas -> `200 OK`.
   - [ ] Om studenten inte finns -> `404 Not Found`.
5. `DELETE /students/{id}`
   - [ ] Om studenten finns och tas bort -> `204 No Content`.
   - [ ] Om studenten inte finns -> `404 Not Found`.

## Övning 4 – CRUD för Course

1. Skapa endpoints för Course:
   - [ ] `GET /courses`
   - [ ] `GET /courses/{id}`
   - [ ] `POST /courses`
   - [ ] `PUT /courses/{id}`
   - [ ] `DELETE /courses/{id}`
2. Använd samma regler för statuskoder som med Student.

## Övning 5 – CRUD för CourseInstance

1. Skapa endpoints för CourseInstance:
   - [ ] `GET /courseinstances`
   - [ ] `GET /courseinstances/{id}`
   - [ ] `POST /courseinstances`
   - [ ] `PUT /courseinstances/{id}`
   - [ ] `DELETE /courseinstances/{id}`
2. Statuskoder:
   - [ ] `201 Created` när nytt kurstillfälle skapas.
   - [ ] `400 Bad Request` om datan är ogiltig.
   - [ ] `404 Not Found` om `Id` inte finns.
3. Valideringsexempel:
   - [ ] `StartDate` och `EndDate` är obligatoriska.
   - [ ] `EndDate` måste vara efter `StartDate` -> annars `400 Bad Request`.

## Övning 6 – (Frivillig utmaning) Hantera Grade

- [ ] Skapa en endpoint `POST /grades` som låter läraren sätta ett betyg för en student i ett kurstillfälle.
- [ ] Regler:
  - [ ] Om både student och kurstillfälle existerar -> `201 Created`.
  - [ ] Om student eller kurstillfälle saknas -> `404 Not Found`.
  - [ ] Om betyget är ogiltigt (t.ex. inte A-F) -> `400 Bad Request`.
