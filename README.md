# Brodihy Habit Tracker

A .NET Blazor web application for tracking daily habits and monitoring progress over time. Users can create, manage, and track habits across different categories with weekly frequency goals.

## 📋 Project Overview

Brodihy Habit Tracker helps users build better habits by:

- Creating personalized habits to track
- Categorizing habits by type (Physical, Mental, Social/Emotional, Spiritual)
- Setting weekly frequency goals (1-7 times per week)
- Marking habits as completed each day
- Visualizing weekly progress

## 🚀 Technology Stack

- **Framework:** .NET 10 Blazor Web App
- **Database:** SQLite (with Entity Framework Core)
- **Authentication:** ASP.NET Core Identity
- **Version Control:** Git & GitHub

## 📦 Prerequisites

Before running this project, make sure you have the following installed:

| Requirement | Version                 | Verification Command |
| ----------- | ----------------------- | -------------------- |
| .NET SDK    | 10.0 or later           | `dotnet --version`   |
| Git         | Latest                  | `git --version`      |
| Code Editor | VS Code / Visual Studio | -                    |

## 🔧 Setup Instructions

### 1. Clone the Repository

```bash
git clone https://github.com/Hyrummorales23/CSE325-TeamProject.git
```

### 2. Navigate to the Project Folder

```bash
cd CSE325-TeamProject
```

### 3. Verify .NET Installation

```bash
dotnet --version
```

Expected output: `10.x.x` or higher

### 4. Restore Dependencies

```bash
dotnet restore
```

### 5. Build the Project

```bash
dotnet build
```

This compiles the project and verifies everything is working correctly.

### 6. Run the Application

```bash
dotnet run
```

### 7. Open in Browser

Once the application starts, you'll see output like:

```
Now listening on: http://localhost:5084
```

Open your browser and navigate to:

- **http://localhost:5084** (or the port shown in your terminal)

## 📁 Project Structure

```
CSE325-TeamProject/
├── Data/
│   └── ApplicationDbContext.cs      # Database context
├── Models/
│   ├── User.cs                       # Custom user model
│   ├── Habit.cs                      # Habit entity
│   └── HabitCompletion.cs            # Daily completion tracking
├── Services/
│   └── HabitService.cs               # Business logic for habits
├── Program.cs                        # Application entry point
├── appsettings.json                  # Configuration settings
└── brodihy.db                        # SQLite database (created at runtime)
```

## 🗄️ Database Setup

The database is automatically created when you run the application for the first time. It includes the following tables:

- **AspNetUsers** - User accounts with custom fields (FirstName, LastName)
- **Habits** - User-created habits with categories and frequency goals
- **HabitCompletions** - Daily habit completions (prevents duplicate entries)
- **AspNetRoles** - User roles
- **AspNetUserRoles** - Role assignments

## 🔐 Authentication

The application uses ASP.NET Core Identity for user management. Users can:

- Register for a new account
- Log in with existing credentials
- Manage their own habits (isolated by user ID)

## 🎯 Core Features (In Development)

| Feature                 | Status         | Description                             |
| ----------------------- | -------------- | --------------------------------------- |
| User Registration/Login | ✅ Complete    | Identity system configured              |
| Create Habits           | 🚧 In Progress | Add new habits with details             |
| Edit Habits             | 🚧 In Progress | Modify existing habits                  |
| Delete Habits           | 🚧 In Progress | Remove unwanted habits                  |
| Daily Checkmarks        | 🚧 In Progress | Mark habits as completed                |
| Habit Categories        | 🚧 In Progress | Filter by type (Physical, Mental, etc.) |
| Weekly Frequency        | 🚧 In Progress | Set and track weekly goals              |
| Progress Visualization  | 🚧 In Progress | View weekly progress charts             |

## 👥 Team Members

- Hyrum Daniel Morales Rosado
- Bruce Bennet
- Rommel Ariel Juarez
- Diego Josue Arriola Vargas

## 📊 Project Management

- **Trello Board:** [Brodihy Habit Tracker Trello](https://trello.com/b/EyvONhh2/brodihy-habit-tracker)
- **GitHub Repository:** [CSE325-TeamProject](https://github.com/Hyrummorales23/CSE325-TeamProject)

## 🛠️ Troubleshooting

### Common Issues and Solutions

**Issue:** `dotnet` command not found

- **Solution:** Install .NET SDK from [dotnet.microsoft.com](https://dotnet.microsoft.com/download)

**Issue:** Port 5084 is already in use

- **Solution:** The app will automatically use a different port. Check the terminal for the correct URL.

**Issue:** Build errors after pulling latest changes

- **Solution:** Run `dotnet restore` and `dotnet build` again

**Issue:** Database migration errors

- **Solution:** Delete the `brodihy.db` file and run `dotnet run` again to recreate it

## 📝 Development Workflow

1. **Always pull latest changes before starting:**

   ```bash
   git pull origin main
   ```

2. **Create a new branch for your feature:**

   ```bash
   git checkout -b feature/your-feature-name
   ```

3. **Commit your changes with meaningful messages:**

   ```bash
   git add .
   git commit -m "Add detailed description of changes"
   ```

4. **Push your branch and create a pull request:**
   ```bash
   git push origin feature/your-feature-name
   ```

## 🚀 Deployment

The application will be deployed to a cloud service (Azure/AWS) upon completion.

## 📚 Resources

- [.NET Documentation](https://docs.microsoft.com/en-us/dotnet/)
- [Blazor Documentation](https://learn.microsoft.com/en-us/aspnet/core/blazor/)
- [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/)
- [ASP.NET Core Identity](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/identity/)

---
