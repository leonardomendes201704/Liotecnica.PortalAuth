# Decisoes - Entrega 0016

## Filtros no servidor

Os filtros sao aplicados no backend antes da paginacao.

Motivos:

- evita carregar auditoria inteira na memoria;
- prepara o modulo para crescimento do volume de eventos;
- permite manter limite por pagina.

## Limite de pagina

O tamanho de pagina e limitado entre 10 e 100 registros.

Isso evita consultas excessivamente grandes pela interface.

## Busca textual

A busca usa campos operacionais de investigacao:

- detalhes;
- correlation ID;
- identificador da entidade;
- IP.

## Datas inclusivas

A data final inclui o dia inteiro selecionado, aplicando limite menor que o proximo dia.
