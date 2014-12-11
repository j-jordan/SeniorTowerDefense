#region File Description
//-----------------------------------------------------------------------------
// GameplayScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameStateManagement;
using System.Collections.Generic;
#endregion

namespace SeniorTowerDefense
{
    /// <summary>
    /// This screen implements the actual game logic. It is just a
    /// placeholder to get the idea across: you'll probably want to
    /// put some more interesting gameplay in here!
    /// </summary>
    class GameplayScreen : GameScreen
    {
        #region Fields

        ContentManager content;
        SpriteFont gameFont;
        SpriteFont hudFont;

        //Game flags
        bool enemiesAlive = false;

        GridWorld gridWorld;

        int healthText = 100;
        int moneyText = 1000;    

        //offsets
        int hudOffSetX = 6;
        int hudOffSetY = 110;

        // Cursor
        Texture2D cursor;
        Rectangle cursorRect;

        // Mouse        
        MouseState mouse;
        MouseState oldMouse;

        //Enemies        
        List<Enemy> enemies;
        int enemyCount;
        TimeSpan enemySpawnTime = new TimeSpan(0, 0, 1);
        TimeSpan countTime;
        Texture2D basicEnemyTexture;

        //Heads Up Display
        Texture2D hudTexture;
        Rectangle hudRect;

        //Background
        Texture2D mapTexture;
        Rectangle mapRectangle;

        //Next round button
        Texture2D nextButtonTexture;
        Rectangle nextButtonRect;

        //Grid Box rekt
        Texture2D gridBoxText;
        Vector2 gridBoxPos;

        //Turret
        List<Tower> towers;
        Texture2D towerTexture;
        Rectangle towerRect;
        Texture2D bulletTexture;

        int round;

        Random random = new Random();

        float pauseAlpha;

        InputAction pauseAction;

        #endregion

        #region Initialization


        /// <summary>
        /// Constructor.
        /// </summary>
        public GameplayScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);

            cursorRect = new Rectangle(0, 0, 15, 15);
            hudRect = new Rectangle(0, 0, 1280, 720);
            nextButtonRect = new Rectangle(1200, 640, 69, 72);
            mapRectangle = new Rectangle(hudOffSetX, hudOffSetY, 1280-hudOffSetX, 720 - hudOffSetY);
            gridWorld = new GridWorld();

            //Towers
            towers = new List<Tower>(100);

            //Enemies
            enemies = new List<Enemy>(100);            
            round = 0;

            pauseAction = new InputAction(
                new Buttons[] { Buttons.Start, Buttons.Back },
                new Keys[] { Keys.Escape },
                true);

        }


        /// <summary>
        /// Load graphics content for the game.
        /// </summary>
        public override void Activate(bool instancePreserved)
        {
            if (!instancePreserved)
            {
                if (content == null)
                    content = new ContentManager(ScreenManager.Game.Services, "Content");

                gameFont = content.Load<SpriteFont>("gamefont");
                hudFont = content.Load<SpriteFont>("HUD_FONT");
                
                cursor = content.Load<Texture2D>("cursor");
                basicEnemyTexture = content.Load<Texture2D>("Minion");
                towerTexture = content.Load<Texture2D>("Kanye Turret"); 
                hudTexture = content.Load<Texture2D>("HUD");
                nextButtonTexture = content.Load<Texture2D>("Next");
                mapTexture = content.Load<Texture2D>("Map");
                gridBoxText = content.Load<Texture2D>("Grid space");
                bulletTexture = content.Load<Texture2D>("Bullet");

                ScreenManager.Game.ResetElapsedTime();
            }

        }


        public override void Deactivate()
        {

            base.Deactivate();
        }


        /// <summary>
        /// Unload graphics content used by the game.
        /// </summary>
        public override void Unload()
        {
            content.Unload();
        }


        #endregion

        #region Update and Draw


        /// <summary>
        /// Updates the state of the game. This method checks the GameScreen.IsActive
        /// property, so the game will stop updating when the pause menu is active,
        /// or if you tab away to a different application.
        /// </summary>
        public override void Update(GameTime gameTime, bool otherScreenHasFocus,
                                                       bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, false);

            // Gradually fade in or out depending on whether we are covered by the pause screen.
            if (coveredByOtherScreen)
                pauseAlpha = Math.Min(pauseAlpha + 1f / 32, 1);
            else
                pauseAlpha = Math.Max(pauseAlpha - 1f / 32, 0);

            if (IsActive)
            {
                countTime += gameTime.ElapsedGameTime;
                updateEnemies(gameTime);
                highlightGridAroundMouse();
                updateTowers();
            }
        }

        private void updateTowers()
        {
            towers.ForEach(delegate(Tower tower)
                {
                   
                });
        }

        private void updateEnemies(GameTime gameTime)
        {
            if (countTime > enemySpawnTime)
            {
                countTime = TimeSpan.Zero; 
                    
                if(enemyCount < (round * 5))
                {
                    Enemy badGuy = new Enemy(new Vector2(gridWorld.getMapSource().X * 45 + hudOffSetX, gridWorld.getMapSource().Y * 45 + hudOffSetY),
                        new Vector2(gridWorld.getMapDestination().X * 45 + hudOffSetX, gridWorld.getMapDestination().Y * 45 + hudOffSetY));
                    enemies.Add(badGuy);
                    enemyCount++;
                    enemiesAlive = true;
                }
            }

            enemies.ForEach(delegate(Enemy enemy)
            {
                if(enemy != null)
                {
                    enemy.moveTowardsDestination();      //ENEMY MOVES

                    towers.ForEach(delegate(Tower t)     // TOWERS SHOOT
                    {
                        t.shootBullet(enemy.Position(), gameTime); 
                        t.updateBullets();
                    });
                }
                if (enemy.reachedDestination())
                {
                    enemies.Remove(enemy);
                    healthText -= 20;
                }
            });

            if(enemies.Count == 0)
            {
                enemiesAlive = false;
            }
        }

        /// <summary>
        /// Lets the game respond to player input. Unlike the Update method,
        /// this will only be called when the gameplay screen is active.
        /// </summary>
        public override void HandleInput(GameTime gameTime, InputState input)
        {
            if (input == null)
                throw new ArgumentNullException("input");

            // Look up inputs for the active player profile.
            int playerIndex = (int)ControllingPlayer.Value;

            KeyboardState keyboardState = input.CurrentKeyboardStates[playerIndex];
            GamePadState gamePadState = input.CurrentGamePadStates[playerIndex];
            

            // The game pauses either if the user presses the pause button, or if
            // they unplug the active gamepad. This requires us to keep track of
            // whether a gamepad was ever plugged in, because we don't want to pause
            // on PC if they are playing with a keyboard and have no gamepad at all!
            bool gamePadDisconnected = !gamePadState.IsConnected &&
                                       input.GamePadWasConnected[playerIndex];

            PlayerIndex player;
            if (pauseAction.Evaluate(input, ControllingPlayer, out player) || gamePadDisconnected)
            {
                ScreenManager.AddScreen(new PauseMenuScreen(), ControllingPlayer);
            }
            else
            {
                cursorFollowMouse();
            }
            if(mouse.LeftButton == ButtonState.Released  && oldMouse.LeftButton == ButtonState.Pressed && nextButtonRect.Contains(mouse.X, mouse.Y) && !enemiesAlive)
            {
                round++;
            }
            oldMouse = mouse;            
            mouse = Mouse.GetState();

        }

        /// <summary>
        ///  Check the nearest grid position at the current coordinates
        /// </summary>
        public Vector2 checkMouseGridPosition()
        {
            for (int i = hudOffSetX; i < 1280-hudOffSetX; i++)
            {
                for(int j = hudOffSetY; j <720-hudOffSetX ; j++)
                {
                    if(mouse.X >= i && mouse.X <= i + 45 && mouse.Y >= j && mouse.Y <= j +45)
                    {
                        int Xpos; //x point of the square with rounding
                        int Ypos; //y ''

                        Xpos = (i / 45);
                        Ypos = (j / 45);

                        if (Xpos < 28 && Xpos > -1 && Ypos < 13 && Ypos > -1)
                        {
                            return new Vector2(Xpos, Ypos);
                        }
                    }                    
                }
            }
            
            return new Vector2(-42, -42);
        }

        /// <summary>
        ///     Highlights the grid square around the mouse
        /// </summary>
        public void highlightGridAroundMouse()
        {
            gridBoxPos = checkMouseGridPosition();
    
            oldMouse = mouse;      
            mouse = Mouse.GetState();       

            if (mouse.LeftButton == ButtonState.Released && oldMouse.LeftButton == ButtonState.Pressed && moneyText >= 200 && !enemiesAlive)
            {
                if(gridWorld.setTowerPosition((int)gridBoxPos.X, (int)gridBoxPos.Y))
                {
                    moneyText -= 200;
                    towers.Add(new Tower(gridBoxPos));
                }
            }
            gridBoxPos.X = gridBoxPos.X * 45 + hudOffSetX;
            gridBoxPos.Y = gridBoxPos.Y * 45 + hudOffSetY;
        }

        /// <summary>
        /// Draws the gameplay screen.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            // This game has a blue background. Why? Because!
            ScreenManager.GraphicsDevice.Clear(ClearOptions.Target,
                                               Color.Black, 0, 0);

            // Our player and enemy are both actually just text strings.
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

            spriteBatch.Begin();

            spriteBatch.Draw(mapTexture, mapRectangle, Color.White); 
            
            spriteBatch.Draw(gridBoxText, gridBoxPos, Color.White);

            spriteBatch.Draw(hudTexture, hudRect, Color.White);

            spriteBatch.DrawString(hudFont, healthText +"", new Vector2(160,20), Color.Red);

            spriteBatch.DrawString(hudFont, "$" + moneyText, new Vector2(470, 20), Color.Black);

            spriteBatch.DrawString(hudFont, "ROUND: " + round, new Vector2(800, 20), Color.Black);

            spriteBatch.Draw(nextButtonTexture, nextButtonRect, Color.White);           

            spriteBatch.Draw(cursor, cursorRect, Color.White);

            for (int i = 0; i < gridWorld.getMapGirth(); i++)
            {
                for (int j = 0; j < gridWorld.getMapHeight(); j++)
                {
                    switch(gridWorld.getMapEntity(i,j))
                    {
                        case (GridWorld.MapEntity.Destination):
                            break;
                        case (GridWorld.MapEntity.Empty):
                            break;
                        case (GridWorld.MapEntity.Enemy):
                            break;
                        case (GridWorld.MapEntity.Obstical):
                            break;
                        case (GridWorld.MapEntity.Source):
                            break;
                        case (GridWorld.MapEntity.Tower):
                            spriteBatch.Draw(towerTexture, new Rectangle((i*45) + hudOffSetX, (j*45) + hudOffSetY,towerTexture.Width,towerTexture.Height), Color.White);
                            break;
                    }
                }
            }

            towers.ForEach(delegate(Tower tower)
                {
                    List<Vector2> bulletPositions = tower.getBulletPositions();

                    bulletPositions.ForEach(delegate(Vector2 pos)
                    {
                        spriteBatch.Draw(bulletTexture, pos, Color.White);
                    });
                });

            foreach(Enemy enemy in enemies)
            {
                if (enemy != null)
                {
                    spriteBatch.Draw(basicEnemyTexture, enemy.Position(), Color.White);
                }
            }

            spriteBatch.End();

            // If the game is transitioning on or off, fade it out to black.
            if (TransitionPosition > 0 || pauseAlpha > 0)
            {
                float alpha = MathHelper.Lerp(1f - TransitionAlpha, 1f, pauseAlpha / 2);

                ScreenManager.FadeBackBufferToBlack(alpha);
            }
        }


        #endregion


        private void cursorFollowMouse()
        {
            mouse = Mouse.GetState();
            cursorRect.X = mouse.X;
            cursorRect.Y = mouse.Y;
        }
    }
}
