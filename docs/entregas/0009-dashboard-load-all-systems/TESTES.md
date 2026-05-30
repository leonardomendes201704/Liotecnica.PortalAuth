# Testes - Entrega 0009

## Validacao De Banco

Executado:

```sql
select name, code, is_active, is_deleted
from portal_auth.corporate_systems
order by created_at, name;
```

Resultado:

- 7 sistemas encontrados.
- Sistema cadastrado manualmente: `Sistema de Clientes` / `LIOCLI`.
- Todos os 7 registros estavam ativos e nao excluidos.

## Validacao Automatizada

Executado:

```powershell
dotnet build .\Liotecnica.PortalAuth.slnx
dotnet test .\Liotecnica.PortalAuth.slnx --no-build
```

Resultado:

- Build aprovado.
- 0 warnings.
- 0 erros.
- 5 testes aprovados.
- Consulta de sistemas ativos retornou 7.
