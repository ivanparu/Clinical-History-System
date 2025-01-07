# Clinical History System 🏥

Welcome to the **Clinical History System**, a project developed to emulate the operations of a hospital's clinical history system. It allows the management of patients, their clinical histories, diagnoses, evaluations, episodes, and epicrises. The system supports interactions from both patients and medical staff.

## Objectives 📋

The goal of this system is to:
- Provide a comprehensive solution for managing a medical practice.
- Support administrators, doctors, and staff in managing clinical data, including patient information, diagnoses, and episodes.
- Enable patients to view their clinical history in a secure and user-friendly way.

Developed using **C# (ASP.NET MVC Core with Entity Framework)**, this system integrates best practices for role-based access control and database management.

---

## Features and Functionalities ⌨️

### General
- **Role-Based Access**:
  - Patients: View-only access to their clinical history.
  - Employees: Manage patients, doctors, and episodes.
  - Doctors: Manage evolutions, diagnoses, and close episodes.

### Patients
- View their clinical history, episodes, and evolutions.
- Update contact information (e.g., phone, address) but cannot modify critical personal details.

### Employees
- Manage patients, employees, and doctors (CRUD operations).
- Create episodes for patients with details like reason and description.
- Cannot modify or delete existing clinical history records.

### Doctors
- Create and manage evolutions within episodes.
- Close evolutions and episodes, ensuring all validations are met.
- Create epicrises with diagnoses and recommendations.

### Clinical History
- Automatically created when a patient is registered.
- Accessible only by authorized roles:
  - Patients can view their clinical history.
  - Employees and doctors can interact with it based on their roles.

### Episodes
- Created by employees for each patient.
- Include details like reason, description, and timestamps.
- Can only be closed by doctors after all evolutions are complete.

### Evolutions
- Managed by doctors to record patient progress.
- Include details like diagnosis, notes, and timestamps.
- Notes can also be added by employees for administrative purposes.

### Epicrises and Diagnoses
- **Epicrisis**: A summary of an episode, created by doctors or automatically generated for administrative closures.
- **Diagnosis**: Detailed descriptions and recommendations, manually added by doctors.

---

## Technologies Used 🛠️
- **C# (ASP.NET MVC Core)**: Framework for the web application.
- **Entity Framework Core**: ORM for database management.
- **SQL Server**: Primary database provider.
- **Identity Framework**: For authentication and role management.
- **Bootstrap**: For responsive UI design.

---

## How to Run the Project 🚀

1. Clone this repository:
   ```bash
   git clone https://github.com/ivanparu/clinical-history-system.git
