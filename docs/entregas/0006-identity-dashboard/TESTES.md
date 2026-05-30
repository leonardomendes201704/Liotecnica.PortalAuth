# Testes - Entrega 0006

## Validacao Executada

- Migration `AddIdentityAuthentication` gerada.
- Migration aplicada no PostgreSQL local.
- Tabelas Identity confirmadas no schema `portal_auth`.

## Bloqueio Encontrado

Existe um processo antigo `Liotecnica.PortalAuth.Web.exe` em execucao com permissao que impede o encerramento pelo terminal atual. Esse processo bloqueia a sobrescrita dos binarios do Web durante o build.

Antes de validar pelo browser, encerrar esse processo pela janela/terminal que o iniciou ou pelo Gerenciador de Tarefas.

## Validacao Manual Recomendada

```powershell
dotnet run --project .\src\Liotecnica.PortalAuth.Web\Liotecnica.PortalAuth.Web.csproj
```

Fluxo:

1. Acessar `/Account/Login`.
2. Entrar com `admin@liotecnica.com.br`.
3. Confirmar redirecionamento para `/Dashboard/Index`.
4. Confirmar dashboard semelhante ao print de referencia.
5. Testar logout.

## Resultado

Executado com sucesso:

```powershell
dotnet build .\src\Liotecnica.PortalAuth.Web\Liotecnica.PortalAuth.Web.csproj -o %TEMP%\PortalAuthWebBuild
dotnet build .\tests\Liotecnica.PortalAuth.UnitTests\Liotecnica.PortalAuth.UnitTests.csproj
dotnet test .\tests\Liotecnica.PortalAuth.UnitTests\Liotecnica.PortalAuth.UnitTests.csproj --no-build
```

Resultado:

- Web compilado em pasta temporaria para contornar binarios bloqueados.
- 0 warnings.
- 0 erros.
- 5 testes aprovados.
- Tabelas Identity confirmadas em `portal_auth`.

Pendente apenas a validacao manual no browser apos encerrar o processo antigo `Liotecnica.PortalAuth.Web.exe`.
