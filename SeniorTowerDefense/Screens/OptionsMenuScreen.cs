#region File Description
//-----------------------------------------------------------------------------
// OptionsMenuScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using Microsoft.Xna.Framework;
#endregion

namespace SeniorTowerDefense
{
    /// <summary>
    /// The options screen is brought up over the top of the main menu
    /// screen, and gives the user a chance to configure the game
    /// in various hopefully useful ways.
    /// </summary>
    class OptionsMenuScreen : MenuScreen
    {
        #region Fields

        MenuEntry albumMenuEntry;
        MenuEntry languageMenuEntry;
        MenuEntry fullScreenMenuEntry;
        MenuEntry volumeMenuEntry;

        static string[] albums = { "The College Dropout" , "Late Registration" , "Graduation" , "808s & Heartbreak" , "My Beatiful Dark Twisted Fantasy" , "Watch the Throne (ft. Jay-Z)" , "Yeezus"};
        static int currentAlbum = 0;

        static string[] languages = { "English", "Spanish", "C#" };
        static int currentLanguage = 0;

        static bool fullScreen = false;

        static int volume = 100;

        #endregion

        #region Initialization


        /// <summary>
        /// Constructor.
        /// </summary>
        public OptionsMenuScreen()
            : base("Options")
        {
            // Create our menu entries.
            albumMenuEntry = new MenuEntry(string.Empty);
            languageMenuEntry = new MenuEntry(string.Empty);
            fullScreenMenuEntry = new MenuEntry(string.Empty);
            volumeMenuEntry = new MenuEntry(string.Empty);

            SetMenuEntryText();

            MenuEntry back = new MenuEntry("Back");

            // Hook up menu event handlers.
            albumMenuEntry.Selected += UngulateMenuEntrySelected;
            languageMenuEntry.Selected += LanguageMenuEntrySelected;
            fullScreenMenuEntry.Selected += FullScreenMenuEntrySelected;
            volumeMenuEntry.Selected += ElfMenuEntrySelected;
            back.Selected += OnCancel;
            
            // Add entries to the menu.
            MenuEntries.Add(albumMenuEntry);
            MenuEntries.Add(languageMenuEntry);
            MenuEntries.Add(fullScreenMenuEntry);
            MenuEntries.Add(volumeMenuEntry);
            MenuEntries.Add(back);
        }


        /// <summary>
        /// Fills in the latest values for the options screen menu text.
        /// </summary>
        void SetMenuEntryText()
        {
            albumMenuEntry.Text = "Album: " + albums[currentAlbum];
            languageMenuEntry.Text = "Language: " + languages[currentLanguage];
            fullScreenMenuEntry.Text = "Fullscreen: " + (fullScreen ? "on" : "off");
            volumeMenuEntry.Text = "Volume: " + volume;
        }


        #endregion

        #region Handle Input


        /// <summary>
        /// Event handler for when the Ungulate menu entry is selected.
        /// </summary>
        void UngulateMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            currentAlbum = (currentAlbum + 1) % albums.Length;

            SetMenuEntryText();
        }


        /// <summary>
        /// Event handler for when the Language menu entry is selected.
        /// </summary>
        void LanguageMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            currentLanguage = (currentLanguage + 1) % languages.Length;

            SetMenuEntryText();
        }


        /// <summary>
        /// Event handler for when the Frobnicate menu entry is selected.
        /// </summary>
        void FullScreenMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            fullScreen = !fullScreen;
            
            SetMenuEntryText();
        }


        /// <summary>
        /// Event handler for when the Elf menu entry is selected.
        /// </summary>
        void ElfMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            volume++;

            SetMenuEntryText();
        }


        #endregion
    }
}
