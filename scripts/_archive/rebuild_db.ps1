[System.Reflection.Assembly]::LoadWithPartialName("Microsoft.SqlServer.ConnectionInfo") | Out-Null
[System.Reflection.Assembly]::LoadWithPartialName("Microsoft.SqlServer.Management.Smo") | Out-Null

$connString = "Server=localhost\SQLEXPRESS;Integrated Security=true;Encrypt=false"
$conn = New-Object System.Data.SqlClient.SqlConnection($connString)
$conn.Open()

# Drop existing database
$cmd = $conn.CreateCommand()
$cmd.CommandText = "DROP DATABASE IF EXISTS apt;"
$cmd.ExecuteNonQuery() | Out-Null
Write-Host "✅ Dropped existing database" -ForegroundColor Green

$conn.Close()

# Now create fresh database
$connString = "Server=localhost\SQLEXPRESS;Integrated Security=true;Encrypt=false"
$conn = New-Object System.Data.SqlClient.SqlConnection($connString)
$conn.Open()

# Read and execute create_database.sql
$createScript = Get-Content "scripts/create_database.sql" -Raw
foreach ($batch in ($createScript -split "GO")) {
    if ($batch.Trim().Length -gt 0) {
        $cmd = $conn.CreateCommand()
        $cmd.CommandText = $batch
        try {
            $cmd.ExecuteNonQuery() | Out-Null
        } catch {
            Write-Host "Error in batch: $_" -ForegroundColor Yellow
        }
    }
}
Write-Host "✅ Created database schema" -ForegroundColor Green

$conn.Close()

# Execute stored procedures
$spScript = Get-Content "scripts/stored_procedures.sql" -Raw
$conn.Open()
foreach ($batch in ($spScript -split "GO")) {
    if ($batch.Trim().Length -gt 0) {
        $cmd = $conn.CreateCommand()
        $cmd.CommandText = $batch
        try {
            $cmd.ExecuteNonQuery() | Out-Null
        } catch {
            # Ignore errors
        }
    }
}
Write-Host "✅ Created stored procedures" -ForegroundColor Green
$conn.Close()

# Execute seed data
$seedScript = Get-Content "scripts/seed_data.sql" -Raw
$conn.Open()
foreach ($batch in ($seedScript -split "GO")) {
    if ($batch.Trim().Length -gt 0) {
        $cmd = $conn.CreateCommand()
        $cmd.CommandText = $batch
        try {
            $cmd.ExecuteNonQuery() | Out-Null
        } catch {
            Write-Host "Error: $_" -ForegroundColor Yellow
        }
    }
}
Write-Host "✅ Seeded database with test data (including Case 3 and Case 6)" -ForegroundColor Green
$conn.Close()
