using Game.Game;
using System;
using System.Threading;

namespace Game
{
    class GameLoop
    {
        public long Time { get; set; }
        public int Points { get; set; }
        public int Lives { get; set; }
        public int CurrentLevel { get; set; }
        public bool Lost { get; set; }
        public bool Exit { get; set; }
        public bool NextLevel { get; set; }
        public bool Resuming { get; set; }

        private Player player;
        private Level level;

        public GameLoop()
        {
            Time = 0;
            Points = 0;
            Lives = 2;
            CurrentLevel = -1;
            Lost = false;
            Exit = false;
            NextLevel = true;
            Resuming = false;

            player = new Player(new Point(0, 0));
        }

        public int Start()
        {
            while (true)
            {
                if (!Resuming)
                {
                    if (Lost) return 1;
                    if (Exit) return 2;
                    if (NextLevel)
                    {
                        Lives++;
                        CurrentLevel++;
                    }
                }

                SetupGame();
                StartPlaying();
            }
        }

        private void SetupGame()
        {
            if (!Resuming)
                player.MoveToPosition(level.PlayerSpawn, level);
            level.Paint();
            player.Paint();

            Lost = false;
            Exit = false;
            NextLevel = false;
            Resuming = false;

            GuiUpdater.SetLevel(level.Name);
            GuiUpdater.SetPoints(Points);
            GuiUpdater.SetLives(Lives);
            GuiUpdater.ShowTopStrip();
        }

        private void StartPlaying()
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
                            Exit = true;
                            return;
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

                    if (Lives < 1)
                    {
                        Lost = true;
                        return;
                    }
                    
                    player.MoveToPosition(level.PlayerSpawn, level);
                    GuiUpdater.SetLevel(level.Name);
                    GuiUpdater.SetLives(Lives);
                    GuiUpdater.SetPoints(Points);
                    GuiUpdater.ShowTopStrip();

                    continue;
                }

                if(level.CheckFinishCollision(player.Pos))
                {
                    NextLevel = true;
                    break;
                }

                Time++;
                Thread.Sleep(16);
            }
        }
    }
}
