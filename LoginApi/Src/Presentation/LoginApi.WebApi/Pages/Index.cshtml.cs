using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Routing;

namespace HyBrForex.WebApi.Pages;

public class IndexModel(EndpointDataSource endpointDataSource) : PageModel
{
    public List<string> Routes { get; set; } = new();

    public void OnGet()
    {
        Routes = endpointDataSource.Endpoints
            .OfType<RouteEndpoint>()
            .Select(e => e.RoutePattern.RawText)
            .ToList();
    }
}