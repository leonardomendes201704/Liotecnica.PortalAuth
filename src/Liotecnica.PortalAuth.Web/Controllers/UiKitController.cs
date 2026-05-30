using Liotecnica.PortalAuth.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Liotecnica.PortalAuth.Web.Controllers;

[Authorize]
public sealed class UiKitController : Controller
{
    public IActionResult Index()
    {
        ViewData["Breadcrumbs"] = new[]
        {
            new BreadcrumbItemViewModel("Tela Inicial", "Dashboard", "Index"),
            new BreadcrumbItemViewModel("UI Kit", IsCurrent: true)
        };

        return View();
    }
}
