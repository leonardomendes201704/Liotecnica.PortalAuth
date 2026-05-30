# Entrega 0007 - Modulos Administrativos

## Objetivo

Implementar os tres proximos blocos do PortalAuth:

1. Cadastro de sistemas corporativos.
2. Perfis/roles e permissoes granulares.
3. Administracao de usuarios.

## Escopo Entregue

- CRUD de sistemas corporativos.
- CRUD de permissoes granulares.
- CRUD de perfis/roles com selecao de permissoes.
- Administracao de usuarios com criacao, edicao, bloqueio e vinculo de perfis.
- Entidades `Permission` e `RolePermission`.
- Mapeamentos EF Core para `permissions` e `role_permissions`.
- Migration `AddPermissionsAndRoleLinks`.
- Seed inicial de sistemas, permissoes e vinculos do perfil Administrador.
- Dashboard passa a buscar sistemas ativos do banco.
- Layout administrativo reutilizavel `_PortalLayout`.

## Rotas

- `/Systems`
- `/Permissions`
- `/Roles`
- `/Users`

Todas protegidas por perfil `Administrador`.

## Fora do Escopo

- Policy-based authorization por permissao.
- Auditoria funcional.
- Exclusao/desativacao completa via acoes dedicadas.
- Reset de senha administrativo.
- Filtros e paginacao server-side.

## Como Validar

1. Fazer login como administrador.
2. Acessar as rotas administrativas.
3. Criar/editar sistemas.
4. Criar permissao.
5. Criar perfil e vincular permissao.
6. Criar usuario e vincular perfil.
7. Voltar ao dashboard e confirmar sistemas ativos listados.
