# Entrega 0023 - PostgreSQL local por sistema

Data: 31/05/2026

## Escopo

Padronizar a arquitetura de dados dos sistemas integrados ao PortalAuth: cada sistema deve ter seu proprio banco PostgreSQL, enquanto o PortalAuth fica responsavel por identidade e SSO.

## Entregue

- Definido padrao de um banco por sistema.
- OpenFIIs migrado para PostgreSQL local `openfiis`.
- Prisma adicionado ao OpenFIIs.
- Schema e migration inicial criados para carteira, FIIs, transacoes, posicoes, dividendos, simulacoes e relatorios.
- APIs server-side do OpenFIIs migradas para Prisma.
- Dependencias Supabase removidas do OpenFIIs.
- Arquivos e migrations Supabase removidos do OpenFIIs.
- Script de setup local do PostgreSQL adicionado ao OpenFIIs.

## Resultado esperado

O OpenFIIs autentica pelo PortalAuth, recebe o usuario pela sessao OIDC e persiste todos os dados no banco PostgreSQL local proprio do sistema.
