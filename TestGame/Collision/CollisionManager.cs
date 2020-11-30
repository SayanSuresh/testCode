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

        public bool Collision { get; set; } = false;

        public Rectangle PlayerRect { get ; set ; }
        public Rectangle TileRect { get ; set ; }
        public Vector2 StopDirection { get ; set ; }

        public Vector2 CheckCollision(Rectangle rect1, Rectangle rect2, Vector2 direction, Vector2 Velocity)
        {
            playerRect = rect1;
            tileRect = rect2;
            stopDirection = new Vector2(0, 0);
            playerRect.X += (int)direction.X * 8;
            playerRect.Y += (int)direction.Y * 8;

            //tileRect.Height += (int)direction.Y * (int)Velocity.Y;

            PlayerRect = playerRect;
            TileRect = tileRect;
            StopDirection = stopDirection;

            if (PlayerRect.Intersects(TileRect))
            {
                Collision = true;
                return StopDirection;
            }
            Collision = false;
            return direction;
        }

        public bool isCollisie()
        {
            return Collision;
        }

        public bool CollisionTopOf()
        {
            return (playerRect.Bottom >= tileRect.Top - 1 &&
                    playerRect.Bottom <= tileRect.Top + (tileRect.Height / 2) &&
                    playerRect.Right >= tileRect.Left + (tileRect.Width / 5) &&
                    playerRect.Left <= tileRect.Right - (tileRect.Width / 5));
        }

        public bool CollisionBottomOf()
        {
            return (playerRect.Top <= tileRect.Bottom + (tileRect.Height / 5) &&
                    playerRect.Top >= tileRect.Bottom - 1 &&
                    playerRect.Right >= tileRect.Left + (tileRect.Width / 5) &&
                    playerRect.Left <= tileRect.Right - (tileRect.Width / 5));
        }

        public bool CollisionLeftOf()
        {
            return (playerRect.Right >= tileRect.Left  &&
                    playerRect.Right <= tileRect.Right &&
                    playerRect.Top <= tileRect.Bottom - (tileRect.Width / 4) &&
                    playerRect.Bottom >= tileRect.Top + (tileRect.Width / 4));
        }

        public bool CollisionRightOf()
        {
            return (playerRect.Left >= tileRect.Left &&
                    playerRect.Left <= tileRect.Right &&
                    playerRect.Top <= tileRect.Bottom - (tileRect.Width / 4) &&
                    playerRect.Bottom >= tileRect.Top + (tileRect.Width / 4));
            //if (rect1.X < rect2.X + rect2.Width &&
            //        rect1.X + rect1.Width > rect2.X &&
            //        rect1.Y < rect2.Y + rect2.Height &&
            //        rect1.Y + rect1.Height > rect2.Y)
                //return  true;
            //return  false;
        }
    }
}
