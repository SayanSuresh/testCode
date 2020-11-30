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
        private float gravity = 100;
        //float jumpspeed = 1200;

        private IInputReader reader;
        private IGameCommand moveCommand;
        IEntityAnimation walkRight, walkLeft, currentAnimation;

        bool jumping; //Is the character jumping?
        float startY, jumpspeed = 0; //startY to tell us //where it lands, jumpspeed to see how fast it jumps


        public Hero(Texture2D texture, IInputReader inputReader)
        {
            this.heroTexture = texture;
            walkRight = new WalkRightAnimation(texture, this);
            walkLeft = new WalkLeftAnimation(texture, this);
            currentAnimation = walkRight;

            //Read input for hero class
            this.reader = inputReader;
            moveCommand = new MoveCommand();

            Position = new Vector2(0, 200);
            _collisionRectangle = new Rectangle((int)Position.X, (int)Position.Y, 65, 110);
            collisionManager = new CollisionManager();

            startY = Position.Y;//Starting position
            jumping = false;//Init jumping to false
            jumpspeed = 0;//Default no speed
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
        bool bla = true;
        private void move(Vector2 _direction)
        {
            if (_direction.X == -1)
                currentAnimation = walkLeft;
            else if (_direction.X == 1)
                currentAnimation = walkRight;

            //jumping movement

            if (jump && collisionManager.isCollisie())
            {
                //Position = new Vector2(Position.X, Position.Y);
                jump = false;

                if (collisionManager.CollisionTopOf())
                {
                    //Position = new Vector2(Position.X, Position.Y);
                }
                //if (!collisionManager.CollisionTopOf())


                //if (_direction.X == -1)
                //    Position = new Vector2(Position.X, 200);
                //else if (_direction.X == 1)
                //    Position = new Vector2(Position.X, 200);
            }

            int i = 0;

            if (jumping)
            {
                //if (!collisionManager.CollisionTopOf())
                Position += new Vector2(0,jumpspeed);//Making it go up
                jumpspeed += 1;//Some math (explained later)

                if (collisionManager.CollisionTopOf())
                {
                    i++;
                    Position = new Vector2(collisionManager.PlayerRect.X, collisionManager.PlayerRect.Y);//Then set it on
                    jumping = false;
                    Debug.WriteLine(i);
                }


                if (Position.Y >= startY)
                //If it's farther than ground
                {
                    //Position = new Vector2(collisionManager.PlayerRect.X, startY);//Then set it on
                    jumping = false;
                }
            }
            else
            {
                if (_direction.Y == -1)
                {
                    jumping = true;
                    jumpspeed = -14;//Give it upward thrust
                }
            }

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
