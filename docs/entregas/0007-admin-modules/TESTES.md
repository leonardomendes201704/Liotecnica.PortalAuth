# Testes - Entrega 0007

## Validacao Automatizada

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

## Validacao De Banco

Validar contagens:

```sql
select count(*) from portal_auth.corporate_systems;
select count(*) from portal_auth.permissions;
select count(*) from portal_auth.role_permissions;
```

Resultado:

- `corporate_systems`: 6 registros.
- `permissions`: 6 registros.
- `role_permissions`: 6 registros.

## Correcao Posterior

Foi identificado que o dashboard carregava sistemas ativos do banco, mas limitava a exibicao aos 6 primeiros por causa de `.Take(6)` na consulta.

Correcao aplicada:

- removido limite fixo de 6 sistemas;
- titulo alterado para "Sistemas disponiveis";
- contador do status usa a quantidade real de sistemas ativos.

Validacao:

- Banco possui 7 sistemas ativos apos cadastro manual de novo sistema.
- Dashboard passa a carregar todos os sistemas ativos.

## Validacao Manual Recomendada

- Login com administrador.
- Acessar `/Systems`, `/Permissions`, `/Roles` e `/Users`.
- Criar registros de teste.
- Editar registros existentes.
- Confirmar que o dashboard lista sistemas do banco.
