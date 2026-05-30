# Decisoes - Entrega 0017

## Configuracoes no banco

Foi criada a tabela `portal_auth.app_settings` para persistir configuracoes operacionais.

Motivo: a diretriz do projeto e manter configuracoes persistidas no banco quando forem administraveis pelo sistema.

## Chave de retencao

A politica usa a chave:

```text
Audit.RetentionDays
```

O valor representa a quantidade de dias de logs de auditoria que devem ser mantidos.

## Limites

O servico limita a retencao entre 30 e 3650 dias.

Isso evita configuracoes acidentais muito agressivas ou sem limite pratico.

## Limpeza manual

A primeira versao executa limpeza manual pela tela de Auditoria.

Uma automacao recorrente pode ser criada futuramente por hosted service, job externo ou scheduler corporativo.

## Auditoria da auditoria

Alteracoes de retencao e execucoes de limpeza tambem geram eventos de auditoria.
