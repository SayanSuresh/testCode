using game2020.Commands;
using game2020.Interfaces;
using Microsoft.Xna.Framework;
using RefactoringCol;
using System;
using System.Collections.Generic;
using System.Text;

namespace game2020.Collision
{
    class CollisionManager: ITransform, ICollision, IJump
    {
        public Vector2 Position { get ; set ; }
        public Rectangle CollisionRectangle { get ; set ; }
        public bool HasJumed { get; set; } = false;

        private Rectangle _collisionRectangle;
        private IInputReader reader;
        private IGameCommand moveCommand;

        ICollisionHelper collisionhelper;
        Vector2 velocity;

        public void setColRect(Rectangle collisionRectangle)
        {
            this._collisionRectangle = collisionRectangle;
        }
        public CollisionManager()
        {

        }
        public CollisionManager(IInputReader inputReader, ICollisionHelper helper)
        {
            this.reader = inputReader;
            this.moveCommand = new MoveCommand();
            this.collisionhelper = helper;
        }

        public void Update()
        {
            // jumping movement
            if (reader.ReadInput().Y == -1 && HasJumed == false)
            {
                velocity.Y = -9f;
                HasJumed = true;
            }

            Position += velocity;
            if (velocity.Y < 20)
                velocity.Y += 0.9f;

            moveCommand.Execute(this, reader.ReadInput());
        }

        public void Collision(Rectangle playerRec, Rectangle newRectangle, int xOffset, int yOffcet)
        {
            if (collisionhelper.CollisionTopOf(playerRec, newRectangle))
            {
                _collisionRectangle.Y = newRectangle.Y - _collisionRectangle.Height;
                velocity.Y = 0f;
                HasJumed = false;
            }

            if (collisionhelper.CollisionLeftOf(playerRec, newRectangle))
            {
                Position = new Vector2(newRectangle.X - _collisionRectangle.Width - 2, Position.Y);
            }

            if (collisionhelper.CollisionRightOf(playerRec, newRectangle))
            {
                Position = new Vector2(newRectangle.X + _collisionRectangle.Width + 2, Position.Y);
            }

            if (collisionhelper.CollisionBottomOf(playerRec, newRectangle))
            {
                velocity.Y = 1f;
            }


            if (Position.X < 0)
                Position = new Vector2(0, Position.Y);

            else if (Position.X > xOffset - _collisionRectangle.Width)
                Position = new Vector2(xOffset - _collisionRectangle.Width, Position.Y);

            else if (Position.Y < 0)
                velocity.Y = 16f;

            else if (Position.Y > yOffcet - _collisionRectangle.Height)
                Position = new Vector2(Position.X, yOffcet - _collisionRectangle.Height);

            CollisionRectangle = _collisionRectangle;
        }
    }
}
