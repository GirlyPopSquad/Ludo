# Ludo Spil

Dette repository indeholder to projekter, der sammen udgør et Ludo-spil. For at køre spillet skal begge projekter startes i den korrekte rækkefølge.

---

## Projekter

- **LudoAPI**  
  En C# .NET API, som håndterer spillets backend-logik og data.

- **LudoUI**  
  Et Python-projekt med brugergrænsefladen til spillet.

- **LudoTest**
  Et .net XUnit test projekt som indeholder software tests.

- **LudoSpec**
  Et .net XUnit ReqNRoll projekt som indeholder reqnroll tests.

---

## Kom godt i gang

### 1. Start LudoAPI i Visual Studio

1. Åbn Visual Studio.
2. Åbn løsningen `Ludo.sln`.
3. Sørg for at sætte `LudoAPI` som startprojekt.
4. Start projektet ved at klikke på **Start** (F5) eller **Kør uden debugging** (Ctrl + F5).
5. API’en kører som standard på `[http://localhost:5000](http://localhost:5276)`.

---

### 2. Start LudoUI i Visual Studio Code eller andet editor til python

1. Åbn projektmappen `LudoUI`.
2. Installer alle packages med "pip install -r requirements.txt" i terminal.
3. Kør filen `LudoUI.py`.

---

## Spil Ludo

Når både API og UI kører, kan du spille Ludo ved at bruge Python-brugerfladen, som kommunikerer med backend-API’en.

---

## Forudsætninger

- Visual Studio med .NET workload (for C# projekter)
- Visual Studio Code eller anden editor til python projekter
- [.NET SDK](https://dotnet.microsoft.com/download) (version 8 eller nyere anbefales)
- [Python](https://www.python.org/downloads/) (version 3.8 eller nyere anbefales)
- Eventuelle Python dependencies specificeret i `requirements.txt` i LudoUI-projektet

---


Har du spørgsmål eller oplever problemer, så åbn en issue i dette repository.

---

*God fornøjelse med Ludo-spillet!*
