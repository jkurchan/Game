using Game.Game;
using Game.Level;
using Game.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
        private List<ILevel> levels;

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
            levels = new List<ILevel>();
            levels.Add(new Level0());
            levels.Add(new Level1());
            levels.Add(new Level2());
            levels.Add(new Level99());
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
            ILevel level = levels[CurrentLevel];

            level.ShowSplash();
            Thread.Sleep(1500);

            if (!Resuming)
                player.MoveToPosition(level.Create(), level);
            level.Paint();
            player.Paint();

            Lost = false;
            Exit = false;
            NextLevel = false;
            Resuming = false;

            GuiUpdater.SetLevel(level.GetNumber());
            GuiUpdater.SetPoints(Points);
            GuiUpdater.SetLives(Lives);
            GuiUpdater.ShowTopStrip();
        }

        private void StartPlaying()
        {
            ILevel level = levels[CurrentLevel];
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
                        case ConsoleKey.F1:
                            if (GameSettings.TipsOn = !GameSettings.TipsOn)
                            {
                                level.SpawnTips();
                                level.PaintTips();
                            }
                            else
                                level.RemoveTips();
                            GuiUpdater.ShowTopStrip();
                            break;
                        case ConsoleKey.F2:
                            if (GameSettings.MusicOn = !GameSettings.MusicOn)
                                MusicPlayer.GetInstance().Resume();
                            else
                                MusicPlayer.GetInstance().Pause();
                            GuiUpdater.ShowTopStrip();
                            break;
                        case ConsoleKey.F3:
                            GameSettings.PolishOn = !GameSettings.PolishOn;
                            if (GameSettings.TipsOn)
                            {
                                level.RemoveTips();
                                level.SpawnTips();
                                level.PaintTips();
                            }
                            GuiUpdater.SetLevel(level.GetNumber());
                            GuiUpdater.SetPoints(Points);
                            GuiUpdater.SetLives(Lives);
                            GuiUpdater.ShowTopStrip();
                            break;
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
                    
                    player.MoveToPosition(level.Restart(), level);
                    GuiUpdater.SetLevel(level.GetNumber());
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
