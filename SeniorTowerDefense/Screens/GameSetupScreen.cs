using System;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameStateManagement;

namespace SeniorTowerDefense
{
    class GameSetupScreen : GameScreen
    {
        ContentManager content;
        SpriteFont gameFont;
        InputAction pauseAction;
        //Play
        Vector2 playButtonTextPosition = new Vector2(500,500);
        Rectangle playButtonRect = new Rectangle(500, 500, 125, 50);
        Texture2D playButtonTexture;

        // Cursor
        Texture2D cursor;
        Rectangle cursorRect;

        // Mouse        
        MouseState mouse;
        MouseState oldMouse;

        public GameSetupScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);

            pauseAction = new InputAction(
                new Buttons[] { Buttons.Start, Buttons.Back },
                new Keys[] { Keys.Escape },
                true);

            cursorRect = new Rectangle(0, 0, 30, 30);
        }

        public override void Activate(bool instancePreserved)
        {
            if (!instancePreserved)
            {
                if (content == null)
                    content = new ContentManager(ScreenManager.Game.Services, "Content");

                gameFont = content.Load<SpriteFont>("gamefont");
                cursor = content.Load<Texture2D>("cursor");
                playButtonTexture = content.Load<Texture2D>("background");

                ScreenManager.Game.ResetElapsedTime();
            }

        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus,
                                                       bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, false);
            
            // Look up inputs for the active player profile.
            PlayerIndex playerIndex = ControllingPlayer.Value;

            if (IsActive)
            {
                cursorFollowMouse();

                
                if (mouse.LeftButton == ButtonState.Pressed  && oldMouse.LeftButton == ButtonState.Released 
                    && mouse.X > playButtonRect.X
                    && mouse.X < playButtonRect.X + playButtonRect.Width
                    && mouse.Y > playButtonRect.Y
                    && mouse.Y < playButtonRect.Y + playButtonRect.Height)
                {
                    LoadingScreen.Load(ScreenManager, true, playerIndex,
                              new GameplayScreen());
                }
                
            }
        }

        private void cursorFollowMouse()
        {
            mouse = Mouse.GetState();
            cursorRect.X = mouse.X;
            cursorRect.Y = mouse.Y;
        }

        public override void HandleInput(GameTime gameTime, InputState input)
        {
            if (input == null)
                throw new ArgumentNullException("input");

            // Look up inputs for the active player profile.
            int playerIndex = (int)ControllingPlayer.Value;

            KeyboardState keyboardState = input.CurrentKeyboardStates[playerIndex];
            MouseState mouseState = Mouse.GetState();


        }



        public override void Draw(GameTime gameTime)
        {
            // This game has a blue background. Why? Because!
            ScreenManager.GraphicsDevice.Clear(ClearOptions.Target,
                                               Color.CornflowerBlue, 0, 0);

            // Our player and enemy are both actually just text strings.
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

            spriteBatch.Begin();

            spriteBatch.Draw(playButtonTexture, playButtonRect, Color.White);
            spriteBatch.DrawString(gameFont, "PLAY!", playButtonTextPosition, Color.Chartreuse);
            spriteBatch.Draw(cursor, cursorRect, Color.White);

            spriteBatch.End();


        }
    }

}
