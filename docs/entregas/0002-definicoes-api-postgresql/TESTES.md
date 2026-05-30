# Testes - Entrega 0002

## Testes Automatizados

Adicionado cenario para `PlatformStatusService`, validando:

- nome da aplicacao;
- ambiente informado;
- status operacional.

## Validacao Manual Recomendada

1. Executar a API:

```powershell
dotnet run --project .\src\Liotecnica.PortalAuth.Api\Liotecnica.PortalAuth.Api.csproj
```

2. Confirmar abertura do browser em `/swagger`.
3. Executar `GET /api/v1/platform/status` pelo Swagger.
4. Confirmar response envelope com `success`, `data`, `message` e `correlationId`.

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
- 4 testes aprovados.
