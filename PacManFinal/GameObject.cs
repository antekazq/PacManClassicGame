using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PacManFinal
{
    public class GameObject
    {
        protected Vector2 position;
        protected Texture2D texture; 
        public bool isShowing;

        public GameObject(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;
            isShowing = true; //Tiles går alltid in här och ser att den är true så den ritar
        }

        public virtual void Draw(SpriteBatch _spriteBatch)
        {
            if (isShowing)
            {
                _spriteBatch.Draw(texture, position, Color.White);
            }

        }
    }
}