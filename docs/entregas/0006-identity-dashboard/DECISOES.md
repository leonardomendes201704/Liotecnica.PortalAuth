# Decisoes - Entrega 0006

## Identity Como Base De Autenticacao

O login real usa ASP.NET Core Identity com EF Core/PostgreSQL para evitar autenticacao customizada neste momento.

## Dashboard Protegido

A tela inicial fica em `DashboardController` com `[Authorize]`, garantindo que apenas usuarios autenticados acessem.

## Seed Apenas Em Desenvolvimento

O usuario administrador inicial e criado apenas em ambiente de desenvolvimento, mediante senha configurada em User Secrets.

## Referencia Visual

O dashboard segue a composicao do print enviado:

- menu lateral;
- barra superior com busca e usuario;
- cards de resumo;
- cards de sistemas;
- area de avisos;
- layout responsivo para telas menores.
