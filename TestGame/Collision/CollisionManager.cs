using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TestGame.Interfaces;

namespace TestGame.Collision
{
    public class CollisionManager: ICollisionWithTiles
    {
        private Rectangle playerRect;
        private Rectangle tileRect;
        private Vector2 stopDirection;

        public Rectangle PlayerRect { get ; set ; }
        public Rectangle TileRect { get ; set ; }
        public Vector2 StopDirection { get ; set ; }

        public Vector2 CheckCollision(Rectangle rect1, Rectangle rect2, Vector2 direction, Vector2 Velocity)
        {
            playerRect = rect1;
            tileRect = rect2;
            stopDirection = new Vector2(0, 0);
            playerRect.X += (int)direction.X * 5;
            playerRect.Y += (int)direction.Y * 1500;

            //tileRect.Height += (int)direction.Y * (int)Velocity.Y;

            PlayerRect = playerRect;
            TileRect = tileRect;
            StopDirection = stopDirection;

            if (PlayerRect.Intersects(TileRect))
            {
                return StopDirection;
            }
            return direction;
        }

        public bool CollisionTopOf(Rectangle rect1, Rectangle rect2)
        {
            return (rect1.Bottom >= rect2.Top - 1 &&
                    rect1.Bottom <= rect2.Top + (rect2.Height / 2) &&
                    rect1.Right >= rect2.Left + (rect2.Width / 5) &&
                    rect1.Left <= rect2.Right - (rect2.Width / 5));
        }

        public bool CollisionBottomOf(Rectangle rect1, Rectangle rect2)
        {
            return (rect1.Top <= rect2.Bottom + (rect2.Height / 5) &&
                    rect1.Top >= rect2.Bottom - 1 &&
                    rect1.Right >= rect2.Left + (rect2.Width / 5) &&
                    rect1.Left <= rect2.Right - (rect2.Width / 5));
        }

        public bool CollisionLeftOf(Rectangle rect1, Rectangle rect2)
        {
            return (rect1.Right >= rect2.Left  &&
                    rect1.Right <= rect2.Right &&
                    rect1.Top <= rect2.Bottom - (rect2.Width / 4) &&
                    rect1.Bottom >= rect2.Top + (rect2.Width / 4));
        }

        public bool CollisionRightOf(Rectangle rect1, Rectangle rect2)
        {
            //return (rect1.Left >= rect2.Left &&
            //        rect1.Left <= rect2.Right &&
            //        rect1.Top <= rect2.Bottom - (rect2.Width / 4) &&
            //        rect1.Bottom >= rect2.Top + (rect2.Width / 4));
            if (rect1.X < rect2.X + rect2.Width &&
                    rect1.X + rect1.Width > rect2.X &&
                    rect1.Y < rect2.Y + rect2.Height &&
                    rect1.Y + rect1.Height > rect2.Y)
                return  true;
            return  false;
        }
    }
}
