using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using TestGame.Interfaces;

namespace TestGame.Animation.HeroAnimations
{
    public class WalkLeftAnimation: IEntityAnimation
    {
        private Animatie _animatie;
        Texture2D texture;
        ITransform transform;

        public WalkLeftAnimation(Texture2D texture, ITransform transform)
        {
            this.transform = transform;
            this.texture = texture;           
            _animatie = new Animatie();
            _animatie.AddFrame(new AnimationFrame(new Rectangle(0, 0, 280, 385)));
            _animatie.AddFrame(new AnimationFrame(new Rectangle(280, 0, 280, 385)));
            _animatie.AddFrame(new AnimationFrame(new Rectangle(560, 0, 280, 385)));
            _animatie.AddFrame(new AnimationFrame(new Rectangle(840, 0, 280, 385)));
            _animatie.AddFrame(new AnimationFrame(new Rectangle(1120, 0, 280, 385)));

        }

        public Animatie Animatie
        {
            get { return _animatie; }
            set { _animatie = value; }
        }
       

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, transform.Position, _animatie.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(0, 0), 0.5f, SpriteEffects.FlipHorizontally, 0);
        }

        public void Update(GameTime gameTime)
        {
            _animatie.Update(gameTime);
        }
    }
}
