using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Diagnostics;
using TestGame.Collision;
using TestGame.Input;
using TestGame.Interfaces;
using TestGame.world;

namespace TestGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D texture, blokTexture;
        Hero hero;
        Blok blok;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here


            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            texture = Content.Load<Texture2D>("thief");
            blokTexture = Content.Load<Texture2D>("blok");


            InitialzeGameObjects();
            // TODO: use this.Content to load your game content here
            hero.getTileRect(blok.CollisionRectangle);
        }

        private void InitialzeGameObjects()
        {          
            hero = new Hero(texture, new KeyBoardReader());
            blok = new Blok(blokTexture, new Vector2(300,200));
        }
        protected override void Update(GameTime gameTime)
        {
            
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            hero.Update(gameTime);
            blok.Update();


            base.Update(gameTime);
        }

       

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.BurlyWood);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();

            blok.Draw(_spriteBatch);
            hero.Draw(_spriteBatch);
           

            _spriteBatch.End();
           

            base.Draw(gameTime);
        }
    }
}
