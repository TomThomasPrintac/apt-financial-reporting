# =====================================================================
#  APT Database Rebuild  --  single authoritative script
# =====================================================================
#  Rebuilds the `apt` database from the canonical SQL sources in scripts\.
#  Idempotent and safe to re-run.
#
#  Steps:
#    0. Ensure the target database exists
#    1. Drop all sp_* stored procedures, then all tables (FK-reverse order)
#    2. scripts\create_database.sql    -> 12 tables
#    3. scripts\stored_procedures.sql  -> 60 stored procedures
#    4. scripts\seed_data.sql          -> test data
#    5. Verify (FAILS LOUDLY and exits 1 if tables != 12 or procedures != 60)
#
#  Loading rule: each .sql file is split on standalone GO lines and every
#  batch is executed AS-IS. Errors are NOT swallowed -- the first failure
#  stops the script and reports the file + batch number.
#
#  Do NOT "improve" this by splitting on ';'/CREATE or by wrapping batches in
#  empty try/catch blocks: the old version did exactly that and silently
#  created 0 stored procedures while printing "success", which is what broke
#  the app (initLists() -> sp_Users_get_all not found -> crash on startup).
# =====================================================================

param(
    [string]$Server   = "localhost\SQLEXPRESS",
    [string]$Database = "apt"
)

$ErrorActionPreference = "Stop"
$base   = if ($PSScriptRoot) { $PSScriptRoot } else { (Get-Location).Path }
$sqlDir = Join-Path $base "scripts"

function New-AptConnection([string]$catalog) {
    $cs = "Server=$Server;Initial Catalog=$catalog;Integrated Security=True;TrustServerCertificate=True;Connection Timeout=30"
    $c = New-Object System.Data.SqlClient.SqlConnection($cs)
    $c.Open()
    return $c
}

function Invoke-Sql($conn, [string]$sql) {
    $cmd = $conn.CreateCommand()
    $cmd.CommandTimeout = 60
    $cmd.CommandText = $sql
    return $cmd.ExecuteNonQuery()
}

# Splits a .sql file on standalone GO lines and runs each batch as-is.
# USE statements are stripped because the database is pinned by the connection
# (this is a delete-only transform, so it can never corrupt a CREATE body).
function Invoke-SqlFile($conn, [string]$path) {
    if (-not (Test-Path $path)) { throw "SQL file not found: $path" }
    $raw = Get-Content $path -Raw -Encoding UTF8
    $raw = $raw -replace '(?im)^\s*USE\s+\w+\s*;?\s*$', ''
    $batches = $raw -split '(?im)^\s*GO\s*$'
    $n = 0
    foreach ($b in $batches) {
        $sql = $b.Trim()
        if ($sql.Length -eq 0) { continue }
        $n++
        try { Invoke-Sql $conn $sql | Out-Null }
        catch { throw ("FAILED in {0} batch #{1}: {2}" -f [IO.Path]::GetFileName($path), $n, $_.Exception.Message) }
    }
    return $n
}

Write-Host "======================================" -ForegroundColor Cyan
Write-Host (" APT Database Rebuild   ({0} / {1})" -f $Server, $Database) -ForegroundColor Cyan
Write-Host "======================================" -ForegroundColor Cyan

# --- Step 0: ensure database exists -----------------------------------
Write-Host "`nStep 0: Ensuring database '$Database' exists..." -ForegroundColor Yellow
$master = New-AptConnection "master"
Invoke-Sql $master "IF DB_ID('$Database') IS NULL CREATE DATABASE [$Database];" | Out-Null
$master.Close()
Write-Host "  OK" -ForegroundColor Green

$conn = New-AptConnection $Database

# --- Step 1: teardown (procedures, then tables in FK-reverse order) ----
Write-Host "`nStep 1: Dropping existing stored procedures and tables..." -ForegroundColor Yellow
$dropProcs = "DECLARE @s NVARCHAR(MAX)=N''; SELECT @s+='DROP PROCEDURE ['+name+'];' FROM sys.procedures WHERE name LIKE 'sp_%'; IF LEN(@s)>0 EXEC sp_executesql @s;"
Invoke-Sql $conn $dropProcs | Out-Null
$tablesReverse = @(
    "FinancialReports", "PayrollLedgerMatches", "PayrollRecords", "LedgerEntries",
    "SourceFiles", "ClientCases", "SystemAdministrators", "Bookkeepers", "Interns",
    "SeniorAccountants", "Clients", "Users"
)
foreach ($t in $tablesReverse) { Invoke-Sql $conn "DROP TABLE IF EXISTS [$t];" | Out-Null }
Write-Host "  Dropped all sp_* procedures and 12 tables" -ForegroundColor Green

# --- Step 2: tables ----------------------------------------------------
Write-Host "`nStep 2: Creating tables (create_database.sql)..." -ForegroundColor Yellow
$n = Invoke-SqlFile $conn (Join-Path $sqlDir "create_database.sql")
Write-Host "  $n batch(es) executed" -ForegroundColor Green

# --- Step 3: stored procedures ----------------------------------------
Write-Host "`nStep 3: Creating stored procedures (stored_procedures.sql)..." -ForegroundColor Yellow
$n = Invoke-SqlFile $conn (Join-Path $sqlDir "stored_procedures.sql")
Write-Host "  $n batch(es) executed" -ForegroundColor Green

# --- Step 4: seed data -------------------------------------------------
Write-Host "`nStep 4: Loading test data (seed_data.sql)..." -ForegroundColor Yellow
$n = Invoke-SqlFile $conn (Join-Path $sqlDir "seed_data.sql")
Write-Host "  $n batch(es) executed" -ForegroundColor Green

# --- Step 5: verify ----------------------------------------------------
Write-Host "`nStep 5: Verifying..." -ForegroundColor Yellow
$cmd = $conn.CreateCommand()
$cmd.CommandText = "SELECT COUNT(*) FROM sys.tables"
$tableCount = [int]$cmd.ExecuteScalar()
$cmd.CommandText = "SELECT COUNT(*) FROM sys.procedures WHERE name LIKE 'sp_%'"
$procCount = [int]$cmd.ExecuteScalar()
Write-Host ("  Tables: {0}/12   Stored procedures: {1}/60" -f $tableCount, $procCount) -ForegroundColor Cyan

foreach ($t in @("Users", "Clients", "ClientCases", "SourceFiles", "LedgerEntries", "PayrollRecords", "PayrollLedgerMatches", "FinancialReports")) {
    $cmd.CommandText = "SELECT COUNT(*) FROM [$t]"
    Write-Host ("    {0,-22} {1} rows" -f $t, $cmd.ExecuteScalar())
}
$conn.Close()

if ($tableCount -ne 12 -or $procCount -ne 60) {
    Write-Host "`nFAILED: expected 12 tables and 60 stored procedures." -ForegroundColor Red
    exit 1
}

Write-Host "`n======================================" -ForegroundColor Green
Write-Host " Rebuild complete - 12 tables, 60 procedures." -ForegroundColor Green
Write-Host "======================================" -ForegroundColor Green
Write-Host "`nNext: run the app (dotnet run) - it will open to the login screen." -ForegroundColor Cyan
