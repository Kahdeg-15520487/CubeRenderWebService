namespace CubeRender.Api.Render;

/// <summary>A character grid plus a Bresenham line drawer (the "canvas").</summary>
public sealed class Canvas(int width, int height)
{
    private readonly char[,] _cells = new char[height, width];

    public int Width { get; } = width;
    public int Height { get; } = height;

    public void Set(int x, int y, char c)
    {
        if (x < 0 || x >= Width || y < 0 || y >= Height) return;
        _cells[y, x] = c;
    }

    public void Line(int x0, int y0, int x1, int y1, char c)
    {
        int dx = Math.Abs(x1 - x0), sx = x0 < x1 ? 1 : -1;
        int dy = -Math.Abs(y1 - y0), sy = y0 < y1 ? 1 : -1;
        int err = dx + dy;

        while (true)
        {
            Set(x0, y0, c);
            if (x0 == x1 && y0 == y1) break;
            int e2 = 2 * err;
            if (e2 >= dy) { err += dy; x0 += sx; }
            if (e2 <= dx) { err += dx; y0 += sy; }
        }
    }

    public string[] Render()
    {
        var lines = new string[Height];
        for (int y = 0; y < Height; y++)
        {
            var row = new char[Width];
            for (int x = 0; x < Width; x++)
                row[x] = _cells[y, x] == '\0' ? '.' : _cells[y, x];
            lines[y] = new string(row);
        }
        return lines;
    }
}
