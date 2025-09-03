using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Football5._0
{
    public class FootballPlayerPicturesManager
    {
        private List<FootballPlayerPictureContainer> allFootballPlayerPictures;

        public FootballPlayerPicturesManager(params PictureBox[] pictures)
        {
            allFootballPlayerPictures = pictures.Select(p => new FootballPlayerPictureContainer(p)).OrderBy(f => f.Y).ToList();

            var goalKeepers = allFootballPlayerPictures.Take(allFootballPlayerPictures.Count / 2).ToHashSet();

            foreach(var player in allFootballPlayerPictures)
            {
                if (goalKeepers.Contains(player))
                {
                    player.IsGoalKeeper = true;
                }
                else
                {
                    player.IsGoalKeeper = false;
                }
            }
        }

        public (int x, int y) CalculatePush(BallPhysics physics, out PlayerType playerType)
        {
            playerType = PlayerType.None;

            foreach (var picture in allFootballPlayerPictures)
            {
                var center = physics.GetCenter();
                var touch = picture.CalculateTouch(center.X, center.Y, physics.radius);

                if (touch == Touch.None)
                    continue;

                playerType = picture.IsGoalKeeper ? PlayerType.GoalKeeper : PlayerType.Attacker;

                int dx = 0;
                int dy = 0;

                if (touch.HasFlag(Touch.Left))
                    dx -= 10;
                if (touch.HasFlag(Touch.Right))
                    dx += 10;
                if (touch.HasFlag(Touch.Top))
                    dy -= 10;
                if (touch.HasFlag(Touch.Bottom))
                    dy += 10;

                // Normalize diagonal push to keep consistent speed (optional)
                if (dx != 0 && dy != 0)
                {
                    double length = Math.Sqrt(dx * dx + dy * dy);
                    dx = (int)(dx / length * 10);
                    dy = (int)(dy / length * 10);
                }

                return (dx, dy);
            }

            return (0, 0);
        }
    }

    public enum PlayerType
    {
        None,
        GoalKeeper,
        Attacker
    }
}
