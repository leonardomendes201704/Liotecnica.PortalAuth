# Decisoes - Entrega 0022

## PortalAuth como Identity Provider

Foi usado OpenIddict no PortalAuth para suportar OpenID Connect com authorization code e PKCE. Isso mantem a identidade centralizada no ASP.NET Core Identity ja existente.

## OpenFIIs como cliente OIDC

O OpenFIIs usa Auth.js/NextAuth como camada de sessao local. O login local por e-mail e senha foi removido do fluxo de usuario.

## Banco do sistema

O acesso a dados privados ocorre somente por APIs server-side do Next.js e filtros pelo `session.user.id` recebido do PortalAuth.

## Claim de usuario

O campo `user_id` das tabelas do OpenFIIs passa a armazenar o `ApplicationUser.Id` do PortalAuth, recebido na claim OIDC `sub`.

## Desenvolvimento local

O client `openfiis-local` usa `http://localhost:3001/api/auth/callback/portalauth` como redirect URI e segredo local documentado. Em producao, o segredo deve ser substituido e armazenado fora do codigo.
