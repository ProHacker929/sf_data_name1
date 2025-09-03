using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Football5._0
{
    public class BallPhysics
    {
        internal readonly int radius;
        private readonly FootballPlayerPicturesManager picturesManager;
        private Size size;
        public PointF position;
        float gravity;
        float a;
        private float dx;
        float impulseY;

        public BallPhysics(Size size, int radius, FootballPlayerPicturesManager picturesManager)
        {
            this.size = size;
            this.radius = radius;
            this.picturesManager = picturesManager;
            position = new PointF(100, 150);
            gravity = 0;
            a = 0.4f;
            impulseY = -15;
            dx = 2;
        }

        public PlayerType ApplyRules()
        {
            CalculatePhysics(out var playerType);

            return playerType;
        }

        public Point GetCenter()
        {
            var x = position.X + size.Width / 2;
            var y = position.Y + size.Height / 2;

            return new Point((int)x, (int)y);
        }

        public void CalculatePhysics(out PlayerType playerType)
        {
            if (position.X > 750)
                dx = -2;
            if (position.X < 0)
                dx = 2;

            position.X += dx;

            if (position.Y < 385 || a < 0)
            {
                if (a != 0.4f)
                {
                    a += 0.05f;
                }
                position.Y += gravity;
                
                gravity += a;
            }
            else
            {
                if (position.Y > 500)
                {
                    if (Math.Abs(impulseY) > 0.1f)

                    {
                        impulseY /= 2;

                        AddForce(a / 2);

                    }
                    else
                    {
                        impulseY = -15;
                    }
                }
            }

            var (pushX, pushY) = picturesManager.CalculatePush(this, out playerType);

            if (pushY != 0)
            {
                gravity = -gravity;
            }
            
            if(pushX != 0)
            {
                dx = -dx;
            }
        }

        public void AddForce(float forceValue)
        {
            gravity = -gravity;
            gravity /= 2;
            a = -forceValue;
        }

        public void AddForceYQuickly(float forceValue)
        {

            var (pushX, pushY) = picturesManager.CalculatePush(this, out var isGoalKeeper);

            if (pushX != 0 || pushY != 0)
            {
                return;
            }

            gravity = forceValue * 30;
            gravity *= -1;
            a = -forceValue;
            impulseY = -15; 
        }
    }

}









