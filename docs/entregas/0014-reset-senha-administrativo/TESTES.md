# Testes - Entrega 0014

## Cenarios Funcionais

- Usuario com `Usuario.Gerenciar` acessa a listagem de usuarios.
- Botao `Redefinir senha` abre o formulario do usuario correto.
- Senha e confirmacao divergentes devem exibir erro de validacao.
- Senha fora da politica do Identity deve exibir erros do Identity.
- Senha valida deve redefinir a senha do usuario.
- Apos redefinicao, deve retornar para a listagem com mensagem de sucesso.
- Evento `PasswordReset` deve ser registrado na auditoria.
- A senha temporaria nao deve aparecer em auditoria ou logs.

## Validacoes Automatizadas

- Build do projeto Web.
- Testes unitarios existentes.
- Leitura de linter nos arquivos alterados.
