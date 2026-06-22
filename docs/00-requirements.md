# Requirements Specification – APT System

## User Stories

### Priority Scale Definition

- **High (גבוהה)**: Requirement that belongs to a core process, critical to develop and provide response in the basic version.
- **Medium (בינונית)**: Important, but can be developed after the base version.
- **Low (נמוכה)**: Nice to have, supplementary requirement.

---

## User Stories Table

| # | Priority | Classification | User Story | Functional Requirements |
|---|----------|-----------------|-----------|------------------------|
| 1 | High | Functional | "As an accountant, I want the system to automatically extract the financial movements of the current month from a cumulative trial balance and cross-match them with payroll system data so that I can generate accurate reports with minimal manual work and minimal time." | **Functional Requirements:**<br/>- The system will enable loading of trial balance and ledger files.<br/>- The system will automatically identify the previous month existing in the database, perform subtraction of its closing balances from the current month's balances, and generate an isolated data layer representing the net movements of the specific month.<br/>- The system will perform automatic parsing of the payroll file according to the fixed column structure of Oketz (Gross, social contributions, employer costs).<br/>- The system will automatically cross-match payroll expense items extracted from the trial balance with the specific data captured from the "Oketz" file.<br/>- The system will display an indication screen (Balanced/Not Balanced) and point out differences at the single-shekel level if there is a mismatch between the records and payroll reporting.<br/>- The system will automatically generate a cash flow report using the indirect method based on changes in balance sheet items and P&L data calculated for the relevant month without the need for manual entry or employee calculation.<br/>- Access to raw payroll file data and cross-match screens will be limited only to employees defined as "authorized to handle" that case.<br/><br/>**Non-Functional Requirements:**<br/>- Access control to sensitive payroll data. |
| 2 | High | Functional - CRUD | "As a department manager, I want the system to generate reports according to a fixed template in order to maintain consistency between clients and provide a professional result." | **Functional Requirements:**<br/>- The system will extract data from the database to create the report and insert it into the report template.<br/>- The system will generate a report as a new copy of the template.<br/>- The system will perform validation of mandatory fields when filling out the report.<br/>- The system will allow filing the created document in the relevant folder.<br/>- The system will support full digital signature on documents.<br/>- The system will be accessible on Desktop and Tablet browsers.<br/>- Access security level for documents will meet the requirements of the digital signature law.<br/><br/>**CRUD Operations for Report Templates:**<br/>1. The system will allow creation of a template that includes text (field definition), attachments from which the report was generated.<br/>2. The system will allow viewing a list of all report templates including versions and last update date.<br/>3. The system will allow updating report template text, changing attachments, and adding new versions.<br/>4. The system will allow disabling a template so it is not available for new processing cycles, while retaining version history.<br/><br/>**Non-Functional Requirements:**<br/>- Template versioning and audit trail. |
| 3 | Medium | Functional | "As a department manager, I want to receive alerts about relevant tax law changes for client cases in order to ensure compliance with legal requirements." | **Functional Requirements:**<br/>- The system will include an AI regulatory engine to analyze tax legislation changes and cross-match with client cases that the engine determined are affected by the change.<br/>- The system will automatically send an alert to the department manager with a list of affected clients for each legislation change.<br/>- The system will allow uploading of legislation and regulation documents to the organizational knowledge repository.<br/><br/>**CRUD Operations for Supporting Documents:**<br/>1. The system will allow creation of a new document type (ID/Business License/Company Certificate).<br/>2. The system will allow viewing a list of document types including definitions of whether they are mandatory and in which processes.<br/>3. The system will allow updating attributes such as mandatory/optional, name, description, or process association.<br/>4. The system will allow disabling a document type so it is not available for use in new processes.<br/><br/>**Non-Functional Requirements:**<br/>- AI-powered regulatory change detection. |
| 4 | Low | Functional | "As a department manager, I want a central dashboard that displays in real-time the status of report preparation for each client in order to track progress and identify bottlenecks and delays quickly." | **Functional Requirements:**<br/>- The system will display a central dashboard that shows in real-time the status of all active client cases in a given reporting month.<br/>- The system will generate a weekly KPI report showing average processing times, number of open cases, and cases exceeding the defined timeline.<br/>- The system will display for each client case a visual indication of the process stage - invoice entry, payroll file entry, balancing stage, or ready for board approval.<br/><br/>**Non-Functional Requirements:**<br/>- Real-time dashboard with performance metrics. |
| 5 | High | Functional | "As an accountant, I want to receive automatic alerts when new ledger accounts are opened or when unusual payroll components are added so that I don't have to search for changes manually and assign them to the appropriate balance sheet items." | **Functional Requirements:**<br/>- The system will automatically detect new ledger accounts opened in the trial balance since the last reporting period and send an alert to the user handling the case.<br/>- The system will detect new payroll components added in the Oketz file and suggest to the user an assignment to a balance sheet section or P&L item based on the history of client handling.<br/>- The system will generate an Exceptions Report for each reporting month, including details of each exception identified and required action from the user.<br/><br/>**Non-Functional Requirements:**<br/>- Anomaly detection and exception reporting. |
| 6 | Low | Functional | "As a department manager, I want a central knowledge repository that documents procedures, work templates, and the history of professional decisions in order to reduce dependence on employees and shorten the training time of new interns." | **Functional Requirements:**<br/>- The system will include a Knowledge Base module to document professional procedures, work guidelines, internal instructions, and solutions for common cases.<br/>- The system will allow free search in the knowledge base by keywords, procedure type, professional field, and client name.<br/>- The system will document the history of professional decisions made for each client (e.g., classification of unique expense items) and display them when handling that client next.<br/><br/>**Non-Functional Requirements:**<br/>- Knowledge management and decision history tracking. |
| 7 | High | Non-Functional | "As a department manager and accountant, I want the system to work fast, stable, and available so that it does not harm the continuous work of the department even under heavy workloads and submission deadlines." | **Non-Functional Requirements:**<br/>- The system will process input files (trial balance, ledger, payroll file from Oketz system) with a volume of up to 50,000 rows and generate financial reports (BS, P&L, CF) within 60 seconds maximum from the moment the upload is completed.<br/>- The system will be available at least 99% of the time during the department's working hours (08:00-20:00, Sunday-Thursday), and will include automatic full database backup once per 24 hours. |

---

## Non-Functional Requirements Summary

1. **Performance**: System must process up to 50,000 rows and generate reports within 60 seconds.
2. **Availability**: 99% uptime during business hours (08:00-20:00, Sun-Thu).
3. **Backup & Recovery**: Automatic daily full database backups.
4. **Security**: Digital signature compliance, access control for sensitive payroll data.
5. **Accessibility**: Desktop and Tablet browser support.
6. **Audit Trail**: All template operations logged.
7. **Scalability**: Support for concurrent users and multiple client cases.

---

## Traceability Matrix

### Matrix 1: Financial Processing and Reporting Use Cases

| Requirement ID | Requirement Description | UC-01 | UC-02 | UC-03 | UC-04 | UC-05 | UC-06 | UC-07 | UC-08 | UC-09 | UC-10 |
|---|---|---|---|---|---|---|---|---|---|---|---|
| US-1.1 | Load trial balance and ledger files | X | X | X | | | | | | | |
| US-1.2 | Automatically identify and extract monthly movements | | | X | | | | | | | |
| US-1.3 | Parse payroll file from Oketz | | | | X | X | | | | | |
| US-1.4 | Cross-match payroll with ledger | | | | X | | | | | | |
| US-1.5 | Display balance/imbalance indicators | | | | X | | | | | | |
| US-1.6 | Generate cash flow report automatically | | | | | X | | | | | |
| US-2.1 | Extract data from database for report | | | | | X | | | | | |
| US-2.2 | Generate report from template | | | | | X | | | | | |
| US-2.3 | Validate mandatory fields | | | | | X | | | | | |
| US-2.4 | Support digital signature | | | | | X | | | | | |
| US-2.5 | Create new report template | | | | | | X | | | | |
| US-2.6 | View and list templates | | | | | | X | | | | |
| US-2.7 | Update template | | | | | | X | | | | |
| US-2.8 | Disable template | | | | | | X | | | | |
| US-3.1 | Detect and alert on tax law changes | | | | | | | X | | | |
| US-3.2 | Upload legislation documents | | | | | | | X | | | |
| US-3.3 | Manage document types (CRUD) | | | | | | | X | | | |
| US-4.1 | Display central dashboard | | | | | | | | X | | |
| US-4.2 | Generate weekly KPI report | | | | | | | | X | | |
| US-4.3 | Visual process stage indicators | | | | | | | | X | | |
| US-5.1 | Detect new ledger accounts | | | | | | | | | X | |
| US-5.2 | Detect new payroll components | | | | | | | | | X | |
| US-5.3 | Generate Exceptions Report | | | | | | | | | X | |
| US-6.1 | Knowledge Base module | | | | | | | | | | X |
| US-6.2 | Free search in knowledge base | | | | | | | | | | X |
| US-6.3 | Document professional decision history | | | | | | | | | | X |

### Matrix 2: Compliance, Monitoring and Knowledge Management Use Cases

| Requirement ID | Requirement Description | UC-11 | UC-12 | UC-13 | UC-14 | UC-15 | UC-16 | UC-17 | UC-18 | UC-19 |
|---|---|---|---|---|---|---|---|---|---|---|
| US-4.1 | Display central dashboard | X | | | | | | | | |
| US-4.2 | Generate weekly KPI report | X | | | | | | | | |
| US-4.3 | Visual process stage indicators | X | | | | | | | | |
| US-5.1 | Detect new ledger accounts | | X | | | | | | | |
| US-5.2 | Detect new payroll components | | X | | | | | | | |
| US-5.3 | Generate Exceptions Report | | X | | | | | | | |
| US-6.1 | Knowledge Base module | | | X | | | | | | |
| US-6.2 | Free search in knowledge base | | | X | | | | | | |
| US-6.3 | Document professional decision history | | | X | | | | | | |

---

## Implementation Notes

The traceability matrix serves as a central project management tool ensuring:
- **Complete Requirements Coverage**: Every functional requirement is linked to at least one Use Case, ensuring no functionality is overlooked.
- **Test Planning**: Each Use Case must be fully tested, and every requirement must be verified through at least one test case.
- **Change Impact Analysis**: When a requirement changes or is deleted, the matrix allows quick identification of affected Use Cases, screens, and tests.
- **Stakeholder Communication**: The matrix documents the link between user wants and technical implementation, providing transparency across the project lifecycle.
