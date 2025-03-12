using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PacManFinal
{
    public class PacManChar : GameObject
    {
        public int health;
        Vector2 direction; 
        Vector2 destination;
        public bool moving = false;
        double frameTimer, frameInterval;
        int frame;
        Rectangle animationRec;
        public static Rectangle destRec; //Används i Tilemap
        public static Rectangle pacManHitBox;
        private int speed = 2;
        SpriteEffects pacManFx = SpriteEffects.None;
        float rotation = 0;
        public PacManChar(Texture2D texture, Vector2 position, int health) : base(texture, position)
        {
            this.health = health;

            //Animation
            frameInterval = 120;
            frameTimer = 120;
            animationRec = new Rectangle(0, 0, 40, 40);
        }
        public void ChangeDirection(Vector2 dir)
        {
            direction = dir;
            Vector2 newDestination = position + dir * TileMap.floortileWidth;
            if (!TileMap.GetTileAtPosition(newDestination))
            {
                destination = newDestination;
                moving = true;
                speed = 2;
            }
        }

        public void Update(GameTime gameTime)
        {
            frameTimer -= gameTime.ElapsedGameTime.TotalMilliseconds; 
            if (frameTimer <= 0)
            {
                frameTimer = frameInterval;
                frame++;
                animationRec.X = (frame % 4) * 40;
            }
            if (!moving)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Right))
                {
                    ChangeDirection(new Vector2(1, 0));
                    pacManFx = SpriteEffects.None;
                    rotation = MathHelper.ToRadians(0);
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Left))
                {
                    ChangeDirection(new Vector2(-1, 0));
                    rotation = MathHelper.ToRadians(0);
                    pacManFx = SpriteEffects.FlipHorizontally;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Up))
                {
                    ChangeDirection(new Vector2(0, -1));
                    rotation = MathHelper.ToRadians(-90);
                    pacManFx = SpriteEffects.None;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Down))
                {
                    ChangeDirection(new Vector2(0, 1));
                    rotation = MathHelper.ToRadians(90);
                    pacManFx = SpriteEffects.None;
                }
            }
            else
            {
                position += direction * speed;
                if (Vector2.Distance(position, destination) < 1)
                {
                    position = destination;
                    moving = false;
                }
            }

            destRec = new Rectangle((int)position.X + TileMap.floortileWidth / 2, (int)position.Y + TileMap.floortileHeight / 2, TileMap.floortileWidth, TileMap.floortileHeight);
            pacManHitBox = new Rectangle((int)position.X, (int)position.Y, TileMap.floortileWidth, TileMap.floortileHeight);
        }
        public override void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(texture, destRec, animationRec, Color.White, rotation, new Vector2(20, 20), pacManFx, 1);
        }

    }
}