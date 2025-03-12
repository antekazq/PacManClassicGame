using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using System.IO;
using System;

namespace PacManFinal
{
    public class TileMap
    {

        public StreamReader sr;
        public List<string> stringList;
        public static Tile[,] tiles;
        public static int floortileWidth, floortileHeight;
        public static List<Food> foodList = new List<Food>();
        public Rectangle ghostDestinationRectangle;
        public Vector2 ghostPosition;
        public Vector2 pacManPosition;
        public void LoadContent()
        {
            floortileWidth = TextureLoad.floortile.Width;
            floortileHeight = TextureLoad.floortile.Height;

            //Läs in fil
            StreamReader sr = new StreamReader(@"map.txt");
            stringList = new List<string>();
            while (!sr.EndOfStream)
            {
                stringList.Add(sr.ReadLine());
            }
            sr.Close();

            tiles = new Tile[stringList[0].Length, stringList.Count];
            for (int i = 0; i < tiles.GetLength(0); i++)
            {
                for (int j = 0; j < tiles.GetLength(1); j++)
                {
                    if (stringList[j][i] == 'f')
                    {
                        tiles[i, j] = new Tile(TextureLoad.floortile, new Vector2(floortileWidth * i - floortileWidth, floortileHeight * j), false);
                        Vector2 position = new Vector2(floortileWidth * i -floortileWidth + floortileWidth / 2 - TextureLoad.foodTexture.Width / 2, floortileHeight * j + floortileHeight / 2 - TextureLoad.foodTexture.Width / 2);
                        foodList.Add(new Food(TextureLoad.foodTexture, position));
                    }
                    else if (stringList[j][i] == 'w')
                    {
                        tiles[i, j] = new Tile(TextureLoad.walltile, new Vector2(floortileWidth * i - floortileWidth, floortileHeight * j), true);
                    }
                    else if (stringList[j][i] == 'p')
                    {
                        tiles[i, j] = new Tile(TextureLoad.floortile, new Vector2(floortileWidth * i - floortileWidth, floortileHeight * j), false);
                        pacManPosition = new Vector2(floortileWidth * i - floortileWidth, floortileHeight * j);
                        //PacMan skapas i Game1
                    }
                    else if (stringList[j][i] == 'g')
                    {
                        tiles[i, j] = new Tile(TextureLoad.floortile, new Vector2(floortileWidth * i - floortileWidth , floortileHeight * j), false);
                        ghostDestinationRectangle = new Rectangle(floortileWidth * i, floortileHeight * j, TextureLoad.floortile.Width, TextureLoad.floortile.Height);
                        ghostPosition = new Vector2(floortileWidth * i, floortileHeight * j);
                        Game1.ghostList.Add(new Ghost(TextureLoad._spriteSheet, ghostPosition, ghostDestinationRectangle));

                        Vector2 position = new Vector2(floortileWidth * i - floortileWidth + floortileWidth / 2 - TextureLoad.foodTexture.Width / 2, floortileHeight * j + floortileHeight / 2 - TextureLoad.foodTexture.Width / 2);
                        foodList.Add(new Food(TextureLoad.foodTexture, position));
                    }
                }
            }
        }
        public static bool GetTileAtPosition(Vector2 position)
        {
            return tiles[(int)position.X / floortileWidth +1, (int)position.Y / floortileHeight].wall;
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            foreach (Tile t in tiles)
            {
                t.Draw(_spriteBatch);
            }
        }
    }
} 
