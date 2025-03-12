using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PacManFinal
{
    public class Food : GameObject
    {
        public Rectangle hitBox;
        public Food(Texture2D texture, Vector2 position) : base(texture, position)
        {
            hitBox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);

        }

    }
}