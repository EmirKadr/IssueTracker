# Projektet

IssueTracker är ett ASP.NET Core-projekt för supportärenden enligt övningen IssueTracker 1.0. Kodstilen ska följa Bolagom4. Arbetet sker stegvis på användarens begäran.

Databasen ska använda SQL Server LocalDB eller Express enligt uppgiften. `IssueTracker.Api/Data/IssueTracker.sql` skapar databasen IssueTracker och tabellen Tickets med automatiskt Id, Title, Description, Status och Priority. Nya ärenden får status Open. Skriptet är endast skapat, inte kört. Befintlig databas eller tabell lämnas kvar vid omkörning; skriptet uppgraderar inte befintligt schema.

Projektmappen ska ha ett eget Git-repo kopplat till https://github.com/emirkadr/issuetracker.git som `origin`. Utan eget repo hittade Git ett överordnat repo i `C:/`, vilket gav fel kontext i Visual Studio.

GitHub-repot var tomt vid kontroll 2026-10-09. Lokal startgren är `main`. Kopplingen innebär inte att filer har publicerats; första commit och push återstår.
