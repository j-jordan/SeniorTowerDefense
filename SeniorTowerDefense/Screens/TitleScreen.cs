using System;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using GameStateManagement;

namespace SeniorTowerDefense
{
    class TitleScreen : GameScreen
    {
        Texture2D TitleScreenBackground;
        ContentManager content;
        bool backgroundDrawn;

        public TitleScreen ()
        {
            TransitionOnTime = TimeSpan.FromSeconds(0.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
            backgroundDrawn = false;
        }
        public override void Activate(bool instancePreserved)
        {
            if (!instancePreserved)
            {
                if (content == null)
                    content = new ContentManager(ScreenManager.Game.Services, "Content");

                TitleScreenBackground = content.Load<Texture2D>("TitleScreenBackground");

                ScreenManager.Game.ResetElapsedTime();
            }

        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus,
                                                      bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, false);

            // Look up inputs for the active player profile.
          //  PlayerIndex playerIndex = ControllingPlayer.Value;

            if (IsActive)
            {
                
                if (backgroundDrawn)
                {
                    Thread.Sleep(3000);
                    ScreenManager.AddScreen(new BackgroundScreen(), null);
                    ScreenManager.AddScreen(new MainMenuScreen(), null);
                  //  LoadingScreen.Load(ScreenManager, true, null,
                 //             new MainMenuScreen());
                    
                }
               // ScreenManager.AddScreen(new MainMenuScreen(), null);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            // This game has a blue background. Why? Because!
            ScreenManager.GraphicsDevice.Clear(ClearOptions.Target,
                                               Color.Black, 0, 0);

            // Our player and enemy are both actually just text strings.
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

            spriteBatch.Begin();

            spriteBatch.Draw(TitleScreenBackground, new Vector2((ScreenManager.GraphicsDevice.Viewport.Width / 2) - TitleScreenBackground.Width / 2, 0), Color.White);

            spriteBatch.End();

            backgroundDrawn = true;
        }
    }
}
