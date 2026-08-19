namespace CubeRender.Api.Render;

/// <summary>Renders a cube as a 2D ASCII wireframe.</summary>
public static class Renderer
{
    public static RenderResult Render(int x, int y, int z)
    {
        var sx = Math.Max(1, x);
        var sy = Math.Max(1, y);
        var sz = Math.Max(1, z);

        var cube = new Cube(new V3(0, 0, 0), sx, sy, sz);
        var vertices = cube.Vertices();

        // isometric projection of each 3D corner to a flat plane
        const float iso = 0.866f, flat = 0.5f, depth = 0.5f;
        var pts = vertices
            .Select(v => (px: (v.X - v.Y) * iso, py: (v.X + v.Y) * flat - v.Z * depth))
            .ToArray();

        float minX = pts.Min(p => p.px), maxX = pts.Max(p => p.px);
        float minY = pts.Min(p => p.py), maxY = pts.Max(p => p.py);

        const int pad = 1;
        int w = (int)Math.Ceiling(maxX - minX) + pad * 2 + 2;
        int h = (int)Math.Ceiling(maxY - minY) + pad * 2 + 2;

        var canvas = new Canvas(Math.Max(w, 4), Math.Max(h, 4));
        var grid = pts
            .Select(p => (
                X: (int)Math.Round(p.px - minX) + pad + 1,
                Y: (int)Math.Round(p.py - minY) + pad + 1))
            .ToArray();

        foreach (var (a, b) in Cube.Edges)
            canvas.Line(grid[a].X, grid[a].Y, grid[b].X, grid[b].Y, '#');

        return new RenderResult(canvas.Render(), canvas.Width, canvas.Height);
    }
}
