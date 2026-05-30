# Testes - Entrega 0013

## Validacoes Funcionais

- `/health/live` deve responder quando a aplicacao estiver viva.
- `/health/ready` deve responder `Healthy` quando PostgreSQL estiver acessivel.
- `/health/ready` deve falhar quando PostgreSQL estiver indisponivel.
- `/Status` deve listar os checks `self` e `postgresql`.
- Link `Ver status` no dashboard deve abrir `/Status`.
- Menu `Status` deve aparecer para usuarios autenticados.

## Validacoes Automatizadas

- Build do projeto Web.
- Build do projeto API.
- Testes unitarios existentes.
- Leitura de linter nos arquivos alterados.
