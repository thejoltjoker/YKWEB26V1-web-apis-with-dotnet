# Övningar

## 1. Skapa en modell för Student

Skapa en klass `Student` med egenskaper:

- `Id` (`int`)
- `Name` (`string`)
- `Email` (`string`)

Det spelar ingen roll vilken typ av klass ni väljer här. Det kan vara traditionella klasser eller primary constructor. Välj det som passar er bäst.

- [x] Använd modellen genom att skapa ett studentobjekt i `Program.cs`.
- [x] Skapa en endpoint som returnerar studentobjektet.
- [x] Skapa en lista med studentobjekt i `Program.cs`.
- [x] Skapa en endpoint som returnerar listan med studentobjekt.

---

## 2. Skapa en modell för Course

Skapa en klass `Course` med egenskaper:

- `Id` (`int`)
- `Title` (`string`)
- `Description` (`string`)

Poängen med denna klass är att vi strax ska koppla ihop den med studenter för att ta reda på vilka studenter som går på vilka kurser. Vi behöver också en endpoint som visar vilka kurser som finns och kanske även en endpoint med information för en given kurs.

- [x] Skapa en lista med kursobjekt i `Program.cs`.
- [x] Skapa en endpoint som returnerar listan med kurser.
- [x] Extrauppgift: Skapa en endpoint som returnerar en specifik kurs baserat på kursens id.

---

## 3. Skapa en modell för CourseInstance (kurstillfälle)

Skapa en klass `CourseInstance` med egenskaper:

- `Id` (`int`)
- `StartDate` (`DateTime`)
- `EndDate` (`DateTime`)
- En egenskap `Course` som visar vilken kurs det gäller
- En lista `Students` som visar vilka studenter som deltar

Poängen med denna klass är att innehålla information om vilka studenter som går på vilka kurser. Vi jobbar vidare med lite fler datatyper för att träna på dem.

- [x] Skapa en lista med kurstillfällen i `Program.cs` (ni börjar se att det blir mycket data i `Program.cs` nu - detta är ett problem som vi ska lösa senare).
- [x] Skapa en endpoint som returnerar listan med kurstillfällen.
- [x] Extrauppgift: Skapa en endpoint som returnerar alla kurser som en given student går på.
- [x] Extrauppgift: Skapa en endpoint som returnerar alla kurser mellan två givna datum.

---

## 4. Skapa en modell för Grade (betyg)

Skapa en klass `Grade` med egenskaper:

- `Id` (`int`)
- `Value` (`string`, t.ex. `"A"`, `"B"`, `"C"`, ...)
- `CourseInstance` - beskriver vilket kurstillfälle som ska användas
- `Student` - beskriver vilken student som ska få betyget för kurstillfället

Viktigt: `Grade` tillhör både en student och ett kurstillfälle, inte studenten direkt.

Poängen med denna klass är att innehålla en beskrivning av våra betyg. Ni kan välja vilka värden på betyg som ni själva vill.

- [x] Skapa en lista med grade-objekt i `Program.cs`.
- [x] Skapa en endpoint som returnerar betygobjekten.
- [x] Skapa egna endpoints, t.ex. en som visar alla betyg för en student och vilka kurser betygen gäller.

---

## 5. Reflektionsfråga

Varför är det en bättre modellering att koppla `Grade` till `Student` + `CourseInstance` än att lägga en lista med betyg direkt på studenten?
