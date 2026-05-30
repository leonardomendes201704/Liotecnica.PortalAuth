using Liotecnica.PortalAuth.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Liotecnica.PortalAuth.Web.Controllers;

[Authorize]
public sealed class ChangelogController : Controller
{
    public IActionResult Index()
    {
        var model = new ChangelogViewModel
        {
            Entries =
            [
                new(
                    "0021",
                    "Seguranca, observabilidade e sistema piloto",
                    "30/05/2026",
                    "O roadmap foi concluido com checklist OWASP, OpenTelemetry, alertas operacionais e um sistema piloto local.",
                    [
                        "OpenTelemetry configurado no Web e na API.",
                        "Exportador OTLP opcional por configuracao.",
                        "Tela de Status passou a exibir alertas operacionais.",
                        "Criado checklist OWASP.",
                        "Criada documentacao de observabilidade.",
                        "Registrado sistema PortalAuth API Piloto no seed.",
                        "Criada documentacao de validacao do piloto."
                    ],
                    [
                        "Pacotes OpenTelemetry adicionados aos projetos Web e API.",
                        "Configuracao `OpenTelemetry:OtlpEndpoint` adicionada.",
                        "Criado modelo `OperationalAlertViewModel`.",
                        "Seed de sistemas agora adiciona defaults ausentes por codigo."
                    ]),
                new(
                    "0020",
                    "UI Kit Razor",
                    "30/05/2026",
                    "A Fase 3 foi concluida com tokens CSS, componentes reutilizaveis, tela viva de referencia e documentacao de uso.",
                    [
                        "Criados tokens CSS para cores, espacamentos, raios, sombras e superficies.",
                        "Criadas classes reutilizaveis para botoes, cards, badges, grids e breadcrumbs.",
                        "Criados partials para sidebar, topbar e breadcrumb.",
                        "Dashboard passou a reutilizar sidebar e topbar compartilhados.",
                        "Criada tela autenticada UI Kit.",
                        "Criada documentacao `docs/UI-KIT.md`."
                    ],
                    [
                        "Criado `UiKitController`.",
                        "Criados partials `_PortalSidebar`, `_PortalTopbar` e `_Breadcrumb`.",
                        "Criado `BreadcrumbItemViewModel`.",
                        "Mantida compatibilidade visual com as telas administrativas existentes."
                    ]),
                new(
                    "0019",
                    "Hardening, perfil do usuario e prontidao para producao",
                    "30/05/2026",
                    "O PortalAuth recebeu ajustes de seguranca, perfil do usuario, roles customizadas, historico operacional, CI e documentacao de deploy.",
                    [
                        "Reset administrativo passou a exigir troca obrigatoria de senha.",
                        "Criadas telas Meu Perfil e Alterar Senha.",
                        "Perfis agora usam role customizada com desativacao e exclusao logica.",
                        "Adicionados rate limiting global e CSP inicial.",
                        "Status operacional passou a registrar historico recente.",
                        "Setup local usa usuario PostgreSQL dedicado.",
                        "Adicionado workflow CI e documentacao de deploy."
                    ],
                    [
                        "Criado `ApplicationRole` e flag `ApplicationUser.MustChangePassword`.",
                        "Criada migration `AddHardeningProfileAndStatusHistory`.",
                        "Criada entidade `OperationalStatusSnapshot`.",
                        "Adicionados testes de policies de permissao e regras criticas de Identity."
                    ]),
                new(
                    "0018",
                    "Exportacao CSV da auditoria",
                    "30/05/2026",
                    "A tela de auditoria passou a exportar os eventos filtrados para CSV.",
                    [
                        "Adicionado botao Exportar CSV na tela de Auditoria.",
                        "Exportacao reutiliza os filtros atuais.",
                        "Arquivo inclui data, acao, entidade, usuario, IP, correlation ID e detalhes.",
                        "Exportacao registra evento na auditoria.",
                        "Volume exportado limitado a 10.000 linhas."
                    ],
                    [
                        "Criada acao `ExportCsv` no `AuditController`.",
                        "Adicionado `AuditAction.AuditExported`.",
                        "CSV gerado com separador ponto e virgula e BOM UTF-8.",
                        "Campos sao escapados para reduzir risco de formula injection em planilhas."
                    ]),
                new(
                    "0017",
                    "Retencao de auditoria configuravel",
                    "30/05/2026",
                    "A auditoria passou a ter politica de retencao persistida em banco e limpeza manual controlada.",
                    [
                        "Criada tabela de configuracoes persistidas.",
                        "Configuracao `Audit.RetentionDays` salva no banco.",
                        "Tela de Auditoria permite alterar dias de retencao.",
                        "Tela de Auditoria permite executar limpeza manual.",
                        "Limpeza registra evento de auditoria com quantidade removida."
                    ],
                    [
                        "Criada entidade `AppSetting`.",
                        "Criado `AuditRetentionService`.",
                        "Migration `AddAppSettingsAndAuditRetention` aplicada.",
                        "Novas acoes de auditoria: `RetentionUpdated` e `AuditPurged`."
                    ]),
                new(
                    "0016",
                    "Filtros avancados na auditoria",
                    "30/05/2026",
                    "A tela de auditoria passou a oferecer filtros e paginacao para investigacoes operacionais.",
                    [
                        "Filtro por periodo.",
                        "Filtro por usuario.",
                        "Filtro por acao.",
                        "Filtro por entidade.",
                        "Busca por detalhes, correlation ID, IP ou identificador de entidade.",
                        "Paginacao com controle de itens por pagina."
                    ],
                    [
                        "Filtros aplicados no backend antes da paginacao.",
                        "Page size limitado entre 10 e 100 registros.",
                        "Opcoes de entidade carregadas dos eventos existentes.",
                        "Acoes disponiveis carregadas a partir do enum `AuditAction`."
                    ]),
                new(
                    "0015",
                    "Desativacao e exclusao segura nos cadastros",
                    "30/05/2026",
                    "CRUDs administrativos passaram a ter acoes seguras para remover, bloquear ou reativar registros sem perder rastreabilidade.",
                    [
                        "Sistemas podem ser excluidos logicamente.",
                        "Permissoes podem ser excluidas logicamente.",
                        "Usuarios podem ser bloqueados e reativados pela listagem.",
                        "Perfis sem usuarios vinculados podem ser removidos.",
                        "Perfis com usuarios vinculados sao protegidos contra remocao."
                    ],
                    [
                        "Exclusao logica usa `IsDeleted` e desativa o registro.",
                        "Usuarios usam `LockoutEnd` do ASP.NET Core Identity.",
                        "Novas acoes de auditoria: `Deactivated`, `Reactivated` e `Deleted`.",
                        "Todas as acoes usam POST com antiforgery."
                    ]),
                new(
                    "0014",
                    "Reset de senha administrativo",
                    "30/05/2026",
                    "Administradores passaram a redefinir senhas temporarias de usuarios pelo portal.",
                    [
                        "Adicionado botao Redefinir senha na listagem de usuarios.",
                        "Criada tela dedicada para nova senha temporaria.",
                        "Reset usa token do ASP.NET Core Identity.",
                        "Acao passou a ser registrada na trilha de auditoria.",
                        "Mensagem de sucesso exibida apos redefinicao."
                    ],
                    [
                        "Criado `UserResetPasswordViewModel`.",
                        "Criadas acoes GET/POST `ResetPassword` no `UsersController`.",
                        "Adicionado `AuditAction.PasswordReset`.",
                        "Senha temporaria nao e registrada em logs ou auditoria."
                    ]),
                new(
                    "0013",
                    "Status operacional e PostgreSQL health check",
                    "30/05/2026",
                    "Readiness passou a validar PostgreSQL e o portal ganhou uma tela de status operacional.",
                    [
                        "Adicionado health check real do PostgreSQL.",
                        "Separado liveness de readiness nos endpoints de health.",
                        "Criada tela autenticada de Status Operacional.",
                        "Conectado link Ver status do dashboard a nova tela.",
                        "Adicionado item Status no menu principal."
                    ],
                    [
                        "Criado `PostgreSqlHealthCheck` usando `NpgsqlConnection`.",
                        "Check `self` marcado com tags `live` e `ready`.",
                        "Check `postgresql` marcado com tags `ready` e `database`.",
                        "Endpoints `/health/live` e `/health/ready` agora usam predicados por tag."
                    ]),
                new(
                    "0012",
                    "Auditoria de login e administracao",
                    "30/05/2026",
                    "Nova trilha de auditoria para rastrear login, logout e alteracoes administrativas relevantes.",
                    [
                        "Registrado login com sucesso.",
                        "Registrada tentativa de login com falha.",
                        "Registrado logout.",
                        "Registradas criacoes e edicoes de sistemas, permissoes, perfis e usuarios.",
                        "Criada tela administrativa de Auditoria."
                    ],
                    [
                        "Adicionada entidade `AuditLog` e enum `AuditAction`.",
                        "Criada tabela `portal_auth.audit_logs` pela migration `AddAuditLogs`.",
                        "Criado `IAuditService` para gravacao centralizada dos eventos.",
                        "Criada permissao `Auditoria.Visualizar` para proteger a consulta."
                    ]),
                new(
                    "0011",
                    "Changelog no portal",
                    "30/05/2026",
                    "Nova tela autenticada para consultar o historico detalhado de entregas do PortalAuth.",
                    [
                        "Adicionado item de menu Changelog no portal.",
                        "Criada view com linha do tempo detalhada das mudancas.",
                        "Centralizado o historico funcional para consulta por usuarios autenticados."
                    ],
                    [
                        "Controller dedicado `ChangelogController` protegido por autenticacao.",
                        "ViewModel proprio para organizar versao, resumo, mudancas e notas tecnicas."
                    ]),
                new(
                    "0010",
                    "Meus Sistemas por perfil",
                    "30/05/2026",
                    "Dashboard passou a exibir apenas sistemas liberados para os perfis do usuario autenticado.",
                    [
                        "Criado vinculo entre perfil e sistema corporativo.",
                        "Perfis agora permitem selecionar sistemas acessiveis.",
                        "Dashboard filtra sistemas pelos perfis do usuario logado.",
                        "Botao Acessar abre a BaseUrl real cadastrada.",
                        "Usuarios sem acesso nao visualizam cards de sistemas."
                    ],
                    [
                        "Adicionada entidade `RoleSystemAccess`.",
                        "Criada tabela `portal_auth.role_system_access`.",
                        "Migration `AddRoleSystemAccess` aplicada no PostgreSQL.",
                        "Seed do perfil Administrador atualizado para receber os sistemas ativos."
                    ]),
                new(
                    "0009",
                    "Dashboard carregando todos os sistemas ativos",
                    "30/05/2026",
                    "Corrigido limite que fazia o dashboard parecer estatico ao exibir apenas parte do catalogo.",
                    [
                        "Removido limite fixo de sistemas exibidos.",
                        "Contador da tela inicial passou a refletir a quantidade real.",
                        "Titulo da secao ajustado para indicar sistemas disponiveis."
                    ],
                    [
                        "Consulta em `DashboardController` deixou de usar `Take(6)`.",
                        "Validada consulta no PostgreSQL com os sistemas cadastrados."
                    ]),
                new(
                    "0008",
                    "Autorizacao granular por permissoes",
                    "30/05/2026",
                    "Administracao passou a usar policies por permissao em vez de depender apenas do papel Administrador.",
                    [
                        "Criadas permissoes para visualizar, criar e editar sistemas.",
                        "Criadas permissoes para gerenciar permissoes, perfis e usuarios.",
                        "Menus e botoes administrativos passaram a respeitar permissoes do usuario."
                    ],
                    [
                        "Criados `PermissionCodes`, `PermissionRequirement`, `PermissionAuthorizationHandler` e `PermissionPolicyProvider`.",
                        "Policies dinamicas consultam `AspNetUserRoles`, `role_permissions` e `permissions`."
                    ]),
                new(
                    "0007",
                    "Modulos administrativos",
                    "30/05/2026",
                    "Criadas telas administrativas para manter sistemas, permissoes, perfis e usuarios.",
                    [
                        "CRUD de sistemas corporativos.",
                        "CRUD de permissoes granulares.",
                        "Cadastro e edicao de perfis com permissoes.",
                        "Administracao de usuarios com vinculo de perfis e bloqueio."
                    ],
                    [
                        "Adicionadas entidades `Permission` e `RolePermission`.",
                        "Criados controllers e views para `Systems`, `Permissions`, `Roles` e `Users`.",
                        "Estilos administrativos adicionados ao `site.css`."
                    ]),
                new(
                    "0006",
                    "Identity e dashboard inicial",
                    "30/05/2026",
                    "Login real com ASP.NET Core Identity e tela inicial protegida apos autenticacao.",
                    [
                        "Configurado Identity com usuario customizado.",
                        "Criado seed do usuario administrador inicial.",
                        "Dashboard protegido por login.",
                        "Logout e pagina de acesso negado implementados."
                    ],
                    [
                        "Criado `ApplicationUser` com nome de exibicao e departamento.",
                        "Identity persistido no PostgreSQL via `PortalAuthDbContext`.",
                        "Cookie authentication configurado no projeto Web."
                    ]),
                new(
                    "0005",
                    "Setup local PostgreSQL",
                    "30/05/2026",
                    "Scripts para preparar o PostgreSQL local e aplicar migrations do projeto.",
                    [
                        "Criado script de setup do banco local.",
                        "Criado script para redefinir senha local do usuario postgres.",
                        "Connection string salva via User Secrets."
                    ],
                    [
                        "Script `Setup-LocalPostgres.ps1` cria banco e aplica migrations.",
                        "Script `Reset-LocalPostgresPassword.ps1` auxilia recuperacao de senha local."
                    ]),
                new(
                    "0004",
                    "Login responsivo",
                    "30/05/2026",
                    "Tela de login criada com layout corporativo e comportamento responsivo para desktop e celular.",
                    [
                        "Criada tela visual de login baseada na referencia enviada.",
                        "Layout adaptado para desktop e mobile.",
                        "Formulario preparado para email, senha e lembrar acesso."
                    ],
                    [
                        "Criado `LoginViewModel`.",
                        "Criada view `Views/Account/Login.cshtml`.",
                        "Adicionados estilos `login-page` ao `site.css`."
                    ]),
                new(
                    "0003",
                    "EF Core e migrations",
                    "30/05/2026",
                    "Persistencia inicial com Entity Framework Core e PostgreSQL.",
                    [
                        "Configurado `PortalAuthDbContext`.",
                        "Criada migration inicial.",
                        "Definido schema `portal_auth`."
                    ],
                    [
                        "Adicionado provider `Npgsql.EntityFrameworkCore.PostgreSQL`.",
                        "Configurada factory de design-time para EF Core.",
                        "Ferramenta `dotnet-ef` versionada no repositório."
                    ]),
                new(
                    "0002",
                    "Definicoes de API e PostgreSQL",
                    "30/05/2026",
                    "Definidos padroes obrigatorios para APIs e banco oficial do projeto.",
                    [
                        "Swagger acessivel em desenvolvimento.",
                        "API organizada em Controller, MediatR, Models, Services, Interfaces e Enums.",
                        "PostgreSQL escolhido como banco padrao."
                    ],
                    [
                        "Criado endpoint `GET /api/v1/platform/status`.",
                        "Configurado MediatR na camada Application.",
                        "Documentado padrao de camadas para novas APIs."
                    ]),
                new(
                    "0001",
                    "Fundacao tecnica",
                    "30/05/2026",
                    "Criada base inicial da solucao PortalAuth com projetos, camadas e building blocks.",
                    [
                        "Criada solucao `.slnx`.",
                        "Criados projetos Web, API, Application, Domain e Infrastructure.",
                        "Criados building blocks iniciais de API, Web, Logging e Observability.",
                        "Criada documentacao inicial de roadmap, backlog, arquitetura e seguranca."
                    ],
                    [
                        "Configurados health checks, correlation ID, request logging e security headers.",
                        "Criados testes unitarios iniciais.",
                        "Projeto preparado para evolucao incremental versionada."
                    ])
            ]
        };

        return View(model);
    }
}
