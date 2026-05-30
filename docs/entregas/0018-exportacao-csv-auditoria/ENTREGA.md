# Entrega 0018 - Exportacao CSV da Auditoria

## Objetivo

Permitir a extracao dos eventos de auditoria para analise externa, respeitando os filtros aplicados na tela.

## Escopo Implementado

- Adicionado botao `Exportar CSV` na tela `/Audit`.
- Exportacao reutiliza filtros de periodo, usuario, acao, entidade e busca.
- Exportacao limitada a 10.000 registros.
- Arquivo gerado em CSV com separador `;`.
- Arquivo gerado com BOM UTF-8.
- Campos exportados:
  - data/hora UTC;
  - acao;
  - entidade;
  - identificador da entidade;
  - usuario;
  - IP;
  - correlation ID;
  - detalhes.
- Exportacao registra evento `AuditExported`.

## Resultado

Usuarios autorizados podem extrair a trilha de auditoria filtrada para suporte, analise de seguranca e governanca, mantendo rastreabilidade da propria exportacao.
