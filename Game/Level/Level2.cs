using Game.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Level
{
    class Level2 : ILevel
    {
        private int number;
        private List<Wall> walls;
        private List<Enemy> enemies;
        private List<Coin> coins;
        private List<TextTip> tips;
        private Finish finish;
        private Point playerSpawn;

        public Level2()
        {
            number = 2;
            walls = new List<Wall>();
            enemies = new List<Enemy>();
            coins = new List<Coin>();
            tips = new List<TextTip>();
            playerSpawn = new Point(47, 15);
        }

        public void ShowSplash()
        {
            Console.ForegroundColor = GameSettings.SplashColors[new Random().Next() % GameSettings.SplashColors.Length];
            Console.Clear();
            Console.SetCursorPosition(30, 10);
            Console.Write(" ___      _______  __   __  _______  ___        _______");
            Console.SetCursorPosition(30, 11);
            Console.Write("|   |    |       ||  | |  ||       ||   |      |       |");
            Console.SetCursorPosition(30, 12);
            Console.Write("|   |    |    ___||  |_|  ||    ___||   |      |____   |");
            Console.SetCursorPosition(30, 13);
            Console.Write("|   |    |   |___ |       ||   |___ |   |       ____|  |");
            Console.SetCursorPosition(30, 14);
            Console.Write("|   |___ |    ___||       ||    ___||   |___   | ______|");
            Console.SetCursorPosition(30, 15);
            Console.Write("|       ||   |___  |     | |   |___ |       |  | |_____");
            Console.SetCursorPosition(30, 16);
            Console.Write("|_______||_______|  |___|  |_______||_______|  |_______|");
        }

        public int GetNumber() { return number; }

        public Point Create()
        {
            SpawnWalls();
            SpawnEnemies();
            SpawnCoins();
            if (GameSettings.TipsOn)
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

            PaintWalls();
            PaintCoins();
            PaintEnemies();
            PaintTips();
            PaintFinish();
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

            walls.Add(new Wall(new Point(46, 14), new Point(50, 14)));      // Spawn point top wall
            walls.Add(new Wall(new Point(46, 16), new Point(50, 16)));      // Spawn point bottom wall
            walls.Add(new Wall(new Point(46, 14), new Point(46, 16)));      // Spawn point left wall

            walls.Add(new Wall(new Point(49, 11), new Point(49, 14)));      // Room 1 top left wall
            walls.Add(new Wall(new Point(49, 16), new Point(49, 19)));      // Room 1 bottom left wall
            walls.Add(new Wall(new Point(49, 11), new Point(69, 11)));      // Room 1 top wall
            walls.Add(new Wall(new Point(49, 19), new Point(69, 19)));      // Room 1 bottom wall
            walls.Add(new Wall(new Point(68, 11), new Point(48, 14)));      // Room 1 top right wall
            walls.Add(new Wall(new Point(68, 16), new Point(48, 19)));      // Room 1 bottom right wall

            walls.Add(new Wall(new Point(68, 14), new Point(71, 14)));      // End point top wall
            walls.Add(new Wall(new Point(68, 16), new Point(72, 16)));      // End point bottom wall
            walls.Add(new Wall(new Point(71, 14), new Point(71, 16)));      // End point right wall
        }

        public void SpawnEnemies()
        {
            enemies.Add(new Enemy(new Point(51, 12), Enemy.FacingVertical));
            enemies.Add(new Enemy(new Point(52, 18), Enemy.FacingVertical));
            enemies.Add(new Enemy(new Point(53, 12), Enemy.FacingVertical));
            enemies.Add(new Enemy(new Point(54, 18), Enemy.FacingVertical));
            enemies.Add(new Enemy(new Point(55, 12), Enemy.FacingVertical));
            enemies.Add(new Enemy(new Point(56, 18), Enemy.FacingVertical));
            enemies.Add(new Enemy(new Point(57, 12), Enemy.FacingVertical));

            enemies.Add(new Enemy(new Point(60, 18), Enemy.FacingVertical));
            enemies.Add(new Enemy(new Point(61, 12), Enemy.FacingVertical));
            enemies.Add(new Enemy(new Point(62, 18), Enemy.FacingVertical));
            enemies.Add(new Enemy(new Point(63, 12), Enemy.FacingVertical));
            enemies.Add(new Enemy(new Point(64, 18), Enemy.FacingVertical));
            enemies.Add(new Enemy(new Point(65, 12), Enemy.FacingVertical));
            enemies.Add(new Enemy(new Point(66, 18), Enemy.FacingVertical));

            enemies.Add(new Enemy(new Point(50, 12), Enemy.FacingHorizontal));
            enemies.Add(new Enemy(new Point(67, 18), Enemy.FacingHorizontal));
        }

        public void SpawnCoins()
        {
            coins.Add(new Coin(new Point(58, 15), 1));
            coins.Add(new Coin(new Point(59, 15), 1));
        }

        public void SpawnTips() { }

        public void SpawnFinish()
        {
            finish = new Finish(new Point(70, 15));
        }

        public void PaintWalls()
        {
            foreach (Wall w in walls)
                w.Paint();
        }

        public void PaintEnemies()
        {
            foreach (Enemy e in enemies)
                e.Paint();
        }

        public void PaintCoins()
        {
            foreach (Coin c in coins)
                c.Paint();
        }

        public void PaintTips()
        {
            foreach (TextTip t in tips)
                t.Paint();
        }

        public void PaintFinish()
        {
            finish.Paint();
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
