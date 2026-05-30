# Testes - Entrega 0008

## Validacao Automatizada

Executado:

```powershell
dotnet build .\Liotecnica.PortalAuth.slnx
dotnet test .\Liotecnica.PortalAuth.slnx --no-build
```

Resultado:

- Build aprovado.
- 0 erros.
- 5 testes aprovados.
- Permissoes confirmadas no banco:
  - `Perfil.Gerenciar`
  - `Permissao.Gerenciar`
  - `Sistema.Criar`
  - `Sistema.Editar`
  - `Sistema.Visualizar`
  - `Usuario.Gerenciar`

Observacao: o build emitiu warnings porque um processo `Liotecnica.PortalAuth.Web.exe` estava em execucao e bloqueou a copia do `apphost.exe`. A DLL foi compilada com sucesso.

## Validacao Manual Recomendada

Administrador:

- Deve ver todos os menus administrativos.
- Deve acessar `/Systems`, `/Permissions`, `/Roles` e `/Users`.

Usuario parcial:

- Criar perfil com apenas `Sistema.Visualizar`.
- Criar usuario vinculado a esse perfil.
- Confirmar que somente menu Sistemas aparece.
- Confirmar que botao "Novo sistema" nao aparece sem `Sistema.Criar`.
- Confirmar que editar sistema nao aparece sem `Sistema.Editar`.
- Confirmar acesso negado ao tentar acessar `/Users`.
