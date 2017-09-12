using Game.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Game
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = GameSettings.Title;
            Console.CursorVisible = false;

            GuiUpdater.ShowCompanyNameScreen();
            MusicPlayer.GetInstance().Play();
            GuiUpdater.ShowTitleScreen();
            GuiUpdater.ShowMainMenu();

            MusicPlayer.GetInstance().Stop();
        }
    }
}
