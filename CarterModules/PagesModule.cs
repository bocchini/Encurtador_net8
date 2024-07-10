using Carter;
using Encurtador.Model;
using LiteDB;

namespace Encurtador.CarterModules;

public class PagesModule : CarterModule
{
    private ILiteDatabase _liteDatabase;

    public PagesModule(ILiteDatabase liteDatabase)
    {
        _liteDatabase = liteDatabase;
    }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(pattern: "/{chunck}", (HttpRequest req, HttpResponse resp) =>
        {
            var chunck = req.RouteValues["chunck"];

            var shortUrl = _liteDatabase.GetCollection<ShortUrl>().FindOne(u => u.Chunck == chunck);

            if (shortUrl == null)
            {
                resp.Redirect("/");
                return Task.CompletedTask;
            }

            resp.Redirect(shortUrl.Url);
            return Task.CompletedTask;
        });

        app.MapGet("/", async (HttpRequest req, HttpResponse res) =>
        {
            res.ContentType = "text/html";
            res.StatusCode = 200;
            await res.SendFileAsync("wwwroot/index.html");
        });

    }
}
