# Entrega 0012 - Auditoria

## Objetivo

Adicionar rastreabilidade ao PortalAuth registrando eventos de autenticacao e alteracoes administrativas relevantes.

## Escopo Implementado

- Criada entidade `AuditLog`.
- Criado enum `AuditAction`.
- Criada tabela `portal_auth.audit_logs`.
- Criado servico `IAuditService`.
- Registrado login com sucesso.
- Registrada tentativa de login com falha.
- Registrado logout.
- Registradas criacoes e edicoes de sistemas corporativos.
- Registradas criacoes e edicoes de permissoes.
- Registradas criacoes e edicoes de perfis.
- Registradas criacoes e edicoes de usuarios.
- Criada permissao `Auditoria.Visualizar`.
- Criada tela administrativa `/Audit`.
- Adicionado item de menu `Auditoria` para usuarios autorizados.

## Dados Registrados

Cada evento registra:

- data/hora UTC;
- acao;
- entidade afetada;
- identificador da entidade;
- usuario;
- IP;
- correlation ID;
- detalhes resumidos da operacao.

## Resultado

O PortalAuth passa a oferecer trilha de auditoria inicial para responder quem realizou cada acao relevante, quando ocorreu, de onde partiu a requisicao e qual entidade foi afetada.
