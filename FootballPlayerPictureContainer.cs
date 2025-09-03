using System;
using System.Windows.Forms;

namespace Football5._0
{
    internal class FootballPlayerPictureContainer
    {
        public FootballPlayerPictureContainer(PictureBox picture)
        {
            X = picture.Location.X;
            Y = picture.Location.Y;
            Width = picture.Width;
            Height = picture.Height;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }

        public bool IsGoalKeeper { get; set; }

        public Touch CalculateTouch(int x, int y, int r)
        {
            int ballLeft = x - r;
            int ballRight = x + r;
            int ballTop = y - r;
            int ballBottom = y + r;

            Touch result = Touch.None;

            // Check sides
            if (ballBottom >= Y && ballTop <= Y && ballRight >= X && ballLeft <= X + Width)
                result |= Touch.Top;

            if (ballTop <= Y + Height && ballBottom >= Y + Height && ballRight >= X && ballLeft <= X + Width)
                result |= Touch.Bottom;

            if (ballRight >= X && ballLeft <= X && ballBottom >= Y && ballTop <= Y + Height)
                result |= Touch.Left;

            if (ballLeft <= X + Width && ballRight >= X + Width && ballBottom >= Y && ballTop <= Y + Height)
                result |= Touch.Right;

            // Check corners (diagonals)
            var corners = new (int cx, int cy, Touch flags)[]
            {
                (X, Y, Touch.Top | Touch.Left),
                (X + Width, Y, Touch.Top | Touch.Right),
                (X, Y + Height, Touch.Bottom | Touch.Left),
                (X + Width, Y + Height, Touch.Bottom | Touch.Right)
            };

            foreach (var (cx, cy, flags) in corners)
            {
                int dx = x - cx;
                int dy = y - cy;
                if (dx * dx + dy * dy <= r * r)
                    result |= flags;
            }

            return result;
        }
    }

    [Flags]
    enum Touch
    {
        None = 0,
        Top = 1,
        Right = 2,
        Left = 4,
        Bottom = 8
    }
}
