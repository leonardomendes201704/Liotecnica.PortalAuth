# Testes - Entrega 0017

## Validacoes Executadas

- Migration `AddAppSettingsAndAuditRetention` gerada.
- Migration aplicada no PostgreSQL local.
- Configuracao `Audit.RetentionDays` inserida em `portal_auth.app_settings`.
- Query SQL confirmou valor `180`.

## Cenarios Funcionais

- Abrir `/Audit` e visualizar a configuracao de retencao.
- Alterar dias de retencao.
- Confirmar persistencia em `portal_auth.app_settings`.
- Confirmar evento `RetentionUpdated` na auditoria.
- Executar limpeza manual.
- Confirmar evento `AuditPurged` na auditoria.
- Confirmar que logs mais novos que a data de corte permanecem.

## Validacoes Automatizadas

- Build do projeto Web.
- Testes unitarios existentes.
- Leitura de linter nos arquivos alterados.
