# Decisoes - Entrega 0008

## Policies Dinamicas

Foi criado um provider de policies dinamicas para evitar registrar manualmente cada permissao no `Program.cs`.

## Consulta No Banco

O handler consulta as permissoes atuais no PostgreSQL a cada autorizacao. Isso reflete alteracoes de perfil/permissao rapidamente.

Cache pode ser adicionado depois se houver necessidade de performance.

## UI Condicional

Menus e botoes usam `IAuthorizationService` para evitar mostrar acoes que o usuario nao pode executar.

## Roles Ainda Existem

Roles continuam sendo os perfis do Identity. A autorizacao fina acontece pelo vinculo `role_permissions`.
