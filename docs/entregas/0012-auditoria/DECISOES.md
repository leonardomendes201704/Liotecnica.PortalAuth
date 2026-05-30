# Decisoes - Entrega 0012

## Auditoria explicita nos controllers

Nesta entrega, os eventos foram registrados explicitamente nos fluxos de sucesso dos controllers.

Motivos:

- deixa claro quais eventos sao auditados;
- evita gravar ruido de consultas simples;
- permite detalhes especificos por entidade;
- reduz risco de registrar dados sensiveis automaticamente.

## Falhas de login

Tentativas de login com e-mail ou senha invalidos tambem sao auditadas.

O registro grava o e-mail informado como usuario, sem armazenar senha ou qualquer dado sensivel.

## Permissao propria

A consulta de auditoria usa a permissao `Auditoria.Visualizar`, separada das demais permissoes administrativas.

Isso permite que a empresa conceda acesso a auditoria sem entregar, necessariamente, permissao para editar usuarios, perfis ou sistemas.

## Retencao e filtros

A primeira versao da tela exibe os 200 eventos mais recentes.

Filtros avancados, exportacao e politica de retencao ficam para etapa futura.
