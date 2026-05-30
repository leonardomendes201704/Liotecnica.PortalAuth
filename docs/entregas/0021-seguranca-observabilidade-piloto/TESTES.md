# Testes - Entrega 0021

## Automatizados

- Build completo da solucao.
- Suite de testes unitarios existente.

## Validacao Manual Recomendada

1. Configurar `OpenTelemetry:OtlpEndpoint` para um collector OTLP e confirmar traces.
2. Acessar `/Status` e validar alertas operacionais.
3. Rodar API em `http://localhost:5057`.
4. Rodar Web em `http://localhost:5112`.
5. Confirmar card `PortalAuth API Piloto` no dashboard do administrador.
6. Acessar o Swagger da API pelo card do piloto.
7. Revisar `docs/OWASP-CHECKLIST.md` antes de homologacao.
