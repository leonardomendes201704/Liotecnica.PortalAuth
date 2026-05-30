# Roadmap

Este documento orienta a evolucao do PortalAuth. Ele deve ser atualizado a cada entrega relevante.

## Fase 1 - Fundacao Tecnica

Status: em execucao.

Objetivo: criar uma base .NET 8 compilavel, com camadas, Building Blocks iniciais, API, MVC Razor, health checks, logging, correlation ID, exception handling e documentacao.

Entregas:

- [x] Criar solucao e projetos base.
- [x] Criar Building Blocks iniciais de API, Web, Logging e Observability.
- [x] Configurar API e Web com pipeline minimo.
- [x] Definir API em camadas com Controller, MediatR, Models, Services, Interfaces e Enums.
- [x] Definir PostgreSQL como banco oficial.
- [x] Configurar EF Core com migrations.
- [x] Criar migration inicial para schema `portal_auth`.
- [x] Configurar Swagger para abrir no browser em desenvolvimento.
- [x] Criar documentacao de governanca.
- [x] Validar build e testes.

## Fase 2 - Portal.Auth MVP

Status: em execucao.

Objetivo: implementar autenticacao e autorizacao corporativa.

Entregas previstas:

- [x] ASP.NET Core Identity.
- [x] Tela de login responsiva.
- [x] Login real com Identity.
- [x] Dashboard inicial apos login.
- [x] Cadastro de sistemas corporativos.
- [x] Cadastro de permissoes.
- [x] Cadastro de perfis/roles com permissoes.
- [x] Administracao de usuarios com vinculo de perfis.
- [x] Autorizacao granular por permissao.
- [x] Persistencia em PostgreSQL.
- [x] Migrations EF Core.
- [x] Cadastro de usuarios.
- [x] Cadastro de sistemas corporativos.
- [x] Cadastro de perfis.
- [x] Cadastro de permissoes.
- [x] Vinculo usuario x perfil.
- [x] Vinculo perfil x permissao.
- [x] Vinculo perfil x sistemas.
- [x] Login e logout.
- [x] Tela "Meus Sistemas".
- [x] Tela de Changelog.
- [x] Policies baseadas em permissoes.
- [x] Auditoria de login/logout e alteracao de perfil/permissao.
- [x] Health check real do PostgreSQL.
- [x] Tela de status operacional.
- [x] Reset de senha administrativo.
- [x] Desativacao/exclusao segura nos CRUDs administrativos.
- [x] Filtros avancados na auditoria.
- [x] Retencao configuravel de logs de auditoria.
- [x] Exportacao CSV da auditoria.
- [x] Troca obrigatoria de senha apos reset administrativo.
- [x] Tela Meu Perfil e alteracao de senha pelo usuario.
- [x] Desativacao logica de perfis com role customizada.
- [x] Testes automatizados de policies de autorizacao.
- [x] Usuario PostgreSQL dedicado no setup local.
- [x] Hardening inicial com rate limiting e CSP.
- [x] Historico basico de status operacional.
- [x] Pipeline CI e documentacao de deploy.

## Fase 3 - UI Kit Razor

Status: concluida.

Objetivo: criar identidade visual e componentes Razor reutilizaveis.

Entregas previstas:

- [x] Layout base.
- [x] Sidebar e topbar.
- [x] Breadcrumb.
- [x] Botoes, cards, tabelas, formularios e badges.
- [x] Tokens CSS.
- [x] Documentacao de uso.

## Fase 4 - Seguranca e Observabilidade

Status: planejada.

Objetivo: elevar maturidade tecnica para uso corporativo.

Entregas previstas:

- Checklist OWASP.
- [x] Rate limiting.
- [x] CSP gradual.
- OpenTelemetry.
- [x] Health checks completos.
- Dashboards e alertas.

## Fase 5 - Sistema Piloto

Status: planejada.

Objetivo: validar a plataforma em um sistema real.
