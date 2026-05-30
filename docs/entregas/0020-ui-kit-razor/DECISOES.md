# Decisoes - Entrega 0020

## Partials antes de ViewComponents

Foram usados partials Razor para `Sidebar`, `Topbar` e `Breadcrumb`, pois o projeto ja usa MVC Razor simples e os componentes precisam compartilhar tag helpers, rotas e dados do usuario com baixo overhead.

## Tela viva

Foi criada a tela `/UiKit` como referencia visual interna. Ela complementa a documentacao Markdown e permite validar rapidamente a aparencia no navegador.

## Tokens CSS incrementais

Os tokens foram adicionados sem reescrever toda a folha de estilos existente. Isso reduz risco visual e permite migração gradual das telas antigas para os novos nomes.

## Dashboard

O dashboard ainda usa a estrutura full bleed, mas passou a consumir os partials compartilhados para remover duplicacao de menu e topbar.
