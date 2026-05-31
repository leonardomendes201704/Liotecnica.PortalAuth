# Entrega 0022 - SSO OpenFIIs

Data: 31/05/2026

## Escopo

Implementar SSO corporativo definitivo entre PortalAuth e OpenFIIs.

## Entregue

- PortalAuth configurado como provedor OpenID Connect via OpenIddict.
- Persistencia OIDC adicionada ao `PortalAuthDbContext`.
- Client `openfiis-local` registrado por seed.
- Endpoints OIDC criados em `ConnectController`.
- OpenFIIs cadastrado como sistema corporativo local.
- OpenFIIs migrado para Auth.js usando PortalAuth como provider.
- Login proprio do OpenFIIs substituido por redirecionamento OIDC.
- Proxy do OpenFIIs passou a validar sessao Auth.js.
- Dados privados passaram a ser acessados por APIs server-side do Next.js.
- Documentacao operacional criada em `docs/SSO-OIDC.md`.

## Resultado esperado

O usuario faz login no PortalAuth, abre o card OpenFIIs e entra no sistema sem digitar credenciais novamente. Se tentar acessar OpenFIIs diretamente sem sessao, e redirecionado para o PortalAuth.
