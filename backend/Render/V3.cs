namespace CubeRender.Api.Render;

/// <summary>3D point (floats) for the cube geometry.</summary>
public readonly record struct V3(float X, float Y, float Z)
{
    public static V3 operator -(V3 a, V3 b) => new(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
}
