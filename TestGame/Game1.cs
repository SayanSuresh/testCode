using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using TestGame.Collision;
using TestGame.Input;
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
        CollisionManager collisionManager;
      


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            collisionManager = new CollisionManager();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            texture = Content.Load<Texture2D>("spritesheetvolt_run");
            blokTexture = Content.Load<Texture2D>("blok");


            InitialzeGameObjects();
            // TODO: use this.Content to load your game content here
        }

        private void InitialzeGameObjects()
        {          
            hero = new Hero(texture, new KeyBoardReader());
            blok = new Blok(blokTexture, new Vector2(300,50));
           
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

            hero.Draw(_spriteBatch);
            blok.Draw(_spriteBatch);
           

            _spriteBatch.End();
           

            base.Draw(gameTime);
        }
    }
}
