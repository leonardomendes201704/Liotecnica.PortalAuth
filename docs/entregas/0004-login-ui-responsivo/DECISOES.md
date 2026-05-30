# Decisoes - Entrega 0004

## Referencia Visual

A imagem enviada foi usada como referencia de composicao e linguagem visual:

- painel institucional a esquerda no desktop;
- card de login a direita;
- paleta azul/branco;
- textos de seguranca, governanca e produtividade;
- formulario limpo e corporativo.

A implementacao nao copia a marca da referencia. Usa identidade provisoria `LiotecnicaOne`.

## Mobile First Responsivo

Em telas menores, o layout deixa de ser duas colunas e empilha conteudo + formulario. O card ocupa a largura disponivel com margens reduzidas.

## Layout Full Screen

O layout compartilhado agora aceita `ViewData["FullBleed"] = true` para esconder navbar/footer do template padrao em paginas como login.

## Autenticacao Ainda Mockada

O POST do login valida o formulario, mas ainda nao autentica. Ele sera conectado ao Identity na proxima etapa.
