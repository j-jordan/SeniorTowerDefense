#region File Description
//-----------------------------------------------------------------------------
// Game.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

using System;
using GameStateManagement;
using Microsoft.Xna.Framework;

namespace SeniorTowerDefense
{
    /// <summary>
    /// Sample showing how to manage different game states, with transitions
    /// between menu screens, a loading screen, the game itself, and a pause
    /// menu. This main game class is extremely simple: all the interesting
    /// stuff happens in the ScreenManager component.
    /// </summary>
    public class TowerDefenseGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        ScreenManager screenManager;
        ScreenFactory screenFactory;

        /// <summary>
        /// The main game constructor.
        /// </summary>
        public TowerDefenseGame()
        {
            Content.RootDirectory = "Content";

            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;

            TargetElapsedTime = TimeSpan.FromTicks(333333);


            // Create the screen factory and add it to the Services
            screenFactory = new ScreenFactory();
            Services.AddService(typeof(IScreenFactory), screenFactory);

            // Create the screen manager component.
            screenManager = new ScreenManager(this);
            Components.Add(screenManager);

            // On Windows and Xbox we just add the initial screens
            AddInitialScreens();

        }

        private void AddInitialScreens()
        {
            // Activate the first screens.
            

            screenManager.AddScreen(new TitleScreen(), null);

        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.Black);

            // The real drawing happens inside the screen manager component.
            base.Draw(gameTime);
        }
    }
}
