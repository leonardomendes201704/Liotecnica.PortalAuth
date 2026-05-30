# Backlog

Backlog vivo do PortalAuth. Itens podem ser promovidos para o roadmap conforme prioridade e maturidade.

## Proximos Itens

1. Avaliar criacao de usuario PostgreSQL dedicado ao projeto, evitando uso de `postgres` no desenvolvimento.
2. Criar troca obrigatoria de senha apos reset administrativo.
3. Criar desativacao logica de perfis com role customizada.
4. Criar automacao recorrente da limpeza de auditoria.
5. Criar testes automatizados de acesso negado por permissao.
6. Criar testes automatizados para "Meus Sistemas" com perfis diferentes.
7. Criar exportacao CSV de usuarios e sistemas.
8. Expandir status operacional com historico e alertas.

## Melhorias Tecnicas

- Adicionar FluentValidation.
- Adicionar testes de arquitetura para dependencias entre camadas.
- Adicionar rate limiting.
- Adicionar redaction para logs.
- Adicionar OpenTelemetry.
- Padronizar versionamento de APIs.
- Criar pipeline CI/CD.
- Criar template de feature com Controller, Command/Query, Handler, Model, Service, Interface e Enum quando aplicavel.

## UI/UX

- Criar layout corporativo inicial.
- Criar tokens CSS da Liotecnica.
- Criar componentes Razor reutilizaveis.
- Criar estados de loading, vazio e erro.
- Criar variacao mobile validada da tela de login.

## Documentacao

- Manter `docs/CHANGELOG.md` atualizado a cada entrega relevante.
- Expandir `docs/ARCHITECTURE.md` conforme os modulos forem implementados.
- Criar `docs/API_CONTRACT.md` quando a API possuir endpoints de dominio.
- Expandir `docs/DATABASE.md` quando novas tabelas e migrations forem criadas.
- Criar `docs/DEPLOYMENT.md` quando houver pipeline ou ambiente definido.
