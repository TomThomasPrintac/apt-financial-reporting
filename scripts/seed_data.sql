-- APT Seed Data
-- Realistic test data in load order
-- All foreign keys reference existing rows

USE apt;
GO

-- ============================================
-- 1. Users (Base Class) — 10 rows
-- ============================================

INSERT INTO Users (user_id, email, passwordHash, role) VALUES
-- SeniorAccountants (2)
(1, 'eduardo.printek@printek.co.il', 'password123', 'SeniorAccountant'),
(2, 'david.cohen@printek.co.il', 'password123', 'SeniorAccountant'),

-- Interns (2)
(3, 'noa.levy@printek.co.il', 'password123', 'Intern'),
(4, 'moran.israeli@printek.co.il', 'password123', 'Intern'),

-- Bookkeepers (2)
(5, 'rachel.shapiro@printek.co.il', 'password123', 'Bookkeeper'),
(6, 'miriam.goldstein@printek.co.il', 'password123', 'Bookkeeper'),

-- SystemAdministrator (1)
(7, 'shmuel.admin@printek.co.il', 'password123', 'SystemAdministrator'),

-- Clients (3)
(8, 'contact@nitzanytech.co.il', 'password123', 'Client'),
(9, 'billing@meital.com', 'password123', 'Client'),
(10, 'admin@globex.co.il', 'password123', 'Client');

GO

-- ============================================
-- 2. SeniorAccountants (extends User) — 2 rows
-- ============================================

INSERT INTO SeniorAccountants (senior_accountant_id, user_id, certificationNumber, department) VALUES
(1, 1, 'CPA2024001', 'Financial Reporting'),
(2, 2, 'CPA2023045', 'Audit & Compliance');

GO

-- ============================================
-- 3. Interns (extends User) — 2 rows
-- ============================================

INSERT INTO Interns (intern_id, user_id, supervisorId, startDate) VALUES
(1, 3, 1, '2024-09-01'),
(2, 4, 2, '2024-08-15');

GO

-- ============================================
-- 4. Bookkeepers (extends User) — 2 rows
-- ============================================

INSERT INTO Bookkeepers (bookkeeper_id, user_id, certifications) VALUES
(1, 5, 'QuickBooks Certified, Excel Advanced'),
(2, 6, 'SAP Basics, Accounting Standards');

GO

-- ============================================
-- 5. SystemAdministrators (extends User) — 1 row
-- ============================================

INSERT INTO SystemAdministrators (system_administrator_id, user_id) VALUES
(1, 7);

GO

-- ============================================
-- 6. Clients (extends User) — 3 rows
-- ============================================

INSERT INTO Clients (client_id, user_id, companyName, taxId, accountManager) VALUES
(1, 8, 'Nitzany Tech Solutions Ltd.', '512234567', 1),
(2, 9, 'Meital Exports Ltd.', '510987654', 2),
(3, 10, 'Globex Insurance Group', '519876543', 1);

GO

-- ============================================
-- 7. ClientCases (Master Entity) — 6 rows
-- Covers all status values: Open, In Progress, Under Review, Completed
-- ============================================

INSERT INTO ClientCases (case_id, clientId, reportingPeriod, status, createdDate, dueDate, assignedTo) VALUES
(1, 1, '2024-01-01', 'Completed', '2024-01-05', '2024-02-15', 3),
(2, 1, '2024-02-01', 'Under Review', '2024-02-10', '2024-03-15', 4),
(3, 1, '2024-03-01', 'In Progress', '2024-03-05', '2024-04-15', 3),
(4, 2, '2024-01-01', 'Open', '2024-01-10', '2024-02-20', 4),
(5, 2, '2024-02-01', 'Completed', '2024-02-15', '2024-03-20', 3),
(6, 3, '2024-01-01', 'Completed', '2024-01-12', '2024-02-25', 4);

GO

-- ============================================
-- 8. SourceFiles — 14 rows
-- Covers all fileType values: Payroll, TrialBalance, Depreciation
-- Covers all status values: Uploaded, Processing, Processed
-- Covers all dataFormat values: CSV, Excel, XML
-- ============================================

INSERT INTO SourceFiles (file_id, case_id, fileName, fileType, uploadDate, status, dataFormat) VALUES
-- Case 1: Nitzany Tech Jan 2024
(1, 1, 'nitzany_payroll_jan2024.xlsx', 'Payroll', '2024-01-15', 'Processed', 'Excel'),
(2, 1, 'nitzany_trial_balance_jan2024.csv', 'TrialBalance', '2024-01-16', 'Processed', 'CSV'),
(3, 1, 'nitzany_depreciation_jan2024.xlsx', 'Depreciation', '2024-01-17', 'Processed', 'Excel'),

-- Case 2: Nitzany Tech Feb 2024
(4, 2, 'nitzany_payroll_feb2024.xlsx', 'Payroll', '2024-02-14', 'Processing', 'Excel'),
(5, 2, 'nitzany_trial_balance_feb2024.csv', 'TrialBalance', '2024-02-15', 'Processed', 'CSV'),
(6, 2, 'nitzany_depreciation_feb2024.xlsx', 'Depreciation', '2024-02-16', 'Processing', 'Excel'),

-- Case 3: Nitzany Tech Mar 2024
(7, 3, 'nitzany_payroll_mar2024.xlsx', 'Payroll', '2024-03-10', 'Uploaded', 'Excel'),
(8, 3, 'nitzany_trial_balance_mar2024.csv', 'TrialBalance', '2024-03-11', 'Uploaded', 'CSV'),

-- Case 4: Meital Exports Jan 2024
(9, 4, 'meital_payroll_jan2024.csv', 'Payroll', '2024-01-20', 'Processed', 'CSV'),
(10, 4, 'meital_trial_balance_jan2024.xlsx', 'TrialBalance', '2024-01-21', 'Processed', 'Excel'),

-- Case 5: Meital Exports Feb 2024
(11, 5, 'meital_payroll_feb2024.csv', 'Payroll', '2024-02-18', 'Processed', 'CSV'),
(12, 5, 'meital_trial_balance_feb2024.xlsx', 'TrialBalance', '2024-02-19', 'Processed', 'Excel'),

-- Case 6: Globex Insurance Jan 2024
(13, 6, 'globex_payroll_jan2024.xml', 'Payroll', '2024-01-22', 'Processed', 'XML'),
(14, 6, 'globex_trial_balance_jan2024.xml', 'TrialBalance', '2024-01-23', 'Processed', 'XML');

GO

-- ============================================
-- 9. LedgerEntries — 22 rows
-- Covers all accountType values: Asset, Liability, Equity, Revenue, Expense
-- ============================================

INSERT INTO LedgerEntries (entry_id, case_id, accountCode, accountName, accountType, amount, reportingPeriod, isNew) VALUES
-- Case 1: Nitzany Tech Jan 2024 (10 entries)
(1, 1, '1000', 'Cash and Cash Equivalents', 'Asset', 125000.50, '2024-01-01', 0),
(2, 1, '1200', 'Accounts Receivable', 'Asset', 85000.00, '2024-01-01', 0),
(3, 1, '1500', 'Equipment', 'Asset', 450000.00, '2024-01-01', 0),
(4, 1, '2000', 'Accounts Payable', 'Liability', 55000.25, '2024-01-01', 0),
(5, 1, '2500', 'Short-term Loans', 'Liability', 100000.00, '2024-01-01', 0),
(6, 1, '3000', 'Capital Stock', 'Equity', 300000.00, '2024-01-01', 0),
(7, 1, '4000', 'Service Revenue', 'Revenue', 250000.00, '2024-01-01', 0),
(8, 1, '5000', 'Salaries Expense', 'Expense', 91500.00, '2024-01-01', 0),
(9, 1, '5100', 'Rent Expense', 'Expense', 15000.00, '2024-01-01', 0),
(10, 1, '5200', 'Utilities Expense', 'Expense', 4500.75, '2024-01-01', 0),

-- Case 2: Nitzany Tech Feb 2024 (5 entries)
(11, 2, '1000', 'Cash and Cash Equivalents', 'Asset', 135000.00, '2024-02-01', 0),
(12, 2, '1200', 'Accounts Receivable', 'Asset', 92000.00, '2024-02-01', 0),
(13, 2, '2000', 'Accounts Payable', 'Liability', 60000.00, '2024-02-01', 0),
(14, 2, '4000', 'Service Revenue', 'Revenue', 275000.00, '2024-02-01', 0),
(15, 2, '5000', 'Salaries Expense', 'Expense', 56000.00, '2024-02-01', 1),

-- Case 3: Nitzany Tech Mar 2024 (3 entries for cross-match)
(23, 3, '1000', 'Cash and Cash Equivalents', 'Asset', 150000.00, '2024-03-01', 0),
(24, 3, '5000', 'Salaries Expense', 'Expense', 95000.00, '2024-03-01', 0),
(25, 3, '4000', 'Service Revenue', 'Revenue', 300000.00, '2024-03-01', 0),

-- Case 4: Meital Exports Jan 2024 (2 entries)
(16, 4, '1000', 'Cash and Cash Equivalents', 'Asset', 210000.00, '2024-01-01', 0),
(17, 4, '5000', 'Salaries Expense', 'Expense', 40000.00, '2024-01-01', 0),

-- Case 5: Meital Exports Feb 2024 (2 entries)
(18, 5, '1000', 'Cash and Cash Equivalents', 'Asset', 225000.00, '2024-02-01', 0),
(19, 5, '5000', 'Salaries Expense', 'Expense', 40500.00, '2024-02-01', 0),

-- Case 6: Globex Insurance Jan 2024 (3 entries)
(20, 6, '1000', 'Cash and Cash Equivalents', 'Asset', 550000.00, '2024-01-01', 0),
(21, 6, '4000', 'Service Revenue', 'Revenue', 875000.00, '2024-01-01', 0),
(22, 6, '5000', 'Salaries Expense', 'Expense', 320000.00, '2024-01-01', 0);

GO

-- ============================================
-- 10. PayrollRecords — 12 rows
-- ============================================

INSERT INTO PayrollRecords (payroll_id, case_id, employeeId, employeeName, grossAmount, socialContributions, employerCosts, reportingPeriod, isNew) VALUES
-- Case 1: Nitzany Tech Jan 2024 (5 employees)
(1, 1, 'E001', 'Avi Bentzur', 18000.00, 2700.00, 3600.00, '2024-01-01', 0),
(2, 1, 'E002', 'Tali Shimshoni', 15000.00, 2250.00, 3000.00, '2024-01-01', 0),
(3, 1, 'E003', 'Gal Rotman', 22000.00, 3300.00, 4400.00, '2024-01-01', 0),
(4, 1, 'E004', 'Ronit Akerman', 16500.00, 2475.00, 3300.00, '2024-01-01', 0),
(5, 1, 'E005', 'Boaz Friedman', 20000.00, 3000.00, 4000.00, '2024-01-01', 0),

-- Case 2: Nitzany Tech Feb 2024 (3 employees)
(6, 2, 'E001', 'Avi Bentzur', 18500.00, 2775.00, 3700.00, '2024-02-01', 0),
(7, 2, 'E002', 'Tali Shimshoni', 15000.00, 2250.00, 3000.00, '2024-02-01', 0),
(8, 2, 'E003', 'Gal Rotman', 22500.00, 3375.00, 4500.00, '2024-02-01', 0),

-- Case 3: Nitzany Tech Mar 2024 (3 employees for cross-match)
(13, 3, 'E001', 'Avi Bentzur', 20000.00, 3000.00, 4000.00, '2024-03-01', 0),
(14, 3, 'E002', 'Tali Shimshoni', 18000.00, 2700.00, 3600.00, '2024-03-01', 0),
(15, 3, 'E003', 'Gal Rotman', 24000.00, 3600.00, 4800.00, '2024-03-01', 0),

-- Case 4: Meital Exports Jan 2024 (2 employees)
(9, 4, 'E101', 'Naomi Katan', 21000.00, 3150.00, 4200.00, '2024-01-01', 0),
(10, 4, 'E102', 'Dvir Yaacov', 19000.00, 2850.00, 3800.00, '2024-01-01', 0),

-- Case 5: Meital Exports Feb 2024 (2 employees)
(11, 5, 'E101', 'Naomi Katan', 21000.00, 3150.00, 4200.00, '2024-02-01', 0),
(12, 5, 'E102', 'Dvir Yaacov', 19500.00, 2925.00, 3900.00, '2024-02-01', 0),

-- Case 6: Globex Insurance Jan 2024 (3 employees for cross-match)
(16, 6, 'E201', 'Moshe Rabin', 35000.00, 5250.00, 7000.00, '2024-01-01', 0),
(17, 6, 'E202', 'Hannah Weiss', 32000.00, 4800.00, 6400.00, '2024-01-01', 0),
(18, 6, 'E203', 'Dov Frank', 38000.00, 5700.00, 7600.00, '2024-01-01', 0);

GO

-- ============================================
-- 11. PayrollLedgerMatches (Association Class) — 12 rows
-- Covers all matchStatus values: Matched, Unmatched, Variance, Requires Review
-- Every match references payroll and entry from same case
-- ============================================

INSERT INTO PayrollLedgerMatches (match_id, payroll_id, entry_id, case_id, matchStatus, varianceAmount, matchedDate, notes) VALUES
-- Case 1: Nitzany Tech Jan 2024 (5 matches — all to entry 8 / Salaries)
(1, 1, 8, 1, 'Matched', 0.00, '2024-01-25', NULL),
(2, 2, 8, 1, 'Matched', 0.00, '2024-01-25', NULL),
(3, 3, 8, 1, 'Matched', 0.00, '2024-01-25', NULL),
(4, 4, 8, 1, 'Matched', 0.00, '2024-01-25', NULL),
(5, 5, 8, 1, 'Matched', 0.00, '2024-01-25', NULL),

-- Case 2: Nitzany Tech Feb 2024 (3 matches — to entry 15 / Salaries)
(6, 6, 15, 2, 'Variance', 500.00, '2024-02-28', 'New employee bonus included'),
(7, 7, 15, 2, 'Matched', 0.00, '2024-02-28', NULL),
(8, 8, 15, 2, 'Unmatched', -1000.00, '2024-02-28', 'Employee on unpaid leave pending review'),

-- Case 4: Meital Exports Jan 2024 (2 matches — to entry 17 / Salaries)
(9, 9, 17, 4, 'Matched', 0.00, '2024-02-05', NULL),
(10, 10, 17, 4, 'Requires Review', 250.00, '2024-02-05', 'Overtime hours need approval'),

-- Case 5: Meital Exports Feb 2024 (2 matches — to entry 19 / Salaries)
(11, 11, 19, 5, 'Matched', 0.00, '2024-03-05', NULL),
(12, 12, 19, 5, 'Variance', -500.00, '2024-03-05', 'Salary adjustment pending final approval');

GO

-- ============================================
-- 12. FinancialReports — 14 rows
-- Covers all reportType values: Balance Sheet, P&L, Cash Flow
-- Covers all reportFormat values: PDF, Excel
-- Covers all status values: Draft, Generated, Signed, Submitted
-- ============================================

INSERT INTO FinancialReports (report_id, case_id, reportType, reportFormat, generatedDate, isSigned, signedDate, status, senior_accountant_id) VALUES
-- Case 1: Nitzany Tech Jan 2024 (3 reports — all Signed)
(1, 1, 'Balance Sheet', 'PDF', '2024-02-05', 1, '2024-02-10', 'Signed', 1),
(2, 1, 'P&L', 'PDF', '2024-02-05', 1, '2024-02-10', 'Signed', 1),
(3, 1, 'Cash Flow', 'Excel', '2024-02-05', 1, '2024-02-10', 'Signed', 1),

-- Case 2: Nitzany Tech Feb 2024 (3 reports — mixed statuses)
(4, 2, 'Balance Sheet', 'PDF', '2024-03-10', 1, '2024-03-12', 'Signed', 2),
(5, 2, 'P&L', 'PDF', '2024-03-10', 0, NULL, 'Generated', 2),
(6, 2, 'Cash Flow', 'Excel', '2024-03-10', 0, NULL, 'Draft', 2),

-- Case 3: Nitzany Tech Mar 2024 (1 report — Draft only)
(7, 3, 'Balance Sheet', 'PDF', '2024-03-20', 0, NULL, 'Draft', 1),

-- Case 4: Meital Exports Jan 2024 (2 reports — Submitted)
(8, 4, 'Balance Sheet', 'PDF', '2024-02-15', 1, '2024-02-18', 'Submitted', 2),
(9, 4, 'P&L', 'Excel', '2024-02-15', 1, '2024-02-18', 'Submitted', 2),

-- Case 5: Meital Exports Feb 2024 (2 reports — Generated)
(10, 5, 'Balance Sheet', 'PDF', '2024-03-15', 0, NULL, 'Generated', 1),
(11, 5, 'P&L', 'PDF', '2024-03-15', 0, NULL, 'Generated', 1),

-- Case 6: Globex Insurance Jan 2024 (3 reports — complete set, all Signed)
(12, 6, 'Balance Sheet', 'PDF', '2024-02-20', 1, '2024-02-25', 'Signed', 1),
(13, 6, 'P&L', 'Excel', '2024-02-20', 1, '2024-02-25', 'Signed', 1),
(14, 6, 'Cash Flow', 'PDF', '2024-02-20', 1, '2024-02-25', 'Signed', 1);

GO

-- ============================================
-- Test Data Summary
-- ============================================
-- Users: 10
--   • SeniorAccountants: 2 (Eduardo, David)
--   • Interns: 2 (Noa, Moran)
--   • Bookkeepers: 2 (Rachel, Miriam)
--   • SystemAdministrators: 1 (Shmuel)
--   • Clients: 3 (Nitzany Tech, Meital Exports, Globex Insurance)
--
-- ClientCases: 6 (covers all status values: Open, In Progress, Under Review, Completed)
--
-- SourceFiles: 14
--   • All fileType values covered: Payroll, TrialBalance, Depreciation
--   • All status values covered: Uploaded, Processing, Processed
--   • All dataFormat values covered: CSV, Excel, XML
--
-- LedgerEntries: 22 (all accountType values covered: Asset, Liability, Equity, Revenue, Expense)
--
-- PayrollRecords: 12
--
-- PayrollLedgerMatches: 12 (all matchStatus values covered: Matched, Unmatched, Variance, Requires Review)
--
-- FinancialReports: 14
--   • All reportType values covered: Balance Sheet, P&L, Cash Flow
--   • All reportFormat values covered: PDF, Excel
--   • All status values covered: Draft, Generated, Signed, Submitted
--
-- No orphaned foreign keys — every FK references an existing row in load order.
