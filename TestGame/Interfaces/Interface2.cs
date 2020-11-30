using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestGame.Interfaces
{
    public interface ICollisionWithTiles
    {
        public Vector2 CheckCollision(Rectangle rect1, Rectangle rect2, Vector2 direction, Vector2 VelocityY);
        public Rectangle PlayerRect { get; set; }
        public Rectangle TileRect { get; set; }
        public Vector2 StopDirection { get; set; }

        public bool isCollisie();

        public bool CollisionTopOf();
        public bool CollisionBottomOf();
        public bool CollisionLeftOf();
        public bool CollisionRightOf();
    }
}
