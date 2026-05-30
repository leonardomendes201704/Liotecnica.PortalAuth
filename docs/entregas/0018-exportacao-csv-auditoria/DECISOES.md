# Decisoes - Entrega 0018

## Reuso dos filtros

A exportacao usa os mesmos filtros da tela de auditoria.

Motivos:

- evita divergencia entre o que o usuario consulta e o que exporta;
- reduz duplicacao conceitual;
- facilita investigacoes focadas.

## Limite de exportacao

A exportacao foi limitada a 10.000 registros.

Isso evita downloads excessivos e consultas muito pesadas pelo fluxo interativo.

## CSV com protecao basica

Campos sao escapados e valores iniciados por `=`, `+`, `-` ou `@` recebem prefixo para reduzir risco de formula injection em planilhas.

## Auditoria da exportacao

Cada exportacao gera evento `AuditExported`, registrando quantidade exportada, limite e resumo dos filtros aplicados.
