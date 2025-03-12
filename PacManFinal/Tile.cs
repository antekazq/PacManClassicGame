using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PacManFinal
{
    public class Tile : GameObject
    {
        public bool wall;

        public Tile(Texture2D texture, Vector2 position, bool wall) : base (texture, position)
        {
            this.wall = wall;
        }
    }
}
