# Deploy e Operacao

Este documento registra o caminho minimo para promover o PortalAuth para ambientes controlados.

## Variaveis obrigatorias

- `ConnectionStrings__PortalAuth`: connection string PostgreSQL com usuario dedicado da aplicacao.
- `Seed__AdminPassword`: senha inicial do administrador, usada apenas no seed de desenvolvimento.
- `ASPNETCORE_ENVIRONMENT`: `Development`, `Staging` ou `Production`.

## Banco de dados

- Use um usuario dedicado, como `portalauth_app`, para a aplicacao.
- Reserve o usuario `postgres` ou equivalente apenas para administracao, criacao do banco e manutencao.
- Execute migrations antes da publicacao da aplicacao ou como etapa controlada do pipeline.
- Mantenha rotina de backup do banco antes de cada deploy com migration.

## Pipeline

O workflow `.github/workflows/dotnet-ci.yml` executa restore, build e testes a cada push ou pull request para `master`/`main`.

## Publicacao

1. Restaurar pacotes e tools.
2. Compilar em `Release`.
3. Rodar testes automatizados.
4. Aplicar migrations com credencial operacional segura.
5. Publicar Web e API usando secrets do ambiente.
6. Validar `/health/live`, `/health/ready` e a tela `Status`.

## Backup e rollback

- Faça backup lógico do PostgreSQL antes de migrations destrutivas.
- Guarde o pacote publicado por versão para rollback rápido.
- Rollback de banco deve ser planejado junto com cada migration, evitando alterações irreversíveis sem janela de manutenção.
