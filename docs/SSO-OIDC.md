# SSO OIDC - PortalAuth e OpenFIIs

## Objetivo

O PortalAuth e o provedor corporativo de identidade. O OpenFIIs e um cliente OpenID Connect e nao autentica usuarios localmente. O banco oficial do OpenFIIs e um PostgreSQL local proprio, acessado por APIs server-side do Next.js.

## PortalAuth

Endpoints:

- Discovery: `http://localhost:5112/.well-known/openid-configuration`
- Authorization: `http://localhost:5112/connect/authorize`
- Token: `http://localhost:5112/connect/token`
- UserInfo: `http://localhost:5112/connect/userinfo`
- Logout: `http://localhost:5112/connect/logout`

Client local:

- Client ID: `openfiis-local`
- Client secret local: `openfiis-local-dev-secret`
- Redirect URI: `http://localhost:3001/api/auth/callback/portalauth`
- Redirect URI alternativa: `http://localhost:3000/api/auth/callback/portalauth`
- Post logout redirect URI: `http://localhost:3001/login`
- Fluxo: authorization code com PKCE

Claims emitidas:

- `sub`: ID do usuario PortalAuth
- `email`: e-mail corporativo
- `name`: nome exibido
- `role`: perfis do usuario
- `permission`: permissoes ativas
- `department`: departamento

Bloqueios:

- Usuario inexistente ou bloqueado nao recebe token.
- Usuario com `MustChangePassword = true` nao autoriza o client.
- Roles inativas ou excluidas nao contribuem com permissoes.

## OpenFIIs

Variaveis esperadas:

```env
NEXTAUTH_URL=http://localhost:3001
NEXTAUTH_SECRET=defina-um-segredo-local
PORTALAUTH_OIDC_ISSUER=http://localhost:5112
PORTALAUTH_OIDC_CLIENT_ID=openfiis-local
PORTALAUTH_OIDC_CLIENT_SECRET=openfiis-local-dev-secret
DATABASE_URL=postgresql://openfiis_app:openfiis_dev_password@localhost:5432/openfiis
```

Fluxo:

1. Usuario entra no PortalAuth.
2. Usuario clica no card OpenFIIs.
3. OpenFIIs verifica a ausencia de sessao local.
4. OpenFIIs redireciona para o PortalAuth via OIDC.
5. PortalAuth reconhece a sessao existente e retorna para o callback Auth.js.
6. OpenFIIs cria cookie HttpOnly local.
7. OpenFIIs chama suas APIs internas para acessar dados no PostgreSQL local.

## PostgreSQL por sistema

Cada sistema deve ter seu proprio banco no servidor PostgreSQL local. Para o OpenFIIs, o banco oficial e `openfiis`, acessado pelo usuario dedicado `openfiis_app`.

As tabelas continuam com campos `user_id uuid`, mas esse valor passa a representar o `ApplicationUser.Id` do PortalAuth recebido pela claim OIDC `sub`.

## Validacao

- Rodar `dotnet build .\Liotecnica.PortalAuth.slnx -p:UseSharedCompilation=false`.
- Rodar `npm run build` em `D:\Leonardo\OpenFIIs`.
- Rodar `npm run db:migrate` em `D:\Leonardo\OpenFIIs` apos preparar o banco local.
- Rodar PortalAuth Web em `http://localhost:5112`.
- Rodar OpenFIIs em `http://localhost:3001`.
- Acessar OpenFIIs pelo card no dashboard do PortalAuth.
- Acessar `http://localhost:3001/login?next=%2F` diretamente e confirmar redirecionamento para PortalAuth.
- Confirmar que telas do OpenFIIs carregam dados por `/api/portfolio`, `/api/onboarding` e `/api/fiis`.
