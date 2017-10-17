using Game.Interfaces;
using Game.Util;
using System;
using System.IO;

namespace Game.Screens
{
    class CampaignLevelsScreen : IScreen
    {
        private Menu menu;

        public int Show()
        {
            menu = new Menu(5, 0);

            LoadLevels();
            Paint();

            return Loop();
        }

        public void Paint()
        {
            Console.Clear();
            Console.ForegroundColor = GameSettings.MenuColor;

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

        public void LoadLevels()
        {
            string[] levels = Directory.GetFiles("custom_maps");
            foreach (string level in levels)
            {
                string text = level.Substring(12).Replace(".mtglvl", "");

                MenuItem item = new MenuItem(text, ConsoleColor.White, ConsoleColor.Green);
                item.OnClick += () =>
                {
                    GameScreen screen = new GameScreen(level);
                    return screen.Show();
                };

                menu.AddItem(item);
            }
        }
    }
}
