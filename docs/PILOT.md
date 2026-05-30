# Sistema Piloto

A Fase 5 valida o PortalAuth com um sistema piloto registrado no proprio portal.

## Sistema escolhido

`PortalAuth API Piloto`

Motivo:

- ja existe no workspace;
- possui Swagger e health checks;
- valida cadastro de sistema corporativo;
- valida acesso pelo dashboard;
- permite observar comportamento de autenticacao, autorizacao, auditoria e status sem depender de outro time.

## Cadastro

O seed de desenvolvimento registra o sistema:

- Nome: `PortalAuth API Piloto`
- Codigo: `PORTALAUTH_API`
- URL: `http://localhost:5057/swagger`
- Icone: `PI`
- MFA: nao obrigatorio no piloto

O perfil `Administrador` recebe acesso automaticamente ao sistema piloto.

## Validacao

1. Rodar a API em `http://localhost:5057`.
2. Rodar o Web em `http://localhost:5112`.
3. Entrar com o usuario administrador.
4. Confirmar card `PortalAuth API Piloto` em `Meus Sistemas`.
5. Acessar o sistema pelo botao `Acessar`.
6. Confirmar eventos de login/acesso administrativo em auditoria.
7. Confirmar `/Status` sem alertas criticos quando API/Web/PostgreSQL estiverem saudaveis.

## Proximos pilotos reais

Depois da validacao local, o proximo candidato deve ser um sistema interno com baixo risco operacional e dono definido.
