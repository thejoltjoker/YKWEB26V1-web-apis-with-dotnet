## Övning 1 – Grundläggande validering för Student

- [x] Skapa en POST-endpoint för att lägga till en student.
- [x] Lägg till logik som kontrollerar:
  - [x] Namn får inte vara tomt.
  - [x] Email måste vara i rätt format (t.ex. innehålla `@`).
- [x] Om valideringen misslyckas ska controllern returnera `400 Bad Request` och ett felmeddelande.
- [x] Testa med Postman:
  - [x] Skicka in giltiga värden -> ska fungera.
  - [x] Skicka in ogiltiga värden -> ska returnera fel.

## Övning 2 – Validering för Course

- [x] Skapa en POST-endpoint för att lägga till en kurs.
- [ ] Lägg till regler:
  - [x] Kursnamn måste vara minst 3 tecken långt.
  - [x] Kurskod får inte vara tom.
- [x] Testa med Postman:
  - [x] Giltig kurs -> `201 Created`.
  - [x] Ogiltig kurs -> `400 Bad Request` + felmeddelande.

## Övning 3 – Validering för CourseInstance

- [x] Skapa en POST-endpoint för att lägga till ett kurstillfälle.
- [x] Lägg till regler:
  - [x] Startdatum får inte ligga i det förflutna.
  - [x] Slutdatum måste vara senare än startdatum.
  - [x] Kursen som kurstillfället tillhör måste finnas (annars `404 Not Found`).
- [x] Testa olika kombinationer i Postman.

## Övning 4 – Validering för Grade

- [x] Skapa en POST-endpoint för att lägga till ett betyg för en student i ett kurstillfälle.
- [x] Lägg till regler:
  - [x] Studenten måste finnas.
  - [x] Kurstillfället måste finnas.
  - [x] Betyg måste vara ett av de giltiga värdena (`A`, `B`, `C`, `D`, `E`, `F`).
- [x] Vid fel ska ni returnera `400 Bad Request` (felaktigt värde) eller `404 Not Found` (om student eller kurs saknas).

## Övning 5 – Samla felmeddelanden

- [ ] Ändra er valideringslogik så att ni kan returnera **flera fel i samma svar**.
  - Exempel: Om både namn och email är fel, ska svaret innehålla båda felen i en lista.
- [ ] Fundera: varför är detta bättre än att bara visa första felet?

## Övning 6 – Bonus: Data Annotation Validation

- [ ] Utforska attribut som `[Required]`, `[EmailAddress]`, `[Range]` och `[StringLength]`.
- [ ] Lägg till dessa i dina modeller (`Student`, `Course`, etc.).
- [ ] Låt .NET automatiskt validera inkommande data och returnera `400 Bad Request`.
- [ ] Jämför med din egen manuella validering – vad är fördelar/nackdelar med respektive metod?

## Poängen med övningarna

- [ ] Ni tränar på **både manuell och automatisk validering**.
- [ ] Ni får fundera på **affärsregler** (ex. datumlogik).
- [ ] Ni lär er att använda **rätt statuskoder och felmeddelanden**.
