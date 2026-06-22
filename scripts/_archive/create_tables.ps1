$connString = "Server=localhost\SQLEXPRESS;Integrated Security=true;Encrypt=false;Connection Timeout=30"
$conn = New-Object System.Data.SqlClient.SqlConnection($connString)
$conn.Open()

$cmd = $conn.CreateCommand()
$cmd.CommandTimeout = 30

$tables = @(
    "CREATE TABLE Users (user_id INT NOT NULL PRIMARY KEY, email NVARCHAR(50) NOT NULL UNIQUE, passwordHash NVARCHAR(MAX) NOT NULL, role NVARCHAR(20) NOT NULL)",
    "CREATE TABLE SeniorAccountants (senior_accountant_id INT NOT NULL PRIMARY KEY, user_id INT NOT NULL, certificationNumber NVARCHAR(50) NOT NULL, department NVARCHAR(50) NOT NULL, CONSTRAINT FK_SeniorAccountants_User FOREIGN KEY (user_id) REFERENCES Users(user_id) ON DELETE NO ACTION ON UPDATE NO ACTION)",
    "CREATE TABLE Interns (intern_id INT NOT NULL PRIMARY KEY, user_id INT NOT NULL, supervisorId INT, startDate DATETIME2 NOT NULL, CONSTRAINT FK_Interns_User FOREIGN KEY (user_id) REFERENCES Users(user_id) ON DELETE NO ACTION ON UPDATE NO ACTION)",
    "CREATE TABLE Bookkeepers (bookkeeper_id INT NOT NULL PRIMARY KEY, user_id INT NOT NULL, certifications NVARCHAR(MAX), CONSTRAINT FK_Bookkeepers_User FOREIGN KEY (user_id) REFERENCES Users(user_id) ON DELETE NO ACTION ON UPDATE NO ACTION)",
    "CREATE TABLE SystemAdministrators (system_administrator_id INT NOT NULL PRIMARY KEY, user_id INT NOT NULL, CONSTRAINT FK_SystemAdministrators_User FOREIGN KEY (user_id) REFERENCES Users(user_id) ON DELETE NO ACTION ON UPDATE NO ACTION)",
    "CREATE TABLE Clients (client_id INT NOT NULL PRIMARY KEY, user_id INT NOT NULL, companyName NVARCHAR(100) NOT NULL, taxId NVARCHAR(20), accountManager INT, CONSTRAINT FK_Clients_User FOREIGN KEY (user_id) REFERENCES Users(user_id) ON DELETE NO ACTION ON UPDATE NO ACTION)",
    "CREATE TABLE ClientCases (case_id INT NOT NULL PRIMARY KEY, clientId INT NOT NULL, reportingPeriod DATETIME2 NOT NULL, status NVARCHAR(20) NOT NULL, createdDate DATETIME2 NOT NULL, dueDate DATETIME2, assignedTo INT, CONSTRAINT FK_ClientCases_Client FOREIGN KEY (clientId) REFERENCES Clients(client_id) ON DELETE NO ACTION ON UPDATE NO ACTION)",
    "CREATE TABLE SourceFiles (file_id INT NOT NULL PRIMARY KEY, case_id INT NOT NULL, fileName NVARCHAR(255) NOT NULL, fileType NVARCHAR(20) NOT NULL, uploadDate DATETIME2 NOT NULL, status NVARCHAR(20) NOT NULL, dataFormat NVARCHAR(20), CONSTRAINT FK_SourceFiles_Case FOREIGN KEY (case_id) REFERENCES ClientCases(case_id) ON DELETE NO ACTION ON UPDATE NO ACTION)",
    "CREATE TABLE LedgerEntries (entry_id INT NOT NULL PRIMARY KEY, case_id INT NOT NULL, accountCode NVARCHAR(20) NOT NULL, accountName NVARCHAR(100) NOT NULL, accountType NVARCHAR(20) NOT NULL, amount DECIMAL(10,2) NOT NULL, reportingPeriod DATETIME2 NOT NULL, isNew BIT, CONSTRAINT FK_LedgerEntries_Case FOREIGN KEY (case_id) REFERENCES ClientCases(case_id) ON DELETE NO ACTION ON UPDATE NO ACTION)",
    "CREATE TABLE PayrollRecords (payroll_id INT NOT NULL PRIMARY KEY, case_id INT NOT NULL, employeeId NVARCHAR(20) NOT NULL, employeeName NVARCHAR(100) NOT NULL, grossAmount DECIMAL(10,2) NOT NULL, socialContributions DECIMAL(10,2), employerCosts DECIMAL(10,2), reportingPeriod DATETIME2 NOT NULL, isNew BIT, CONSTRAINT FK_PayrollRecords_Case FOREIGN KEY (case_id) REFERENCES ClientCases(case_id) ON DELETE NO ACTION ON UPDATE NO ACTION)",
    "CREATE TABLE PayrollLedgerMatches (match_id INT NOT NULL PRIMARY KEY, payroll_id INT NOT NULL, entry_id INT NOT NULL, case_id INT NOT NULL, matchStatus NVARCHAR(20) NOT NULL, varianceAmount DECIMAL(10,2), matchedDate DATETIME2, notes NVARCHAR(MAX), CONSTRAINT FK_PayrollLedgerMatches_Payroll FOREIGN KEY (payroll_id) REFERENCES PayrollRecords(payroll_id) ON DELETE NO ACTION ON UPDATE NO ACTION, CONSTRAINT FK_PayrollLedgerMatches_Entry FOREIGN KEY (entry_id) REFERENCES LedgerEntries(entry_id) ON DELETE NO ACTION ON UPDATE NO ACTION)",
    "CREATE TABLE FinancialReports (report_id INT NOT NULL PRIMARY KEY, case_id INT NOT NULL, reportType NVARCHAR(20) NOT NULL, reportFormat NVARCHAR(20) NOT NULL, generatedDate DATETIME2 NOT NULL, isSigned BIT, signedDate DATETIME2, status NVARCHAR(20) NOT NULL, senior_accountant_id INT, CONSTRAINT FK_FinancialReports_Case FOREIGN KEY (case_id) REFERENCES ClientCases(case_id) ON DELETE NO ACTION ON UPDATE NO ACTION)"
)

foreach ($table in $tables) {
    try {
        $cmd.CommandText = "USE apt; $table"
        $cmd.ExecuteNonQuery() | Out-Null
        $tableName = ($table -split "CREATE TABLE ")[1].Split(" ")[0]
        Write-Host "Created: $tableName" -ForegroundColor Green
    } catch {
        $tableName = ($table -split "CREATE TABLE ")[1].Split(" ")[0]
        Write-Host "Error on $tableName : $($_.Exception.Message)" -ForegroundColor Yellow
    }
}

$conn.Close()
Write-Host ""
Write-Host "All tables created!" -ForegroundColor Green
