# Decisoes - Entrega 0005

## Usar PostgreSQL Existente

Foi detectado PostgreSQL 17 instalado localmente. A decisao foi usar essa instalacao em vez de criar um container Docker.

## Nao Versionar Senha

O servidor local exige senha para o usuario `postgres`. A senha nao foi solicitada nem versionada.

O projeto usa User Secrets para armazenar a connection string local completa.

## Script De Setup

Foi criado `scripts/Setup-LocalPostgres.ps1` para automatizar:

- criacao do banco;
- configuracao de User Secrets;
- aplicacao das migrations.

## Usuario Dedicado Fica Para Depois

Por enquanto o setup usa o usuario `postgres` porque ele ja existe na instalacao local. O backlog registra a criacao futura de um usuario dedicado do projeto.
