using Game.Game;
using Game.Interfaces;
using System;
using System.Threading;

namespace Game
{
    class GameScreen : IScreen
    {
        public static readonly int CODE_EXIT = 1;
        public static readonly int CODE_PAUSE = 2;
        public static readonly int CODE_WON = 3;
        public static readonly int CODE_LOST = 4;

        public long Time { get; set; }
        public int Points { get; set; }
        public int Lives { get; set; }
        public bool Resuming { get; set; }

        private Player player;
        private Level level;

        public GameScreen(string filePath)
        {
            Time = 0;
            Points = 0;
            Lives = 2;
            Resuming = false;

            player = new Player(new Point(0, 0));
            level = Level.Load(filePath);
        }

        public int Show()
        {
            SetupGame();
            return StartPlaying();
        }

        public void Paint()
        {
            level.Paint();
            player.Paint();

            GuiUpdater.SetLevel(level.Name);
            GuiUpdater.SetPoints(Points);
            GuiUpdater.SetLives(Lives);
            GuiUpdater.ShowTopStrip();
        }

        private void SetupGame()
        {
            player.MoveToPosition(level.Spawn, level);
            Paint();
        }

        private int StartPlaying()
        {
            ConsoleKeyInfo keyInfo;

            while (true)
            {
                while (Console.KeyAvailable)
                {
                    keyInfo = Console.ReadKey(true);
                    switch (keyInfo.Key)
                    {
                        case ConsoleKey.UpArrow:
                            player.Move(new Point(0, -1), level, Time);
                            break;
                        case ConsoleKey.DownArrow:
                            player.Move(new Point(0, 1), level, Time);
                            break;
                        case ConsoleKey.LeftArrow:
                            player.Move(new Point(-1, 0), level, Time);
                            break;
                        case ConsoleKey.RightArrow:
                            player.Move(new Point(1, 0), level, Time);
                            break;
                        case ConsoleKey.Escape:
                            return CODE_PAUSE;
                        default:
                            break;
                    }
                }

                level.MoveEnemies(Time);

                if(level.CheckCoinCollision(player.Pos))
                {
                    Points++;
                    GuiUpdater.SetPoints(Points);
                }

                if (level.CheckEnemyCollision(player.Pos))
                {
                    Lives--;
                    Time = 0;

                    if (Lives < 1) return CODE_LOST;
                    
                    player.MoveToPosition(level.Spawn, level);
                    GuiUpdater.SetLevel(level.Name);
                    GuiUpdater.SetLives(Lives);
                    GuiUpdater.SetPoints(Points);
                    GuiUpdater.ShowTopStrip();

                    continue;
                }

                if (level.CheckFinishCollision(player.Pos)) return CODE_WON;

                Time++;
                Thread.Sleep(16);
            }
        }
    }
}
