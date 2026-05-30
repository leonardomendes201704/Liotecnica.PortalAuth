# Entrega 0001 - Fundacao Tecnica

## Objetivo

Criar a base inicial do PortalAuth conforme a proposta de arquitetura corporativa, mantendo a entrega pequena, compilavel e preparada para evolucao incremental.

## Escopo Entregue

- Repositorio Git inicializado.
- Remote configurado para `https://github.com/leonardomendes201704/Liotecnica.PortalAuth.git`.
- Solucao `Liotecnica.PortalAuth.slnx`.
- Projetos Web, API, Application, Domain, Infrastructure e Building Blocks.
- Response padrao para API.
- Paginacao padrao.
- Correlation ID.
- Logging de requisicoes.
- Headers de seguranca.
- Tratamento global de excecoes para API.
- Health checks.
- Entidade base com campos de auditoria tecnica.
- API organizada em Controller, MediatR, Models, Services, Interfaces e Enums.
- Swagger configurado para abrir automaticamente no browser em desenvolvimento.
- PostgreSQL definido como banco oficial.
- `PortalAuthDbContext` preparado com provider Npgsql.
- Testes unitarios iniciais.
- Documentacao de roadmap, backlog, arquitetura, seguranca e entrega.

## Fora do Escopo

- Identity.
- Banco de dados.
- Login/logout.
- Cadastro de usuarios, sistemas, perfis e permissoes.
- UI Kit corporativo completo.
- Deploy e pipeline.

## Como Validar

```powershell
dotnet restore .\Liotecnica.PortalAuth.slnx
dotnet build .\Liotecnica.PortalAuth.slnx
dotnet test .\Liotecnica.PortalAuth.slnx
```

Executar a API:

```powershell
dotnet run --project .\src\Liotecnica.PortalAuth.Api\Liotecnica.PortalAuth.Api.csproj
```

Rotas iniciais:

- `/api/v1/platform/status`
- `/health`
- `/health/live`
- `/health/ready`

## Proximo Passo

Iniciar a Fase 2: Portal.Auth MVP com Identity, modelo de usuario/perfil/sistema/permissao e login/logout.
