# Entrega 0006 - Identity e Dashboard Inicial

## Objetivo

Implementar login real com ASP.NET Core Identity e redirecionar o usuario autenticado para uma tela inicial baseada na referencia visual enviada.

## Escopo Entregue

- Adicionado ASP.NET Core Identity com PostgreSQL.
- `PortalAuthDbContext` passou a herdar de `IdentityDbContext`.
- Criado `ApplicationUser`.
- Configurado cookie authentication no Web.
- `AccountController` conectado ao `SignInManager`.
- Criado logout autenticado.
- Criado seed de usuario administrador inicial em desenvolvimento.
- Criada migration `AddIdentityAuthentication`.
- Migration aplicada no PostgreSQL local.
- Criada tela `Dashboard/Index` protegida por `[Authorize]`.
- Dashboard responsivo com sidebar, topbar, cards de resumo, sistemas principais e avisos.

## Usuario Inicial

E-mail:

```text
admin@liotecnica.com.br
```

Senha:

```text
Configurada localmente em User Secrets na chave Seed:AdminPassword.
```

## Observacao Operacional

Foi detectado um processo antigo `Liotecnica.PortalAuth.Web.exe` em execucao bloqueando os binarios de build. Para testar a nova versao pelo `dotnet run`, encerre esse processo ou feche a janela/terminal que iniciou o Web anteriormente.

## Fora do Escopo

- Cadastro de usuarios via tela.
- Recuperacao de senha real.
- SSO real.
- Permissoes granulares por sistema.
- Auditoria de login/logout.

## Como Validar

1. Encerrar qualquer processo antigo `Liotecnica.PortalAuth.Web.exe`.
2. Executar:

```powershell
dotnet run --project .\src\Liotecnica.PortalAuth.Web\Liotecnica.PortalAuth.Web.csproj
```

3. Acessar `/Account/Login`.
4. Entrar com o usuario inicial.
5. Confirmar redirecionamento para `/Dashboard/Index`.
