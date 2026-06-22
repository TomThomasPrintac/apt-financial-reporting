# APT — Advanced Processing of Financial Reports

A WinForms desktop system that automates financial-report generation and
payroll-ledger reconciliation for a small accounting firm. Built as a Systems
Analysis & Design (SAD) course project at Ben-Gurion University (Group 10).

---

## The Problem

A small accounting firm managing ~70 clients spends 60–70% of intern time on
manual Excel work: extracting data from external systems, cross-matching
payroll against ledger accounts by hand, hunting for mismatches, and assembling
reports. The result is 15–20 hours/month per intern of non-analytical work,
with high error rates and audit-trail gaps.

## The Solution

APT automates the data pipeline end to end:

1. **Upload** — source files (trial balance, payroll, depreciation) are ingested
2. **Process** — files are validated, parsed, and consolidated; duplicates detected
3. **Cross-match** — payroll totals are reconciled against ledger balances; mismatches flagged
4. **Generate** — Balance Sheet, P&L, and Cash Flow reports are produced from the processed data
5. **Sign** — reports are digitally signed and ready to submit

---

## Tech Stack

| Layer | Technology |
|---|---|
| Runtime | .NET 8 (`net8.0-windows`) |
| UI | Windows Forms (single-window, panel navigation, RTL Hebrew UI) |
| Database | SQL Server Express (`localhost\SQLEXPRESS`, database `apt`) |
| Data access | `Microsoft.Data.SqlClient` — **all DB access via stored procedures** |

## Architecture Highlights

- **Entity pattern** — each entity class owns its full lifecycle (`create` / `update` / `delete` / `init` / `seek`), one file per entity, no service layer.
- **Primary keys assigned in C#** (`getNextXYZId()` → `max(id)+1`), not by the database — the full ID lifecycle is visible in one place.
- **In-memory lists** — all data is loaded into static `Program.*` lists at startup; no DB reads during normal use, only writes.
- **No ad-hoc SQL** — every database operation goes through a stored procedure.
- **Table-per-subclass inheritance** for the `User` hierarchy (Senior Accountant, Intern, Bookkeeper, System Administrator, Client).
- **Association class** `PayrollLedgerMatch` carries the cross-match result (variance, status, notes) and holds object references, not IDs.

## MVP Scope

| UC | Name | Actor |
|---|---|---|
| UC-02 | Upload Source Files | Intern |
| UC-03 | Process Cumulative Files | Intern |
| UC-04 | Cross-Match Payroll with Ledger | Intern |
| UC-05 | Generate Financial Reports | Intern / Senior Accountant |
| UC-06 | Manage Report Templates | Senior Accountant |

---

## Getting Started

> **Platform:** Windows only (WinForms). Requires the .NET 8 SDK and a running
> SQL Server Express instance at `localhost\SQLEXPRESS`.

### 1. Build the database

The database is rebuilt from the canonical SQL sources in `scripts/` using a
single idempotent PowerShell script (12 tables, 60 stored procedures, seed data):

```powershell
.\rebuild_database.ps1
```

Override the server/database if needed:

```powershell
.\rebuild_database.ps1 -Server "localhost\SQLEXPRESS" -Database "apt"
```

The script fails loudly if it does not end with exactly 12 tables and 60 stored
procedures.

### 2. Run the application

```bash
dotnet run --configuration Release
```

The app opens to the login screen. The database is seeded with a test user for
each role — see `scripts/seed_data.sql` for credentials. The login screen also
offers a developer shortcut for testing.

---

## Project Structure

```
.
├── *.cs                      # Entity classes + WinForms panels (one file per entity/panel)
├── Program.cs                # Entry point; initLists() load order
├── SQL_CON.cs                # Single DB-access gateway (stored procedures only)
├── app.config                # Connection string (Integrated Security)
├── rebuild_database.ps1      # One-shot DB rebuild (tables + procedures + seed)
├── scripts/
│   ├── create_database.sql   # 12 tables
│   ├── stored_procedures.sql # 60 stored procedures
│   └── seed_data.sql         # Test data
└── docs/                     # Requirements, use-case specs, class diagram, org analysis
```

## Documentation

- `docs/00-requirements.md` — user stories + NFRs + traceability
- `docs/00e-use-cases.md` — UC specifications (behavioral + implementation notes)
- `docs/design/` — class diagram and design rationale
- `docs/org-analysis/` — organization context, interviews, business processes
- `CLAUDE.md` — full architecture conventions and domain model

---

## Conventions

- **C# code** (classes, methods, variables) in English.
- **UI text** (labels, buttons, messages) and **DB text fields** in Hebrew (`NVARCHAR`); panels use RTL layout.
- Login/authentication is treated as a non-functional requirement, **not** a use case.

---

*Academic project — Ben-Gurion University, Industrial Engineering & Management,
Systems Analysis & Design.*
