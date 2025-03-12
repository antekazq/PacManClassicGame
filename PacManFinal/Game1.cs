using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using System.IO;
using System;


namespace PacManFinal
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public GameState gameState;
        KeyboardState keyboardState;
        bool win;
        int points;
        public static int floortileWidth, floortileHeight;
        public static Tile[,] tiles;
        private TileMap map;
        PacManChar pacMan;
        public static List<Ghost> ghostList;

        public enum GameState
        {
            Start,
            Play,
            End
        }

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            map = new TileMap();
            ghostList = new List<Ghost>();
            TextureLoad.Load(Content);
            map.LoadContent();
            pacMan = new PacManChar(TextureLoad.pacMan, map.pacManPosition, 3);

            foreach (Ghost ghost in ghostList)
            {
                ghost.LoadContent();
            }

            floortileWidth = TextureLoad.floortile.Width;
            floortileHeight = TextureLoad.floortile.Height;

            _graphics.PreferredBackBufferWidth = floortileWidth * TileMap.tiles.GetLength(0) - 2 * floortileWidth; //Bredden på fönstret
            _graphics.PreferredBackBufferHeight = floortileHeight * TileMap.tiles.GetLength(1); //Höjden på fönstret
            _graphics.ApplyChanges(); //Nu har spelfönstret ändrats

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            keyboardState = Keyboard.GetState();
            switch (gameState)
            {
                case GameState.Start:
                    foreach (Ghost ghost in ghostList)
                    {
                        ghost.GhostStartAnimation(gameTime);
                    }
                    if (keyboardState.IsKeyDown(Keys.Enter))
                    {
                        gameState = GameState.Play;
                    }
                    break;

                case GameState.Play:
                    //map.Update(gameTime);
                    pacMan.Update(gameTime);
                    foreach (Ghost ghost in ghostList)
                    {
                        ghost.GhostStartAnimation(gameTime);
                        ghost.Update(gameTime);
                        if (ghost.destinationRectangle.Intersects(PacManChar.pacManHitBox))
                        {
                            ghostList.Remove(ghost);
                            pacMan.health = pacMan.health - 1;
                            break;
                        }
                        if (pacMan.health == 0)
                        {
                            win = false;
                            gameState = GameState.End;
                        }
                    }
                    List<Food> isFoodShowingList = new List<Food>();
                    foreach (Food food in TileMap.foodList)
                    {
                        if (PacManChar.pacManHitBox.Intersects(food.hitBox) && food.isShowing)
                        {
                            points++;
                            food.isShowing = false;
                        }
                        if (food.isShowing)
                            isFoodShowingList.Add(food);
                    }
                    if (isFoodShowingList.Count == 0)
                    {
                        win = true;
                        gameState = GameState.End;
                    }    
                    else
                        isFoodShowingList.Clear();

                    break;
                case GameState.End:
                    break;
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            switch (gameState)
            {
                case GameState.Start:
                    foreach (Ghost ghost in ghostList)
                    {
                        ghost.Draw(_spriteBatch);
                    }
                    _spriteBatch.DrawString(TextureLoad.spriteFont, "Press Enter to start the game", new Vector2(floortileWidth * TileMap.tiles.GetLength(0) / 2 - 190, floortileHeight * TileMap.tiles.GetLength(1) / 2), Color.White);
                    break;

                case GameState.Play:
                    map.Draw(_spriteBatch);
                    pacMan.Draw(_spriteBatch);

                    foreach (Food food in TileMap.foodList)
                        food.Draw(_spriteBatch);
                    foreach (Ghost ghost in ghostList)
                        ghost.Draw(_spriteBatch);

                    _spriteBatch.DrawString(TextureLoad.spriteFont, "Lives: " + pacMan.health, Vector2.Zero, Color.Red);
                    _spriteBatch.DrawString(TextureLoad.spriteFont, "Points: " + points, new Vector2(1220, 0), Color.Red);
                    
                    break;
                case GameState.End:
                    if (!win)
                        _spriteBatch.DrawString(TextureLoad.spriteFont, "Game Over, you scored "+points+ " points", new Vector2(floortileWidth * TileMap.tiles.GetLength(0) / 2 - 90, floortileHeight * TileMap.tiles.GetLength(1) / 2), Color.White);
                    else
                        _spriteBatch.DrawString(TextureLoad.spriteFont, "You won! Points: " + points, new Vector2(floortileWidth * TileMap.tiles.GetLength(0) / 2 - 110, floortileHeight * TileMap.tiles.GetLength(1) / 2), Color.White);
                    break;
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
