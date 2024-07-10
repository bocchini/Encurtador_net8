using Carter;
using LiteDB;
using System.Net;
using CarterRequest = Carter.Request;
using Encurtador.Model;

namespace Encurtador.CarterModules;

public class UrlModule : CarterModule
{
    private readonly ILiteDatabase _liteDatabase;
    public UrlModule(ILiteDatabase liteDatabase) : base("/urls")
    {
        _liteDatabase = liteDatabase;
    }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/", async (HttpRequest req, HttpResponse resp) =>
        {
            var shortUrl = await req.ReadFromJsonAsync<ShortUrl>();
            if (Uri.TryCreate(shortUrl.Url, UriKind.Absolute, out var uriParsed))
            {
                _liteDatabase.GetCollection<ShortUrl>(BsonAutoId.Guid).Insert(shortUrl);

                resp.StatusCode = (int)HttpStatusCode.OK;
                var rawShortUrl = $"{req.Scheme}://{req.Host}/{shortUrl.Chunck}";
                await resp.WriteAsJsonAsync(new { ShortUrl = rawShortUrl });

            }
            else
            {
                resp.StatusCode = (int)HttpStatusCode.BadRequest;
                await resp.WriteAsJsonAsync(new { ErrorMessage = "Invalid Url" });
            }
        });       
    }

}
