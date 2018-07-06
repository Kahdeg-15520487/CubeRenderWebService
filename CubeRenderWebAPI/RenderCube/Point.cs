using System;
using System.Collections.Generic;
using System.Text;

namespace RenderCube
{
    public struct Point
    {
        public double X;
        public double Y;

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        public static Point operator +(Point p, Vector2D vt)
        {
            return new Point(p.X + vt.X, p.Y + vt.Y);
        }

        public static Point operator -(Point p, Vector2D vt)
        {
            return new Point(p.X - vt.X, p.Y - vt.Y);
        }

        public static readonly Point Zero = new Point(0, 0);

        public override string ToString()
        {
            return $"x={X};y={Y}";
        }
    }
}
