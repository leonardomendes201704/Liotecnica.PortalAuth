# Decisoes - Entrega 0014

## Uso do token do Identity

O reset usa `GeneratePasswordResetTokenAsync` e `ResetPasswordAsync`.

Motivos:

- segue o fluxo oficial do ASP.NET Core Identity;
- nao exige conhecer a senha atual;
- invalida o estado de credenciais conforme o comportamento do Identity;
- evita manipular hash de senha diretamente.

## Sem registro da senha

A senha temporaria nao e registrada em auditoria nem logs.

A auditoria registra apenas que houve redefinicao administrativa para determinado usuario.

## Permissao reutilizada

Foi reutilizada a permissao `Usuario.Gerenciar`.

Motivo: redefinir senha faz parte da administracao de usuarios e deve acompanhar o mesmo grupo de acesso do modulo.

## Troca obrigatoria futura

Ainda nao foi implementada troca obrigatoria de senha no proximo login. Isso fica como evolucao futura quando houver fluxo de perfil do usuario ou politica de senha temporaria.
