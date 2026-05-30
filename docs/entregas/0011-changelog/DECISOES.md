# Decisoes - Entrega 0011

## Changelog autenticado

O Changelog foi protegido com `[Authorize]`, ficando disponivel para usuarios autenticados do portal.

## Historico inicial estatico

Nesta entrega, o historico foi implementado como dados estruturados no `ChangelogController`.

Motivos:

- evita dependencias adicionais neste momento;
- permite entregar rapidamente uma view navegavel;
- mantem o conteudo organizado em um ViewModel;
- combina com o estado atual do MVP.

## Markdown complementar

Tambem foi criado `docs/CHANGELOG.md` para manter o historico versionado fora da aplicacao.

Em uma etapa futura, a view pode ser conectada a esse arquivo, a uma tabela de banco ou a artefatos de release.
