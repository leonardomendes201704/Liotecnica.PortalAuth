# Entrega 0010 - Meus Sistemas

## Objetivo

Transformar o dashboard em uma tela de "Meus Sistemas", exibindo apenas os sistemas corporativos liberados para os perfis do usuario autenticado.

## Escopo Implementado

- Criado vinculo `RoleSystemAccess` entre perfil do Identity e sistema corporativo.
- Criada tabela `portal_auth.role_system_access`.
- Adicionada selecao de sistemas acessiveis no cadastro/edicao de perfis.
- Dashboard passou a consultar os sistemas liberados pelos perfis do usuario logado.
- Botao "Acessar" passou a usar a `BaseUrl` real do sistema.
- Usuario sem vinculo de acesso nao visualiza o sistema no dashboard.
- Adicionado estado vazio quando nenhum sistema esta liberado.

## Arquivos Principais

- `src/Liotecnica.PortalAuth.Domain/Entities/RoleSystemAccess.cs`
- `src/Liotecnica.PortalAuth.Infrastructure/Persistence/Configurations/RoleSystemAccessConfiguration.cs`
- `src/Liotecnica.PortalAuth.Infrastructure/Persistence/PortalAuthDbContext.cs`
- `src/Liotecnica.PortalAuth.Infrastructure/Identity/IdentitySeeder.cs`
- `src/Liotecnica.PortalAuth.Web/Controllers/RolesController.cs`
- `src/Liotecnica.PortalAuth.Web/Controllers/DashboardController.cs`
- `src/Liotecnica.PortalAuth.Web/Views/Roles/_RoleForm.cshtml`
- `src/Liotecnica.PortalAuth.Web/Views/Dashboard/Index.cshtml`

## Resultado

O portal agora separa permissao administrativa de acesso a sistemas. Um perfil pode ter ou nao permissao para administrar cadastros, e separadamente pode receber acesso aos sistemas que devem aparecer no dashboard do usuario.
