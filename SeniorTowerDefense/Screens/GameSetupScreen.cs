using System;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameStateManagement;

namespace SeniorTowerDefense
{
    class GameSetupScreen : MenuScreen
    {
        ContentManager content;
        SpriteFont gameFont;
        InputAction pauseAction;

        //Play
        Vector2 playButtonTextPosition = new Vector2(1000,650);
        Rectangle playButtonRect = new Rectangle(1000, 650, 125, 50);
        Texture2D playButtonTexture;

        // Cursor
        Texture2D cursor;
        Rectangle cursorRect;

        // Mouse        
        MouseState mouse;
        MouseState oldMouse;

        //MenuEntries
        MenuEntry classMenuEntry;
        MenuEntry mapMenuEntry;
        MenuEntry powerLevelMenuEntry;
        MenuEntry playMenuEntry;

        static string[] classes = { "Peasant", "Engi", "Sayian" };
        static int currentClass = 0;

        static string[] maps = { "Green Fields", "Rock Outcrop", "Hell" };
        static int currentMap = 0;

        static int powerLevel = 9001;

       
        public GameSetupScreen() :
            base("Setup")
        {
            //Menu Entries
            classMenuEntry = new MenuEntry(string.Empty);
            mapMenuEntry = new MenuEntry(string.Empty);
            powerLevelMenuEntry = new MenuEntry(string.Empty);
            playMenuEntry = new MenuEntry(string.Empty);

            SetMenuEntryText();

            //Menu Event Handlers
            classMenuEntry.Selected += classMenuEntry_Selected;
            mapMenuEntry.Selected += mapMenuEntry_Selected;
            powerLevelMenuEntry.Selected += powerLevelMenuEntry_Selected;
            playMenuEntry.Selected += playMenuEntry_Selected;

            //Add entries to menu
            MenuEntries.Add(classMenuEntry);
            MenuEntries.Add(mapMenuEntry);
            MenuEntries.Add(powerLevelMenuEntry);
            MenuEntries.Add(playMenuEntry);

            TransitionOnTime = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);

            pauseAction = new InputAction(
                new Buttons[] { Buttons.Start, Buttons.Back },
                new Keys[] { Keys.Escape },
                true);

          
        }

        void playMenuEntry_Selected(object sender, PlayerIndexEventArgs e)
        {
            // Look up inputs for the active player profile.
            PlayerIndex playerIndex = ControllingPlayer.Value;

            LoadingScreen.Load(ScreenManager, true, playerIndex,
                              new GameplayScreen());
        }
       
        void SetMenuEntryText()
        {
            classMenuEntry.Text = "Class: " + classes[currentClass];
            mapMenuEntry.Text = "Map: " + maps[currentMap];
            powerLevelMenuEntry.Text = "PowerLevel: " + powerLevel;
            playMenuEntry.Text = "Play"; 
        }

        void powerLevelMenuEntry_Selected(object sender, PlayerIndexEventArgs e)
        {
            powerLevel++;

            SetMenuEntryText();
        }

        void mapMenuEntry_Selected(object sender, PlayerIndexEventArgs e)
        {
            currentMap = (currentMap + 1) % maps.Length;

            SetMenuEntryText();
        }

        void classMenuEntry_Selected(object sender, PlayerIndexEventArgs e)
        {
            currentClass = (currentClass + 1) % maps.Length;

            SetMenuEntryText();
        }
 /*      
        public override void Activate(bool instancePreserved)
        {
            if (!instancePreserved)
            {
                if (content == null)
                    content = new ContentManager(ScreenManager.Game.Services, "Content");

                gameFont = content.Load<SpriteFont>("gamefont");
                
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
           // ScreenManager.GraphicsDevice.Clear(ClearOptions.Target,
           //                                    Color.CornflowerBlue, 0, 0);

      
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

            spriteBatch.Begin();

            spriteBatch.Draw(playButtonTexture, playButtonRect, Color.White);
            spriteBatch.DrawString(gameFont, "PLAY!", playButtonTextPosition, Color.Chartreuse);
            

            spriteBatch.End();
        }
         */
        
    }

}
