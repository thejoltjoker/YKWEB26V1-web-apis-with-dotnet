## Övning 1 – Utforska HTTP-svarskoder

- [x] Skriv ner en kort förklaring av följande koder och när de används i API:et:
  - [x] 200 OK
    - Används när en begäran har lyckats och det har returnerats en resurs.
  - [x] 201 Created
    - Används när en resurs har skapats.
  - [x] 204 No Content
    - Används när en begäran har lyckats och det har inte returnerats en resurs.
  - [x] 400 Bad Request
    - Används när användaren har gjort något fel i sin request
  - [x] 404 Not Found
    - Används när resursen inte hittats/finns.
  - [x] 500 Internal Server Error
    - Övriga fel som händer på servern

Här sätter vi ramarna för vad som är "korrekt" svar i olika situationer.
För mer information angående svarskoder kan ni kika på följande länk: [HTTP Status Codes explained](https://http.dev/status)

## Övning 2 – CRUD för Student med rätt statuskoder

Skapa följande endpoint:

1. GET /students

   - [x] Returnera alla studenter → `200 OK`.
   - [x] Om inga studenter finns → returnera en tom lista men fortfarande 200 OK.

2. GET /students/{id}

   - [x] Om studenten finns → returnera `200 OK` + student.
   - [x] Om studenten inte finns → returnera `404 Not Found`.

3. POST /students
   - [x] Om all data är giltig → skapa studenten och returnera `201 Created`.
   - [x] Om något fält saknas eller är ogiltigt → returnera `400 Bad Request`.
4. PUT /students/{id}
   - [x] Om studenten finns och uppdateringen lyckas → `200 OK`.
   - [x] Om studenten inte finns → `404 Not Found`.
5. DELETE /students/{id}
   - [x] Om studenten finns och tas bort → `204 No Content`.
   - [x] Om studenten inte finns → `404 Not Found`.

## Övning 4 – CRUD för Course

1. Skapa endpoints för Course:

   - [x] GET /courses
   - [x] GET /courses/{id}
   - [x] POST /courses
   - [x] PUT /courses/{id}
   - [x] DELETE /courses/{id}

2. Använd samma regler för statuskoder som med Student.

## Övning 5 – CRUD för CourseInstance

1. Skapa endpoints för CourseInstance.

   - [x] GET /courseinstances
   - [x] GET /courseinstances/{id}
   - [x] POST /courseinstances
   - [x] PUT /courseinstances/{id}
   - [x] DELETE /courseinstances/{id}

2. Statuskoder:

   - [x] 201 Created när nytt kurstillfälle skapas.
   - [x] 400 Bad Request om datan är ogiltig.
   - [x] `404 Not Found` om Id inte finns.

3. Valideringsexempel:

   - [x] `StartDate` och `EndDate` är obligatoriska.
   - [x] `EndDate` måste vara efter `StartDate` → annars `400 Bad Request`.

## Övning 6 – (Frivillig utmaning) Hantera Grade

- [x] Skapa en endpoint `POST /grades` som låter läraren sätta ett betyg för en student i ett kurstillfälle.
- [x] Regler:
  - [x] Om både student och kurstillfälle existerar → 201 Created.
  - [x] Om student eller kurstillfälle saknas → 404 Not Found.
  - [x] Om betyget är ogiltigt (t.ex. inte A-F) → 400 Bad Request.
