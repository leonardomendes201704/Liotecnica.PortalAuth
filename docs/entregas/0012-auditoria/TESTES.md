# Testes - Entrega 0012

## Validacoes Executadas

- Migration `AddAuditLogs` gerada com sucesso.
- Migration aplicada no PostgreSQL local.
- Permissao `Auditoria.Visualizar` criada/vinculada ao perfil `Administrador`.
- Verificacao SQL confirmou a permissao vinculada a 1 perfil.

## Cenarios Funcionais

1. Login com sucesso:
   - deve registrar evento `LoginSucceeded`.

2. Login com credenciais invalidas:
   - deve registrar evento `LoginFailed`;
   - nao deve registrar senha.

3. Logout:
   - deve registrar evento `Logout`.

4. Criacao/edicao administrativa:
   - sistemas, permissoes, perfis e usuarios devem registrar eventos `Created` ou `Updated`.

5. Tela de auditoria:
   - usuario com `Auditoria.Visualizar` deve acessar `/Audit`;
   - usuario sem permissao deve receber acesso negado;
   - tela deve listar os 200 eventos mais recentes.

## Validacoes Automatizadas

- Build do projeto Web.
- Testes unitarios existentes.
- Leitura de linter nos arquivos alterados.
