# Testes - Entrega 0015

## Cenarios Funcionais

- Excluir sistema:
  - deve marcar `IsDeleted` e inativar o sistema;
  - dashboard nao deve listar o sistema;
  - auditoria deve registrar `Deleted`.

- Excluir permissao:
  - deve marcar `IsDeleted` e inativar a permissao;
  - seletores de perfil nao devem listar a permissao;
  - auditoria deve registrar `Deleted`.

- Bloquear usuario:
  - deve definir `LockoutEnd`;
  - listagem deve mostrar `Bloqueado`;
  - auditoria deve registrar `Deactivated`.

- Reativar usuario:
  - deve limpar `LockoutEnd`;
  - listagem deve mostrar `Ativo`;
  - auditoria deve registrar `Reactivated`.

- Remover perfil:
  - perfil com usuarios vinculados deve ser bloqueado;
  - perfil sem usuarios vinculados deve ser removido;
  - auditoria deve registrar `Deleted`.

## Validacoes Automatizadas

- Build do projeto Web.
- Testes unitarios existentes.
- Leitura de linter nos arquivos alterados.
