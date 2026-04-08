# Brodihy Habit Tracker — Contexto del proyecto

## Descripción general

Aplicación web de seguimiento de hábitos construida con Blazor (.NET 10), ASP.NET Identity y SQLite.
Nombre del ensamblado: `BrodihyHabitTracker`. Nombre del proyecto en solución: `CSE325-TeamProject`.

---

## Stack tecnológico

| Capa | Tecnología |
|------|-----------|
| Frontend | Blazor Web App (.NET 10), Bootstrap 5, Bootstrap Icons 1.11.3 |
| Tipografía | Poppins (Google Fonts, pesos 300/400/500/600) |
| Backend | ASP.NET Core 10, Blazor Server (Interactive Server render mode) |
| Autenticación | ASP.NET Identity (`UserManager`, `SignInManager`) |
| ORM | Entity Framework Core 10 |
| Base de datos | SQLite (`habits.db`, creada automáticamente en startup) |

---

## Arquitectura de rendering

Las páginas usan dos modos de renderizado:

- **Static SSR** (sin `@rendermode`): páginas de auth (`/login`, `/register`, `/logout`).
  Pueden escribir cookies y hacer redirect HTTP. Formularios manejados con `[SupplyParameterFromForm]`.
- **Interactive Server** (`@rendermode InteractiveServer`): dashboard y páginas con estado reactivo.
  Se conectan via SignalR circuit. Auth state viene de `CascadingAuthenticationState`.

---

## Estructura de archivos relevante

```
CSE325-TeamProject/
├── Components/
│   ├── _Imports.razor              # Usings globales para todos los componentes
│   ├── App.razor                   # Root HTML: carga Poppins, Bootstrap Icons, CSS
│   ├── Routes.razor                # Router con AuthorizeRouteView + RedirectToLogin
│   ├── Layout/
│   │   ├── AuthLayout.razor        # Layout sin sidebar — usado por Login y Register
│   │   ├── MainLayout.razor        # Layout con sidebar — usado por páginas protegidas
│   │   └── NavMenu.razor           # Sidebar con nav + info de usuario + logout
│   ├── Pages/
│   │   ├── Home.razor              # Dashboard principal [Authorize] + skeleton loaders
│   │   └── Auth/
│   │       ├── Login.razor         # Static SSR — POST con SignInManager
│   │       ├── Register.razor      # Static SSR — POST con UserManager.CreateAsync
│   │       └── Logout.razor        # Static SSR — SignOutAsync + redirect /login
│   └── Shared/
│       └── RedirectToLogin.razor   # Componente usado en NotAuthorized de AuthorizeRouteView
├── Data/
│   └── ApplicationDbContext.cs     # IdentityDbContext<User> con DbSet<Habit> y DbSet<HabitCompletion>
├── Models/
│   ├── User.cs                     # Extiende IdentityUser: FirstName, LastName, CreatedAt
│   ├── Habit.cs                    # Id, Name, Description, Type(enum), WeeklyFrequency, UserId, IsActive
│   └── HabitCompletion.cs          # Id, HabitId, CompletionDate, UserId — índice único (HabitId+Date+UserId)
├── Services/
│   └── HabitService.cs             # CRUD de hábitos + ToggleCompletion + GetWeeklyProgress
├── Program.cs                      # DI, Identity config, cookie paths, EF, startup DB creation
├── appsettings.json                # ConnectionStrings.DefaultConnection = "Data Source=habits.db"
└── wwwroot/
    └── app.css                     # Design system completo con CSS variables y skeleton animation
```

---

## Modelos de datos

### `User` (extiende `IdentityUser`)
```
FirstName   string?
LastName    string?
CreatedAt   DateTime
Habits      ICollection<Habit>
```

### `Habit`
```
Id                int         (PK)
Name              string      (max 100, required)
Description       string?     (max 500)
Type              HabitType   (Physical | Mental | SocialEmotional | Spiritual)
WeeklyFrequency   int         (1–7)
UserId            string      (FK → User)
CreatedAt         DateTime
IsActive          bool        (soft delete)
Completions       ICollection<HabitCompletion>
```

### `HabitCompletion`
```
Id              int       (PK)
HabitId         int       (FK → Habit)
CompletionDate  DateTime
UserId          string    (FK → User)
```
Índice único en `(HabitId, CompletionDate, UserId)` para evitar duplicados.

---

## Flujo de autenticación

1. Usuario no autenticado accede a cualquier ruta → `AuthorizeRouteView` detecta falta de auth → `RedirectToLogin` redirige a `/login`.
2. `/login` — static SSR, `EditForm` con `FormName="login"`, `OnValidSubmit` llama `SignInManager.PasswordSignInAsync`. Si OK → redirect `/` con `forceLoad: true`.
3. `/register` — static SSR, crea `User` via `UserManager.CreateAsync`, luego `SignInManager.SignInAsync`, redirect `/`.
4. `/logout` — static SSR, `SignInManager.SignOutAsync`, redirect `/login`.
5. Layout principal accede a auth state via `[CascadingParameter] Task<AuthenticationState>` para mostrar nombre/initiales del usuario.

---

## Servicios

### `HabitService`
```csharp
GetUserHabitsAsync(userId)           // hábitos activos del usuario con completions
GetHabitByIdAsync(id, userId)
CreateHabitAsync(habit)
UpdateHabitAsync(habit)
DeleteHabitAsync(id, userId)         // soft delete (IsActive = false)
ToggleCompletionAsync(habitId, date, userId)  // retorna bool (true=added, false=removed)
GetWeeklyProgressAsync(habitId, userId)       // cuenta de completions en semana actual
```

---

## Configuración Identity

Requisitos de contraseña relajados para desarrollo:
- Mínimo 6 caracteres
- No requiere mayúsculas, dígitos ni caracteres especiales
- Email único requerido

Cookie configurada con `LoginPath = "/login"`, `SlidingExpiration = true`, `ExpireTimeSpan = 14 días`.

---

## Sistema de diseño

Fuente: **Poppins** (pesos 300, 400, 500, 600 — sin bold 700/800).
Iconos: **Bootstrap Icons 1.11.3** via CDN.
Paleta principal: indigo (`#6366F1`) sobre sidebar oscura (`#1E1B4B`), fondo gris claro (`#F1F5F9`).
Skeleton loaders: animación `shimmer` con `background-size: 200%` en gradiente.

---

## Lo que falta implementar

- [ ] Página `/habits` — lista completa de hábitos con opciones de editar/eliminar
- [ ] Página `/habits/new` y `/habits/edit/:id` — formulario CRUD de hábito
- [ ] Toggle de completions desde la UI (checkbox diario)
- [ ] Página `/progress` — gráficas de progreso semanal/mensual
- [ ] Página `/settings` — editar perfil (nombre, contraseña)
- [ ] Cálculo real de `completedToday` y `currentStreak` en Home
- [ ] Refactor de `DbContext` en Blazor Server a `IDbContextFactory` para evitar problemas de concurrencia en circuitos de larga vida
