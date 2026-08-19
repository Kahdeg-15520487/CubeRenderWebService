using CubeRender.Api.Render;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(o =>
    o.AddPolicy("any", p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

var app = builder.Build();

app.UseCors("any");

// health probe used by k8s readiness/liveness
app.MapGet("/api/health", () => Results.Ok(new { status = "ok", ts = DateTimeOffset.UtcNow }));

// GET /api/render/{x}/{y}/{z} -> 2D ASCII wireframe of a cube
app.MapGet("/api/render/{x:int}/{y:int}/{z:int}", (int x, int y, int z) =>
    Results.Ok(Renderer.Render(x, y, z)));

app.Run();
