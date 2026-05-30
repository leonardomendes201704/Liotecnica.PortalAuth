# Seguranca

## Baseline Inicial

Esta primeira entrega aplica apenas a fundacao de seguranca. O objetivo e garantir que os proximos modulos nascam com pontos basicos ja preparados.

## Implementado

- HTTPS redirection.
- HSTS no projeto Web fora de desenvolvimento.
- Headers de seguranca:
  - `X-Content-Type-Options: nosniff`
  - `X-Frame-Options: DENY`
  - `Referrer-Policy: no-referrer`
  - `Permissions-Policy`
- Correlation ID por requisicao.
- Logging de metodo, rota, status, tempo e correlation ID.
- Tratamento global de excecoes para API sem expor stack trace ao cliente.
- Health checks com liveness e readiness.
- Health check real do PostgreSQL.
- PostgreSQL definido como banco oficial.
- Connection string local sem senha versionada.
- Swagger acessivel em desenvolvimento para produtividade.
- ASP.NET Core Identity com cookie authentication.
- Login/logout com antiforgery nos formularios.
- Autorizacao por policies baseadas em permissoes.
- Permissoes granulares para administracao.
- Dashboard restrito aos sistemas vinculados aos perfis do usuario.
- Auditoria funcional para login, logout e alteracoes administrativas.
- Reset de senha administrativo sem registro da senha temporaria em logs ou auditoria.
- Acoes destrutivas administrativas executadas por POST com antiforgery.
- Exclusao logica para sistemas e permissoes.

## Ainda Nao Implementado

- Rate limiting.
- CSP gradual.
- Redaction de dados sensiveis em logs.
- Protecao de Swagger em producao.

## Swagger

Swagger deve abrir automaticamente ao iniciar o perfil da API em desenvolvimento.

Em producao, o Swagger devera ser protegido, restrito ou desabilitado conforme decisao de deploy e seguranca.

## PostgreSQL e Secrets

PostgreSQL e o banco oficial da plataforma.

Nao versionar senhas de banco. Para ambientes locais, usar User Secrets ou variaveis de ambiente quando houver senha:

```powershell
dotnet user-secrets set "ConnectionStrings:PortalAuth" "Host=localhost;Port=5432;Database=liotecnica_portalauth;Username=postgres;Password=SENHA_LOCAL"
```

O setup local tambem aceita a senha via variavel `PORTALAUTH_POSTGRES_PASSWORD`, usada pelo script `scripts/Setup-LocalPostgres.ps1`.

## Politica de Logs

Nao registrar:

- senha;
- token completo;
- cookie de sessao;
- refresh token;
- documentos completos;
- dados bancarios;
- dados pessoais sensiveis;
- connection strings;
- secrets de integracao.

## Auditoria

A trilha de auditoria registra metadados operacionais, mas nao deve registrar senha, token, cookie, secrets ou payloads sensiveis completos.

A consulta de auditoria e protegida pela permissao `Auditoria.Visualizar`.

A retencao dos logs e configurada em banco pela chave `Audit.RetentionDays`. Alteracoes de retencao e execucoes de limpeza tambem sao auditadas.

A exportacao CSV da auditoria e limitada, auditada e aplica escaping nos campos para reduzir risco de formula injection em planilhas.

## Proximo Passo de Seguranca

Na fase Portal.Auth MVP, implementar testes automatizados de autorizacao, rate limiting e hardening gradual de seguranca.
