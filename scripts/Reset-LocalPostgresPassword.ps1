param(
    [int] $Version = 18,
    [string] $ServiceName = "",
    [Parameter(Mandatory = $true)]
    [string] $NewPassword
)

$ErrorActionPreference = "Stop"

$postgresRoot = "C:\Program Files\PostgreSQL\$Version"
$postgresBin = Join-Path $postgresRoot "bin"
$postgresData = Join-Path $postgresRoot "data"
$psql = Join-Path $postgresBin "psql.exe"
$pgHba = Join-Path $postgresData "pg_hba.conf"

if (-not $ServiceName) {
    $ServiceName = "postgresql-x64-$Version"
}

if (-not (Test-Path $psql)) {
    throw "psql.exe nao encontrado em $psql"
}

if (-not (Test-Path $pgHba)) {
    throw "pg_hba.conf nao encontrado em $pgHba"
}

$isAdmin = ([Security.Principal.WindowsPrincipal] [Security.Principal.WindowsIdentity]::GetCurrent()).
    IsInRole([Security.Principal.WindowsBuiltInRole]::Administrator)

if (-not $isAdmin) {
    throw "Execute este script em um PowerShell aberto como Administrador."
}

$backupPath = "$pgHba.portal-auth-backup"
Copy-Item $pgHba $backupPath -Force

try {
    $original = Get-Content $pgHba -Raw
    $temporaryRules = @"
# TEMP PortalAuth password recovery - remove after reset
host    all             postgres        127.0.0.1/32            trust
host    all             postgres        ::1/128                 trust

"@

    if ($original -notmatch "TEMP PortalAuth password recovery") {
        $updated = $original -replace "(# TYPE\s+DATABASE\s+USER\s+ADDRESS\s+METHOD\r?\n)", "`$1`r`n$temporaryRules"
        Set-Content -Path $pgHba -Value $updated -Encoding ASCII
    }

    Restart-Service -Name $ServiceName -Force

    & $psql -h 127.0.0.1 -p 5432 -U postgres -d postgres -w -c "ALTER USER postgres WITH PASSWORD '$NewPassword';"
    if ($LASTEXITCODE -ne 0) {
        throw "Falha ao alterar a senha do usuario postgres."
    }
}
finally {
    Copy-Item $backupPath $pgHba -Force
    Restart-Service -Name $ServiceName -Force
}

$env:PGPASSWORD = $NewPassword
& $psql -h 127.0.0.1 -p 5432 -U postgres -d postgres -w -c "select current_user, current_database();"
Remove-Item Env:\PGPASSWORD -ErrorAction SilentlyContinue

Write-Host "Senha do usuario postgres redefinida com sucesso no servico $ServiceName."
