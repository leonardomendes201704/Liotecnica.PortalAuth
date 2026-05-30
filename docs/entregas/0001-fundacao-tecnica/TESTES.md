# Testes - Entrega 0001

## Testes Automatizados

Projeto:

- `tests/Liotecnica.PortalAuth.UnitTests`

Cenarios cobertos:

- `ApiResponse<T>` cria envelope de sucesso.
- `PagedResult<T>` calcula total de paginas.
- `BaseEntity` marca exclusao logica e auditoria de atualizacao.

## Validacao Manual Recomendada

API:

1. Executar `dotnet run --project .\src\Liotecnica.PortalAuth.Api\Liotecnica.PortalAuth.Api.csproj`.
2. Acessar `/api/v1/platform/status`.
3. Confirmar retorno com `success`, `data`, `message` e `correlationId`.
4. Acessar `/health`, `/health/live` e `/health/ready`.

Web:

1. Executar `dotnet run --project .\src\Liotecnica.PortalAuth.Web\Liotecnica.PortalAuth.Web.csproj`.
2. Acessar a home MVC.
3. Acessar `/health`, `/health/live` e `/health/ready`.

## Resultado

Executado com sucesso:

```powershell
dotnet restore .\Liotecnica.PortalAuth.slnx
dotnet build .\Liotecnica.PortalAuth.slnx --no-restore
dotnet test .\Liotecnica.PortalAuth.slnx --no-build
```

Resultado:

- Build aprovado.
- 0 warnings.
- 0 erros.
- 3 testes aprovados.
