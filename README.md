# Glossary

## Glossery architecture 

Full-stack sample: .NET (CQRS + MediatR) backend and Angular frontend.

### Repo layout

- **Glossary.API** – Web API (startup)  
- **Glossary.Application** – CQRS commands/queries, behaviors  
- **Glossary.Domain** – entities, specs, domain rules  
- **Glossary.Infrastructure** – EF Core, repository, UoW, DB context/migrations  
- **Glossery.UI** – Angular app (frontend UI)  

---

# Prerequisites

- .NET SDK 8  
- Node.js LTS (>= 18) + npm  
- Angular CLI: `npm i -g @angular/cli`  

---

# Setup 

## Backend 

Open **Package Manager Console** in Visual Studio and run:  

```powershell
Update-Database -Project Glossary.Infrastructure -StartupProject Glossary.Api -Context AppDbContext
```

Or with **dotnet CLI**:  

```bash
dotnet ef database update
```

## Frontend 

Install and serve:

```bash
cd Glossery.UI
npm install
ng serve
```

Open: [http://localhost:4200](http://localhost:4200)

---

# Dotnet CLI commands

```bash
dotnet ef database update
dotnet run --project Glossary.API
cd Glossery.UI && npm i && ng serve
```
