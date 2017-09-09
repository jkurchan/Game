using Game.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Game
{
    static class GuiUpdater
    {
        public static void SetLevel(int level)
        {
            string text = "Level: " + level;
            Console.SetCursorPosition(109, 1);
            Console.ForegroundColor = GameSettings.ObstacleColor;
            Console.Write(text);
        }

        public static void SetPoints(int points)
        {
            string text = "Points: " + points;
            Console.SetCursorPosition(109, 2);
            Console.ForegroundColor = GameSettings.ObstacleColor;
            Console.Write(text);
        }

        public static void SetLives(int lives)
        {
            string text = "Lives: " + lives;
            Console.SetCursorPosition(109, 3);
            Console.ForegroundColor = GameSettings.ObstacleColor;
            Console.Write(text);
        }

        public static void ShowTopStrip()
        {
            string text = "Build: " + Application.Version + ", " +
                "[Esc] Exit, " +
                "[F1] Tips " + (GameSettings.TipsOn ? "ON" : "OFF");
            Console.SetCursorPosition(2, 1);
            Console.Write(text);
        }

        public static void ShowCompanyNameScreen()
        {
            Console.BackgroundColor = GameSettings.CompanyNameBackground;
            Console.Clear();
            Console.ForegroundColor = GameSettings.CompanyNameColor;
            Console.SetCursorPosition(37, 5);
            Console.Write(" ██ ▄█▀▄▄▄       █     █░ ▄▄▄       ██▓ ██▓");
            Console.SetCursorPosition(37, 6);
            Console.Write(" ██▄█▒▒████▄    ▓█░ █ ░█░▒████▄    ▓██▒▓██▒");
            Console.SetCursorPosition(37, 7);
            Console.Write("▓███▄░▒██  ▀█▄  ▒█░ █ ░█ ▒██  ▀█▄  ▒██▒▒██▒");
            Console.SetCursorPosition(37, 8);
            Console.Write("▓██ █▄░██▄▄▄▄██ ░█░ █ ░█ ░██▄▄▄▄██ ░██░░██░");
            Console.SetCursorPosition(37, 9);
            Console.Write("▒██▒ █▄▓█   ▓██▒░░██▒██▓  ▓█   ▓██▒░██░░██░");
            Console.SetCursorPosition(37, 10);
            Console.Write("▒ ▒▒ ▓▒▒▒   ▓▒█░░ ▓░▒ ▒   ▒▒   ▓▒█░░▓  ░▓  ");
            Console.SetCursorPosition(37, 11);
            Console.Write("░ ░▒ ▒░ ▒   ▒▒ ░  ▒ ░ ░    ▒   ▒▒ ░ ▒ ░ ▒ ░");
            Console.SetCursorPosition(37, 12);
            Console.Write("░ ░░ ░  ░   ▒     ░   ░    ░   ▒    ▒ ░ ▒ ░");
            Console.SetCursorPosition(37, 13);
            Console.Write("░  ░        ░  ░    ░          ░  ░ ░   ░");
            Console.SetCursorPosition(37, 14);
            Console.Write("                                         ");
            Console.SetCursorPosition(30, 15);
            Console.Write("  ██████ ▄▄▄█████▓ █    ██ ▓█████▄  ██▓ ▒█████    ██████");
            Console.SetCursorPosition(30, 16);
            Console.Write("▒██    ▒ ▓  ██▒ ▓▒ ██  ▓██▒▒██▀ ██ ▓██▒▒██▒  ██▒▒██    ▒");
            Console.SetCursorPosition(30, 17);
            Console.Write("░ ▓██▄   ▒ ▓██░ ▒░▓██  ▒██░░██   █ ▒██▒▒██░  ██▒░ ▓██▄   ");
            Console.SetCursorPosition(30, 18);
            Console.Write("  ▒   ██▒░ ▓██▓ ░ ▓▓█  ░██░░▓█▄    ░██░▒██   ██░  ▒   ██▒");
            Console.SetCursorPosition(30, 19);
            Console.Write("▒██████▒▒  ▒██▒ ░ ▒▒█████▓ ░▒████▓ ░██░░ ████▓▒░▒██████▒▒");
            Console.SetCursorPosition(30, 20);
            Console.Write("▒ ▒▓▒ ▒ ░  ▒ ░░   ░▒▓▒ ▒ ▒  ▒▒▓  ▒ ░▓  ░ ▒░▒░▒░ ▒ ▒▓▒ ▒ ░");
            Console.SetCursorPosition(30, 21);
            Console.Write("░ ░▒  ░ ░    ░    ░░▒░ ░ ░  ░ ▒  ▒  ▒ ░  ░ ▒ ▒░ ░ ░▒  ░ ░");
            Console.SetCursorPosition(30, 22);
            Console.Write("░  ░  ░    ░       ░░░ ░ ░  ░ ░  ░  ▒ ░░ ░ ░ ▒  ░  ░  ░");
            Console.SetCursorPosition(30, 23);
            Console.Write("      ░              ░        ░     ░      ░ ░        ░");
            Console.SetCursorPosition(30, 24);
            Console.Write("                            ░");
            Thread.Sleep(3000);
        }

        public static void ShowTitleScreen()
        {
            Console.BackgroundColor = GameSettings.Background;
            Console.Clear();
            int colorIndex = 0;

            while (!Console.KeyAvailable)
            {
                Console.ForegroundColor = GameSettings.SplashColors[colorIndex++ % GameSettings.SplashColors.Length];
                Console.SetCursorPosition(30, 3);
                Console.WriteLine(" ▄▀▀▄ ▄▀▄  ▄▀▀▄▀▀▀▄  ▄▀▀▀▀▄   ▄▀▀▄    ▄▀▀▄  ▄▀▀▄ █  ▄▀▀█▄");
                Console.SetCursorPosition(30, 4);
                Console.WriteLine("█  █ ▀  █ █   █   █ █      █ █   █       █ █  █ ▄▀   ▄▀ ▀▄");
                Console.SetCursorPosition(30, 5);
                Console.WriteLine("   █    █    █▀▀█▀  █      █    █        █    █▀▄    █▄▄▄█");
                Console.SetCursorPosition(30, 6);
                Console.WriteLine("  █    █   ▄▀    █  ▀▄    ▄▀   █   ▄    █    █   █  ▄▀   █");
                Console.SetCursorPosition(30, 7);
                Console.WriteLine("▄▀   ▄▀   █     █     ▀▀▀▀      ▀▄▀ ▀▄ ▄▀  ▄▀   █  █   ▄▀");
                Console.SetCursorPosition(30, 8);
                Console.WriteLine("█    █                                ▀    █");
                Console.SetCursorPosition(30, 9);
                Console.WriteLine("");
                Console.SetCursorPosition(43, 10);
                Console.WriteLine(" ▄▀▀▀█▀▀▄  ▄▀▀▄ ▄▄   ▄▀▀█▄▄▄▄");
                Console.SetCursorPosition(43, 11);
                Console.WriteLine("█    █    █  █   ▄▀    ▄▀");
                Console.SetCursorPosition(43, 12);
                Console.WriteLine("    █        █▄▄▄█    █▄▄▄▄▄");
                Console.SetCursorPosition(43, 13);
                Console.WriteLine("   █         █   █    █");
                Console.SetCursorPosition(43, 14);
                Console.WriteLine(" ▄▀         ▄▀  ▄▀   ▄▀▄▄▄▄");
                Console.SetCursorPosition(43, 15);
                Console.WriteLine("█          █   █     █");
                Console.SetCursorPosition(43, 16);
                Console.WriteLine("");
                Console.SetCursorPosition(38, 17);
                Console.WriteLine(" ▄▀▀▀▀▄    ▄▀▀█▄   ▄▀▀▄ ▄▀▄  ▄▀▀█▄▄▄▄");
                Console.SetCursorPosition(38, 18);
                Console.WriteLine("█           ▄▀ ▀▄ █  █ ▀  █    ▄▀");
                Console.SetCursorPosition(38, 19);
                Console.WriteLine("█    ▀▄▄    █▄▄▄█    █    █   █▄▄▄▄▄");
                Console.SetCursorPosition(38, 20);
                Console.WriteLine("█     █ █  ▄▀   █   █    █    █");
                Console.SetCursorPosition(38, 21);
                Console.WriteLine(" ▀▄▄▄▄▀   █   ▄▀  ▄▀   ▄▀    ▄▀▄▄▄▄");
                Console.SetCursorPosition(38, 22);
                Console.WriteLine("                  █    █     █");
                Console.SetCursorPosition(43, 26);
                Console.Write("Press any key to continue...");
            }

            while (Console.KeyAvailable)
                Console.ReadKey(true);
        }

        public static void ShowGameLostScreen()
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            Console.ForegroundColor = GameSettings.ObstacleColor;
            Console.WriteLine("You lost. :<");
            Console.Write("Press any key to exit...");
        }

        public static void ShowGameWonScreen()
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            Console.ForegroundColor = GameSettings.ObstacleColor;
            Console.WriteLine("You won. :>");
            Console.Write("Press any key to exit...");
        }
    }
}
