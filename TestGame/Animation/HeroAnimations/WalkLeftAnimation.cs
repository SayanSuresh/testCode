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
            _animatie.AddFrame(new AnimationFrame(new Rectangle(0, 197, 48, 62)));
            _animatie.AddFrame(new AnimationFrame(new Rectangle(48, 197, 48, 62)));
            _animatie.AddFrame(new AnimationFrame(new Rectangle(96, 197, 48, 62)));
        }

        public Animatie Animatie
        {
            get { return _animatie; }
            set { _animatie = value; }
        }
       

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, transform.Position, _animatie.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(0, 0), 2f, SpriteEffects.None, 0);
        }

        public void Update(GameTime gameTime)
        {
            _animatie.Update(gameTime);
        }
    }
}
