# Testes - Entrega 0003

## Testes Automatizados

Adicionado teste para garantir que `CorporateSystem` nasce ativo e preserva propriedades principais.

## Validacao De Migration

Comando executado:

```powershell
dotnet tool run dotnet-ef migrations add InitialPortalAuthSchema `
  --project .\src\Liotecnica.PortalAuth.Infrastructure\Liotecnica.PortalAuth.Infrastructure.csproj `
  --startup-project .\src\Liotecnica.PortalAuth.Api\Liotecnica.PortalAuth.Api.csproj `
  --context PortalAuthDbContext `
  --output-dir Persistence\Migrations
```

Resultado:

- Build previo aprovado pelo EF Tools.
- Migration criada com sucesso.

## Resultado

Executado com sucesso:

```powershell
dotnet tool restore
dotnet restore .\Liotecnica.PortalAuth.slnx
dotnet build .\Liotecnica.PortalAuth.slnx --no-restore
dotnet test .\Liotecnica.PortalAuth.slnx --no-build
```

Resultado:

- `dotnet-ef` restaurado.
- Build aprovado.
- 0 warnings.
- 0 erros.
- 5 testes aprovados.
