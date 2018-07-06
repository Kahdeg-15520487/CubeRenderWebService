using System;
using System.Collections.Generic;
using System.Text;

namespace RenderCube
{
    public class LineRenderer
    {
        public static void Render(Canvas canvas, Point A, Point B, char lineCharacter = '*')
        {
            Render(canvas, A, B, Point.Zero, lineCharacter);
        }

        public static void Render(Canvas canvas, Point A, Point B, Point offset, char lineCharacter = '*')
        {
            var line = LinearInterpolation.GetLine(A, B);
            foreach (var p in line)
            {
                var xOS = (int)(p.X + offset.X);
                var yOS = (int)(p.Y + offset.Y);
                canvas[xOS, yOS] = lineCharacter;
            }
        }
    }
}
