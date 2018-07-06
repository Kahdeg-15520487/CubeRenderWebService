using System;
using System.IO;
using System.Text;

namespace RenderCube
{
    public struct Canvas
    {
        public readonly char[,] canvas;
        public readonly int Width;
        public readonly int Height;

        public Canvas(int width, int height)
        {
            canvas = new char[width, height];
            Width = width;
            Height = height;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    canvas[x, y] = ' ';
                }
            }
        }

        public char this[int x, int y]
        {
            get => canvas[x, y];
            set
            {
                try
                {
                    canvas[x, y] = value;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"x={x};y={y}");

                }
            }
        }

        public string Render()
        {
            StringBuilder sb = new StringBuilder();
            using (StringWriter sw = new StringWriter(sb))
            {
                for (int y = 0; y < Height; y++)
                {
                    for (int x = 0; x < Width; x++)
                    {
                        sw.Write(canvas[x, y]);
                    }
                    sw.WriteLine();
                }
            }
            return sb.ToString();
        }

        public void Clear(int x0, int y0, int x1, int y1)
        {
            for (int y = y0; y < y1; y++)
            {
                for (int x = x0; x < x1; x++)
                {
                    canvas[x, y] = ' ';
                }
            }
        }
    }
}
