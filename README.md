# El-Mooo Clinic Management System

A comprehensive, full-stack enterprise solution developed using **ASP.NET Core MVC** and **Clean Architecture** principles. This system is engineered to streamline healthcare workflows, optimize patient data management, and automate complex medical scheduling.

---

## Core Functionalities

* **Intelligent Appointment Scheduling**: An algorithm-driven module that dynamically calculates available time slots based on medical staff shifts, patient queues, and procedure durations to eliminate scheduling conflicts.
* **High-Performance Data Filtering**: Implementation of **AJAX** for real-time, asynchronous searching across patient and doctor records, ensuring a seamless user experience.
* **Automated Asset Management**: A secure file-handling system for medical profiles, featuring logical defaults and gender-based avatar management.
* **Architectural Integrity**: Built on a decoupled architecture using the **Repository Pattern** and **Unit of Work** to ensure the codebase remains maintainable and testable.
* **Data Validation & Integrity**: Multi-layered validation (Client-side and Server-side) to ensure all medical records meet strict integrity standards.

---

## Technical Architecture & Stack

* **Backend**: **C#**, **ASP.NET Core MVC 9.0** (Primary Development Focus).
* **Database Layer**: **Entity Framework Core**, **SQL Server**.
* **Design Patterns**: **Repository Pattern**, **Unit of Work**, **Dependency Injection (DI)**.
* **Data Handling**: Implementation of **DTOs (Data Transfer Objects)** for secure and efficient data mapping.

> **Note on UI/UX Implementation**: While the system features a responsive interface built with HTML5, CSS3, and Bootstrap, the **Frontend components were developed with AI assistance**. This strategic choice allowed for a concentrated focus on **Backend Logic, Database Design, and API Architecture**, ensuring the system’s core is built to production standards.

---

## Project Previews


### APIs

<img width="1891" height="864" alt="image" src="https://github.com/user-attachments/assets/948c5857-2b80-492f-b4d9-84efb67b73c2" />
<img width="1838" height="799" alt="Screenshot 2026-04-19 012509" src="https://github.com/user-attachments/assets/07a63163-e821-4816-a224-041e2098fe44" />
<img width="1865" height="876" alt="Screenshot 2026-04-19 012447" src="https://github.com/user-attachments/assets/3f062858-d3b0-452c-9706-09833194a8dd" />
<img width="1861" height="895" alt="Screenshot 2026-04-19 012613" src="https://github.com/user-attachments/assets/21dc148e-761b-4aa3-a4eb-b583f5e4c68c" />



### Accessed Pages Authorization
<img width="1888" height="872" alt="image" src="https://github.com/user-attachments/assets/10867bdc-8e8e-4a17-accb-ae9c126a1307" />
<img width="1882" height="884" alt="image" src="https://github.com/user-attachments/assets/59a21902-f311-42fd-9c09-0705c681902c" />


### Departments Management
<img width="1907" height="747" alt="image" src="https://github.com/user-attachments/assets/0ae6a6c8-7fcb-4549-83f6-f8f5afae8360" />
<img width="1905" height="831" alt="image" src="https://github.com/user-attachments/assets/b53095b8-0cc4-48ee-8c88-b2ea92d97cc3" />


### Medical Staff Directory
<img width="1768" height="908" alt="image" src="https://github.com/user-attachments/assets/999880ce-5a36-4e9e-9f16-5f57cc4826b1" />
<img width="1896" height="901" alt="image" src="https://github.com/user-attachments/assets/0e1f6a34-497a-4cd7-812e-d8ca004f9c5d" />
<img width="1893" height="870" alt="image" src="https://github.com/user-attachments/assets/cd3a8564-1ec7-4efd-8ad6-f3a3ae6a7990" />
<img width="1889" height="842" alt="image" src="https://github.com/user-attachments/assets/a165ad7c-3427-442e-816d-a4606d92d252" />
<img width="1916" height="854" alt="image" src="https://github.com/user-attachments/assets/74f5b54d-17e6-4089-b5ab-29b92a2200be" />


### Patient Medical History
<img width="1828" height="911" alt="image" src="https://github.com/user-attachments/assets/ca22d977-9ca7-4dcc-b5ca-4a1e478cb71e" />
<img width="1893" height="833" alt="image" src="https://github.com/user-attachments/assets/30fbfaad-b486-40ac-ba69-316ce320c5f1" />


### Appointment Management
<img width="1700" height="858" alt="image" src="https://github.com/user-attachments/assets/184c1b1c-3c7c-4936-8f34-68a3d7113397" />
<img width="1918" height="900" alt="image" src="https://github.com/user-attachments/assets/48eca9b4-f70c-4c71-b4f5-947946b1fb25" />
<img width="1892" height="878" alt="image" src="https://github.com/user-attachments/assets/17931af2-3ad0-49d2-8330-039aae1d310a" />
<img width="1887" height="889" alt="image" src="https://github.com/user-attachments/assets/6246bf44-2181-4958-86eb-b6f174676e88" />
<img width="1886" height="867" alt="image" src="https://github.com/user-attachments/assets/23479b90-a8a8-4b73-8e68-844f8dce1c64" />
<img width="1893" height="906" alt="image" src="https://github.com/user-attachments/assets/70c949c6-bee3-4366-b6cc-f8628a6b660f" />

---

## Development Roadmap

The El-Mooo Clinic project is an ongoing initiative. Future iterations will include:
* **Financial Module**: Implementation of billing, insurance processing, and automated invoicing.
* **Security Enhancement**: Transitioning to **JWT Authentication** for cross-platform API security.
* **Data Analytics**: A dashboard for visualizing clinic performance and patient demographics.

---

## Feedback & Contributions

This project is open for **professional feedback and architectural suggestions**. I am committed to continuous improvement and welcome insights regarding performance optimization or feature enhancements. If you have any suggestions, please:
1. Open a **Technical Issue**.
2. Submit a **Pull Request** with detailed documentation of changes.
3. Connect via **LinkedIn** for professional inquiries.

---

## 🔐 Default Credentials (For Testing)

Once you run the migrations and start the app, you can use the following credentials to explore the system:

* **Role:** Administrator
* **Email:** Receptionist1@elmooo.com
* **Password:** Receptionist1@123

* **Role:** Doctor
* **Email:** Moamenazm@elmooo.com
* **Password:** Moamenazm@123
---

## Local Environment Setup

1. **Clone Repository**:
  ```
   git clone (https://github.com/moamenAboelazm/El_Mooo-Clinic.git)
  ```
   
3. Configuration: Open the solution in Visual Studio and update the DefaultConnection string in appsettings.json to point to your local SQL Server.

4. Database Migration: Execute the following command in the Package Manager Console:
```Update-Database```

5. Execution: Press F5 to build and run the application.

Developed by **Moamen Aboelazm**
