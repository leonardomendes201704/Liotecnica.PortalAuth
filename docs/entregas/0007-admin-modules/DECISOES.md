# Decisoes - Entrega 0007

## Roles Do Identity Como Perfis

Os perfis usam `IdentityRole<Guid>` para aproveitar a infraestrutura do ASP.NET Core Identity.

## Permissoes Separadas

Permissoes granulares ficam em tabela propria `permissions`, e o vinculo com perfis fica em `role_permissions`.

## Controllers MVC Diretos

Como esta entrega e de administracao MVC, os controllers usam `PortalAuthDbContext`, `UserManager` e `RoleManager` diretamente. A camada MediatR permanece obrigatoria para APIs REST.

## Seed Inicial

O seed cria dados minimos para desenvolvimento:

- seis sistemas corporativos;
- seis permissoes;
- vinculo das permissoes ao perfil Administrador.

## Dashboard Com Dados Reais

O dashboard agora busca sistemas ativos em `corporate_systems`, substituindo os cards mockados.
