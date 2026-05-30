# Decisoes - Entrega 0003

## Migrations no Infrastructure

As migrations ficam no projeto `Liotecnica.PortalAuth.Infrastructure`, junto com `PortalAuthDbContext` e os mapeamentos EF Core.

## Tool Local

`dotnet-ef` foi instalado como ferramenta local para manter o fluxo versionado no repositorio.

Com isso, qualquer desenvolvedor pode executar:

```powershell
dotnet tool restore
```

## Entidade Inicial Real

A migration inicial usa `CorporateSystem`, uma entidade real prevista na proposta, em vez de criar uma migration vazia.

## Schema PostgreSQL

O schema padrao e `portal_auth`.

## Secrets

A factory design-time usa uma connection string local sem senha. Senhas devem entrar por User Secrets, variavel de ambiente ou cofre de segredos.
