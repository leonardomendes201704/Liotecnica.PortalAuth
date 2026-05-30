# UI Kit Razor

O UI Kit do PortalAuth padroniza layout, navegacao e componentes visuais para as telas Razor do portal.

## Tokens CSS

Os tokens ficam em `src/Liotecnica.PortalAuth.Web/wwwroot/css/site.css` no bloco `:root`.

Principais grupos:

- cores: `--color-primary`, `--color-success`, `--color-warning`, `--color-danger`;
- superficies: `--color-surface`, `--color-page`, `--color-border`;
- texto: `--color-text`, `--color-muted`;
- espacamento: `--space-xs` ate `--space-xl`;
- raio e sombra: `--radius-*`, `--shadow-card`.

## Layout

O layout compartilhado e `_PortalLayout.cshtml`.

Partials reutilizaveis:

- `_PortalSidebar.cshtml`: menu lateral com itens condicionais por permissao;
- `_PortalTopbar.cshtml`: busca, avatar, usuario e logout;
- `_Breadcrumb.cshtml`: trilha de navegacao baseada em `BreadcrumbItemViewModel`.

## Componentes CSS

- `.ui-button` e `.ui-button--secondary` para acoes.
- `.ui-card` para agrupamento visual.
- `.ui-badge`, `.ui-badge--success`, `.ui-badge--warning`, `.ui-badge--danger` para status.
- `.ui-grid` para grids responsivos.
- `.ui-breadcrumb` para contexto de navegacao.
- `.admin-table` e `.admin-form` seguem como padroes de tabela e formulario.

## Tela viva

A tela `/UiKit` exibe exemplos reais dos tokens e componentes. Ela deve ser atualizada sempre que novos componentes forem adicionados.

## Regra de uso

Novas telas administrativas devem preferir:

1. `Layout = "_PortalLayout"`;
2. `admin-header` para titulo e acao principal;
3. `ui-card` ou `admin-card` para conteudo agrupado;
4. `admin-table` para listagens;
5. `admin-form` para formularios;
6. `ui-badge` para status.
