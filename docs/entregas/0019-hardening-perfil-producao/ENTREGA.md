# Entrega 0019 - Hardening, Perfil do Usuario e Prontidao para Producao

## Objetivo

Consolidar os principais pontos pendentes do PortalAuth em uma entrega unica: seguranca de senha, role customizada, hardening inicial, historico operacional, usuario PostgreSQL dedicado, testes e documentacao de deploy.

## Escopo Entregue

- `ApplicationRole` com status ativo, exclusao logica, desativacao e reativacao.
- `ApplicationUser.MustChangePassword` para troca obrigatoria apos reset administrativo.
- Telas `Meu Perfil` e `Alterar Senha`.
- Middleware de enforcement para usuarios com senha obrigatoria pendente.
- Rate limiting global e CSP inicial.
- Historico de status operacional persistido em banco.
- Setup local com usuario PostgreSQL dedicado `portalauth_app`.
- Workflow CI no GitHub Actions.
- `docs/DEPLOYMENT.md`.
- Testes unitarios para policies de permissao e regras criticas de Identity.

## Impacto

A entrega aumenta a maturidade minima para uso corporativo: reduz uso de credenciais administrativas, melhora governanca de perfis, impede senha temporaria permanente e cria uma base de deploy/validacao automatizada.
