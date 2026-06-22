USE apt;

-- Add LedgerEntries for Case 3
INSERT INTO LedgerEntries (entry_id, case_id, accountCode, accountName, accountType, amount, reportingPeriod, isNew) VALUES
(23, 3, '1000', 'Cash and Cash Equivalents', 'Asset', 150000.00, '2024-03-01', 0),
(24, 3, '5000', 'Salaries Expense', 'Expense', 95000.00, '2024-03-01', 0),
(25, 3, '4000', 'Service Revenue', 'Revenue', 300000.00, '2024-03-01', 0);

-- Add PayrollRecords for Case 3
INSERT INTO PayrollRecords (payroll_id, case_id, employeeId, employeeName, grossAmount, socialContributions, employerCosts, reportingPeriod, isNew) VALUES
(13, 3, 'E001', 'Avi Bentzur', 20000.00, 3000.00, 4000.00, '2024-03-01', 0),
(14, 3, 'E002', 'Tali Shimshoni', 18000.00, 2700.00, 3600.00, '2024-03-01', 0),
(15, 3, 'E003', 'Gal Rotman', 24000.00, 3600.00, 4800.00, '2024-03-01', 0);

-- Add PayrollRecords for Case 6
INSERT INTO PayrollRecords (payroll_id, case_id, employeeId, employeeName, grossAmount, socialContributions, employerCosts, reportingPeriod, isNew) VALUES
(16, 6, 'E201', 'Moshe Rabin', 35000.00, 5250.00, 7000.00, '2024-01-01', 0),
(17, 6, 'E202', 'Hannah Weiss', 32000.00, 4800.00, 6400.00, '2024-01-01', 0),
(18, 6, 'E203', 'Dov Frank', 38000.00, 5700.00, 7600.00, '2024-01-01', 0);
