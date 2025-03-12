using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace PacManFinal
{
    public class TextureLoad
    {
        public static Texture2D pacMan;
        public static Texture2D ghost;
        public static Texture2D walltile, floortile;
        public static Texture2D foodTexture;
        public static Texture2D whiteRectangle;
        public static Texture2D spriteSheet, _spriteSheet;
        public static SpriteFont spriteFont;

        public static void Load(ContentManager content) //Static metod, vilket betyder jag inte behöver skapa ett objekt
        {
            spriteFont = content.Load<SpriteFont>(@"font");

            floortile = content.Load<Texture2D>("floortile");
            walltile = content.Load<Texture2D>("walltile");
            pacMan = content.Load<Texture2D>("pacman");
            ghost = content.Load<Texture2D>("ghost");
            foodTexture = content.Load<Texture2D>("Food");
            whiteRectangle = content.Load<Texture2D>("whiteRectangle");
            _spriteSheet = content.Load<Texture2D>("SpriteSheet1");
        }
    }
}
