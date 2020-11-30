using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.DXGI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using TestGame.Animation;
using TestGame.Animation.HeroAnimations;
using TestGame.Collision;
using TestGame.Commands;
using TestGame.Input;
using TestGame.Interfaces;

namespace TestGame
{
    public class Hero :ITransform, ICollision
    {
        private ICollisionWithTiles collisionManager;
        private Rectangle tileRectangle;

        private Rectangle _collisionRectangle;
        public Rectangle CollisionRectangle { get; set; }
        public Vector2 Position { get; set; }

        Texture2D heroTexture;
        GameTime gameTime;
        Vector2 velocity;
        private bool jump = false;
        const float gravity = 100;
        float jumpspeed = 1500;

        private IInputReader reader;
        private IGameCommand moveCommand;
        IEntityAnimation walkRight, walkLeft, currentAnimation;

        public Hero(Texture2D texture, IInputReader inputReader)
        {
            this.heroTexture = texture;
            walkRight = new WalkRightAnimation(texture, this);
            walkLeft = new WalkLeftAnimation(texture, this);
            currentAnimation = walkRight;

            //Read input for hero class
            this.reader = inputReader;
            moveCommand = new MoveCommand();

            _collisionRectangle = new Rectangle((int)Position.X, (int)Position.Y, 48, 62);
            collisionManager = new CollisionManager();
        }

        public void Update(GameTime gameTime)
        {
            var direction = reader.ReadInput();
            this.gameTime = gameTime;
            if (direction.X != 0 || direction.Y != 0)
                //animatie.Update(gameTime);
                currentAnimation.Update(this.gameTime);
            move(direction);

            //_direction = collisionManager.CheckCollision(CollisionRectangle, blokRectangle, _direction);
            _collisionRectangle.X = (int)Position.X;
            _collisionRectangle.Y = (int)Position.Y;
            CollisionRectangle = _collisionRectangle;
        }

        public void getTileRect(Rectangle collisionRectangle)
        {
            this.tileRectangle = collisionRectangle;
        }

        private void move(Vector2 _direction)
        {
            if (_direction.X == -1)
                currentAnimation = walkLeft;
            else if (_direction.X == 1)
                currentAnimation = walkRight;

            // jumping movement
            if (_direction.Y == -1 && jump)
            {
                velocity.Y = -jumpspeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                jump = false;
            }

            if (!jump)
                velocity.Y += gravity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            else
                velocity.Y = 0;

            Position += velocity;

            jump = Position.Y >= 200;

            //if (jump)
            //{
            //    Position = new Vector2(Position.X, 300);
            //    if (_direction.X == -1)
            //        Position = new Vector2(Position.X, 300);
            //    else if (_direction.X == 1)
            //        Position = new Vector2(Position.X, 300);
            //}

            _direction = collisionManager.CheckCollision(CollisionRectangle, this.tileRectangle, _direction, velocity);

            moveCommand.Execute(this, _direction);
        }



        private void Move(Vector2 mouse) 
        {
           this.moveCommand.Execute(this, mouse);
        }

   

        public void Draw(SpriteBatch spriteBatch)
        {
            currentAnimation.Draw(spriteBatch);
            //spriteBatch.Draw(heroTexture, Position, animatie.CurrentFrame.SourceRectangle, Color.White,0, new Vector2(0,0),0.5f, SpriteEffects.None,0);
        }

      

      

       

    }
}
