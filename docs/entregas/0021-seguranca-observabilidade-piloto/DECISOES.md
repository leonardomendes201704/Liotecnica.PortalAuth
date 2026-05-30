# Decisoes - Entrega 0021

## OpenTelemetry opcional

O exportador OTLP so e ativado quando `OpenTelemetry:OtlpEndpoint` estiver preenchido. Isso evita dependencia de collector local para desenvolvimento.

## Alertas derivados

Os alertas operacionais sao calculados pela tela de Status a partir do health check atual e do historico recente. A decisao evita criar infraestrutura de jobs ou notificacoes antes de haver ambiente produtivo.

## Piloto local

O primeiro piloto escolhido foi a propria API do PortalAuth. Ela permite validar o fluxo de sistema corporativo sem depender de outro produto interno.

## OWASP como checklist vivo

O checklist OWASP foi documentado como baseline rastreavel, mantendo itens futuros explicitamente pendentes.
