using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PacManFinal
{
    public class Ghost : GameObject
    {
        Rectangle sourceRectangle;
        public Rectangle destinationRectangle;
        Vector2 gPosition;
        int frame;
        double frameTimer, frameInterval;
        Vector2 direction;
        Vector2 destination;
        bool moving = false;
        int speed = 2;
        Random random = new Random();
        ghostDirection currentDirection;

        enum ghostDirection
        {
            Up = 1,
            Down = 2,
            Left = 3,
            Right = 4
        }

        public Ghost(Texture2D texture, Vector2 position, Rectangle destinationRectangle) : base(texture, position)
        {
            this.destinationRectangle = destinationRectangle;
            currentDirection = ghostDirection.Down;
        }

        public void LoadContent()
        {
            sourceRectangle = new Rectangle(0, 16, 16, 16);
            frameTimer = 90;
            frameInterval = 90;

        }


        public void GhostStartAnimation(GameTime gameTime)
        {
            frameTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;
            if (frameTimer <= 0)
            {
                frameTimer = frameInterval;
                frame++;
                sourceRectangle.X = (frame % 8) * 16;
            }
        }
        public void Update(GameTime gameTime)
        {
            if (!moving)
            {
                switch (currentDirection)
                {
                    case ghostDirection.Down:
                        ChangeDirection(new Vector2(0, -1));
                        break;
                    case ghostDirection.Up:
                        ChangeDirection(new Vector2(0, 1));
                        break;
                    case ghostDirection.Left:
                        ChangeDirection(new Vector2(-1, 0));
                        break;
                    case ghostDirection.Right:
                        ChangeDirection(new Vector2(1, 0));

                        break;
                }
                
            }
            else
            {
                position += direction * speed;// * (float)gameTime.ElapsedGameTime.TotalSeconds;

                //Check if we are near enough to the destination
                if (Vector2.Distance(position, destination) < 1)
                {
                    position = destination;
                    moving = false;
                }
            }
            destinationRectangle = new Rectangle((int)position.X, (int)position.Y, TileMap.floortileWidth, TileMap.floortileHeight);

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

            else if (TileMap.GetTileAtPosition(newDestination))
            {
                int randomDirection = random.Next(1, 5);
                currentDirection = (ghostDirection)randomDirection;
            }
        }

        public override void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);
        }

    }
}
