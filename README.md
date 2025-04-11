# PetControl Backend

## Introduction
PetControl Backend is an ASP.NET Core-based project designed to manage pet-related data and operations efficiently. It serves as the backend for the PetControl system, providing APIs and business logic to handle features like pet registration, appointments, and more.

## Features
- Pet, products, Customers and PetSuports registration and management
- Appointment scheduling
- JWT Authentication and authorization

## Tech Stack
- **Language:** C#
- **Framework:** ASP.NET Core
- **Database:** SQL SERVER


## Getting Started

### Prerequisites
- .NET SDK (version 8)
- SQL Server

### Installation
1. Clone the repository:
   ```bash
   git clone https://github.com/mateusf-prog/petcontrol-backend-aspnet.git
   Navigate to the project directory:

2. Navigate to the project directory:
   ```bash
    cd petcontrol-backend-aspnet

3. Restore NuGet packages:
   ```bash
    dotnet restore
4. Update the database connection string in appsettings.json.

## Running the application
1. Apply database migrations:
 bash
 dotnet ef database update
 dotnet run

### Contact
Linkedin: [Mateus Fonseca](https://www.linkedin.com/in/mateus-fprog/)
