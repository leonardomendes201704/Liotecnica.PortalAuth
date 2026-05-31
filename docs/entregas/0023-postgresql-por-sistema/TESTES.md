# Testes - Entrega 0023

## Validacoes previstas

```powershell
npm run db:generate
npm run build
```

Diretorio: `D:\Leonardo\OpenFIIs`

## Validacao manual

1. Preparar banco local:

```powershell
$env:OPENFIIS_POSTGRES_ADMIN_PASSWORD = "senha-do-postgres"
powershell.exe -ExecutionPolicy Bypass -File .\scripts\Setup-LocalPostgres.ps1
npm run db:migrate
```

2. Iniciar PortalAuth Web.
3. Iniciar OpenFIIs.
4. Acessar OpenFIIs pelo card do PortalAuth.
5. Criar onboarding/carteira e confirmar gravacao no banco `openfiis`.
