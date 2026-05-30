# Decisoes - Entrega 0010

## Vinculo por perfil

Foi adotado `RoleSystemAccess` em vez de `UserSystemAccess`.

Motivos:

- reduz administracao manual por usuario;
- reaproveita o modelo de perfis ja existente;
- permite que usuarios recebam acesso por associacao a um ou mais perfis;
- combina com o fluxo atual de `AspNetUserRoles`.

## Separacao entre permissao e acesso

Permissoes continuam controlando funcoes administrativas, como cadastrar sistemas, usuarios e perfis.

`RoleSystemAccess` controla apenas quais sistemas aparecem no dashboard e podem ser acessados pelo usuario.

## Dashboard restritivo por padrao

O dashboard lista apenas sistemas ativos, nao excluidos e vinculados aos perfis do usuario autenticado.

Se o usuario nao possuir nenhum sistema liberado, a tela exibe uma mensagem orientando a solicitar acesso ao administrador.

## Seed do administrador

O seed foi atualizado para conceder ao perfil `Administrador` acesso a todos os sistemas ativos. Isso preserva a experiencia local e evita que o usuario administrador fique sem sistemas apos a migration.
