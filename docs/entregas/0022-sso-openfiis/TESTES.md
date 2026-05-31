# Testes - Entrega 0022

## Executados

```powershell
dotnet build .\Liotecnica.PortalAuth.slnx -p:UseSharedCompilation=false
```

Resultado: sucesso.

```powershell
npm run build
```

Diretorio: `D:\Leonardo\OpenFIIs`

Resultado: sucesso.

## Observacoes

- O primeiro build do PortalAuth falhou porque `Liotecnica.PortalAuth.Api` estava executando e bloqueando DLLs. O processo foi encerrado e o build passou.
- O primeiro build do OpenFIIs apontou um padrao suspeito no check de encoding em `/login`; a linha foi ajustada e o build passou.

## Validacao manual recomendada

- Iniciar PortalAuth Web.
- Iniciar OpenFIIs em `http://localhost:3001`.
- Entrar no PortalAuth com usuario administrador.
- Acessar o card OpenFIIs.
- Confirmar que OpenFIIs abre sem tela local de login.
- Acessar `http://localhost:3001/login?next=%2F` sem sessao e confirmar redirecionamento para o PortalAuth.
- Validar que carteira, onboarding e dashboard continuam carregando dados pelo usuario autenticado.
