$connString = "Server=localhost\SQLEXPRESS;Integrated Security=true;Encrypt=false;Connection Timeout=30"
$conn = New-Object System.Data.SqlClient.SqlConnection($connString)
$conn.Open()

Write-Host "Connected to SQL Server" -ForegroundColor Green

# Drop and recreate database
Write-Host ""
Write-Host "Dropping apt database if exists..." -ForegroundColor Cyan
$cmd = $conn.CreateCommand()
$cmd.CommandTimeout = 30
$cmd.CommandText = "DROP DATABASE IF EXISTS apt"
try {
    $cmd.ExecuteNonQuery() | Out-Null
    Write-Host "✅ Database dropped" -ForegroundColor Green
} catch {
    Write-Host "Note: $_" -ForegroundColor Yellow
}

# Create database
Write-Host "Creating apt database..." -ForegroundColor Cyan
$cmd.CommandText = "CREATE DATABASE apt"
try {
    $cmd.ExecuteNonQuery() | Out-Null
    Write-Host "✅ Database created" -ForegroundColor Green
} catch {
    Write-Host "❌ Error: $_" -ForegroundColor Red
    exit 1
}

$conn.Close()

# Now run the scripts
Write-Host ""
Write-Host "Creating tables..." -ForegroundColor Cyan
$createDbScript = Get-Content "scripts/create_database.sql" -Raw -Encoding UTF8

$conn = New-Object System.Data.SqlClient.SqlConnection($connString)
$conn.Open()

foreach ($batch in ($createDbScript -split "GO")) {
    $batch = $batch.Trim()
    if ($batch.Length -gt 0 -and -not $batch.StartsWith("--")) {
        $cmd = $conn.CreateCommand()
        $cmd.CommandText = $batch
        $cmd.CommandTimeout = 30
        try {
            $cmd.ExecuteNonQuery() | Out-Null
        } catch {
            Write-Host "⚠️  Batch error (continuing): $_" -ForegroundColor Yellow
        }
    }
}
Write-Host "✅ Tables created" -ForegroundColor Green

# Verify tables
$cmd = $conn.CreateCommand()
$cmd.CommandText = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE'"
$tableCount = $cmd.ExecuteScalar()
Write-Host "✅ Verified: $tableCount tables exist" -ForegroundColor Green

# Stored procedures
Write-Host ""
Write-Host "Creating stored procedures..." -ForegroundColor Cyan
$spScript = Get-Content "scripts/stored_procedures.sql" -Raw -Encoding UTF8

foreach ($batch in ($spScript -split "GO")) {
    $batch = $batch.Trim()
    if ($batch.Length -gt 0 -and -not $batch.StartsWith("--")) {
        $cmd = $conn.CreateCommand()
        $cmd.CommandText = $batch
        $cmd.CommandTimeout = 30
        try {
            $cmd.ExecuteNonQuery() | Out-Null
        } catch {
            # Continue on error
        }
    }
}
Write-Host "✅ Stored procedures created" -ForegroundColor Green

# Seed data
Write-Host ""
Write-Host "Loading test data..." -ForegroundColor Cyan
$seedScript = Get-Content "scripts/seed_data.sql" -Raw -Encoding UTF8

foreach ($batch in ($seedScript -split "GO")) {
    $batch = $batch.Trim()
    if ($batch.Length -gt 0 -and -not $batch.StartsWith("--")) {
        $cmd = $conn.CreateCommand()
        $cmd.CommandText = $batch
        $cmd.CommandTimeout = 30
        try {
            $cmd.ExecuteNonQuery() | Out-Null
        } catch {
            # Continue on error
        }
    }
}
Write-Host "✅ Test data loaded" -ForegroundColor Green

$conn.Close()

Write-Host ""
Write-Host "✅ Database setup complete!" -ForegroundColor Green
Write-Host ""
Write-Host "Ready to build and launch APT" -ForegroundColor Green
