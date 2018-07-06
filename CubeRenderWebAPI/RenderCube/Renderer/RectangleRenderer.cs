using System;
using System.Collections.Generic;
using System.Text;

namespace RenderCube
{
    public class RectangleRenderer
    {
        public static void Render(Canvas canvas, Rectangle rect)
        {
            Render(canvas, rect, Point.Zero);
        }

        public static void Render(Canvas canvas, Rectangle rect, Point offset)
        {
            var A = rect.Corners[0];
            var B = rect.Corners[1];
            var C = rect.Corners[2];
            var D = rect.Corners[3];


            //var x0 = (int)(A.X + offset.X);
            //var y0 = (int)(A.Y + offset.Y);
            //var x1 = (int)(C.X + offset.X);
            //var y1 = (int)(C.Y + offset.Y);
            //canvas.Clear(x0, y0, x1, y1);

            Render(canvas, A, B, C, D, offset);
        }

        public static void Render(Canvas canvas, Point A, Point B, Point C, Point D, Point offset)
        {
            foreach (var corner in new[] { A, B, C, D })
            {
                var xOS = (int)(corner.X + offset.X);
                var yOS = (int)(corner.Y + offset.Y);
                canvas.canvas[xOS, yOS] = '*';
            }

            LineRenderer.Render(canvas, A + Vector2D.Right, B + Vector2D.Left, offset, '-');
            LineRenderer.Render(canvas, B + Vector2D.Up, C + Vector2D.Down, offset, '|');
            LineRenderer.Render(canvas, C + Vector2D.Left, D + Vector2D.Right, offset, '-');
            LineRenderer.Render(canvas, D + Vector2D.Down, A + Vector2D.Up, offset, '|');
        }
    }
}
