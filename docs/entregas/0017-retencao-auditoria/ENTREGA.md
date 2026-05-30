# Entrega 0017 - Retencao de Auditoria

## Objetivo

Implementar politica de retencao dos logs de auditoria com configuracao persistida em banco, conforme diretriz de manter configuracoes no PostgreSQL.

## Escopo Implementado

- Criada entidade `AppSetting`.
- Criada tabela `portal_auth.app_settings`.
- Criada configuracao `Audit.RetentionDays`.
- Valor padrao local persistido: `180`.
- Criado `IAuditRetentionService`.
- Criado `AuditRetentionService`.
- Tela `/Audit` passou a exibir configuracao de retencao.
- Tela `/Audit` permite atualizar os dias de retencao.
- Tela `/Audit` permite executar limpeza manual de logs expirados.
- Atualizacao da politica registra evento `RetentionUpdated`.
- Limpeza registra evento `AuditPurged`.

## Resultado

A auditoria passa a ter governanca de retencao configuravel e auditavel, sem depender de arquivos de configuracao para esta regra operacional.
