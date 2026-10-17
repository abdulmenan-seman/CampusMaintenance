# 🏫 Campus Issue Reporting & Maintenance Tracker

A cross-platform campus maintenance management system that helps universities report, assign, track, and resolve facility and equipment issues through a centralized, role-based platform.

**Status:** 🚧 In Development

## 📌 Overview

Universities face daily maintenance challenges, including malfunctioning laboratory computers, Wi-Fi outages, broken projectors, electrical problems, leaking pipes, and damaged classroom facilities.

Traditionally, these problems are reported through phone calls, face-to-face communication, or chat groups. This can lead to lost reports, unclear responsibilities, delayed maintenance, and limited visibility into maintenance operations.

The **Campus Issue Reporting & Maintenance Tracker** aims to solve these problems by providing a centralized platform where campus users can report issues, administrators can coordinate maintenance activities, and technicians can manage and document assigned work.

The system is designed to support a university with multiple campuses and buildings.

## 🎯 Project Objectives

- Centralize campus maintenance reports in one platform.
- Make issue reporting simple and accessible.
- Improve assignment and accountability for maintenance work.
- Allow reporters to track the progress of their issues.
- Establish a consistent ticket lifecycle from submission to closure.
- Help administrators monitor technician workloads and maintenance performance.
- Maintain a reliable history of assignments, status changes, and resolutions.
- Provide a responsive experience across mobile, tablet, and web platforms.

## 👥 System Actors and Roles

### 1. Reporter

A registered student, staff member, or other authorized campus user who identifies and reports a maintenance problem.

**Responsibilities:**
- Register and sign in.
- Submit maintenance tickets with descriptions, categories, priorities, and locations.
- Browse permitted campus-wide issue summaries.
- View and track their own reports.
- Review ticket status and permitted resolution details.

### 2. Technician

An authorized maintenance worker whose account is created by an administrator.

**Responsibilities:**
- View assigned maintenance tickets.
- Review issue details and locations.
- Start work and record progress notes.
- Document completed repairs.
- Mark assigned tickets as resolved.
- Review current assignments and previously completed work.

### 3. Administrator

The user responsible for managing the maintenance process and system configuration.

**Responsibilities:**
- Monitor maintenance operations through dashboards.
- Manage campuses and buildings.
- Create and manage technician accounts.
- Assign and reassign maintenance tickets.
- Review technician resolutions.
- Close resolved tickets and reopen tickets when additional work is required.
- Monitor ticket history, technician workloads, and maintenance statistics.

**Security principle:** Role permissions are enforced by the backend API. Hiding a button in the user interface is not sufficient authorization.

## 🔄 Ticket Lifecycle

The standard maintenance workflow is:

`Submitted → Assigned → In Progress → Resolved → Closed`

- **Submitted:** A reporter creates a maintenance ticket.
- **Assigned:** An administrator assigns an eligible technician.
- **In Progress:** The technician begins maintenance work.
- **Resolved:** The technician records the completed work.
- **Closed:** The administrator reviews and officially closes the ticket.

An administrator can reopen a resolved or closed ticket when further maintenance is needed. The reason and subsequent actions must be recorded in the ticket history.

## 🛠️ Technology Stack

### Frontend
- **Flutter** — Cross-platform application development.
- **Dart** — Programming language.
- **Riverpod** — State management and dependency management.
- Responsive UI for mobile, tablet, and web.

### Backend
- **ASP.NET Core Web API** — REST API and business operations.
- **C#** — Backend programming language.
- **ASP.NET Core Identity** — User and role management.
- **JWT Bearer Authentication** — API authentication, with an appropriate token lifecycle strategy.
- **Entity Framework Core** — Database access and migrations.
- **Clean Architecture** — Separation of business rules, application logic, and infrastructure.

### Database
- **PostgreSQL** — Relational database management system.

### Development and Quality
- **Git and GitHub** — Version control and collaboration.
- Automated unit, integration, API, and Flutter widget tests.
- Environment-based configuration and database migrations.

> Technology choices and dependencies may be refined as implementation progresses.

## 🏗️ System Architecture

The system follows a layered architecture.

```text
┌─────────────────────────────────────────────┐
│             Flutter Application            │
│                                             │
│  Reporter UI | Technician UI | Admin UI     │
│  Riverpod | Routing | API Client            │
└─────────────────────┬───────────────────────┘
                      │ HTTPS / REST API
                      ▼
┌─────────────────────────────────────────────┐
│            ASP.NET Core Web API             │
│                                             │
│  API Layer                                  │
│  Application Layer                          │
│  Domain Layer                               │
│  Infrastructure Layer                       │
│                                             │
│  Authentication | Authorization             │
│  Validation | Business Rules | Audit        │
└─────────────────────┬───────────────────────┘
                      │ Entity Framework Core
                      ▼
┌─────────────────────────────────────────────┐
│                PostgreSQL                   │
│                                             │
│  Users | Campuses | Buildings | Tickets     │
│  Assignments | History | Work Notes         │
└─────────────────────────────────────────────┘
```

The Flutter application communicates with the backend through the API. The backend validates requests, enforces permissions, applies business rules, and manages database operations.

The Flutter client never connects directly to PostgreSQL.

## 🧩 Main System Modules

| Module | Description |
|---|---|
| Authentication and Accounts | Registration, login, roles, sessions, and account management |
| Campus and Building Management | Management of campus locations and buildings |
| Ticket Reporting and Browsing | Issue submission, ticket details, search, filtering, and pagination |
| Technician Management | Technician provisioning, profiles, specialties, and account status |
| Assignment and Maintenance Workflow | Assignment, reassignment, work notes, resolution, closure, and reopening |
| Dashboards and Analytics | Role-specific summaries, workload monitoring, and maintenance statistics |
| Administration, Security, and Audit | Authorization, operational controls, and traceable activity history |
| Testing and Deployment | Automated tests, migrations, deployment, backups, and monitoring |

## 🗄️ Core Data Entities

The initial data model includes:

- **ApplicationUser:** User identity and account information managed through ASP.NET Core Identity.
- **TechnicianProfile:** Technician-specific information and specialties.
- **Campus:** University campus information.
- **Building:** A building belonging to a campus.
- **Ticket:** A reported maintenance issue and its current state.
- **TicketAssignment:** Assignment and reassignment records over time.
- **TicketHistory:** A chronological audit trail of important ticket events.
- **TicketComment / WorkNote:** Progress notes, resolution details, and comments with appropriate visibility controls.

Database relationships, constraints, and indexes will be defined before production migrations are finalized.

## 🔐 Security and Business Rules

- Public registration creates Reporter accounts only.
- Only authorized administrators can provision technicians.
- Privileged roles cannot be self-assigned during public registration.
- Users can access only the records permitted by their roles and resource ownership.
- Technicians can work only on authorized assigned tickets.
- Only administrators can close or reopen tickets.
- Resolution details are required before a ticket can be marked resolved.
- Important status and assignment changes are recorded in ticket history.
- Database relationships and transactions preserve data consistency.
- Sensitive configuration and credentials must not be committed to Git.

## 🚀 Implementation Roadmap

Development will proceed incrementally, with testing throughout the project.

- [ ] **Phase 1:** Project foundation and development environment
- [ ] **Phase 2:** Database design and authentication
- [ ] **Phase 3:** Campus and building management
- [ ] **Phase 4:** Ticket reporting and browsing
- [ ] **Phase 5:** Technician management and assignment
- [ ] **Phase 6:** Maintenance workflow and audit history
- [ ] **Phase 7:** Dashboards and analytics
- [ ] **Phase 8:** Automated testing, security review, and deployment

Each phase should produce a working and testable result before the project moves to the next major capability.

## 🧪 Testing Strategy

The project will use multiple levels of testing:

- **Unit tests:** Validate business rules and status transitions.
- **Integration tests:** Verify database persistence and transaction behavior.
- **API tests:** Verify endpoints, authentication, authorization, and validation.
- **Flutter widget tests:** Verify screen behavior and form validation.
- **End-to-end tests:** Validate the complete maintenance lifecycle.

The primary acceptance scenario is:

`Report Issue → Assign Technician → Start Work → Resolve → Close`

The scenario must preserve correct ticket state, permissions, and activity history.

## 🌱 Future Enhancements

Potential future improvements include:

- Maintenance issue photo attachments.
- Push notifications and email alerts.
- Advanced maintenance analytics.
- Service-level targets and escalation.
- Recurring and preventive maintenance scheduling.
- AI-assisted issue classification and priority suggestions.
- Duplicate issue detection.
- Offline support and synchronization.

These enhancements will be considered after the essential reporting and maintenance workflow is reliable.

## 🤝 Development and Contribution

The project is being developed using a modular architecture to make the codebase easier to understand, test, maintain, and extend.

Contributions should follow the established folder structure, coding conventions, security rules, and testing requirements.

Before contributing:

1. Review the system requirements and architecture.
2. Understand the responsibilities of the module you are changing.
3. Keep business rules in the appropriate backend layer.
4. Add or update relevant tests.
5. Avoid committing secrets, credentials, or environment-specific configuration.

## 📄 Project Documentation

Project documentation will cover:

- System requirements and actor responsibilities.
- Architecture and module specifications.
- Database entity relationships.
- API endpoint contracts.
- Ticket lifecycle and authorization rules.
- Development and deployment instructions.
- Testing procedures.

## 📜 License

A license will be selected and added when the project's distribution and contribution policy is finalized.

---

**Campus Issue Reporting & Maintenance Tracker**  
*Making campus maintenance more organized, transparent, and accountable.*
