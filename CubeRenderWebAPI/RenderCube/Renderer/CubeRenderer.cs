using System;
using System.Collections.Generic;
using System.Text;

namespace RenderCube
{
    public class CubeRenderer
    {
        public static void Render(Canvas canvas, Cube cube)
        {
            Render(canvas, cube, Point.Zero);
        }

        public static void Render(Canvas canvas, Cube cube, Point offset)
        {
            var A = cube.Corners[0];
            var B = cube.Corners[1];
            var C = cube.Corners[2];
            var D = cube.Corners[3];

            var A_ = cube.Corners[4];
            var B_ = cube.Corners[5];
            var C_ = cube.Corners[6];
            var D_ = cube.Corners[7];

            var depthVector = new Vector2D(cube.Depth);
            var unitVector = new Vector2D(1);

            RectangleRenderer.Render(canvas, A, B, C, D, offset);
            RectangleRenderer.Render(canvas, A_, B_, C_, D_, offset);

            LineRenderer.Render(canvas, A + unitVector, A_ - unitVector, '\\');
            LineRenderer.Render(canvas, B + unitVector, B_ - unitVector, '\\');
            LineRenderer.Render(canvas, C + unitVector, C_ - unitVector, '\\');
            LineRenderer.Render(canvas, D + unitVector, D_ - unitVector, '\\');
        }
    }
}
