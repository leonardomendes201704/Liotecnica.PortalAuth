# Entrega 0008 - Autorizacao Granular Por Permissao

## Objetivo

Ativar autorizacao real por permissao, deixando de depender apenas da role `Administrador` nas telas administrativas.

## Escopo Entregue

- Criado `PermissionCodes`.
- Criado `PermissionRequirement`.
- Criado `PermissionAuthorizationHandler`.
- Criado `PermissionPolicyProvider` para policies dinamicas.
- Configurado handler/provider no `Program.cs`.
- Controllers administrativos protegidos por policies de permissao.
- Menus do portal renderizados conforme permissao do usuario.
- Botao "Novo sistema" e link "Editar" renderizados conforme permissao.
- Tela de acesso negado.

## Policies Aplicadas

- `Sistema.Visualizar`
- `Sistema.Criar`
- `Sistema.Editar`
- `Permissao.Gerenciar`
- `Perfil.Gerenciar`
- `Usuario.Gerenciar`

## Fora do Escopo

- Claims persistidas no cookie.
- Cache de permissoes.
- Testes automatizados de autorizacao.
- Auditoria de acesso negado.

## Como Validar

1. Login como administrador.
2. Confirmar acesso aos menus administrativos.
3. Criar um perfil com permissao parcial.
4. Criar usuario vinculado a esse perfil.
5. Login com o novo usuario.
6. Confirmar que menus e rotas aparecem conforme permissao.
7. Confirmar que rota sem permissao redireciona para acesso negado.
