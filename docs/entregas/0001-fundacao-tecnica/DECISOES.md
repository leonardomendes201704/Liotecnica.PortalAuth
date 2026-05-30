# Decisoes - Entrega 0001

## Usar `.slnx`

O SDK instalado gerou a solucao no formato `.slnx`. A entrega manteve esse formato por ser o padrao criado pelo ambiente atual.

## Comecar Pela Fundacao

A proposta original e ampla. A primeira entrega foca apenas na fundacao tecnica para evitar misturar infraestrutura, autenticacao, UI Kit e governanca em um unico passo.

## Prefixo `Liotecnica`

Os projetos usam o prefixo `Liotecnica` para refletir o repositorio e evitar nomes genericos como `Company`.

## Building Blocks Separados

Os blocos foram separados por responsabilidade:

- `Api`: contratos de response e paginacao.
- `Web`: middlewares HTTP.
- `Logging`: correlation ID.
- `Observability`: health checks.
- `Security`: reservado para policies, claims e autorizacao.

## Identity Fica Para a Proxima Entrega

Identity, banco e migrations serao implementados no Portal.Auth MVP, depois da fundacao compilar e estar validada.

## APIs em Camadas

Toda API deve usar Controller como entrada HTTP e MediatR para acionar comandos/queries da Application.

Padrao minimo:

```text
Controller
  -> MediatR
  -> Handler
  -> Service
  -> Interface
  -> Model/DTO
  -> Enum quando aplicavel
```

Controllers nao devem conter regra de negocio.

## Swagger Acessivel

Toda API deve abrir Swagger no browser em ambiente de desenvolvimento. O perfil da API deve manter `launchBrowser: true` e `launchUrl: swagger`.

## PostgreSQL

PostgreSQL e o banco oficial da plataforma. O provider EF Core padrao e `Npgsql.EntityFrameworkCore.PostgreSQL`.

Secrets, incluindo senhas de banco, nao devem ser versionados.
