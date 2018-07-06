using System;
using System.Collections.Generic;
using System.Text;

namespace RenderCube
{
    public struct Vector2D
    {
        public double X;
        public double Y;

        public Vector2D(double x, double y)
        {
            X = x;
            Y = y;
        }
        public Vector2D(double x)
        {
            X = x;
            Y = x;
        }

        public double Magnitude
        {
            get
            {
                return Math.Sqrt(X * X + Y * Y);
            }
        }

        public void Normalize()
        {
            var mag = Magnitude;
            X /= mag;
            Y /= mag;
        }

        public static Vector2D operator +(Vector2D a, Vector2D b)
        {
            return new Vector2D(a.X + b.X, a.Y + b.Y);
        }
        public static Vector2D operator -(Vector2D a, Vector2D b)
        {
            return new Vector2D(a.X - b.X, a.Y - b.Y);
        }

        public static readonly Vector2D Up = new Vector2D(0, 1);
        public static readonly Vector2D Down = new Vector2D(0, -1);
        public static readonly Vector2D Left = new Vector2D(-1, 0);
        public static readonly Vector2D Right = new Vector2D(1, 0);
    }
}
