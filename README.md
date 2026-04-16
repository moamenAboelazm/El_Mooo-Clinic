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

### Departments Management
<img width="1907" height="747" alt="image" src="https://github.com/user-attachments/assets/0ae6a6c8-7fcb-4549-83f6-f8f5afae8360" />
<img width="1905" height="831" alt="image" src="https://github.com/user-attachments/assets/b53095b8-0cc4-48ee-8c88-b2ea92d97cc3" />


### Medical Staff Directory
<img width="1768" height="908" alt="image" src="https://github.com/user-attachments/assets/999880ce-5a36-4e9e-9f16-5f57cc4826b1" />
<img width="1896" height="901" alt="image" src="https://github.com/user-attachments/assets/0e1f6a34-497a-4cd7-812e-d8ca004f9c5d" />
<img width="1892" height="845" alt="image" src="https://github.com/user-attachments/assets/d5f5b4ec-9a7e-4f5c-95a4-c8cc8b168c85" />
<img width="1893" height="870" alt="image" src="https://github.com/user-attachments/assets/cd3a8564-1ec7-4efd-8ad6-f3a3ae6a7990" />


### Appointment Management
<img width="1700" height="858" alt="image" src="https://github.com/user-attachments/assets/184c1b1c-3c7c-4936-8f34-68a3d7113397" />
<img width="1918" height="900" alt="image" src="https://github.com/user-attachments/assets/48eca9b4-f70c-4c71-b4f5-947946b1fb25" />
<img width="1893" height="906" alt="image" src="https://github.com/user-attachments/assets/70c949c6-bee3-4366-b6cc-f8628a6b660f" />


### Patient Medical History
<img width="1893" height="833" alt="image" src="https://github.com/user-attachments/assets/30fbfaad-b486-40ac-ba69-316ce320c5f1" />


### Accessed Pages Authorization
<img width="1882" height="884" alt="image" src="https://github.com/user-attachments/assets/59a21902-f311-42fd-9c09-0705c681902c" />


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
