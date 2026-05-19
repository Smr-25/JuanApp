# 🛍️ JuanApp

JuanApp is a modern multi-layered ASP.NET Core MVC e-commerce application built with **.NET 10**.

The project includes both customer-facing shopping functionality and a fully featured admin management panel.

---

# 📌 Table of Contents

- [Overview](#overview)
- [Architecture](#architecture)
- [Features](#features)
- [Tech Stack](#tech-stack)
- [Requirements](#requirements)
- [Installation](#installation)
- [Configuration](#configuration)
- [Database & Migrations](#database--migrations)
- [Running the Project](#running-the-project)
- [Default Users & Roles](#default-users--roles)
- [Project Structure](#project-structure)
- [Useful Commands](#useful-commands)
- [Notes](#notes)

---

# 📖 Overview

JuanApp is a complete e-commerce solution developed using clean layered architecture principles.

The application contains essential modules required for modern online shopping platforms, including:

- Product catalog
- Shopping cart & checkout
- User authentication & authorization
- Order management
- Blog system
- Subscriber module
- Admin dashboard & content management

---

# 🏗️ Architecture

The solution is organized into 4 main layers:

## 🔹 JuanApp.PL (Presentation Layer)

Responsible for:

- ASP.NET Core MVC Controllers & Views
- Routing
- UI rendering
- Application startup (`Program.cs`)

---

## 🔹 JuanApp.BLL (Business Logic Layer)

Contains:

- Business services
- Interfaces
- DTOs
- Application rules & use-cases

---

## 🔹 JuanApp.DLL (Data Access Layer)

Responsible for:

- Entity Framework Core `DbContext`
- Database configurations
- Migrations
- Database seeding

---

## 🔹 JuanApp.Core (Domain Layer)

Contains:

- Domain entities
- Base models
- Core abstractions

---

# ✨ Features

- User registration & login
- Profile management
- Role-based authorization using ASP.NET Core Identity
- Google OAuth authentication
- Product, category, color & size management
- Shopping basket & checkout flow
- Order management system
- Blog & subscriber modules
- Admin area for product & content management

---

# ⚙️ Tech Stack

| Technology | Description |
|------------|-------------|
| .NET 10 | ASP.NET Core MVC Framework |
| Entity Framework Core 10 | ORM |
| SQL Server | Database provider |
| ASP.NET Core Identity | Authentication & authorization |
| MailKit | Email sending service |

---

# 📋 Requirements

Before running the project, make sure you have:

- **.NET SDK 10.0**
- **SQL Server** (local or remote instance)

Optional:

```bash
dotnet tool install --global dotnet-ef
```

---

# 🚀 Installation

Clone the repository and restore dependencies:

```bash
dotnet restore JuanApp.slnx
```

---

# ⚡ Configuration

Main configuration files:

- `JuanApp.PL/appsettings.json`
- `JuanApp.PL/appsettings.Development.json`

Configure the following settings inside `appsettings.Development.json`:

- `ConnectionStrings:DefaultConnection`
- `EmailSettings:SmtpServer`
- `EmailSettings:Port`
- `EmailSettings:Username`
- `EmailSettings:From`
- `EmailSettings:Password`
- `Authentication:Google:ClientId`
- `Authentication:Google:ClientSecret`

Example connection string:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=JuanAppDb;Trusted_Connection=True;TrustServerCertificate=True"
}
```

---

# 🗄️ Database & Migrations

Migration files are located under:

```text
JuanApp.DLL/Data/Migrations
```

Update the database using:

```bash
dotnet ef database update --project JuanApp.DLL --startup-project JuanApp.PL
```

---

# ▶️ Running the Project

Run the application from the root directory:

```bash
dotnet run --project JuanApp.PL
```

Default application URL:

```text
http://localhost:5195
```

---

# 🔐 Default Users & Roles

The application automatically seeds default roles and admin accounts using `DbSeeder`.

## SuperAdmin

| Field | Value |
|------|------|
| Email | `superadmin@juanapp.com` |
| Password | `SuperAdmin@123` |

---

## Admin

| Field | Value |
|------|------|
| Email | `admin@juanapp.com` |
| Password | `Admin@123` |

> ⚠️ For security reasons, change these credentials immediately in production environments.

---

# 📂 Project Structure

```text
JuanApp/
├── JuanApp.PL/      # MVC UI, Controllers, Views, Program.cs
├── JuanApp.BLL/     # Business logic, DTOs, Services
├── JuanApp.DLL/     # EF Core DbContext, Migrations, Seeding
├── JuanApp.Core/    # Domain entities & models
└── JuanApp.slnx
```

---

# 🧑‍💻 Useful Commands

## Build

```bash
dotnet build JuanApp.slnx -c Release
```

## Run Tests

```bash
dotnet test JuanApp.slnx -c Release
```

---

# 📝 Notes

- The project targets **.NET 10** (`net10.0`)
- If Google OAuth credentials are missing, only a warning is displayed and Google login will be disabled
- Environment-based configuration is supported using:

```text
appsettings.{Environment}.json
```

---

# 🤝 Contributing

Contributions are welcome!

1. Fork the repository
2. Create a feature branch
3. Commit your changes
4. Open a Pull Request

---

# 📄 License

This project is intended for educational and demonstration purposes.
