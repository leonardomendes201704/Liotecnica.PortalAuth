# Changelog

Historico detalhado das entregas do PortalAuth.

## 0018 - Exportacao CSV da auditoria

Data: 30/05/2026

Mudancas:

- Adicionado botao `Exportar CSV` na tela `/Audit`.
- Exportacao reutiliza os filtros atuais da auditoria.
- Exportacao limitada a 10.000 registros.
- CSV inclui data/hora UTC, acao, entidade, ID da entidade, usuario, IP, correlation ID e detalhes.
- Exportacao registra evento `AuditExported`.
- Campos do CSV sao escapados para reduzir risco de formula injection em planilhas.

## 0017 - Retencao de auditoria configuravel

Data: 30/05/2026

Mudancas:

- Criada tabela `portal_auth.app_settings`.
- Criada entidade `AppSetting`.
- Configuracao `Audit.RetentionDays` passou a ser persistida em banco.
- Criado `AuditRetentionService`.
- Tela `/Audit` permite alterar dias de retencao.
- Tela `/Audit` permite executar limpeza manual de logs expirados.
- Limpeza registra evento de auditoria com quantidade removida.
- Criadas acoes de auditoria `RetentionUpdated` e `AuditPurged`.

## 0016 - Filtros avancados na auditoria

Data: 30/05/2026

Mudancas:

- Adicionado filtro por periodo na tela de auditoria.
- Adicionado filtro por usuario.
- Adicionado filtro por acao.
- Adicionado filtro por entidade.
- Adicionada busca por detalhes, correlation ID, IP ou identificador da entidade.
- Adicionada paginacao simples.
- Adicionado controle de itens por pagina.
- Limite de pagina protegido entre 10 e 100 registros.

## 0015 - Desativacao e exclusao segura nos cadastros

Data: 30/05/2026

Mudancas:

- Adicionada exclusao logica de sistemas corporativos.
- Adicionada exclusao logica de permissoes.
- Adicionados bloqueio e reativacao de usuarios pela listagem.
- Adicionada remocao segura de perfis sem usuarios vinculados.
- Perfis com usuarios vinculados agora sao protegidos contra remocao.
- Criadas acoes de auditoria `Deactivated`, `Reactivated` e `Deleted`.
- Acoes administrativas usam POST com antiforgery.

## 0014 - Reset de senha administrativo

Data: 30/05/2026

Mudancas:

- Adicionado botao `Redefinir senha` na listagem de usuarios.
- Criada tela `Users/ResetPassword`.
- Criado `UserResetPasswordViewModel`.
- Reset usa token do ASP.NET Core Identity.
- Adicionada acao de auditoria `PasswordReset`.
- Senha temporaria nao e registrada em logs ou auditoria.
- Mensagem de sucesso exibida apos redefinicao.

## 0013 - Status operacional e PostgreSQL health check

Data: 30/05/2026

Mudancas:

- Criado health check real do PostgreSQL.
- Separados endpoints de liveness e readiness por tags.
- `/health/live` valida a aplicacao.
- `/health/ready` valida aplicacao e PostgreSQL.
- Criado `PostgreSqlHealthCheck`.
- Criada tela autenticada `/Status`.
- Adicionado item `Status` no menu principal.
- Link `Ver status` do dashboard passou a abrir a tela operacional.

## 0012 - Auditoria de login e administracao

Data: 30/05/2026

Mudancas:

- Criada entidade `AuditLog`.
- Criado enum `AuditAction`.
- Criada tabela `portal_auth.audit_logs`.
- Criado servico `IAuditService`.
- Registrado login com sucesso.
- Registrada tentativa de login com falha.
- Registrado logout.
- Registradas criacoes e edicoes de sistemas, permissoes, perfis e usuarios.
- Criada tela `/Audit` para consulta dos ultimos eventos.
- Criada permissao `Auditoria.Visualizar`.
- Menu `Auditoria` exibido apenas para usuarios autorizados.

## 0011 - Changelog no portal

Data: 30/05/2026

Mudancas:

- Adicionado item de menu `Changelog` no portal.
- Criada tela autenticada para exibir o historico detalhado de entregas.
- Criado `ChangelogController`.
- Criado `ChangelogViewModel`.
- Criada view `Views/Changelog/Index.cshtml`.
- Adicionados estilos de resumo e linha do tempo no `site.css`.

## 0010 - Meus Sistemas

Data: 30/05/2026

Mudancas:

- Criado vinculo entre perfil e sistema corporativo.
- Criada entidade `RoleSystemAccess`.
- Criada tabela `portal_auth.role_system_access`.
- Perfis passaram a permitir selecao de sistemas acessiveis.
- Dashboard passou a listar apenas sistemas liberados para os perfis do usuario.
- Botao `Acessar` passou a usar a `BaseUrl` real do sistema.
- Usuario sem acesso nao visualiza sistemas no dashboard.

## 0009 - Dashboard carregando todos os sistemas ativos

Data: 30/05/2026

Mudancas:

- Removido limite fixo de exibicao no dashboard.
- Dashboard passou a exibir todos os sistemas ativos cadastrados.
- Contador da tela inicial passou a refletir a quantidade real.

## 0008 - Autorizacao granular por permissoes

Data: 30/05/2026

Mudancas:

- Criadas policies dinamicas por codigo de permissao.
- Criados componentes de autorizacao granular.
- Controllers administrativos passaram a usar policies.
- Menus e botoes passaram a respeitar permissoes do usuario.

## 0007 - Modulos administrativos

Data: 30/05/2026

Mudancas:

- Criado CRUD de sistemas corporativos.
- Criado CRUD de permissoes.
- Criado CRUD de perfis.
- Criada administracao de usuarios.
- Criados vinculos de usuario com perfis e perfil com permissoes.

## 0006 - Identity e dashboard inicial

Data: 30/05/2026

Mudancas:

- ASP.NET Core Identity configurado.
- Login real implementado.
- Usuario administrador inicial criado via seed.
- Dashboard inicial protegido por autenticacao.
- Logout e pagina de acesso negado implementados.

## 0005 - Setup local PostgreSQL

Data: 30/05/2026

Mudancas:

- Criado script de setup local do PostgreSQL.
- Criado script para redefinir senha local do usuario `postgres`.
- Configurada aplicacao de migrations via script.
- Connection string local configurada por User Secrets.

## 0004 - Login responsivo

Data: 30/05/2026

Mudancas:

- Criada tela de login baseada na referencia visual enviada.
- Layout responsivo para desktop e celular.
- Criado `LoginViewModel`.
- Adicionados estilos dedicados para a tela de login.

## 0003 - EF Core e migrations

Data: 30/05/2026

Mudancas:

- EF Core configurado.
- PostgreSQL configurado como provider oficial.
- Criado `PortalAuthDbContext`.
- Criada migration inicial.
- Definido schema `portal_auth`.

## 0002 - Definicoes de API e PostgreSQL

Data: 30/05/2026

Mudancas:

- Definido padrao de API em camadas.
- Swagger configurado em desenvolvimento.
- MediatR adicionado ao fluxo da API.
- Criado endpoint de status da plataforma.
- PostgreSQL definido como banco oficial.

## 0001 - Fundacao tecnica

Data: 30/05/2026

Mudancas:

- Criada solucao inicial.
- Criados projetos Web, API, Application, Domain e Infrastructure.
- Criados building blocks iniciais.
- Configurados health checks, logging, correlation ID e security headers.
- Criada documentacao inicial de roadmap, backlog, arquitetura e seguranca.
