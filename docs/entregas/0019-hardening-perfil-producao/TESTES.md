# Testes - Entrega 0019

## Automatizados

- Policy conhecida cria `PermissionRequirement`.
- Policy desconhecida retorna `null`.
- `ApplicationRole` suporta desativacao logica.
- `ApplicationUser` permite flag de troca obrigatoria de senha.

## Validacao Manual Recomendada

1. Redefinir senha de um usuario em `Usuarios`.
2. Entrar com a senha temporaria.
3. Confirmar redirecionamento obrigatorio para `Alterar Senha`.
4. Alterar senha e confirmar acesso ao dashboard.
5. Desativar um perfil e confirmar que ele nao concede permissoes/sistemas.
6. Acessar `Status` e validar que o historico recente registra snapshots.
7. Rodar `scripts/Setup-LocalPostgres.ps1` com `PORTALAUTH_POSTGRES_PASSWORD` e `PORTALAUTH_APP_POSTGRES_PASSWORD`.
