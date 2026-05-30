# Decisoes - Entrega 0015

## Sistemas e permissoes

Sistemas e permissoes usam exclusao logica com `IsDeleted`.

Ao excluir logicamente, o registro tambem e marcado como inativo para nao aparecer em seletores e fluxos operacionais.

## Usuarios

Usuarios nao sao removidos do Identity.

Foi usado `LockoutEnd` para bloquear/desativar e `null` para reativar. Isso preserva historico, vinculos e auditoria.

## Perfis

Perfis usam `IdentityRole<Guid>`, que ainda nao possui campos de ciclo de vida como `IsActive` ou `IsDeleted`.

Por isso, a regra adotada foi:

- perfil com usuarios vinculados nao pode ser removido;
- perfil sem usuarios vinculados pode ser removido, com limpeza previa de permissoes e sistemas vinculados.

Uma evolucao futura pode trocar `IdentityRole<Guid>` por uma role customizada com desativacao logica.

## Auditoria

As operacoes usam `AuditAction.Deactivated`, `AuditAction.Reactivated` e `AuditAction.Deleted`.

Todas as acoes sao executadas por POST com antiforgery.
