# Full Use Case Specifications – APT System

## System Actors

| # | Actor (English) | בעברית (Hebrew) | Type | Description |
|---|---|---|---|---|
| 1 | Senior Accountant | רואה חשבון בכיר ומנהל המשרד | Primary | Eduardo Printek – Owner of the firm. Approves final reports, receives regulatory alerts, views Dashboard, documents professional decisions in the knowledge base. |
| 2 | Audit Intern | מתמחה בראיית חשבון | Primary | Noa – Performs financial report audits, uploads source files, reviews exceptions list, and works with the Dashboard. |
| 3 | Bookkeeper | מנהלת חשבונות | Primary | Captures ongoing data, performs bank reconciliations and client/vendor reconciliations, generates periodic reports. |
| 4 | System Administrator | מנהל מערכת | Primary | Manages user permissions, views Audit Log, responsible for backups and system maintenance. |
| 5 | Client | לקוח | Secondary | Consumer of system outputs – receives digitally signed final financial reports from the firm. |

---

## Use Case Specifications

### UC-03: Process Cumulative Files

**Use Case Number:** UC-03  
**Use Case Name:** Process Cumulative Files

#### Behavioral Specification (Technology-Neutral)

**Pre-conditions:**
- The user is logged into the system
- Source files were uploaded successfully
- The uploaded files are in a supported format

**Post-conditions:**
- The cumulative data is processed and stored in the system
- Processing results are available for review

**Main Success Scenario (MSS):**
1. The user selects uploaded files for processing
2. The system validates the selected files
3. The system verifies that all required fields exist
4. The user confirms the processing operation
5. The system extracts payroll and financial data from the files
6. The system consolidates the extracted data into a cumulative dataset
7. The system identifies duplicate or missing records
8. The system stores the processed cumulative data
9. The system displays a processing summary to the user

**Extensions:**
- **2a. Unsupported file format:** The system displays an error message and stops the processing
- **3a. Missing required fields:** The system marks the file as invalid and displays the missing fields
- **7a. Duplicate records detected:** The system marks the duplicate records for review and continues processing

**Alternative Scenarios:**
- Successful processing of cumulative files (Main scenario)
- Unsupported file format detected
- Missing required fields detected
- Duplicate records detected during processing

#### Implementation Notes

TODO: Add implementation-specific details about data structures, validation algorithms, and duplicate detection mechanisms to be determined in the design phase.

---

### UC-04: Cross-Match Payroll with Ledger

**Use Case Number:** UC-04  
**Use Case Name:** Cross-Match Payroll with Ledger

#### Behavioral Specification (Technology-Neutral)

**Pre-conditions:**
- UC-02 (Upload Source Files) completed successfully
- UC-03 (Process Cumulative Files) completed successfully
- Payroll and ledger data exist for the selected reporting period
- The Audit Intern is logged into the system

**Post-conditions:**
- Payroll and ledger records are cross-matched and validated
- A cross-match report is generated and stored in the system
- Detected mismatches and anomalies are marked for further review
- If no critical mismatches are detected, the reporting period status is updated to "Cross-Match Completed"
- If mismatches require review, the reporting period status is updated to "Requires Review"

**Main Success Scenario (MSS):**
1. The Audit Intern selects a client and reporting period
2. The Audit Intern selects "Cross-Match Payroll with Ledger"
3. The system retrieves payroll records and ledger entries for the selected reporting period
4. The system validates that all required payroll and ledger data exists
5. The system applies matching rules based on account code, reporting period, and cost center
6. The system compares payroll totals against ledger entries
7. The system identifies matched records and mismatches
8. The system calculates the total imbalance amount between payroll and ledger data
9. The system generates a cross-match summary report
10. The system displays matched records, mismatches, and imbalance indicators
11. The Audit Intern reviews the cross-match results and selects Approve, Save Draft, or Send for Review
12. The system stores the cross-match results and updates the reporting period status according to the selected action and validation outcome

**Extensions:**
- **4a. Missing payroll or ledger data:**
  - The system detects missing payroll or ledger data for the selected reporting period
  - The system displays an error message indicating the missing data
  - The process is terminated until the required data is uploaded
- **5a. Invalid matching configuration:**
  - The system detects missing or invalid matching rules
  - The system displays a configuration error message
  - The process is terminated until the issue is resolved
- **7a. Mismatched records detected:**
  - The system flags mismatched payroll and ledger records
  - The mismatched records are marked for further review
  - The system allows the Audit Intern to continue reviewing the cross-match results
- **8a. Imbalance exceeds allowed threshold:**
  - The system detects that the imbalance amount exceeds the configured threshold
  - The system displays a warning message
  - The reporting period is marked as "Requires Review"
- **11a. Audit Intern selects "Save Draft":**
  - The system saves the cross-match report with "Draft" status
  - The report remains editable for future review
- **11b. Audit Intern selects "Send for Review":**
  - The system sends a notification to the Senior Accountant
  - The cross-match report status is updated to "Under Review"
  - The reporting period is marked as "Requires Review"

**Alternative Scenarios:**
- Payroll and ledger records are matched successfully with no significant imbalance, and the Audit Intern approves the result (Main scenario)
- Missing payroll or ledger data is detected
- Mismatched records are detected during validation
- The imbalance amount exceeds the configured threshold
- The report is saved as draft
- The report is sent for Senior Accountant review

#### Implementation Notes

TODO: Define specific matching rules, threshold values, and imbalance calculation algorithms. Clarify notification mechanisms and approval workflow in detailed design.

---

### UC-05: Automatically Generate Financial Reports

**Use Case Number:** UC-05  
**Use Case Name:** Automatically Generate Financial Reports

#### Behavioral Specification (Technology-Neutral)

**Pre-conditions:**
- The user is logged into the system
- The client exists in the system and is active
- Source files (payroll, trial balance, depreciation) have been uploaded for the relevant reporting period
- A report template exists for the relevant report type

**Post-conditions:**
- Financial reports (Cash Flow, P&L, Balance Sheet) are generated and saved in the client's folder
- The reporting period status is updated to "Reports Generated"

**Main Success Scenario:**
- The user selects a client and a reporting period
- The user selects the report types to generate (Cash Flow / P&L / Balance Sheet)
- The system validates that all required source files are present for the selected period
- The system performs cross-validation between payroll, trial balance, and depreciation data
- The system automatically derives period-over-period changes (month / quarter / half-year / annual)
- The system maps all data fields to the selected report template
- The user confirms the generation operation
- The system generates the output reports in a standardized format
- The system saves the reports in the client's folder and updates the period status
- The system displays a generation summary to the user

**Extensions:**
- **3a. One or more source files are missing:**
  - The system blocks generation and displays the list of missing files for the selected period
- **4a. Cross-validation fails due to data mismatch between sources:**
  - The system flags the mismatch, displays the conflicting fields, and blocks generation until resolved
- **4b. An anomaly or outlier is detected in the data:**
  - The system flags the anomaly and notifies the user before proceeding
- **8a. Report template not found for the selected report type:**
  - The system notifies the user and prompts them to select an existing template or create a new one

**Alternative Scenarios:**
- All three source files are uploaded correctly and all three reports are generated successfully (Main scenario)
- Payroll file is missing for the selected period
- Mismatch detected between payroll totals and trial balance entries
- No report template exists for the selected business type

#### Implementation Notes

TODO: Specify report template structure, data mapping logic, period-over-period calculation methods, and validation rules for cross-data consistency checks.

---

## Use Case Summary Overview

### Upload and Processing Use Cases

| UC# | Name | Actor | Classification |
|---|---|---|---|
| UC-01 | Login to System | User | Authentication |
| UC-02 | Upload Source Files | Bookkeeper / Audit Intern | Data Entry |
| UC-03 | Process Cumulative Files | System | Processing |
| UC-04 | Cross-Match Payroll with Ledger | Audit Intern | Validation |
| UC-05 | Automatically Generate Financial Reports | Senior Accountant / Audit Intern | Report Generation |
| UC-06 | Manage Report Templates | Senior Accountant | CRUD - Template Management |

### Compliance and Monitoring Use Cases

| UC# | Name | Actor | Classification |
|---|---|---|---|
| UC-07 | Upload Legislation Documents | Senior Accountant | Knowledge Management |
| UC-08 | Manage Supporting Documents | Senior Accountant | CRUD - Document Management |
| UC-09 | Receive Regulatory Change Alerts | Senior Accountant | Alert System |
| UC-10 | Monitor Exceptions | Audit Intern | Exception Management |
| UC-11 | View Status Dashboard | Senior Accountant / Audit Intern | Monitoring |
| UC-12 | Generate KPI Report | Senior Accountant | Reporting |

### Knowledge Management Use Cases

| UC# | Name | Actor | Classification |
|---|---|---|---|
| UC-13 | Access Knowledge Base | All Users | Knowledge Management |
| UC-14 | Search Knowledge Base | All Users | Search |
| UC-15 | Document Professional Decision | Senior Accountant | Knowledge Capture |
| UC-16 | Retrieve Decision History | All Users | Knowledge Retrieval |
| UC-17 | Digitally Sign Document | Senior Accountant | Digital Signature |
| UC-18 | Compare Report Versions | Senior Accountant | Report Analysis |
| UC-19 | Extended via Dashboard | Audit Intern | Dashboard Extension |

---

## Implementation Notes for Design Phase

### General Implementation Considerations

1. **Data Persistence**: Specify which data should be persisted in the database, what is transient, and backup/recovery mechanisms.
2. **API Contracts**: Define REST/GraphQL endpoints for each use case's main success scenario and extensions.
3. **Error Handling**: Implement comprehensive error handling for all extension scenarios.
4. **User Experience**: Design clear UI flows for each use case, including error states and confirmation dialogs.
5. **Performance**: Ensure compliance with non-functional requirements (60 seconds for report generation, 99% availability).
6. **Security**: Implement role-based access control and audit logging for all operations.
7. **Integration**: Design integration points with legacy systems (Oketz, Chashbshbta, Priority ERP).

### Behavioral Spec to Implementation Mapping

This specification provides the "what" users need to accomplish. The implementation phase will define:
- **HOW**: Technology stack, database schema, UI frameworks, integration methods
- **WHERE**: System components, microservices, cloud/on-premises deployment
- **WITH WHAT**: Tools, libraries, APIs, third-party services

The behavioral specifications remain technology-neutral to allow flexibility in implementation choices while ensuring user needs are met.
