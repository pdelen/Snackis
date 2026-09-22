# Snackis

A discussion forum built as a web application in C# on .NET 9. Users register,
read and write posts in categories, comment on each other and send private
messages. Posts that break the rules can be reported by anyone and are then
handled by a moderator in the admin view.

The project follows onion architecture, where the domain sits at the centre
without outward dependencies and the user interface is replaceable. Both a
Blazor application and a separate Web API run on the same domain and
application layers, which is what the split is there to make possible.

The user interface is in Swedish.

## Features

- Categories in two levels, where top-level categories hold topics and the
  topics hold the posts
- Posts and comments with author and timestamp
- Private messages with an inbox and a conversation view per counterpart
- Reports, where a user flags a post with a reason and the moderator either
  marks the report as handled or deletes the post
- Accounts and roles through ASP.NET Core Identity, with a display name and
  profile image on top of the standard fields. Admin pages are protected with
  `[Authorize(Roles = "Admin")]`
- An admin section for managing categories, reviewing open reports and
  assigning roles
- A Web API that exposes the posts of a category as JSON, documented with
  Swagger

## Architecture

Dependencies point inwards: `Domain` knows nothing about the other projects, and
neither `Presentation` nor `API` knows how data is stored.

```
Presentation (Blazor)   API (REST)
            \            /
             Application
              /        \
        Domain      Infrastructure
```

| Project | Responsibility |
|---|---|
| `Snackis.Domain` | Entities (`ForumCategory`, `ForumPost`, `ForumComment`, `PrivateMessage`, `PostReport`) and the repository interfaces that infrastructure has to satisfy |
| `Snackis.Application` | Services holding the rules and flows, such as a deleted report also being marked as handled. Depends only on interfaces, never on EF Core |
| `Snackis.Infrastructure` | `MyDbContext`, repositories, migrations and the Identity customisation `ApplicationUser` |
| `Snackis.Presentation` | The Blazor Server interface with pages, layout and sign-in |
| `Snackis.API` | REST API with `ForumPostsController`, which maps domain objects onto `PostDto` before returning them |

Services and repositories are registered as scoped in each `Program.cs` and
injected through the constructor, so implementations can be swapped without
touching the pages or the controller.

## Tech

| | |
|---|---|
| Language and framework | C# on .NET 9 |
| Interface | Blazor Server with interactive components, Bootstrap |
| API | ASP.NET Core Web API, Swagger/OpenAPI |
| Sign-in | ASP.NET Core Identity with roles and cookie authentication |
| Database | SQL Server through Entity Framework Core 9, code first with migrations |

## Getting started

Requires the .NET 9 SDK and a SQL Server instance.

```bash
git clone https://github.com/pdelen/Snackis.git
cd Snackis
```

Put the connection string in user secrets rather than in `appsettings.json`, so
it never follows the code into git:

```bash
dotnet user-secrets init --project Snackis.Presentation
dotnet user-secrets set "ConnectionStrings:MyConnectionString" "<your connection>" --project Snackis.Presentation
```

Create the database and start the web application:

```bash
dotnet ef database update --project Snackis.Infrastructure --startup-project Snackis.Presentation
dotnet run --project Snackis.Presentation
```

Register an account in the application, create the `Admin` role under
`/admin/roles` and assign it to your account to reach the admin pages.

The API is started separately and answers on
`GET /api/categories/{categoryId}/posts`:

```bash
dotnet run --project Snackis.API
```
