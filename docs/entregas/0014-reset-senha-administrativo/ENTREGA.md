# Entrega 0014 - Reset de Senha Administrativo

## Objetivo

Permitir que administradores redefinam a senha de usuarios pelo PortalAuth sem acesso direto ao banco de dados.

## Escopo Implementado

- Adicionado botao `Redefinir senha` na tela de usuarios.
- Criada tela `Users/ResetPassword`.
- Criado `UserResetPasswordViewModel`.
- Criadas acoes GET/POST `ResetPassword` no `UsersController`.
- Reset feito com token do ASP.NET Core Identity.
- Criada acao de auditoria `PasswordReset`.
- Evento de reset gravado em `audit_logs`.
- Mensagem de sucesso exibida apos redefinicao.

## Seguranca

A nova senha temporaria nao e registrada em logs, auditoria, changelog ou qualquer documento.

O fluxo exige a permissao ja existente `Usuario.Gerenciar`, pois faz parte da administracao de usuarios.

## Resultado

O suporte administrativo passa a conseguir recuperar acesso de usuarios de forma controlada, auditavel e sem intervenções manuais no PostgreSQL.
