using Game.Screens;
using System;

namespace Game
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = GameSettings.Title;
            Console.CursorVisible = false;

            GuiUpdater.ShowCompanyNameScreen();
            GuiUpdater.ShowTitleScreen();

            MainMenuScreen menu = new MainMenuScreen();
            menu.Show();
        }
    }
}
