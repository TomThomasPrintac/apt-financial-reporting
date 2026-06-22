# CLAUDE.md — APT (Advanced Processing of Financial Reports) System

## What This Project Is

**APT** is an accounting firm's internal system for automating financial report generation and payroll-ledger reconciliation. It solves a real business problem: Printek & Co., a small accounting firm in Nes Ziona (Israel), spends 60-70% of intern time on manual Excel work, duplicate entry across linked files, error-prone copy-paste, and slow report assembly. APT replaces this manual, error-prone process with automated data ingestion, cross-matching, and report generation.

This is a **SAD course project** (Ben-Gurion University, Industrial Engineering & Management) implementing a WinForms desktop system for internal use by the firm's 6-person team: the managing partner (Senior Accountant), interns, bookkeepers, and system admin.

### The Business Problem

The firm manages ~70 clients, each with monthly/quarterly reporting cycles. The workflow is:
1. Files arrive from external systems: trial balance (Chashbshbta), payroll (Oketz), depreciation forms
2. Interns manually extract data into Excel, cross-match payroll against ledger accounts
3. Errors are discovered during manual review (40-minute hunts for mismatches)
4. Reports are assembled in Word, then converted to PDF, then signed

The cost: 15-20 hours/month per intern on non-analytical drudgery. The risk: high error rates, audit trail gaps, regulatory exposure.

### The Solution

APT automates the data pipeline:
1. **Upload** → Source files land in the system
2. **Process** → Files are parsed, validated, consolidated
3. **Cross-match** → Payroll totals are automatically reconciled against ledger accounts; mismatches are flagged
4. **Generate** → Financial reports (Balance Sheet, P&L, Cash Flow) are produced from templates
5. **Sign** → Reports are digitally signed and ready to submit

---

## Architecture Conventions

This project **inherits the shared SAD conventions from [`PATTERNS.md`](./cloned/PATTERNS.md) verbatim**, reproduced below for convenience. Student work follows these rules without exception; deviations require instructor approval.

### The Human vs. AI Distinction — Course-Wide Principle

This distinction is central to how you should approach work in this course. Do not blur it.

**Humans must do this** (AI cannot substitute, even though AI will later use the output):
- Organizational context: who the business is, what problem it has, why it matters
- Stakeholder identification and needs elicitation
- Problem statement and project scope
- Initial domain modeling: what entities exist, what relationships make sense
- Deciding which use cases exist and which don't (e.g., deciding Login is an NFR, not a UC)
- Prioritization and tradeoffs

**AI can accelerate this** (once the human thinking is done):
- Structuring requirements into user story format
- Writing VP18-style UC specs from a brief description
- Generating the UC diagram HTML from the spec data
- Generating entity classes, stored procedures, and panels from UC specs + implementation notes
- Checking consistency between artifacts (traceability)

Project artifacts are produced in this order: organization/problem context → requirements → UC specs → code. Each is input to the next.

---

## Architecture — Key Patterns

### Entity Pattern
Every entity class is self-contained. Each one owns:
- Private fields + getters/setters
- Constructor with `bool is_new` — if `true`, calls `getNextXYZId()` to assign a new PK, then calls `createXYZ()`, then adds to `Program.list`; if `false`, just sets fields (used during loading)
- `createXYZ()`, `updateXYZ()`, `deleteXYZ()` — each builds a `SqlCommand` with a stored procedure
- `static initXYZs()` — loads all records from DB into `Program.XYZs`, always calls constructor with `is_new = false`
- `static seekXYZ(id)` — searches `Program.XYZs` by ID
- `static getNextXYZId()` — returns `max(id) + 1` over `Program.XYZs` (or `1` if the list is empty). See "Primary Key Strategy" below.

### Primary Key Strategy
**Primary keys are assigned in C#, not by the database.**

- DDL: every PK is `INT NOT NULL PRIMARY KEY`. Do **not** use `IDENTITY(1,1)`.
- The entity class's `static getNextXYZId()` returns `max(id) + 1` over the in-memory list.
- The `is_new` constructor calls `getNextXYZId()` before `createXYZ()` to assign the new row's PK.
- Create stored procedures take the PK as the first parameter (`@<entity>_id`). They do **not** use `SCOPE_IDENTITY()` and do **not** return the new ID.

This is deliberate: students can read the full lifecycle of an ID in one place (the C# constructor), and DB writes are deterministic from the entity's state. Concurrency is not a concern in the single-user teaching context.

### In-Memory Lists
All data lives in `Program.*` static lists after startup. No DB calls during normal use except writes.

`initLists()` load order is strict: **base entities first, then entities with FK references, then association classes last.** Each project's CLAUDE.md states its own concrete order.

### DB Operations
All DB access goes through stored procedures. **No ad-hoc SQL strings in application code.** This is an NFR.

### Panel Navigation
Single-window model. All screens are `UserControl` panels. Navigation: `mainForm.showPanel(new XYZPanel())`. Every panel has a Back button. **No additional Forms or dialogs during normal operation.**

### Inheritance — Table-per-Subclass
When an entity has subtypes, use table-per-subclass: a base table for the parent + one table per subclass holding only the subclass's unique fields + a FK to the base table. Load with a LEFT JOIN and check for `DBNull.Value` to determine subtype. *(Sample project example: `Order` base with `DeliveryOrder` / `PickupOrder` subclasses.)*

### Association Class
When a many-to-many relationship has its own attributes, model it as an association class linking the two sides. In the C# class, both sides are stored as **object references, not IDs**. *(Sample project example: `OrderItem` linking `Order` ↔ `Product` with quantity and unit price.)*

### No Service Layer
Entity classes own their own DB methods. One file per entity. This is intentional for teaching — students see the full lifecycle of an entity in one place.

---

## UC Diagram — Conventions

The diagram is generated from inline JavaScript data and rendered by an external shared script. Rules:

- All data globals must use `var` (not `const`/`let`) so they become `window` properties
- Wireframes are embedded as `useCaseDocs[id].wireframe` HTML strings — **not** as separate files
- All wireframe visible text must be in Hebrew; all form fields use `disabled`; no `<script>` tags inside wireframes
- The `[hidden] { display: none !important; }` style override is required in `<head>` for tab switching to work
- **Login/authentication must never appear as a UC.** Note it only in the `assumptions` array.

### Two-Layer UC Spec Format
Each detailed UC has two sections:
1. **Formal spec** (analysis level) — behavioral, technology-neutral. No class names, SP names, or field names.
2. **Implementation Notes** (design level, clearly labelled) — maps behavioral steps to specific classes, methods, and stored procedures.

This separation is intentional and pedagogically important. Do not merge them.

---

## Language Conventions

| Context | Language |
|---|---|
| C# code (classes, methods, variables) | English |
| UI labels, button text, MessageBox text | Hebrew |
| DB text fields | Hebrew — use `NVARCHAR`, never `VARCHAR` |
| Student guide docs (`docs/*.md`) | Hebrew |
| Requirements and UC spec documents | English |
| UC diagram text (actor labels, UC names, flow steps) | Hebrew |

### RTL Layout for Hebrew UI

Every `Form` and `UserControl` with Hebrew visible text **must** be set up for right-to-left rendering:

- `RightToLeft = Yes` on the form/panel (mirrors text direction, button alignment, scrollbar position).
- `RightToLeftLayout = true` on the root form (mirrors the entire layout, including TabControl direction and DataGridView column order).
- Set these on the parent — children inherit unless overridden.

Generate panels with these properties set from the start. Retrofitting RTL onto LTR-built panels is painful — labels overlap, alignment breaks, the designer file fights you.

---

## Decisions Already Made — Do Not Revisit Without Discussion

These apply across all SAD projects:

- **Login is not a UC.** Authentication is an NFR precondition. A `LoginPanel` is a technical artifact. Do not add Login to UC diagrams or UC specs.
- **Wireframes belong inside the UC diagram modal**, not in separate files.
- **No ad-hoc SQL.** All DB operations use stored procedures.
- **No service layer.** Entity classes own their own DB methods.
- **Single window, panel navigation.** No additional forms or dialogs during normal operation.

---

## APT Project-Specific Domain

### What APT Solves

The firm processes financial data from multiple sources (payroll system, accounting software, depreciation records) monthly. Currently:
- Files are manually parsed into Excel
- Data is error-prone copied between linked Excel sheets
- Mismatches are discovered during slow manual review
- Reports are assembled in Word, exported to PDF, and manually signed

APT automates the ingestion → validation → cross-match → report generation → signature pipeline.

### Scope: Which Use Cases Are Implemented

The full system includes 19 UCs across three workflows:

**Financial Processing & Reporting (implemented MVP):**
- UC-02: Upload Source Files
- UC-03: Process Cumulative Files
- UC-04: Cross-Match Payroll with Ledger
- UC-05: Generate Financial Reports
- UC-06: Manage Report Templates (CRUD)

**Monitoring & Compliance (post-MVP):**
- UC-11: View Status Dashboard
- UC-07 to UC-10: Regulatory alerts, exception tracking, document management

**Knowledge Management (future):**
- UC-13 to UC-19: Knowledge base, decision history, report versioning

**For the SAD course, implement the MVP workflow (UC-02 through UC-06).**

### Domain Entities and Load Order

The in-memory list load order is **strictly**:

```
1. User (base)
2. SeniorAccountant, Intern, Bookkeeper, SystemAdministrator, Client (User subtypes)
3. ClientCase
4. SourceFile
5. LedgerEntry
6. PayrollRecord
7. PayrollLedgerMatch (association class — last)
8. FinancialReport
```

**Rationale:** User has no domain FK dependencies. ClientCase has no FK dependencies within domain entities. SourceFile, LedgerEntry, and PayrollRecord all reference ClientCase (loaded third). PayrollLedgerMatch is an association class linking PayrollRecord ↔ LedgerEntry (loaded last). FinancialReport references both ClientCase and SeniorAccountant (loaded last).

### Entity Descriptions (Class Diagram Summary)

#### User (Base Class) + Subtypes
`User` is an abstract base with subtypes using **table-per-subclass inheritance**:
- **SeniorAccountant** (Eduardo): Approves reports, receives alerts, documents decisions
- **Intern** (Noa): Uploads files, performs cross-matching, reviews exceptions
- **Bookkeeper**: Captures transactions, reconciles accounts
- **SystemAdministrator**: Manages permissions, audit logs, backups
- **Client** (external actor): Receives digitally signed reports

Each subtype has its own table (e.g., `SeniorAccountants`, `Interns`) with a FK to the `Users` base table.

#### ClientCase
Master entity binding all work for a client during a reporting period.
- References: Client ID
- Composed of: SourceFile(s), LedgerEntry records, PayrollRecord(s), FinancialReport(s)
- Attributes: `caseId`, `clientId`, `reportingPeriod`, `status`, `createdDate`, `dueDate`, `assignedTo`
- Key methods: `getProcess()`, `associateSourceFile()`, `generateReports()`

#### SourceFile
Represents raw input files (trial balance, payroll, depreciation forms).
- References: ClientCase (M:1)
- Attributes: `fileId`, `fileName`, `fileType`, `uploadDate`, `status`, `dataFormat`
- Key methods: `validate()`, `parse()`

#### LedgerEntry
Single account line from trial balance.
- References: ClientCase (M:1)
- Attributes: `entryId`, `accountCode`, `accountName`, `accountType`, `amount`, `reportingPeriod`, `isNew`
- Key methods: `validate()`, `getMovement()`
- Relationship: May be matched with PayrollRecord via PayrollLedgerMatch

#### PayrollRecord
Single employee's payroll for a period (from Oketz system).
- References: ClientCase (M:1)
- Attributes: `payrollId`, `employeeId`, `employeeName`, `grossAmount`, `socialContributions`, `employerCosts`, `reportingPeriod`, `isNew`
- Key methods: `parse()`, `getTotalCost()`
- Relationship: May be matched with LedgerEntry via PayrollLedgerMatch

#### PayrollLedgerMatch (Association Class)
**Association class** capturing the result of cross-matching payroll against ledger.
- References: PayrollRecord, LedgerEntry, ClientCase
- Attributes: `matchId`, `matchStatus`, `varianceAmount`, `matchedDate`, `notes`
- Key methods: `calculateVariance()`, `flag()`
- **In C#: stores object references to PayrollRecord and LedgerEntry, not IDs** (per PATTERNS.md)

#### FinancialReport
Generated report (Balance Sheet, P&L, Cash Flow).
- References: ClientCase (M:1), SeniorAccountant (M:1 — signer)
- Attributes: `reportId`, `reportType`, `reportFormat`, `generatedDate`, `isSigned`, `signedDate`, `status`
- Key methods: `generate()`, `sign()`, `export()`

### Key Design Decisions

1. **PayrollLedgerMatch as Association Class**: This relationship isn't just "matched or not" — it carries semantic data (variance amount, match status, notes, date). Modeling as an association class enables audit trails and exception management.

2. **User Inheritance via Table-per-Subclass**: All users share core auth fields (`userId`, `email`, `passwordHash`), but each role has unique attributes. Table-per-subclass allows role-specific queries and loads.

3. **SourceFile as Separate Entity**: Not just a reference to uploaded data; tracks file history, validation status, format for traceability and reprocessing.

4. **ClientCase as Master Entity**: Provides single source of truth for a client engagement. All work items (files, ledger entries, reports) hang off this entity, enabling status tracking and lifecycle management.

5. **Primary Keys in C#**: Assigned client-side (`getNextXYZId()`) to keep the full ID lifecycle visible in one place and ensure deterministic writes.

### Use Case Overview (MVP: UC-02 → UC-06)

| UC# | Name | Actor | Type | Brief Description |
|---|---|---|---|---|
| UC-02 | Upload Source Files | Intern | Data Entry | User uploads Excel/CSV files (trial balance, payroll, depreciation) for a client case |
| UC-03 | Process Cumulative Files | Intern | Processing | System validates, parses, and consolidates uploaded files; detects duplicates |
| UC-04 | Cross-Match Payroll with Ledger | Intern | Validation | System matches payroll totals to ledger account balances; flags mismatches and imbalances |
| UC-05 | Generate Financial Reports | Intern/Senior | Report Generation | System automatically generates Balance Sheet, P&L, Cash Flow from processed data |
| UC-06 | Manage Report Templates | Senior Accountant | CRUD | Senior Accountant defines, updates, disables report templates |

### NFRs (Non-Functional Requirements)

From requirements analysis (`docs/00-requirements.md`):

1. **Performance**: Process up to 50,000 rows and generate all three financial reports within 60 seconds
2. **Availability**: 99% uptime during business hours (08:00–20:00, Sun–Thu)
3. **Backup & Recovery**: Automatic daily full database backup
4. **Security**: Digital signature compliance, access control for sensitive payroll data, audit logging of all operations
5. **Accessibility**: Desktop and Tablet browser support (WinForms primary)
6. **Audit Trail**: All template operations and report generations logged for compliance

### Assumptions

- **Single Client Instance**: MVP assumes one firm (Printek) with one database. Multi-tenancy is future scope.
- **Monthly Reporting Cycles**: System optimized for monthly. Quarterly/annual supported via configuration.
- **Payroll System Source: Oketz**: Payroll data is parsed from Oketz's standard export format. Custom payroll systems require adapter code.
- **File Formats**: Trial balance and depreciation files are expected in standard formats (Excel/CSV with defined column headers). Non-standard files fail validation.
- **User Authentication**: Handled externally (Windows auth, LDAP, or OAuth). System stores password hashes for secondary verification only; login panel is technical artifact, not a UC.
- **No Concurrency**: Single-user teaching context. Pessimistic locking or optimistic versioning is not implemented.
- **Imbalance Threshold**: (TODO in design phase) Define variance threshold triggering "Requires Review" status in UC-04.
- **Report Signature**: Digital signature requirement is met; the actual crypto implementation is a TODO based on regulatory guidance.

### Outstanding Design Questions

These are TODO items for the detailed design phase (not decided yet):

- Specific matching rules for UC-04 cross-match (by GL account code? By cost center? Both?)
- Imbalance threshold amount (what % of total triggers "Requires Review"?)
- File upload size limit
- Report template versioning strategy (keep all versions, or archive old ones?)
- Notification mechanism for Senior Accountant alerts (email, in-app message, both?)
- Integration with digital signature provider (which vendor/library?)

---

## Document Map

| File | Purpose |
|---|---|
| `docs/org-analysis/01-organization.md` | Organization context: Printek & Co., 6 employees, ~70 clients, current systems (Chashbshbta, Oketz, Priority ERP) |
| `docs/org-analysis/02-interviews.md` | Stakeholder interviews: Eduardo (founder), Noa (intern) — pain points, current workflow |
| `docs/org-analysis/03-problems.md` | 10 business problems + observations from 8 hours of field work in the firm |
| `docs/org-analysis/04-business-processes.md` | Three business processes (Audit Reports, Ongoing Bookkeeping, Tax Planning) + BPMN narratives |
| `docs/00-requirements.md` | 7 user stories (MVP scope) + NFRs + traceability matrix |
| `docs/00e-use-cases.md` | UC specs for UC-03, UC-04, UC-05 (two-layer format: behavioral + implementation notes TODO) |
| `docs/design/class-diagram.md` | Full entity descriptions, relationships, rationale, design assumptions |
| `PartA_Group10.pdf`, `PartB_Group10.pdf` | Original PDF submissions (analysis context + use case tables) |

---

---

## Database

### Database Name

The project uses **SQL Server on `localhost\SQLEXPRESS`** with a single database named **`apt`**.

```sql
-- Database created:
CREATE DATABASE apt;
```

### Database Access Rule

**Every SQL operation executed via `mssql.execute_sql` must begin with `USE apt;`**

```sql
-- Correct:
USE apt;
SELECT * FROM Users;

-- Incorrect (will fail):
SELECT * FROM Users;
```

This ensures all queries run against the correct database context and is non-negotiable for CI/CD scripting.

---

## Entry Flow — Multi-Actor Authentication & Role-Based Navigation

### Design Pattern
**Pattern: (a) Login → Per-Role Home Panels**

Multi-actor system with 5 distinct user roles. Each role has separate credentials (email + passwordHash) and routes to a dedicated home panel after successful authentication.

### Human Actors & Credential Sources

| Role | Credential Entity | Home Panel | MVP UCs |
|------|-------------------|-----------|---------|
| Senior Accountant | `SeniorAccountant` (extends User) | `SeniorAccountantHomePanel` | UC-05, UC-06 |
| Audit Intern | `Intern` (extends User) | `InternHomePanel` | UC-02, UC-03, UC-04, UC-05 |
| Bookkeeper | `Bookkeeper` (extends User) | `BookkeeperHomePanel` | (no MVP scope) |
| System Administrator | `SystemAdministrator` (extends User) | `SystemAdministratorHomePanel` | (no MVP scope) |
| Client | `Client` (extends User) | `ClientHomePanel` | (no MVP scope) |

### Entry Flow

```
Application Start (mainForm)
    ↓
LoginPanel (email + password input)
    ↓ (credential verification)
    ├→ Check Program.SeniorAccountants
    ├→ Check Program.Interns
    ├→ Check Program.Bookkeepers
    ├→ Check Program.SystemAdministrators
    └→ Check Program.Clients
    ↓ (on match)
[Role-Specific Home Panel]
    ├→ SeniorAccountantHomePanel (UC-05, UC-06 buttons)
    ├→ InternHomePanel (UC-02, UC-03, UC-04, UC-05 buttons)
    ├→ BookkeeperHomePanel (placeholder)
    ├→ SystemAdministratorHomePanel (placeholder)
    └→ ClientHomePanel (placeholder)
```

### Implementation Details

**LoginPanel.cs**
- Hebrew labels: דוא״ל (email), סיסמה (password)
- RTL layout (RightToLeft = Yes, RightToLeftLayout = true)
- Credential matching: Iterates all 5 Program.XYZ lists; compares email + passwordHash
- Success: Calls `mainForm.showPanel(new <Role>HomePanel(user))`
- Failure: MessageBox in Hebrew ("דוא״ל או סיסמה שגויה")
- Dev shortcut: "כניסת מפתח" button bypasses auth (loads first available user for testing)

**mainForm.cs**
- Single window hosting all panels via `panelMain` (Panel control)
- Static method `showPanel(UserControl panel)` — universal navigation
- Entry point: Loads `LoginPanel()` on startup
- All screens are UserControl subclasses (no additional Forms)

**Home Panels** (one per role)
- Each receives the logged-in user instance in constructor
- Displays welcome message with user email/company name
- MVP roles (Senior Accountant, Intern): Buttons wired to UC placeholders with `MessageBox.Show("UC-XX: Name\n(TODO)")`
- Non-MVP roles (Bookkeeper, SystemAdministrator, Client): Placeholder message "לא הוקצו תפקידים בגרסה זו של המערכת."
- Logout button: Calls `mainForm.showPanel(new LoginPanel())`

### Credential Flow Example

**User logs in as Intern:**
1. User enters: email="noa.levy@printek.co.il", password="password123"
2. LoginPanel iterates Program.Interns
3. Finds match: `Intern.getEmail() == email && Intern.getPasswordHash() == password`
4. Constructs `InternHomePanel(matchedInternInstance)`
5. Calls `mainForm.showPanel(new InternHomePanel(...))`
6. mainForm clears panelMain, adds InternHomePanel
7. Intern sees: Welcome message + 4 UC buttons (UC-02, UC-03, UC-04, UC-05) + Logout

---

## Getting Started

1. **Review** `docs/org-analysis/*.md` to understand the firm's context and pain points
2. **Review** `docs/00-requirements.md` and `docs/00e-use-cases.md` to understand the MVP scope
3. **Create entities** in C# following the Entity Pattern: `User`, `SeniorAccountant`, `Intern`, `Bookkeeper`, `SystemAdministrator`, `Client`, `ClientCase`, `SourceFile`, `LedgerEntry`, `PayrollRecord`, `PayrollLedgerMatch`, `FinancialReport`
4. **Create stored procedures** for each entity's CRUD operations
5. **Implement panels** for UC-02 through UC-06 workflows; use Hebrew UI labels and RTL layout
6. **Fill in Implementation Notes** in `docs/00e-use-cases.md` mapping behavioral specs to specific classes and SPs
7. **Create UC diagram HTML** from UC data (generator TODO)

---

## Inheritance Structure (User)

```
User (base table: Users)
├── SeniorAccountant (table: SeniorAccountants, FK to Users)
├── Intern (table: Interns, FK to Users)
├── Bookkeeper (table: Bookkeepers, FK to Users)
├── SystemAdministrator (table: SystemAdministrators, FK to Users)
└── Client (table: Clients, FK to Users)
```

Each subtype's table holds only its unique fields; common fields (`userId`, `email`, `passwordHash`, `role`) live in the `Users` base table. Load with a LEFT JOIN; check for `DBNull.Value` to determine subtype.

---

## No Changes to PATTERNS.md

Do not deviate from the shared architecture conventions without instructor approval. These rules are pedagogical; the cost of breaking them is borne by your team and by anyone reading your code.
