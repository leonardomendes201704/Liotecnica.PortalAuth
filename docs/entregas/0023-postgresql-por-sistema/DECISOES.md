# Decisoes - Entrega 0023

## Um banco por sistema

Cada sistema corporativo deve ter banco PostgreSQL proprio. Isso evita acoplamento indevido entre dominios, permite permissoes dedicadas e reduz impacto de migrations.

## PortalAuth como identidade central

O PortalAuth nao armazena dados transacionais dos sistemas. Ele emite identidade via OIDC. Os sistemas usam a claim `sub` como `user_id` local.

## Prisma no OpenFIIs

Foi adotado Prisma no OpenFIIs por integrar bem com Next.js e TypeScript, oferecer client tipado e versionar schema/migrations junto ao repositorio do sistema.

## Sem Supabase

Supabase foi removido do runtime e das migrations do OpenFIIs. O banco oficial passa a ser PostgreSQL local.
