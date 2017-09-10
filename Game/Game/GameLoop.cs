using Game.Game;
using Game.Level;
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
        private static GameLoop gameLoop;

        public long Time { get; set; }
        public int Points { get; set; }
        public int Lives { get; set; }
        public int CurrentLevel { get; set; }

        private Player player;
        private List<ILevel> levels;

        private GameLoop()
        {
            Time = 0;
            Points = 0;
            Lives = 2;
            CurrentLevel = -1;

            player = new Player(new Point(0, 0));
            levels = new List<ILevel>();
        }

        public static GameLoop GetInstance()
        {
            if (gameLoop == null)
                gameLoop = new GameLoop();
            return gameLoop;
        }

        public void Start()
        {
            loadLevels();

            while (true)
            {
                switch (Application.State)
                {
                    case GameState.Exit:
                        return;
                    case GameState.Lost:
                        return;
                    case GameState.Won:
                        return;
                    case GameState.NextLevel:
                        setupGame();
                        startPlaying();
                        break;
                    default:
                        break;
                }
            }
        }

        private void setupGame()
        {
            ILevel level = levels[++CurrentLevel];

            level.ShowSplash();
            Thread.Sleep(2000);

            player.MoveToPosition(level.Create(), level);
            player.Paint();
            Lives++;

            GuiUpdater.SetLevel(level.GetNumber());
            GuiUpdater.SetPoints(Points);
            GuiUpdater.SetLives(Lives);
            GuiUpdater.ShowTopStrip();
        }

        private void startPlaying()
        {
            ILevel level = levels[CurrentLevel];
            ConsoleKeyInfo keyInfo;

            while (true)
            {
                if (Console.KeyAvailable)
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
                            Application.State = GameState.Exit;
                            return;
                        case ConsoleKey.F1:
                            if (GameSettings.TipsOn = !GameSettings.TipsOn)
                                level.SpawnTips();
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
                        Application.State = GameState.Lost;
                        return;
                    }

                    GuiUpdater.SetLevel(level.GetNumber());
                    GuiUpdater.SetLives(Lives);

                    player.MoveToPosition(level.Restart(), level);
                    continue;
                }

                if(level.CheckFinishCollision(player.Pos))
                {
                    Application.State = GameState.NextLevel;
                    break;
                }

                Time++;
                Thread.Sleep(16);
            }
        }

        private void loadLevels()
        {
            levels.Add(new Level0());
            levels.Add(new Level1());
            levels.Add(new Level99());
        }
    }
}
