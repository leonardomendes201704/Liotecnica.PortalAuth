# Testes - Entrega 0004

## Testes Automatizados

Esta entrega nao adicionou testes automatizados especificos de UI.

## Validacao Manual Recomendada

Desktop:

- Abrir `/Account/Login`.
- Confirmar duas colunas: painel institucional e card de login.
- Submeter vazio e conferir mensagens de validacao.

Mobile:

- Reduzir viewport para largura de celular.
- Confirmar que o conteudo empilha verticalmente.
- Confirmar que campos e botoes permanecem legiveis e clicaveis.

## Resultado

Executado com sucesso:

```powershell
dotnet build .\Liotecnica.PortalAuth.slnx
dotnet test .\Liotecnica.PortalAuth.slnx --no-build
```

Resultado:

- Build aprovado.
- 0 warnings.
- 0 erros.
- 5 testes aprovados.
