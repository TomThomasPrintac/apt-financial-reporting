-- APT Stored Procedures
-- Basic CRUD operations for all 12 entities
-- No business logic, no SCOPE_IDENTITY() — primary keys assigned in C#

USE apt;
GO

-- ============================================
-- Users Table Procedures
-- ============================================

CREATE PROCEDURE sp_Users_create
    @user_id INT,
    @email NVARCHAR(50),
    @passwordHash NVARCHAR(MAX),
    @role NVARCHAR(20)
AS
BEGIN
    INSERT INTO Users (user_id, email, passwordHash, role)
    VALUES (@user_id, @email, @passwordHash, @role);
END;
GO

CREATE PROCEDURE sp_Users_update
    @user_id INT,
    @email NVARCHAR(50),
    @passwordHash NVARCHAR(MAX),
    @role NVARCHAR(20)
AS
BEGIN
    UPDATE Users
    SET email = @email,
        passwordHash = @passwordHash,
        role = @role
    WHERE user_id = @user_id;
END;
GO

CREATE PROCEDURE sp_Users_delete
    @user_id INT
AS
BEGIN
    DELETE FROM Users
    WHERE user_id = @user_id;
END;
GO

CREATE PROCEDURE sp_Users_get_all
AS
BEGIN
    SELECT * FROM Users
    ORDER BY user_id;
END;
GO

CREATE PROCEDURE sp_Users_get_by_id
    @user_id INT
AS
BEGIN
    SELECT * FROM Users
    WHERE user_id = @user_id;
END;
GO

-- ============================================
-- SeniorAccountants Table Procedures
-- ============================================

CREATE PROCEDURE sp_SeniorAccountants_create
    @senior_accountant_id INT,
    @user_id INT,
    @certificationNumber NVARCHAR(50),
    @department NVARCHAR(50)
AS
BEGIN
    INSERT INTO SeniorAccountants (senior_accountant_id, user_id, certificationNumber, department)
    VALUES (@senior_accountant_id, @user_id, @certificationNumber, @department);
END;
GO

CREATE PROCEDURE sp_SeniorAccountants_update
    @senior_accountant_id INT,
    @user_id INT,
    @certificationNumber NVARCHAR(50),
    @department NVARCHAR(50)
AS
BEGIN
    UPDATE SeniorAccountants
    SET user_id = @user_id,
        certificationNumber = @certificationNumber,
        department = @department
    WHERE senior_accountant_id = @senior_accountant_id;
END;
GO

CREATE PROCEDURE sp_SeniorAccountants_delete
    @senior_accountant_id INT
AS
BEGIN
    DELETE FROM SeniorAccountants
    WHERE senior_accountant_id = @senior_accountant_id;
END;
GO

CREATE PROCEDURE sp_SeniorAccountants_get_all
AS
BEGIN
    SELECT * FROM SeniorAccountants
    ORDER BY senior_accountant_id;
END;
GO

CREATE PROCEDURE sp_SeniorAccountants_get_by_id
    @senior_accountant_id INT
AS
BEGIN
    SELECT * FROM SeniorAccountants
    WHERE senior_accountant_id = @senior_accountant_id;
END;
GO

-- ============================================
-- Interns Table Procedures
-- ============================================

CREATE PROCEDURE sp_Interns_create
    @intern_id INT,
    @user_id INT,
    @supervisorId INT,
    @startDate DATETIME2
AS
BEGIN
    INSERT INTO Interns (intern_id, user_id, supervisorId, startDate)
    VALUES (@intern_id, @user_id, @supervisorId, @startDate);
END;
GO

CREATE PROCEDURE sp_Interns_update
    @intern_id INT,
    @user_id INT,
    @supervisorId INT,
    @startDate DATETIME2
AS
BEGIN
    UPDATE Interns
    SET user_id = @user_id,
        supervisorId = @supervisorId,
        startDate = @startDate
    WHERE intern_id = @intern_id;
END;
GO

CREATE PROCEDURE sp_Interns_delete
    @intern_id INT
AS
BEGIN
    DELETE FROM Interns
    WHERE intern_id = @intern_id;
END;
GO

CREATE PROCEDURE sp_Interns_get_all
AS
BEGIN
    SELECT * FROM Interns
    ORDER BY intern_id;
END;
GO

CREATE PROCEDURE sp_Interns_get_by_id
    @intern_id INT
AS
BEGIN
    SELECT * FROM Interns
    WHERE intern_id = @intern_id;
END;
GO

-- ============================================
-- Bookkeepers Table Procedures
-- ============================================

CREATE PROCEDURE sp_Bookkeepers_create
    @bookkeeper_id INT,
    @user_id INT,
    @certifications NVARCHAR(MAX)
AS
BEGIN
    INSERT INTO Bookkeepers (bookkeeper_id, user_id, certifications)
    VALUES (@bookkeeper_id, @user_id, @certifications);
END;
GO

CREATE PROCEDURE sp_Bookkeepers_update
    @bookkeeper_id INT,
    @user_id INT,
    @certifications NVARCHAR(MAX)
AS
BEGIN
    UPDATE Bookkeepers
    SET user_id = @user_id,
        certifications = @certifications
    WHERE bookkeeper_id = @bookkeeper_id;
END;
GO

CREATE PROCEDURE sp_Bookkeepers_delete
    @bookkeeper_id INT
AS
BEGIN
    DELETE FROM Bookkeepers
    WHERE bookkeeper_id = @bookkeeper_id;
END;
GO

CREATE PROCEDURE sp_Bookkeepers_get_all
AS
BEGIN
    SELECT * FROM Bookkeepers
    ORDER BY bookkeeper_id;
END;
GO

CREATE PROCEDURE sp_Bookkeepers_get_by_id
    @bookkeeper_id INT
AS
BEGIN
    SELECT * FROM Bookkeepers
    WHERE bookkeeper_id = @bookkeeper_id;
END;
GO

-- ============================================
-- SystemAdministrators Table Procedures
-- ============================================

CREATE PROCEDURE sp_SystemAdministrators_create
    @system_administrator_id INT,
    @user_id INT
AS
BEGIN
    INSERT INTO SystemAdministrators (system_administrator_id, user_id)
    VALUES (@system_administrator_id, @user_id);
END;
GO

CREATE PROCEDURE sp_SystemAdministrators_update
    @system_administrator_id INT,
    @user_id INT
AS
BEGIN
    UPDATE SystemAdministrators
    SET user_id = @user_id
    WHERE system_administrator_id = @system_administrator_id;
END;
GO

CREATE PROCEDURE sp_SystemAdministrators_delete
    @system_administrator_id INT
AS
BEGIN
    DELETE FROM SystemAdministrators
    WHERE system_administrator_id = @system_administrator_id;
END;
GO

CREATE PROCEDURE sp_SystemAdministrators_get_all
AS
BEGIN
    SELECT * FROM SystemAdministrators
    ORDER BY system_administrator_id;
END;
GO

CREATE PROCEDURE sp_SystemAdministrators_get_by_id
    @system_administrator_id INT
AS
BEGIN
    SELECT * FROM SystemAdministrators
    WHERE system_administrator_id = @system_administrator_id;
END;
GO

-- ============================================
-- Clients Table Procedures
-- ============================================

CREATE PROCEDURE sp_Clients_create
    @client_id INT,
    @user_id INT,
    @companyName NVARCHAR(50),
    @taxId NVARCHAR(50),
    @accountManager INT
AS
BEGIN
    INSERT INTO Clients (client_id, user_id, companyName, taxId, accountManager)
    VALUES (@client_id, @user_id, @companyName, @taxId, @accountManager);
END;
GO

CREATE PROCEDURE sp_Clients_update
    @client_id INT,
    @user_id INT,
    @companyName NVARCHAR(50),
    @taxId NVARCHAR(50),
    @accountManager INT
AS
BEGIN
    UPDATE Clients
    SET user_id = @user_id,
        companyName = @companyName,
        taxId = @taxId,
        accountManager = @accountManager
    WHERE client_id = @client_id;
END;
GO

CREATE PROCEDURE sp_Clients_delete
    @client_id INT
AS
BEGIN
    DELETE FROM Clients
    WHERE client_id = @client_id;
END;
GO

CREATE PROCEDURE sp_Clients_get_all
AS
BEGIN
    SELECT * FROM Clients
    ORDER BY client_id;
END;
GO

CREATE PROCEDURE sp_Clients_get_by_id
    @client_id INT
AS
BEGIN
    SELECT * FROM Clients
    WHERE client_id = @client_id;
END;
GO

-- ============================================
-- ClientCases Table Procedures
-- ============================================

CREATE PROCEDURE sp_ClientCases_create
    @case_id INT,
    @clientId INT,
    @reportingPeriod DATETIME2,
    @status NVARCHAR(20),
    @createdDate DATETIME2,
    @dueDate DATETIME2,
    @assignedTo INT
AS
BEGIN
    INSERT INTO ClientCases (case_id, clientId, reportingPeriod, status, createdDate, dueDate, assignedTo)
    VALUES (@case_id, @clientId, @reportingPeriod, @status, @createdDate, @dueDate, @assignedTo);
END;
GO

CREATE PROCEDURE sp_ClientCases_update
    @case_id INT,
    @clientId INT,
    @reportingPeriod DATETIME2,
    @status NVARCHAR(20),
    @createdDate DATETIME2,
    @dueDate DATETIME2,
    @assignedTo INT
AS
BEGIN
    UPDATE ClientCases
    SET clientId = @clientId,
        reportingPeriod = @reportingPeriod,
        status = @status,
        createdDate = @createdDate,
        dueDate = @dueDate,
        assignedTo = @assignedTo
    WHERE case_id = @case_id;
END;
GO

CREATE PROCEDURE sp_ClientCases_delete
    @case_id INT
AS
BEGIN
    DELETE FROM ClientCases
    WHERE case_id = @case_id;
END;
GO

CREATE PROCEDURE sp_ClientCases_get_all
AS
BEGIN
    SELECT * FROM ClientCases
    ORDER BY case_id;
END;
GO

CREATE PROCEDURE sp_ClientCases_get_by_id
    @case_id INT
AS
BEGIN
    SELECT * FROM ClientCases
    WHERE case_id = @case_id;
END;
GO

-- ============================================
-- SourceFiles Table Procedures
-- ============================================

CREATE PROCEDURE sp_SourceFiles_create
    @file_id INT,
    @case_id INT,
    @fileName NVARCHAR(50),
    @fileType NVARCHAR(50),
    @uploadDate DATETIME2,
    @status NVARCHAR(20),
    @dataFormat NVARCHAR(50)
AS
BEGIN
    INSERT INTO SourceFiles (file_id, case_id, fileName, fileType, uploadDate, status, dataFormat)
    VALUES (@file_id, @case_id, @fileName, @fileType, @uploadDate, @status, @dataFormat);
END;
GO

CREATE PROCEDURE sp_SourceFiles_update
    @file_id INT,
    @case_id INT,
    @fileName NVARCHAR(50),
    @fileType NVARCHAR(50),
    @uploadDate DATETIME2,
    @status NVARCHAR(20),
    @dataFormat NVARCHAR(50)
AS
BEGIN
    UPDATE SourceFiles
    SET case_id = @case_id,
        fileName = @fileName,
        fileType = @fileType,
        uploadDate = @uploadDate,
        status = @status,
        dataFormat = @dataFormat
    WHERE file_id = @file_id;
END;
GO

CREATE PROCEDURE sp_SourceFiles_delete
    @file_id INT
AS
BEGIN
    DELETE FROM SourceFiles
    WHERE file_id = @file_id;
END;
GO

CREATE PROCEDURE sp_SourceFiles_get_all
AS
BEGIN
    SELECT * FROM SourceFiles
    ORDER BY file_id;
END;
GO

CREATE PROCEDURE sp_SourceFiles_get_by_id
    @file_id INT
AS
BEGIN
    SELECT * FROM SourceFiles
    WHERE file_id = @file_id;
END;
GO

-- ============================================
-- LedgerEntries Table Procedures
-- ============================================

CREATE PROCEDURE sp_LedgerEntries_create
    @entry_id INT,
    @case_id INT,
    @accountCode NVARCHAR(50),
    @accountName NVARCHAR(50),
    @accountType NVARCHAR(20),
    @amount DECIMAL(10, 2),
    @reportingPeriod DATETIME2,
    @isNew BIT
AS
BEGIN
    INSERT INTO LedgerEntries (entry_id, case_id, accountCode, accountName, accountType, amount, reportingPeriod, isNew)
    VALUES (@entry_id, @case_id, @accountCode, @accountName, @accountType, @amount, @reportingPeriod, @isNew);
END;
GO

CREATE PROCEDURE sp_LedgerEntries_update
    @entry_id INT,
    @case_id INT,
    @accountCode NVARCHAR(50),
    @accountName NVARCHAR(50),
    @accountType NVARCHAR(20),
    @amount DECIMAL(10, 2),
    @reportingPeriod DATETIME2,
    @isNew BIT
AS
BEGIN
    UPDATE LedgerEntries
    SET case_id = @case_id,
        accountCode = @accountCode,
        accountName = @accountName,
        accountType = @accountType,
        amount = @amount,
        reportingPeriod = @reportingPeriod,
        isNew = @isNew
    WHERE entry_id = @entry_id;
END;
GO

CREATE PROCEDURE sp_LedgerEntries_delete
    @entry_id INT
AS
BEGIN
    DELETE FROM LedgerEntries
    WHERE entry_id = @entry_id;
END;
GO

CREATE PROCEDURE sp_LedgerEntries_get_all
AS
BEGIN
    SELECT * FROM LedgerEntries
    ORDER BY entry_id;
END;
GO

CREATE PROCEDURE sp_LedgerEntries_get_by_id
    @entry_id INT
AS
BEGIN
    SELECT * FROM LedgerEntries
    WHERE entry_id = @entry_id;
END;
GO

-- ============================================
-- PayrollRecords Table Procedures
-- ============================================

CREATE PROCEDURE sp_PayrollRecords_create
    @payroll_id INT,
    @case_id INT,
    @employeeId NVARCHAR(50),
    @employeeName NVARCHAR(50),
    @grossAmount DECIMAL(10, 2),
    @socialContributions DECIMAL(10, 2),
    @employerCosts DECIMAL(10, 2),
    @reportingPeriod DATETIME2,
    @isNew BIT
AS
BEGIN
    INSERT INTO PayrollRecords (payroll_id, case_id, employeeId, employeeName, grossAmount, socialContributions, employerCosts, reportingPeriod, isNew)
    VALUES (@payroll_id, @case_id, @employeeId, @employeeName, @grossAmount, @socialContributions, @employerCosts, @reportingPeriod, @isNew);
END;
GO

CREATE PROCEDURE sp_PayrollRecords_update
    @payroll_id INT,
    @case_id INT,
    @employeeId NVARCHAR(50),
    @employeeName NVARCHAR(50),
    @grossAmount DECIMAL(10, 2),
    @socialContributions DECIMAL(10, 2),
    @employerCosts DECIMAL(10, 2),
    @reportingPeriod DATETIME2,
    @isNew BIT
AS
BEGIN
    UPDATE PayrollRecords
    SET case_id = @case_id,
        employeeId = @employeeId,
        employeeName = @employeeName,
        grossAmount = @grossAmount,
        socialContributions = @socialContributions,
        employerCosts = @employerCosts,
        reportingPeriod = @reportingPeriod,
        isNew = @isNew
    WHERE payroll_id = @payroll_id;
END;
GO

CREATE PROCEDURE sp_PayrollRecords_delete
    @payroll_id INT
AS
BEGIN
    DELETE FROM PayrollRecords
    WHERE payroll_id = @payroll_id;
END;
GO

CREATE PROCEDURE sp_PayrollRecords_get_all
AS
BEGIN
    SELECT * FROM PayrollRecords
    ORDER BY payroll_id;
END;
GO

CREATE PROCEDURE sp_PayrollRecords_get_by_id
    @payroll_id INT
AS
BEGIN
    SELECT * FROM PayrollRecords
    WHERE payroll_id = @payroll_id;
END;
GO

-- ============================================
-- PayrollLedgerMatches Table Procedures
-- ============================================

CREATE PROCEDURE sp_PayrollLedgerMatches_create
    @match_id INT,
    @payroll_id INT,
    @entry_id INT,
    @case_id INT,
    @matchStatus NVARCHAR(20),
    @varianceAmount DECIMAL(10, 2),
    @matchedDate DATETIME2,
    @notes NVARCHAR(MAX)
AS
BEGIN
    INSERT INTO PayrollLedgerMatches (match_id, payroll_id, entry_id, case_id, matchStatus, varianceAmount, matchedDate, notes)
    VALUES (@match_id, @payroll_id, @entry_id, @case_id, @matchStatus, @varianceAmount, @matchedDate, @notes);
END;
GO

CREATE PROCEDURE sp_PayrollLedgerMatches_update
    @match_id INT,
    @payroll_id INT,
    @entry_id INT,
    @case_id INT,
    @matchStatus NVARCHAR(20),
    @varianceAmount DECIMAL(10, 2),
    @matchedDate DATETIME2,
    @notes NVARCHAR(MAX)
AS
BEGIN
    UPDATE PayrollLedgerMatches
    SET payroll_id = @payroll_id,
        entry_id = @entry_id,
        case_id = @case_id,
        matchStatus = @matchStatus,
        varianceAmount = @varianceAmount,
        matchedDate = @matchedDate,
        notes = @notes
    WHERE match_id = @match_id;
END;
GO

CREATE PROCEDURE sp_PayrollLedgerMatches_delete
    @match_id INT
AS
BEGIN
    DELETE FROM PayrollLedgerMatches
    WHERE match_id = @match_id;
END;
GO

CREATE PROCEDURE sp_PayrollLedgerMatches_get_all
AS
BEGIN
    SELECT * FROM PayrollLedgerMatches
    ORDER BY match_id;
END;
GO

CREATE PROCEDURE sp_PayrollLedgerMatches_get_by_id
    @match_id INT
AS
BEGIN
    SELECT * FROM PayrollLedgerMatches
    WHERE match_id = @match_id;
END;
GO

-- ============================================
-- FinancialReports Table Procedures
-- ============================================

CREATE PROCEDURE sp_FinancialReports_create
    @report_id INT,
    @case_id INT,
    @reportType NVARCHAR(20),
    @reportFormat NVARCHAR(50),
    @generatedDate DATETIME2,
    @isSigned BIT,
    @signedDate DATETIME2,
    @status NVARCHAR(20),
    @senior_accountant_id INT,
    @periodStart DATETIME2,
    @periodEnd DATETIME2
AS
BEGIN
    INSERT INTO FinancialReports (report_id, case_id, reportType, reportFormat, generatedDate, isSigned, signedDate, status, senior_accountant_id, periodStart, periodEnd)
    VALUES (@report_id, @case_id, @reportType, @reportFormat, @generatedDate, @isSigned, @signedDate, @status, @senior_accountant_id, @periodStart, @periodEnd);
END;
GO

CREATE PROCEDURE sp_FinancialReports_update
    @report_id INT,
    @case_id INT,
    @reportType NVARCHAR(20),
    @reportFormat NVARCHAR(50),
    @generatedDate DATETIME2,
    @isSigned BIT,
    @signedDate DATETIME2,
    @status NVARCHAR(20),
    @senior_accountant_id INT,
    @periodStart DATETIME2,
    @periodEnd DATETIME2
AS
BEGIN
    UPDATE FinancialReports
    SET case_id = @case_id,
        reportType = @reportType,
        reportFormat = @reportFormat,
        generatedDate = @generatedDate,
        isSigned = @isSigned,
        signedDate = @signedDate,
        status = @status,
        senior_accountant_id = @senior_accountant_id,
        periodStart = @periodStart,
        periodEnd = @periodEnd
    WHERE report_id = @report_id;
END;
GO

CREATE PROCEDURE sp_FinancialReports_delete
    @report_id INT
AS
BEGIN
    DELETE FROM FinancialReports
    WHERE report_id = @report_id;
END;
GO

CREATE PROCEDURE sp_FinancialReports_get_all
AS
BEGIN
    SELECT * FROM FinancialReports
    ORDER BY report_id;
END;
GO

CREATE PROCEDURE sp_FinancialReports_get_by_id
    @report_id INT
AS
BEGIN
    SELECT * FROM FinancialReports
    WHERE report_id = @report_id;
END;
GO

-- ============================================
-- UC-06: Report Template Procedures
-- ============================================

CREATE PROCEDURE sp_ReportTemplates_get_all
AS
BEGIN
    SELECT template_id, reportType, templateName, lastUpdated FROM ReportTemplates;
END;
GO

CREATE PROCEDURE sp_ReportTemplates_create
    @template_id INT,
    @reportType NVARCHAR(50),
    @templateName NVARCHAR(100),
    @lastUpdated DATETIME2
AS
BEGIN
    INSERT INTO ReportTemplates (template_id, reportType, templateName, lastUpdated)
    VALUES (@template_id, @reportType, @templateName, @lastUpdated);
END;
GO

CREATE PROCEDURE sp_ReportTemplates_update
    @template_id INT,
    @reportType NVARCHAR(50),
    @templateName NVARCHAR(100),
    @lastUpdated DATETIME2
AS
BEGIN
    UPDATE ReportTemplates
    SET reportType = @reportType, templateName = @templateName, lastUpdated = @lastUpdated
    WHERE template_id = @template_id;
END;
GO

CREATE PROCEDURE sp_ReportTemplates_delete
    @template_id INT
AS
BEGIN
    DELETE FROM ReportTemplates WHERE template_id = @template_id;
END;
GO

CREATE PROCEDURE sp_TemplateFields_get_all
AS
BEGIN
    SELECT field_id, template_id, fieldName FROM TemplateFields;
END;
GO

CREATE PROCEDURE sp_TemplateFields_create
    @field_id INT,
    @template_id INT,
    @fieldName NVARCHAR(100)
AS
BEGIN
    INSERT INTO TemplateFields (field_id, template_id, fieldName)
    VALUES (@field_id, @template_id, @fieldName);
END;
GO

CREATE PROCEDURE sp_TemplateFields_update
    @field_id INT,
    @template_id INT,
    @fieldName NVARCHAR(100)
AS
BEGIN
    UPDATE TemplateFields
    SET template_id = @template_id, fieldName = @fieldName
    WHERE field_id = @field_id;
END;
GO

CREATE PROCEDURE sp_TemplateFields_delete
    @field_id INT
AS
BEGIN
    DELETE FROM TemplateFields WHERE field_id = @field_id;
END;
GO
