using Liotecnica.PortalAuth.Domain.Entities;
using Liotecnica.PortalAuth.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Liotecnica.PortalAuth.Infrastructure.Identity;

public static class IdentitySeeder
{
    public const string AdminEmail = "admin@liotecnica.com.br";
    public const string AdminRole = "Administrador";

    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();

        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var dbContext = scope.ServiceProvider.GetRequiredService<PortalAuthDbContext>();
        var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
        var adminPassword = configuration["Seed:AdminPassword"];

        if (string.IsNullOrWhiteSpace(adminPassword))
        {
            throw new InvalidOperationException("Seed:AdminPassword nao configurado em User Secrets ou variavel de ambiente.");
        }

        var adminRole = await roleManager.FindByNameAsync(AdminRole);

        if (adminRole is null)
        {
            adminRole = new ApplicationRole(AdminRole);
            await roleManager.CreateAsync(adminRole);
        }

        var admin = await userManager.FindByEmailAsync(AdminEmail);

        if (admin is null)
        {
            admin = new ApplicationUser
            {
                UserName = AdminEmail,
                Email = AdminEmail,
                EmailConfirmed = true,
                DisplayName = "Carlos Almeida",
                Department = "Gerente de Operacoes"
            };

            var result = await userManager.CreateAsync(admin, adminPassword);

            if (!result.Succeeded)
            {
                var errors = string.Join("; ", result.Errors.Select(error => error.Description));
                throw new InvalidOperationException($"Falha ao criar usuario administrador inicial: {errors}");
            }
        }

        if (!await userManager.IsInRoleAsync(admin, AdminRole))
        {
            await userManager.AddToRoleAsync(admin, AdminRole);
        }

        await SeedCorporateSystemsAsync(dbContext);
        await SeedPermissionsAsync(dbContext, adminRole.Id);
    }

    private static async Task SeedCorporateSystemsAsync(PortalAuthDbContext dbContext)
    {
        var defaults = new[]
        {
            new CorporateSystem("RH", "RH", "Gestao de pessoas e folha de pagamento", "https://rh.local", "RH", false),
            new CorporateSystem("Financeiro", "FINANCEIRO", "Contas a pagar/receber, conciliacao e relatorios", "https://financeiro.local", "FI", true),
            new CorporateSystem("Compras", "COMPRAS", "Solicitacoes, cotacoes e contratos", "https://compras.local", "CP", true),
            new CorporateSystem("CRM", "CRM", "Gestao de clientes e oportunidades", "https://crm.local", "CR", false),
            new CorporateSystem("Documentos", "DOCUMENTOS", "Repositorio corporativo e arquivos", "https://documentos.local", "DC", false),
            new CorporateSystem("Aprovacoes", "APROVACOES", "Fluxos, tarefas e autorizacoes", "https://aprovacoes.local", "AP", true),
            new CorporateSystem("PortalAuth API Piloto", "PORTALAUTH_API", "Sistema piloto para validar SSO, dashboard, permissoes e observabilidade.", "http://localhost:5057/swagger", "PI", false)
        };

        foreach (var system in defaults)
        {
            if (!await dbContext.CorporateSystems.AnyAsync(existing => existing.Code == system.Code))
            {
                dbContext.CorporateSystems.Add(system);
            }
        }

        await dbContext.SaveChangesAsync();
    }

    private static async Task SeedPermissionsAsync(PortalAuthDbContext dbContext, Guid adminRoleId)
    {
        var defaults = new[]
        {
            new Permission("Sistema.Visualizar", "Visualizar sistemas corporativos", "Sistemas", false),
            new Permission("Sistema.Criar", "Criar sistemas corporativos", "Sistemas", true),
            new Permission("Sistema.Editar", "Editar sistemas corporativos", "Sistemas", true),
            new Permission("Permissao.Gerenciar", "Gerenciar permissoes", "Seguranca", true),
            new Permission("Perfil.Gerenciar", "Gerenciar perfis", "Seguranca", true),
            new Permission("Usuario.Gerenciar", "Gerenciar usuarios", "Seguranca", true),
            new Permission("Auditoria.Visualizar", "Visualizar trilha de auditoria", "Seguranca", true)
        };

        foreach (var permission in defaults)
        {
            if (!await dbContext.Permissions.AnyAsync(existing => existing.Code == permission.Code))
            {
                dbContext.Permissions.Add(permission);
            }
        }

        await dbContext.SaveChangesAsync();

        var permissionIds = await dbContext.Permissions
            .Where(permission => permission.IsActive && !permission.IsDeleted)
            .Select(permission => permission.Id)
            .ToListAsync();

        var existingPermissionIds = await dbContext.RolePermissions
            .Where(rolePermission => rolePermission.RoleId == adminRoleId)
            .Select(rolePermission => rolePermission.PermissionId)
            .ToListAsync();

        foreach (var permissionId in permissionIds.Except(existingPermissionIds))
        {
            dbContext.RolePermissions.Add(new RolePermission(adminRoleId, permissionId));
        }

        await dbContext.SaveChangesAsync();

        var systemIds = await dbContext.CorporateSystems
            .Where(system => system.IsActive && !system.IsDeleted)
            .Select(system => system.Id)
            .ToListAsync();

        var existingSystemIds = await dbContext.RoleSystemAccesses
            .Where(access => access.RoleId == adminRoleId)
            .Select(access => access.SystemId)
            .ToListAsync();

        foreach (var systemId in systemIds.Except(existingSystemIds))
        {
            dbContext.RoleSystemAccesses.Add(new RoleSystemAccess(adminRoleId, systemId));
        }

        await dbContext.SaveChangesAsync();
    }
}
