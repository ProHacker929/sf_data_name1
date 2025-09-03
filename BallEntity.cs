using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Football5._0
{
     public class BallEntity 
    {
        public BallPhysics myPhysics;
        public Image sprite;
        public Size mySize;

        public BallEntity(Size size, int radius, FootballPlayerPicturesManager picturesManager)
        {
            myPhysics = new BallPhysics(size, radius, picturesManager);
            var fn = "C:\\Users\\adria\\Downloads\\Football5.0\\Football5.0\\ball.png";
            sprite = new Bitmap(fn);
            mySize = size;
        }


        public void Drawsprite(Graphics g)
        {
            g.DrawImage(sprite, myPhysics.position.X, myPhysics.position.Y, mySize.Width, mySize.Height);
        }

    }
}
