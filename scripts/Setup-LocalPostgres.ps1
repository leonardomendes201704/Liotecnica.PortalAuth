param(
    [int] $Version = 18,
    [string] $HostName = "localhost",
    [int] $Port = 5432,
    [string] $AdminUser = "postgres",
    [string] $AppUser = "portalauth_app",
    [string] $DatabaseName = "liotecnica_portalauth",
    [string] $ProjectPath = ".\src\Liotecnica.PortalAuth.Api\Liotecnica.PortalAuth.Api.csproj"
)

$ErrorActionPreference = "Stop"

$postgresBin = "C:\Program Files\PostgreSQL\$Version\bin"
$psql = Join-Path $postgresBin "psql.exe"
$createdb = Join-Path $postgresBin "createdb.exe"
$pgIsReady = Join-Path $postgresBin "pg_isready.exe"

if (-not (Test-Path $psql)) {
    throw "psql.exe nao encontrado em $psql"
}

if (-not $env:PORTALAUTH_POSTGRES_PASSWORD) {
    throw "Defina a variavel de ambiente PORTALAUTH_POSTGRES_PASSWORD com a senha do usuario PostgreSQL '$AdminUser'."
}

if (-not $env:PORTALAUTH_APP_POSTGRES_PASSWORD) {
    throw "Defina a variavel de ambiente PORTALAUTH_APP_POSTGRES_PASSWORD com a senha do usuario dedicado '$AppUser'."
}

$env:PGPASSWORD = $env:PORTALAUTH_POSTGRES_PASSWORD

& $pgIsReady -h $HostName -p $Port
if ($LASTEXITCODE -ne 0) {
    throw "PostgreSQL nao esta aceitando conexoes em ${HostName}:${Port}."
}

$databaseExists = & $psql -h $HostName -p $Port -U $AdminUser -d postgres -tAc "SELECT 1 FROM pg_database WHERE datname = '$DatabaseName';"
if ($LASTEXITCODE -ne 0) {
    throw "Falha ao autenticar no PostgreSQL com o usuario '$AdminUser'. Verifique a senha em PORTALAUTH_POSTGRES_PASSWORD."
}

$databaseExistsValue = ($databaseExists | Out-String).Trim()
$escapedAppPassword = $env:PORTALAUTH_APP_POSTGRES_PASSWORD.Replace("'", "''")
$roleExists = & $psql -h $HostName -p $Port -U $AdminUser -d postgres -tAc "SELECT 1 FROM pg_roles WHERE rolname = '$AppUser';"
$roleExistsValue = ($roleExists | Out-String).Trim()

if ($roleExistsValue -ne "1") {
    & $psql -h $HostName -p $Port -U $AdminUser -d postgres -c "CREATE ROLE $AppUser WITH LOGIN PASSWORD '$escapedAppPassword';"
    if ($LASTEXITCODE -ne 0) {
        throw "Falha ao criar o usuario dedicado '$AppUser'."
    }
}

if ($databaseExistsValue -ne "1") {
    & $createdb -h $HostName -p $Port -U $AdminUser -E UTF8 -O $AppUser $DatabaseName
    if ($LASTEXITCODE -ne 0) {
        throw "Falha ao criar o banco '$DatabaseName'."
    }
}
else {
    & $psql -h $HostName -p $Port -U $AdminUser -d postgres -c "ALTER DATABASE $DatabaseName OWNER TO $AppUser;"
    if ($LASTEXITCODE -ne 0) {
        throw "Falha ao ajustar o owner do banco '$DatabaseName'."
    }
}

& $psql -h $HostName -p $Port -U $AdminUser -d $DatabaseName -c "GRANT ALL PRIVILEGES ON DATABASE $DatabaseName TO $AppUser; GRANT ALL ON SCHEMA public TO $AppUser;"
if ($LASTEXITCODE -ne 0) {
    throw "Falha ao conceder permissoes para '$AppUser'."
}

$connectionString = "Host=$HostName;Port=$Port;Database=$DatabaseName;Username=$AppUser;Password=$env:PORTALAUTH_APP_POSTGRES_PASSWORD"

dotnet user-secrets set "ConnectionStrings:PortalAuth" $connectionString --project $ProjectPath

dotnet tool restore

dotnet tool run dotnet-ef database update `
    --project ".\src\Liotecnica.PortalAuth.Infrastructure\Liotecnica.PortalAuth.Infrastructure.csproj" `
    --startup-project $ProjectPath `
    --context PortalAuthDbContext

Write-Host "PostgreSQL local configurado para o PortalAuth."
