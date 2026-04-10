# Del 1 – Grundläggande controllers

I dessa övningar vill jag att ni använder er av en hårdkodad lista med studenter och tillhörande kurser och betyg. Detta kommer att bli en utmaning för oss som vi sedan skall lösa på bättre sätt. Men ett steg i taget.

## Övning 1 – Skapa en egen controller

- [x] Skapa en ny controller `StudentController` (om du inte redan har en).
- [ ] Lägg till endpointen `GET /students` som returnerar en lista av hårdkodade studenter.
- [ ] Lägg till endpointen `GET /students/{id}` som returnerar en student baserat på id.
  - [ ] Om ingen student finns: returnera `404 Not Found`.

## Övning 2 – CRUD i controllern

- [ ] Lägg till följande endpoints i `StudentController`:
  - [ ] `POST /students` – Skapa en ny student.
  - [ ] `PUT /students/{id}` – Uppdatera en student.
  - [ ] `DELETE /students/{id}` – Ta bort en student.
- [ ] Testa varje endpoint i Postman.

## Övning 3 – Bygg fler controllers

- [ ] Skapa `CourseController` och `CourseInstanceController`.
- [ ] Implementera minst en GET-metod i vardera controller för att returnera hårdkodad data.
- [ ] Testa alla tre controllers i Postman.

Poängen här är att se att controllers fungerar som bryggor mellan HTTP och kod.


