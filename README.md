# 🏋️‍♂️ FitHub - Fitness Management Platform

![.NET](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white)
![Angular](https://img.shields.io/badge/Angular-DD0031?style=for-the-badge&logo=angular&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQLServer-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)
![Stripe](https://img.shields.io/badge/Stripe-626CD9?style=for-the-badge&logo=Stripe&logoColor=white)

FitHub is a comprehensive web application designed for fitness centers and their clients. It enables the browsing, purchasing, and reviewing of personalized training plans, integrating cutting-edge technologies such as a **Machine Learning Recommendation Engine** and an **AI Chatbot Assistant**.

This project was developed as part of the **Software Development 1 (RS1)** course for the 2025/26 academic year.

---

## 🚀 Key Features

The application is divided into core (CRUD) and advanced features, aligning with complex business workflows in the e-commerce and fitness industries.

### Core Features
* **Authentication & Authorization:** Secure JWT-based login, registration with a *Password Strength Meter* visualization, and Captcha protection.
* **User Management (Profile Settings):** Management of personal details and body metrics (weight, height) via reactive forms.
* **Dashboard:** A quick overview of user activities (purchased plans, cart items, favorites).
* **Landing Page:** An attractive entry point designed to engage new users.

### Advanced Features
* **Stripe Integration (Payment Processing):** Secure card tokenization and payment processing compliant with PCI-DSS standards.
* **Smart Cart:** Item management with a *"Save for later"* option and center validation (preventing the mixing of programs from different fitness centers in a single order).
* **Machine Learning Recommendations:** An algorithm that suggests personalized fitness programs based on user preferences and history.
* **AI Chatbot Assistant:** An integrated AI assistant to guide users in selecting workouts and navigating the application.
* **Rate & Review System:** A strictly controlled system that allows users to leave reviews **only** for previously purchased programs (Verified Purchases).
* **OAuth2 Integration:** Third-party login via external providers (Google/Facebook).
* **Multitenancy Architecture:** Content and logic filtering based on a `CenterId` token claim, allowing the application to serve multiple fitness centers from a single database.

---

## 🛠️ Technology Stack & Architecture

The project employs a modern *Client-Server* architecture with a strict separation of concerns.

### Backend (.NET Core / C#)
* **Architecture:** Clean Architecture implementing the **CQRS** pattern (Command Query Responsibility Segregation).
* **Libraries:**
  * `MediatR` for CQRS implementation.
  * `FluentValidation` for server-side request validation.
  * `Entity Framework Core` (Code-First approach) for ORM.
* **Security:** JWT Bearer tokens with password encryption (`IPasswordHasher`).

### Frontend (Angular / TypeScript)
* **Approach:** Modular structure utilizing *Lazy Loading*.
* **State Management:** Leveraging Angular **Signals** (`computed`, `signal`) for reactive user state tracking (`AuthFacadeService`).
* **Forms:** Strict implementation of `ReactiveFormsModule` for building and validating client-side forms.
* **Communication:** `HttpClient` and RxJS (`Observables`, `Pipes`) for asynchronous communication with the API and Stripe services.

---

## 📂 Repository Structure

```text
FitHub-RS1/
│
├── backend/                 # .NET API source code (FitHub.API, Application, Domain, Infrastructure, Tests)
│
├── frontend/                # Angular application (components, services, guards, interceptors)
│
├── db-backups/              # SQL Server / SQLite database backup (.bak) for quick restoration
│
├── dokumenti/               # Project documentation, ER diagrams, and the completed defense submission form
│
└── README.md                # This file
