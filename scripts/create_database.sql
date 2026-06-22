-- APT Database Schema
-- All primary keys are assigned in C# (no IDENTITY)
-- Table creation order respects foreign key dependencies

USE apt;

-- ============================================
-- 1. Users (Base Class for Inheritance)
-- ============================================
CREATE TABLE Users (
    user_id INT NOT NULL PRIMARY KEY,
    email NVARCHAR(50) NOT NULL UNIQUE,
    passwordHash NVARCHAR(MAX) NOT NULL,
    role NVARCHAR(20) NOT NULL,
    CONSTRAINT CK_Users_Role CHECK (role IN (
        N'SeniorAccountant',
        N'Intern',
        N'Bookkeeper',
        N'SystemAdministrator',
        N'Client'
    ))
);

-- ============================================
-- 2. SeniorAccountant (extends User)
-- Table-per-Subclass inheritance
-- ============================================
CREATE TABLE SeniorAccountants (
    senior_accountant_id INT NOT NULL PRIMARY KEY,
    user_id INT NOT NULL,
    certificationNumber NVARCHAR(50) NOT NULL,
    department NVARCHAR(50) NOT NULL,
    CONSTRAINT FK_SeniorAccountants_User
        FOREIGN KEY (user_id) REFERENCES Users(user_id)
        ON DELETE NO ACTION ON UPDATE NO ACTION
);

-- ============================================
-- 3. Intern (extends User)
-- Table-per-Subclass inheritance
-- ============================================
CREATE TABLE Interns (
    intern_id INT NOT NULL PRIMARY KEY,
    user_id INT NOT NULL,
    supervisorId INT, -- nullable: intern may not have supervisor assigned initially
    startDate DATETIME2 NOT NULL,
    CONSTRAINT FK_Interns_User
        FOREIGN KEY (user_id) REFERENCES Users(user_id)
        ON DELETE NO ACTION ON UPDATE NO ACTION,
    CONSTRAINT FK_Interns_Supervisor
        FOREIGN KEY (supervisorId) REFERENCES SeniorAccountants(senior_accountant_id)
        ON DELETE NO ACTION ON UPDATE NO ACTION
);

-- ============================================
-- 4. Bookkeeper (extends User)
-- Table-per-Subclass inheritance
-- ============================================
CREATE TABLE Bookkeepers (
    bookkeeper_id INT NOT NULL PRIMARY KEY,
    user_id INT NOT NULL,
    certifications NVARCHAR(MAX), -- TODO: clarify format (comma-separated, JSON, or separate table)
    CONSTRAINT FK_Bookkeepers_User
        FOREIGN KEY (user_id) REFERENCES Users(user_id)
        ON DELETE NO ACTION ON UPDATE NO ACTION
);

-- ============================================
-- 5. SystemAdministrator (extends User)
-- Table-per-Subclass inheritance
-- ============================================
CREATE TABLE SystemAdministrators (
    system_administrator_id INT NOT NULL PRIMARY KEY,
    user_id INT NOT NULL,
    CONSTRAINT FK_SystemAdministrators_User
        FOREIGN KEY (user_id) REFERENCES Users(user_id)
        ON DELETE NO ACTION ON UPDATE NO ACTION
);

-- ============================================
-- 6. Client (extends User)
-- Table-per-Subclass inheritance
-- ============================================
CREATE TABLE Clients (
    client_id INT NOT NULL PRIMARY KEY,
    user_id INT NOT NULL,
    companyName NVARCHAR(50) NOT NULL,
    taxId NVARCHAR(50) NOT NULL UNIQUE,
    accountManager INT, -- nullable: client may not have assigned account manager initially
    CONSTRAINT FK_Clients_User
        FOREIGN KEY (user_id) REFERENCES Users(user_id)
        ON DELETE NO ACTION ON UPDATE NO ACTION,
    CONSTRAINT FK_Clients_AccountManager
        FOREIGN KEY (accountManager) REFERENCES SeniorAccountants(senior_accountant_id)
        ON DELETE NO ACTION ON UPDATE NO ACTION
);

-- ============================================
-- 7. ClientCase
-- Master entity aggregating all work for a client engagement
-- ============================================
CREATE TABLE ClientCases (
    case_id INT NOT NULL PRIMARY KEY,
    clientId INT NOT NULL,
    reportingPeriod DATETIME2 NOT NULL,
    status NVARCHAR(20) NOT NULL,
    createdDate DATETIME2 NOT NULL,
    dueDate DATETIME2 NOT NULL,
    assignedTo INT, -- nullable: case may not be assigned yet
    CONSTRAINT FK_ClientCases_Client
        FOREIGN KEY (clientId) REFERENCES Clients(client_id)
        ON DELETE NO ACTION ON UPDATE NO ACTION,
    CONSTRAINT FK_ClientCases_AssignedTo
        FOREIGN KEY (assignedTo) REFERENCES Users(user_id)
        ON DELETE NO ACTION ON UPDATE NO ACTION,
    CONSTRAINT CK_ClientCases_Status CHECK (status IN (
        N'Open',
        N'In Progress',
        N'Under Review',
        N'Completed'
    )),
    CONSTRAINT UQ_ClientCases_ClientPeriod UNIQUE (clientId, reportingPeriod)
);

-- ============================================
-- 8. SourceFile
-- Raw input files (trial balance, payroll, depreciation)
-- ============================================
CREATE TABLE SourceFiles (
    file_id INT NOT NULL PRIMARY KEY,
    case_id INT NOT NULL,
    fileName NVARCHAR(50) NOT NULL,
    fileType NVARCHAR(50) NOT NULL,
    uploadDate DATETIME2 NOT NULL,
    status NVARCHAR(20) NOT NULL,
    dataFormat NVARCHAR(50) NOT NULL,
    CONSTRAINT FK_SourceFiles_ClientCase
        FOREIGN KEY (case_id) REFERENCES ClientCases(case_id)
        ON DELETE NO ACTION ON UPDATE NO ACTION,
    CONSTRAINT CK_SourceFiles_FileType CHECK (fileType IN (
        N'Payroll',
        N'TrialBalance',
        N'Depreciation'
    )),
    CONSTRAINT CK_SourceFiles_Status CHECK (status IN (
        N'Uploaded',
        N'Processing',
        N'Processed'
    )),
    CONSTRAINT CK_SourceFiles_DataFormat CHECK (dataFormat IN (
        N'CSV',
        N'Excel',
        N'XML'
    ))
);

-- ============================================
-- 9. LedgerEntry
-- Single account line from trial balance
-- ============================================
CREATE TABLE LedgerEntries (
    entry_id INT NOT NULL PRIMARY KEY,
    case_id INT NOT NULL,
    accountCode NVARCHAR(50) NOT NULL,
    accountName NVARCHAR(50) NOT NULL,
    accountType NVARCHAR(20) NOT NULL,
    amount DECIMAL(10, 2) NOT NULL,
    reportingPeriod DATETIME2 NOT NULL,
    isNew BIT NOT NULL,
    CONSTRAINT FK_LedgerEntries_ClientCase
        FOREIGN KEY (case_id) REFERENCES ClientCases(case_id)
        ON DELETE NO ACTION ON UPDATE NO ACTION,
    CONSTRAINT CK_LedgerEntries_AccountType CHECK (accountType IN (
        N'Asset',
        N'Liability',
        N'Equity',
        N'Revenue',
        N'Expense'
    ))
);

-- ============================================
-- 10. PayrollRecord
-- Single employee's payroll for a period (from Oketz)
-- ============================================
CREATE TABLE PayrollRecords (
    payroll_id INT NOT NULL PRIMARY KEY,
    case_id INT NOT NULL,
    employeeId NVARCHAR(50) NOT NULL,
    employeeName NVARCHAR(50) NOT NULL,
    grossAmount DECIMAL(10, 2) NOT NULL,
    socialContributions DECIMAL(10, 2) NOT NULL,
    employerCosts DECIMAL(10, 2) NOT NULL,
    reportingPeriod DATETIME2 NOT NULL,
    isNew BIT NOT NULL,
    CONSTRAINT FK_PayrollRecords_ClientCase
        FOREIGN KEY (case_id) REFERENCES ClientCases(case_id)
        ON DELETE NO ACTION ON UPDATE NO ACTION
);

-- ============================================
-- 11. PayrollLedgerMatch (Association Class)
-- Documents cross-matching result between payroll and ledger
-- ============================================
CREATE TABLE PayrollLedgerMatches (
    match_id INT NOT NULL PRIMARY KEY,
    payroll_id INT NOT NULL,
    entry_id INT NOT NULL,
    case_id INT NOT NULL,
    matchStatus NVARCHAR(20) NOT NULL,
    varianceAmount DECIMAL(10, 2) NOT NULL,
    matchedDate DATETIME2 NOT NULL,
    notes NVARCHAR(MAX), -- nullable: not all matches have notes
    CONSTRAINT FK_PayrollLedgerMatches_PayrollRecord
        FOREIGN KEY (payroll_id) REFERENCES PayrollRecords(payroll_id)
        ON DELETE NO ACTION ON UPDATE NO ACTION,
    CONSTRAINT FK_PayrollLedgerMatches_LedgerEntry
        FOREIGN KEY (entry_id) REFERENCES LedgerEntries(entry_id)
        ON DELETE NO ACTION ON UPDATE NO ACTION,
    CONSTRAINT FK_PayrollLedgerMatches_ClientCase
        FOREIGN KEY (case_id) REFERENCES ClientCases(case_id)
        ON DELETE NO ACTION ON UPDATE NO ACTION,
    CONSTRAINT CK_PayrollLedgerMatches_Status CHECK (matchStatus IN (
        N'Matched',
        N'Unmatched',
        N'Variance',
        N'Requires Review'
    )),
    CONSTRAINT UQ_PayrollLedgerMatches_Pair UNIQUE (payroll_id, entry_id)
);

-- ============================================
-- 12. FinancialReport
-- Generated report (Balance Sheet, P&L, Cash Flow)
-- ============================================
CREATE TABLE FinancialReports (
    report_id INT NOT NULL PRIMARY KEY,
    case_id INT NOT NULL,
    reportType NVARCHAR(20) NOT NULL,
    reportFormat NVARCHAR(50) NOT NULL,
    generatedDate DATETIME2 NOT NULL,
    isSigned BIT NOT NULL,
    signedDate DATETIME2, -- nullable: report may not be signed yet
    status NVARCHAR(20) NOT NULL,
    senior_accountant_id INT, -- nullable: unsigned reports have no signer
    CONSTRAINT FK_FinancialReports_ClientCase
        FOREIGN KEY (case_id) REFERENCES ClientCases(case_id)
        ON DELETE NO ACTION ON UPDATE NO ACTION,
    CONSTRAINT FK_FinancialReports_SeniorAccountant
        FOREIGN KEY (senior_accountant_id) REFERENCES SeniorAccountants(senior_accountant_id)
        ON DELETE NO ACTION ON UPDATE NO ACTION,
    CONSTRAINT CK_FinancialReports_ReportType CHECK (reportType IN (
        N'Balance Sheet',
        N'P&L',
        N'Cash Flow'
    )),
    CONSTRAINT CK_FinancialReports_ReportFormat CHECK (reportFormat IN (
        N'PDF',
        N'Excel'
    )),
    CONSTRAINT CK_FinancialReports_Status CHECK (status IN (
        N'Draft',
        N'Generated',
        N'Signed',
        N'Submitted'
    ))
);

-- ============================================
-- TODO Items for Future Enhancements
-- ============================================
-- TODO: Create AuditLog table for soft-delete tracking and change history
-- TODO: Create indexes on frequently searched columns (email, clientId, reportingPeriod, status)
-- TODO: Define backup/recovery strategy and retention policies
-- TODO: Add computed columns for derived values (e.g., totalPayrollCost = grossAmount + employerCosts)
