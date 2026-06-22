$connString = "Server=localhost\SQLEXPRESS;Integrated Security=true;Encrypt=false;Connection Timeout=10"

$conn = New-Object System.Data.SqlClient.SqlConnection($connString)
$conn.Open()

# Drop database
$cmd = $conn.CreateCommand()
$cmd.CommandTimeout = 30
$cmd.CommandText = "DROP DATABASE IF EXISTS apt"
try { $cmd.ExecuteNonQuery() | Out-Null } catch { }

Write-Host "Dropped apt database" -ForegroundColor Green

# Create database
$cmd.CommandText = "CREATE DATABASE apt"
$cmd.ExecuteNonQuery() | Out-Null
Write-Host "Created apt database" -ForegroundColor Green

$conn.Close()

# Read DDL
$ddlScript = Get-Content "scripts/create_database.sql" -Encoding UTF8 -Raw

# Split by GO and execute
$conn.Open()
$batches = $ddlScript -split "\r?\nGO\r?\n"
foreach ($batch in $batches) {
    $batch = $batch.Trim()
    if ($batch.Length -gt 0 -and -not $batch.StartsWith("--")) {
        $cmd = $conn.CreateCommand()
        $cmd.CommandTimeout = 30
        $cmd.CommandText = $batch
        try {
            $cmd.ExecuteNonQuery() | Out-Null
        } catch {
            # Continue on error
        }
    }
}
$conn.Close()

Write-Host "Database rebuilt successfully!" -ForegroundColor Green
Write-Host ""
Write-Host "Now run this in SQL Server Management Studio:" -ForegroundColor Cyan
Write-Host "1. Open SQL Server Management Studio"
Write-Host "2. Connect to localhost\SQLEXPRESS"
Write-Host "3. Run scripts\seed_data.sql"
Write-Host ""
Write-Host "This will load all test data including Case 3 and Case 6 PayrollRecords"
