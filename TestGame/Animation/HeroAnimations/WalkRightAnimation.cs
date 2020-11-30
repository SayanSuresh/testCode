﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using TestGame.Interfaces;

namespace TestGame.Animation.HeroAnimations
{
    public class WalkRightAnimation: IEntityAnimation
    {
        private Animatie _animatie;
        private Texture2D texture;
        private ITransform transform;

        public WalkRightAnimation(Texture2D texture, ITransform transform)
        {
            this.texture = texture;
            this.transform = transform;
            _animatie = new Animatie();
            _animatie.AddFrame(new AnimationFrame(new Rectangle(0, 70, 48, 62)));
            _animatie.AddFrame(new AnimationFrame(new Rectangle(48, 70, 48, 62)));
            _animatie.AddFrame(new AnimationFrame(new Rectangle(96, 70, 48, 62)));
        }

        public Animatie Animatie { 
            get { return _animatie; } 
            set { _animatie = value; } 
        }

        public Texture2D Texture { get; set ; }

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
