using System;
using System.Collections.Generic;
using System.Text;

namespace RenderCube
{
    public struct Cube
    {
        /* A-----B
         * | \   |\
         * |  \  | \
         * D---\-C  \
         *  \   \ \  \
         *   \ A'----B'
         *    \ |   \ |
         *     \|    \|
         *     D'-----c'
         */

        /// <summary>
        /// 8 corner of the cube
        /// </summary>
        public Point[] Corners { get; private set; }
        public double Width { get; private set; }
        public double Height { get; private set; }
        public double Depth { get; private set; }

        public Cube(Point A, double w, double h, double d)
        {
            Corners = new Point[8];
            Corners[0] = A;
            Corners[1] = new Point(A.X + w, A.Y);
            Corners[2] = new Point(A.X + w, A.Y + h);
            Corners[3] = new Point(A.X, A.Y + h);

            Vector2D depthVector = new Vector2D(d);

            Corners[4] = A + depthVector;
            Corners[5] = new Point(A.X + w, A.Y) + depthVector;
            Corners[6] = new Point(A.X + w, A.Y + h) + depthVector;
            Corners[7] = new Point(A.X, A.Y + h) + depthVector;

            Width = w;
            Height = h;
            Depth = d;
        }
    }
}
