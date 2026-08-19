namespace CubeRender.Api.Render;

/// <summary>An axis-aligned box in 3D with 8 corners and the 12 wireframe edges.</summary>
public readonly record struct Cube(V3 Origin, float Sx, float Sy, float Sz)
{
    public V3[] Vertices()
    {
        var (ox, oy, oz) = Origin;
        return
        [
            new(ox, oy, oz),
            new(ox + Sx, oy, oz),
            new(ox + Sx, oy + Sy, oz),
            new(ox, oy + Sy, oz),
            new(ox, oy, oz + Sz),
            new(ox + Sx, oy, oz + Sz),
            new(ox + Sx, oy + Sy, oz + Sz),
            new(ox, oy + Sy, oz + Sz),
        ];
    }

    /// <summary>Index pairs into <see cref="Vertices"/> for the wireframe edges.</summary>
    public static readonly (int A, int B)[] Edges =
    [
        (0, 1), (1, 2), (2, 3), (3, 0),   // front face
        (4, 5), (5, 6), (6, 7), (7, 4),   // back face
        (0, 4), (1, 5), (2, 6), (3, 7),   // connectors
    ];
}
