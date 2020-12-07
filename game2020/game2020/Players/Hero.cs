using game2020.Animation;
using game2020.Animation.HeroAnimations;
using game2020.Collision;
using game2020.Commands;
using game2020.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RefactoringCol;
using System;
using System.Collections.Generic;
using System.Text;

namespace game2020.Players
{
    public class Hero : ITransform, ICollision
    {
        private Rectangle _collisionRectangle;
        public Rectangle CollisionRectangle { get; set; }
        public Vector2 Position { get; set; }

        Texture2D heroTexture;
        GameTime gameTime;

        private IInputReader reader;
        private IGameCommand moveCommand;
        IEntityAnimation walkRight, walkLeft, walkUp, walkDown, currentAnimation;

        ICollisionHelper collisionhelper;
        Vector2 velocity;
        private bool hasJumped = false;


        CollisionManager manager;

        public Hero(Texture2D texture, IInputReader inputReader, ICollisionHelper helper)
        {
            this.heroTexture = texture;
            walkRight = new WalkRightAnimation(texture, this);
            walkLeft = new WalkLeftAnimation(texture, this);
            walkUp = new WalkUpAnimation(texture, this);
            walkDown = new WalkDownAnimation(texture, this);
            currentAnimation = walkDown;

            //Read input for hero class
            this.reader = inputReader;
            moveCommand = new MoveCommand();


            _collisionRectangle = new Rectangle((int)Position.X, (int)Position.Y, 62, 110);
            this.collisionhelper = helper;
            Position = new Vector2(65, 340);

            //manager = new CollisionManager(inputReader, helper);
        }

        private void move(Vector2 _direction)
        {
            if (_direction.X == -1)
                currentAnimation = walkLeft;
            else if (_direction.X == 1)
                currentAnimation = walkRight;
            else if (_direction.Y == -1)
                currentAnimation = walkUp;
            if (_direction.X == 0 && _direction.Y == 0)
                currentAnimation = walkDown;

            //jumping movement
            if (_direction.Y == -1 && hasJumped == false)
            {
                velocity.Y = -9f;
                hasJumped = true;
            }


            Position += velocity;
            if (velocity.Y < 20)
                velocity.Y += 0.9f;

            moveCommand.Execute(this, _direction);
        }

        public void Collision(Rectangle playerRec, Rectangle newRectangle, int xOffset, int yOffset)
        {
            //manager.Collision(playerRec, newRectangle, xOffset, yOffcet);
            //manager.setColRect(_collisionRectangle);

            if (collisionhelper.CollisionTop(playerRec, newRectangle))
            {
                _collisionRectangle.Y = newRectangle.Y - _collisionRectangle.Height;
                velocity.Y = 0f;
                hasJumped = false;
            }

            if (collisionhelper.CollisionLeft(playerRec, newRectangle))
                Position = new Vector2(newRectangle.X - _collisionRectangle.Width - 2, Position.Y);

            if (collisionhelper.CollisionRight(playerRec, newRectangle))
                Position = new Vector2(newRectangle.X + _collisionRectangle.Width + 2, Position.Y);

            if (collisionhelper.CollisionBottom(playerRec, newRectangle))
                velocity.Y = 1f;


            // Trap hero inside window borders 
            if (Position.X < 0)
                Position = new Vector2(0, Position.Y);

            if (Position.X > xOffset - _collisionRectangle.Width)
                Position = new Vector2(xOffset - _collisionRectangle.Width, Position.Y);

            if (Position.Y < 0)
                velocity.Y = 7.5f;

            if (Position.Y > yOffset - _collisionRectangle.Height)
                Position = new Vector2(Position.X, yOffset - _collisionRectangle.Height);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            currentAnimation.Draw(spriteBatch);
        }

        public void Update(GameTime gameTime)
        {
            var direction = reader.ReadInput();
            this.gameTime = gameTime;
            if (direction.X != 0 || direction.Y != 0)
                //animatie.Update(gameTime);
                currentAnimation.Update(this.gameTime);

            move(direction);

            _collisionRectangle.X = (int)Position.X;
            _collisionRectangle.Y = (int)Position.Y;
            CollisionRectangle = _collisionRectangle;

            //manager.Update();
        }
    }
}
