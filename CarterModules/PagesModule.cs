using Carter;

namespace Encurtador.CarterModules;

public class PagesModule : ICarterModule
{

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        Get(app);
    }

    private static void Get(IEndpointRouteBuilder app) => 
        app.MapGet("/", async (req, res) =>
        {
            res.ContentType = "text/html";
            res.StatusCode = 200;
            await res.SendFileAsync("wwwroot/index.html");
        });
}
