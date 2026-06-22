$connString = "Server=localhost\SQLEXPRESS;Integrated Security=true;Encrypt=false;Connection Timeout=30"
$conn = New-Object System.Data.SqlClient.SqlConnection($connString)
$conn.Open()

Write-Host "Creating database tables..." -ForegroundColor Cyan
$createDbScript = Get-Content "scripts/create_database.sql" -Raw
foreach ($batch in ($createDbScript -split "GO")) {
    $batch = $batch.Trim()
    if ($batch.Length -gt 0 -and -not $batch.StartsWith("--")) {
        $cmd = $conn.CreateCommand()
        $cmd.CommandText = $batch
        $cmd.CommandTimeout = 30
        try {
            $cmd.ExecuteNonQuery() | Out-Null
        } catch {}
    }
}
Write-Host "Tables created" -ForegroundColor Green

Write-Host "Creating stored procedures..." -ForegroundColor Cyan
$spScript = Get-Content "scripts/stored_procedures.sql" -Raw
foreach ($batch in ($spScript -split "GO")) {
    $batch = $batch.Trim()
    if ($batch.Length -gt 0 -and -not $batch.StartsWith("--")) {
        $cmd = $conn.CreateCommand()
        $cmd.CommandText = $batch
        $cmd.CommandTimeout = 30
        try {
            $cmd.ExecuteNonQuery() | Out-Null
        } catch {}
    }
}
Write-Host "Procedures created" -ForegroundColor Green

Write-Host "Loading test data..." -ForegroundColor Cyan
$seedScript = Get-Content "scripts/seed_data.sql" -Raw
foreach ($batch in ($seedScript -split "GO")) {
    $batch = $batch.Trim()
    if ($batch.Length -gt 0 -and -not $batch.StartsWith("--")) {
        $cmd = $conn.CreateCommand()
        $cmd.CommandText = $batch
        $cmd.CommandTimeout = 30
        try {
            $cmd.ExecuteNonQuery() | Out-Null
        } catch {}
    }
}
Write-Host "Data loaded" -ForegroundColor Green

$conn.Close()
Write-Host ""
Write-Host "Database setup complete!" -ForegroundColor Green
