# Testes - Entrega 0010

## Validacoes Executadas

- Migration `AddRoleSystemAccess` gerada com sucesso.
- Migration aplicada no PostgreSQL local.
- Perfil `Administrador` recebeu acesso aos 7 sistemas ativos do banco local.
- Query de verificacao confirmou `Administrador | 7`.

## Cenarios Funcionais

1. Usuario com perfil que possui sistemas vinculados:
   - deve visualizar apenas esses sistemas na tela "Meus Sistemas";
   - o botao "Acessar" deve abrir a `BaseUrl` cadastrada.

2. Usuario com perfil sem sistemas vinculados:
   - nao deve visualizar cards de sistemas;
   - deve visualizar a mensagem de nenhum sistema liberado.

3. Perfil com permissao administrativa, mas sem sistema vinculado:
   - pode acessar menus administrativos conforme permissoes;
   - nao recebe acesso automatico a sistemas no dashboard.

## Observacao Local

Ainda existe um processo antigo `Liotecnica.PortalAuth.Web.exe` bloqueando DLLs no diretório `bin` do projeto Web. Por isso, validacoes de build podem exigir fechar manualmente esse processo ou usar saida de build isolada.
