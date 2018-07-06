using System;
using System.Collections.Generic;
using System.Text;

namespace RenderCube
{
    public struct Rectangle
    {
        /* A----B
         * |    |
         * |    |
         * |    |
         * D----C
         */

        /// <summary>
        /// 4 corner of the rectangle
        /// </summary>
        public Point[] Corners { get; private set; }
        public double Width { get; private set; }
        public double Height { get; private set; }

        public Rectangle(Point A, double w, double h)
        {
            Corners = new Point[4];

            Corners[0] = A;
            Corners[1] = new Point(A.X + w, A.Y);
            Corners[2] = new Point(A.X + w, A.Y + h);
            Corners[3] = new Point(A.X, A.Y + h);

            Width = w;
            Height = h;
        }
    }
}
