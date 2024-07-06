using Carter;

namespace Encurtador.CarterModules;

public class PagesModule : ICarterModule
{

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        Get(app);
    }

    private static void Get(IEndpointRouteBuilder app) => app.MapGet("/", async () => ("wwwroot/index.html"));
}
