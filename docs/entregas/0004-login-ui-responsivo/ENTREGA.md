# Entrega 0004 - Login UI Responsivo

## Objetivo

Criar a primeira tela de login do PortalAuth usando a referencia visual enviada como inspiracao: visual corporativo, azul/branco, split screen no desktop e experiencia mobile friendly.

## Escopo Entregue

- Criado `AccountController`.
- Criado `LoginViewModel` com validacoes iniciais.
- Criada view `Views/Account/Login.cshtml`.
- Ajustado layout compartilhado para permitir paginas full screen sem navbar/footer.
- Criado CSS responsivo para desktop, tablet e celular.
- Rota padrao do Web alterada para `Account/Login`.
- `Home/Index` redireciona para o login.
- Formulario com anti-forgery token.
- Campos de e-mail, senha, lembrar-me, esqueci senha, entrar e entrar com SSO.

## Fora do Escopo

- Autenticacao real com Identity.
- Recuperacao de senha.
- SSO real.
- Logo oficial final da Liotecnica.
- Teste visual automatizado.

## Como Validar

```powershell
dotnet run --project .\src\Liotecnica.PortalAuth.Web\Liotecnica.PortalAuth.Web.csproj
```

Validar:

- `/Account/Login` abre a tela.
- A raiz do Web abre o login.
- Layout desktop exibe painel institucional + card de login.
- Layout mobile empilha conteudo e mantem formulario usavel.
- Validacoes aparecem ao submeter vazio.

## Proximo Passo

Conectar o POST de login ao ASP.NET Core Identity quando a camada de autenticacao for implementada.
