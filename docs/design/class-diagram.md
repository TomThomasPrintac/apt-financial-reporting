# Class Diagram – APT System Design

![class diagram](class-diagram.png)

## Entity Descriptions

### SourceFile
Represents a source system file (e.g., from Oketz, trial balance, depreciation form) that contains raw financial data to be processed.

**Attributes:**
- `fileId` (String): Unique identifier for the source file
- `fileName` (String): Original name of the uploaded file
- `fileType` (String): Type of file (e.g., "Payroll", "TrialBalance", "Depreciation")
- `uploadDate` (Date): Date the file was uploaded
- `status` (Enum): Status of the file (e.g., "Uploaded", "Processing", "Processed")
- `dataFormat` (String): Format of the file (e.g., "CSV", "Excel", "XML")

**Methods:**
- `validate()`: Validates the file format and required fields
- `parse()`: Extracts data from the file according to its format

**Relationships:**
- One SourceFile belongs to exactly one ClientCase (M:1)

**Rationale:** SourceFile represents source data from external systems that forms the basis for all processing. It's modeled as a separate entity to track the lineage and history of data throughout the system.

---

### ClientCase
Represents a complete accounting engagement for a specific client during a reporting period. It acts as the central aggregator for all work items, files, and outputs related to that client's case.

**Attributes:**
- `caseId` (String): Unique identifier for the client case
- `clientId` (String): Reference to the client
- `reportingPeriod` (Date): The month/quarter/year being reported on
- `status` (Enum): Current status of the case (e.g., "Open", "In Progress", "Under Review", "Completed")
- `createdDate` (Date): When the case was created
- `dueDate` (Date): Deadline for completing the case
- `assignedTo` (String): User ID of the person handling this case

**Methods:**
- `getProcess()`: Retrieves the processing workflow for this case
- `associateSourceFile()`: Links a source file to this case
- `generateReports()`: Initiates report generation for this case

**Relationships:**
- One ClientCase has many SourceFiles (1:M)
- One ClientCase has many FinancialReports (1:M)
- One ClientCase has many LedgerEntry records (1:M)
- One ClientCase is associated with one PayrollRecord (1:M through a process)

**Rationale:** ClientCase is the master entity that binds together all activities for a specific client's accounting work. It provides context for all related data and enables tracking of the complete engagement lifecycle.

---

### LedgerEntry
Represents a single entry or account line from the trial balance or ledger data.

**Attributes:**
- `entryId` (String): Unique identifier
- `accountCode` (String): GL account code
- `accountName` (String): Description of the account
- `accountType` (Enum): Type of account (Asset, Liability, Equity, Revenue, Expense)
- `amount` (Decimal): Amount in this account
- `reportingPeriod` (Date): Period this entry is for
- `isNew` (Boolean): Flag indicating if this is a newly opened account

**Methods:**
- `validate()`: Checks account code validity and amount format
- `getMovement()`: Calculates the movement from previous period

**Relationships:**
- Many LedgerEntry records belong to one ClientCase (M:1)
- One LedgerEntry may be matched with a PayrollRecord through PayrollLedgerMatch (1:1 or 0..1)

**Rationale:** LedgerEntry captures the detail of financial records from the trial balance. It's separately modeled to enable detailed reconciliation, exception detection, and audit trail maintenance.

---

### PayrollRecord
Represents a single employee's payroll information for a specific period, extracted from the payroll system (Oketz).

**Attributes:**
- `payrollId` (String): Unique identifier
- `employeeId` (String): Employee identifier
- `employeeName` (String): Employee name
- `grossAmount` (Decimal): Gross salary amount
- `socialContributions` (Decimal): Employee social security contributions
- `employerCosts` (Decimal): Employer-side costs
- `reportingPeriod` (Date): Period this record is for
- `isNew` (Boolean): Flag indicating if this is a new payroll component

**Methods:**
- `parse()`: Extracts structured payroll data from raw file
- `getTotalCost()`: Returns gross + employer costs

**Relationships:**
- Many PayrollRecord records relate to one ClientCase (M:1)
- One PayrollRecord may be matched with a LedgerEntry through PayrollLedgerMatch (0..1:1)

**Rationale:** PayrollRecord captures individual payroll details. It's separately modeled to enable cross-matching with ledger accounts and detection of new payroll components.

---

### PayrollLedgerMatch
**Association Class** that represents the result of matching a payroll record against a ledger entry.

**Attributes:**
- `matchId` (String): Unique identifier
- `matchStatus` (Enum): Status (e.g., "Matched", "Unmatched", "Variance", "Requires Review")
- `varianceAmount` (Decimal): Difference between payroll total and ledger amount (if any)
- `matchedDate` (Date): When the match was attempted
- `notes` (String): Any notes about the match or mismatch

**Methods:**
- `calculateVariance()`: Computes the difference between payroll and ledger
- `flag()`: Marks a mismatch for review

**Relationships:**
- Connects one PayrollRecord to one LedgerEntry
- Belongs to one ClientCase (context)

**Rationale:** PayrollLedgerMatch is an association class (mediator) that documents the cross-matching process. It records not just which records matched, but also the results of the matching—whether they balanced, what variances exist, and what action is needed. This is critical for audit trails and exception management.

---

### FinancialReport
Represents a complete financial report generated from processed financial data.

**Attributes:**
- `reportId` (String): Unique identifier
- `reportType` (Enum): Type of report (e.g., "Balance Sheet", "P&L", "Cash Flow")
- `reportFormat` (String): Output format (e.g., "PDF", "Excel")
- `generatedDate` (Date): When the report was generated
- `isSigned` (Boolean): Whether the report has been digitally signed
- `signedDate` (Date): Date of digital signature (null if not signed)
- `status` (Enum): Status (e.g., "Draft", "Generated", "Signed", "Submitted")

**Methods:**
- `generate()`: Creates the report from processed data
- `sign()`: Digitally signs the report
- `export()`: Exports the report to file format

**Relationships:**
- Many FinancialReports belong to one ClientCase (M:1)
- One FinancialReport is signed by one SeniorAccountant (M:1)

**Rationale:** FinancialReport represents the end-product output of the system. It's separately modeled to track report lifecycle, versions, signatures, and audit trail.

---

### User (Base Class)
Abstract base class representing any system user.

**Attributes:**
- `userId` (String): Unique identifier
- `email` (String): User's email
- `passwordHash` (String): Hashed password for authentication
- `role` (Enum): User role/type

**Methods:**
- `login(email, password)`: Authenticates the user
- `logout()`: Ends the user session
- `hasPermission(action)`: Checks if user can perform an action

**Relationships:**
- Abstract; specialized by SeniorAccountant, Intern, Bookkeeper, SystemAdministrator, Client

---

### SeniorAccountant (extends User)
Specialized user type representing senior accounting staff with management authority.

**Attributes:**
- (inherited from User)
- `certificationNumber` (String): Professional CPA certification number
- `department` (String): Department assignment

**Methods:**
- `approveReport()`: Signs off on final reports
- `receiveAlerts()`: Receives regulatory and status alerts
- `createKnowledgeBase()`: Documents procedures and decisions

**Rationale:** SeniorAccountant extends User to capture role-specific attributes and capabilities, particularly approval authority and knowledge management responsibility.

---

### Intern (extends User)
Specialized user type representing audit interns or junior staff.

**Attributes:**
- (inherited from User)
- `supervisorId` (String): Reference to supervising SeniorAccountant
- `startDate` (Date): Start date of internship

**Methods:**
- `uploadSourceFiles()`: Can upload files to cases
- `performCrossMatch()`: Can run payroll/ledger matching
- `reviewExceptions()`: Can review exception reports

---

### Bookkeeper (extends User)
Specialized user type representing bookkeeping staff.

**Attributes:**
- (inherited from User)
- `certifications` (String[]): Array of accounting certifications

**Methods:**
- `captureTransaction()`: Records transactions
- `performReconciliation()`: Performs bank/supplier reconciliations
- `generatePeriodReports()`: Creates periodic reports

---

### SystemAdministrator (extends User)
Specialized user type with system maintenance authority.

**Attributes:**
- (inherited from User)

**Methods:**
- `manageUserPermissions()`: Controls user access
- `viewAuditLog()`: Accesses system audit logs
- `executeBackup()`: Initiates manual backups
- `monitorSystemHealth()`: Checks system status

---

### Client (extends User)
Specialized user type representing external clients.

**Attributes:**
- (inherited from User)
- `companyName` (String): Legal company name
- `taxId` (String): Tax identification number
- `accountManager` (String): Assigned SeniorAccountant ID

**Methods:**
- `receiveReports()`: Gets digital copies of reports
- `uploadDocuments()`: Can upload required documents
- `trackCaseStatus()`: Can view their case status

---

## Relationships Summary

| From | To | Type | Multiplicity | Description |
|------|----|----|---|---|
| ClientCase | SourceFile | Composition | 1:M | A case contains many source files |
| ClientCase | FinancialReport | Composition | 1:M | A case generates multiple reports |
| ClientCase | LedgerEntry | Composition | 1:M | A case contains ledger data |
| SourceFile | LedgerEntry | Association | 1:M | Files contain ledger entries |
| PayrollRecord | LedgerEntry | Association (through Match) | M:M | Payroll records are cross-matched with ledger entries |
| PayrollLedgerMatch | PayrollRecord | Reference | M:1 | Match connects to payroll |
| PayrollLedgerMatch | LedgerEntry | Reference | M:1 | Match connects to ledger |
| FinancialReport | SeniorAccountant | Reference | M:1 | Report is signed by senior accountant |
| SeniorAccountant | ClientCase | Reference | M:1 | Accountant handles cases |

## Design Assumptions

1. **Single Reporting Currency**: All amounts are in a single currency (ILS). Multi-currency support is not in the scope of this version.

2. **Monthly Reporting Cycles**: The system is optimized for monthly reporting periods, though quarterly and annual periods may be supported through configuration.

3. **File Format Standardization**: SourceFiles are expected to conform to specific, documented formats for each file type. Custom formats require system configuration changes.

4. **User Authentication**: Authentication is handled externally (e.g., LDAP/OAuth). The User class password hash is for secondary verification only.

5. **Audit Trail**: All entity modifications are logged separately in an AuditLog table not shown here, providing a complete history of changes.

6. **Soft Deletes**: Entities are rarely hard-deleted; instead, a `deletedDate` field marks inactive records to preserve referential integrity and audit trails.

7. **Stateless Services**: The system's processing engines (matching, report generation) are stateless and can run in parallel for multiple cases.

8. **Association Class Necessity**: PayrollLedgerMatch is modeled as an association class (not just a foreign key relationship) because it carries semantic information about the match result—variance amounts, match status, and decision history—that belongs to the relationship itself, not to either entity individually.

---

## TODO Items

- [ ] Define database schema with indexes and constraints
- [ ] Specify persistence layer (ORM, SQL, NoSQL)
- [ ] Design API contracts for each use case
- [ ] Implement comprehensive error handling
- [ ] Design UI/UX flows for each user role
- [ ] Specify security controls and encryption requirements
- [ ] Define backup/recovery procedures and RTO/RPO targets
