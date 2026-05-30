# Checklist OWASP

Checklist inicial para elevar o PortalAuth ao baseline corporativo de seguranca.

## A01 - Broken Access Control

- [x] Controllers administrativos protegidos por policies de permissao.
- [x] Menu e acoes sensiveis renderizados condicionalmente.
- [x] Dashboard filtra sistemas por vinculo de perfil.
- [x] Perfis inativos/excluidos nao concedem permissoes.

## A02 - Cryptographic Failures

- [x] Senhas gerenciadas pelo ASP.NET Core Identity.
- [x] Senhas e tokens nao sao registrados em auditoria.
- [x] Connection strings com senha ficam fora do versionamento.
- [ ] Definir TLS e certificados do ambiente produtivo.

## A03 - Injection

- [x] Persistencia usa EF Core com queries parametrizadas.
- [x] Exportacao CSV aplica escaping contra formula injection.
- [ ] Criar testes especificos para entradas maliciosas em formularios administrativos.

## A04 - Insecure Design

- [x] Auditoria registra acoes administrativas e autenticacao.
- [x] Reset administrativo exige troca obrigatoria de senha.
- [x] Acoes destrutivas usam POST com antiforgery.
- [x] Retencao de auditoria e configuravel.

## A05 - Security Misconfiguration

- [x] Swagger habilitado apenas em desenvolvimento.
- [x] HSTS fora de desenvolvimento.
- [x] Headers de seguranca e CSP inicial.
- [x] Rate limiting global.
- [ ] Revisar `AllowedHosts` por ambiente antes de producao.

## A06 - Vulnerable and Outdated Components

- [x] CI executa restore/build/test.
- [ ] Ativar auditoria de dependencias no pipeline.

## A07 - Identification and Authentication Failures

- [x] ASP.NET Core Identity.
- [x] Politica de senha forte.
- [x] Lockout em falhas de login.
- [x] Troca obrigatoria apos reset.
- [ ] Avaliar MFA para usuarios administrativos.

## A08 - Software and Data Integrity Failures

- [x] Pipeline CI versionado.
- [x] Migrations EF Core versionadas.
- [ ] Assinar artefatos ou imagens em pipeline de CD futuro.

## A09 - Security Logging and Monitoring Failures

- [x] Auditoria de login/logout e administracao.
- [x] Correlation ID por requisicao.
- [x] OpenTelemetry para traces Web/API.
- [x] Status operacional com historico e alertas.

## A10 - Server-Side Request Forgery

- [x] Nao ha consumo de URLs arbitrarias pelo backend no escopo atual.
- [ ] Reavaliar quando integrações externas forem adicionadas.
