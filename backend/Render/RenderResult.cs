namespace CubeRender.Api.Render;

/// <summary>Payload returned by the render endpoint.</summary>
public sealed record RenderResult(string[] Lines, int Width, int Height);
