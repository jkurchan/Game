using Game.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Level
{
    class Level0 : ILevel
    {
        private int number;
        private List<Wall> walls;
        private List<Enemy> enemies;
        private List<Coin> coins;
        private List<TextTip> tips;
        private Finish finish;
        private Point playerSpawn;

        public Level0()
        {
            number = 0;
            walls = new List<Wall>();
            enemies = new List<Enemy>();
            coins = new List<Coin>();
            tips = new List<TextTip>();
            playerSpawn = new Point(12, 14);
        }

        public void ShowSplash()
        {
            Console.ForegroundColor = GameSettings.SplashColors[new Random().Next() % GameSettings.SplashColors.Length];
            Console.Clear();
            Console.SetCursorPosition(25, 10);
            Console.Write(" _______  __   __  _______  _______  ______    ___   _______  ___");
            Console.SetCursorPosition(25, 11);
            Console.Write("|       ||  | |  ||       ||       ||    _ |  |   | |   _   ||   |");
            Console.SetCursorPosition(25, 12);
            Console.Write("|_     _||  | |  ||_     _||   _   ||   | ||  |   | |  |_|  ||   |");
            Console.SetCursorPosition(25, 13);
            Console.Write("  |   |  |  |_|  |  |   |  |  | |  ||   |_||_ |   | |       ||   |");
            Console.SetCursorPosition(25, 14);
            Console.Write("  |   |  |       |  |   |  |  |_|  ||    __  ||   | |       ||   |___");
            Console.SetCursorPosition(25, 15);
            Console.Write("  |   |  |       |  |   |  |       ||   |  | ||   | |   _   ||       |");
            Console.SetCursorPosition(25, 16);
            Console.Write("  |___|  |_______|  |___|  |_______||___|  |_||___| |__| |__||_______|");
        }

        public int GetNumber() { return number; }

        public Point Create()
        {
            SpawnWalls();
            SpawnEnemies();
            SpawnCoins();
            if(GameSettings.TipsOn)
                SpawnTips();
            SpawnFinish();

            Paint();

            return playerSpawn;
        }

        public Point Restart()
        {
            RemoveEnemies();
            RemoveCoins();

            SpawnEnemies();
            SpawnCoins();

            Paint();

            return playerSpawn;
        }

        public void Paint()
        {
            Console.BackgroundColor = GameSettings.Background;
            Console.Clear();

            foreach (Wall w in walls)
                w.Paint();

            foreach (Enemy e in enemies)
                e.Paint();

            foreach (Coin c in coins)
                c.Paint();

            foreach (TextTip t in tips)
                t.Paint();

            finish.Paint();
        }

        public void Remove()
        {
            RemoveWalls();
            RemoveEnemies();
            RemoveCoins();
            RemoveTips();
            RemoveFinish();
        }

        public void SpawnWalls()
        {
            walls.Add(new Wall(new Point(0, 0), new Point(119, 0)));        // Main frame top
            walls.Add(new Wall(new Point(0, 0), new Point(0, 29)));         // Main frame left
            walls.Add(new Wall(new Point(0, 29), new Point(120, 29)));      // Main frame bottom
            walls.Add(new Wall(new Point(119, 0), new Point(120, 29)));     // Main frame right
            
            walls.Add(new Wall(new Point(107, 1), new Point(107, 4)));      // Top right square left
            walls.Add(new Wall(new Point(107, 4), new Point(120, 4)));      // Top right square bottom
            walls.Add(new Wall(new Point(0, 2), new Point(107, 2)));        // Top strip
            
            walls.Add(new Wall(new Point(10, 13), new Point(109, 13)));     // Room 1 top
            walls.Add(new Wall(new Point(10, 15), new Point(70, 15)));      // Room 1 bottom 1
            walls.Add(new Wall(new Point(71, 15), new Point(109, 15)));     // Room 1 bottom 2
            walls.Add(new Wall(new Point(10, 13), new Point(10, 15)));      // Room 1 left
            walls.Add(new Wall(new Point(109, 13), new Point(109, 16)));    // Room 1 right

            walls.Add(new Wall(new Point(69, 15), new Point(69, 21)));      // Room 2 left
            walls.Add(new Wall(new Point(71, 15), new Point(71, 21)));      // Room 2 right
            walls.Add(new Wall(new Point(69, 21), new Point(72, 21)));      // Room 2 bottom
        }

        public void SpawnEnemies()
        {
            enemies.Add(new Enemy(new Point(70, 17), Enemy.FacingVertical));
        }

        public void SpawnCoins()
        {
            coins.Add(new Coin(new Point(40, 14), 1));
        }

        public void SpawnTips()
        {
            if (GameSettings.PolishOn)
            {
                tips.Add(new TextTip("To jesteś ty", new Point(10, 17), 2, TextTip.SideUp, true));
                tips.Add(new TextTip("To jest piniondz", new Point(34, 17), 6, TextTip.SideUp, true));
                tips.Add(new TextTip("Tu musisz się dostać", new Point(88, 17), 19, TextTip.SideUp, true));
                tips.Add(new TextTip("To jest przeciwnik. Auć!", new Point(62, 23), 8, TextTip.SideUp, true));
                tips.Add(new TextTip("Poruszaj się przy użyciu strzałek", new Point(48, 11), 0, 0, false));
            }
            else
            {
                tips.Add(new TextTip("This is you", new Point(10, 17), 2, TextTip.SideUp, true));
                tips.Add(new TextTip("This is a coin", new Point(34, 17), 6, TextTip.SideUp, true));
                tips.Add(new TextTip("This is where you have to get", new Point(88, 17), 19, TextTip.SideUp, true));
                tips.Add(new TextTip("This is an enemy. Ouch!", new Point(62, 23), 8, TextTip.SideUp, true));
                tips.Add(new TextTip("Move by using arrow keys", new Point(48, 11), 0, 0, false));
            }
        }

        public void SpawnFinish()
        {
            finish = new Finish(new Point(107, 14));
        }

        public void RemoveWalls()
        {
            foreach (Wall w in walls)
                w.Remove();
            walls.Clear();
        }

        public void RemoveEnemies()
        {
            foreach (Enemy e in enemies)
                e.Remove();
            enemies.Clear();
        }

        public void RemoveCoins()
        {
            foreach (Coin c in coins)
                c.Remove();
            coins.Clear();
        }

        public void RemoveTips()
        {
            foreach (TextTip t in tips)
                t.Remove();
            tips.Clear();
        }

        public void RemoveFinish()
        {
            finish.Remove();
        }

        public void MoveEnemies(long time)
        {
            foreach (Enemy e in enemies)
                e.Move(null, this, time);
        }

        public bool CheckWallCollision(Point p)
        {
            foreach (Wall w in walls)
                if (w.IsOccupying(p)) return true;
            return false;
        }

        public bool CheckEnemyCollision(Point p)
        {
            foreach (Enemy e in enemies)
                if (e.IsOccupying(p)) return true;
            return false;
        }

        public bool CheckCoinCollision(Point p)
        {
            foreach (Coin c in coins)
                if (c.IsOccupying(p))
                {
                    coins.Remove(c);
                    return true;
                }

            return false;
        }

        public bool CheckFinishCollision(Point p)
        {
            if (finish.IsOccupying(p)) return true;
            return false;
        }
    }
}
