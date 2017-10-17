using Game.Game;
using Game.Interfaces;
using Game.Util;
using System;

namespace Game.Screens
{
    class MainMenuScreen : IScreen
    {
        private static GameScreen gameScreen;
        private Menu menu;

        public int Show()
        {
            menu = new Menu(17, 0);

            MenuItem itemStart = new MenuItem("Start new", ConsoleColor.White, GameSettings.MenuColor);
            itemStart.OnClick += ItemStart_OnClick;

            MenuItem itemResume = new MenuItem("Resume previous", ConsoleColor.White, GameSettings.MenuColor);
            itemResume.OnClick += ItemResume_OnClick;

            MenuItem itemCreate = new MenuItem("Level creator", ConsoleColor.White, GameSettings.MenuColor);
            itemCreate.OnClick += ItemCreate_OnClick;

            MenuItem itemCustomLevels = new MenuItem("Custom levels", ConsoleColor.White, GameSettings.MenuColor);
            itemCustomLevels.OnClick += ItemCustomLevels_OnClick;

            MenuItem itemLeave = new MenuItem("Leave", ConsoleColor.White, GameSettings.MenuColor);
            itemLeave.OnClick += ItemLeave_OnClick;

            menu.AddItem(itemStart);
            menu.AddItem(itemResume);
            menu.AddItem(itemCreate);
            menu.AddItem(itemCustomLevels);
            menu.AddItem(itemLeave);

            Paint();

            return Loop();
        }

        public void Paint()
        {
            Console.Clear();
            Console.ForegroundColor = GameSettings.MenuColor;

            Console.SetCursorPosition(0, 0);
            Console.WriteLine($"Build {Application.Version}");

            Console.SetCursorPosition(30, 6);
            Console.WriteLine(" ▄▀▀▄ ▄▀▄  ▄▀▀▄▀▀▀▄  ▄▀▀▀▀▄   ▄▀▀▄    ▄▀▀▄  ▄▀▀▄ █  ▄▀▀█▄");
            Console.SetCursorPosition(30, 7);
            Console.WriteLine("█  █ ▀  █ █   █   █ █      █ █   █       █ █  █ ▄▀   ▄▀ ▀▄");
            Console.SetCursorPosition(30, 8);
            Console.WriteLine("   █    █    █▀▀█▀  █      █    █        █    █▀▄    █▄▄▄█");
            Console.SetCursorPosition(30, 9);
            Console.WriteLine("  █    █   ▄▀    █  ▀▄    ▄▀   █   ▄    █    █   █  ▄▀   █");
            Console.SetCursorPosition(30, 10);
            Console.WriteLine("▄▀   ▄▀   █     █     ▀▀▀▀      ▀▄▀ ▀▄ ▄▀  ▄▀   █  █   ▄▀");
            Console.SetCursorPosition(30, 11);
            Console.WriteLine("█    █                                ▀    █");

            menu.Paint();
        }

        private int Loop()
        {
            while (true)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        menu.SelectPrevious();
                        break;
                    case ConsoleKey.DownArrow:
                        menu.SelectNext();
                        break;
                    case ConsoleKey.Enter:
                        int result = menu.Click();
                        if (result != -1)
                            Paint();
                        break;
                    case ConsoleKey.Escape:
                        return 1;
                    default:
                        break;
                }
            }
        }

        private static int ItemCreate_OnClick()
        {
            LevelCreatorScreen creator = new LevelCreatorScreen();
            return creator.Show();
        }

        private static int ItemResume_OnClick()
        {
            if (gameScreen == null) return -1;

            gameScreen.Resuming = true;
            int result = gameScreen.Show();
            if (result == 1) gameScreen = null;
            return result;
        }

        private static int ItemLeave_OnClick()
        {
            Environment.Exit(1);
            return 0;
        }

        private static int ItemCustomLevels_OnClick()
        {
            return -1;
        }

        private static int ItemStart_OnClick()
        {
            CampaignLevelsScreen screen = new CampaignLevelsScreen();
            return screen.Show();
        }
    }
}
