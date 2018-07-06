using System;
using System.Collections.Generic;
using System.Text;

namespace RenderCube
{
    public class LinearInterpolation
    {
        static double lerp(double start, double end, double t)
        {
            return start + t * (end - start);
        }

        static Point lerp_point(Point p0, Point p1, double t)
        {
            return new Point(lerp(p0.X, p1.X, t),
                             lerp(p0.Y, p1.Y, t));
        }

        static double diagonal_distance(Point p0, Point p1)
        {
            var dx = p1.X - p0.X;
            var dy = p1.Y - p0.Y;
            return Math.Max(Math.Abs(dx), Math.Abs(dy));
        }

        static Point round_point(Point p)
        {
            return new Point(Math.Round(p.X), Math.Round(p.Y));
        }

        public static List<Point> GetLine(Point start, Point end)
        {
            var points = new List<Point>();
            var N = diagonal_distance(start, end);
            for (var step = 0; step <= N; step++)
            {
                var t = N == 0 ? 0.0 : step / N;
                points.Add(round_point(lerp_point(start, end, t)));
            }
            return points;
        }
    }
}
