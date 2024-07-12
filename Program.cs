using Carter;
using LiteDB;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCarter();
builder.Services.AddSingleton<ILiteDatabase, LiteDatabase>(x => 
        new LiteDatabase(builder.Configuration.GetConnectionString("Default"))
        );

var app = builder.Build();

app.UseStaticFiles();
app.MapCarter();

app.Run();
