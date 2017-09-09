using Game.GameObjects;
using Game.Level;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class Level100 : ILevel
    {
        private int number;
        private List<Wall> walls;
        private List<Enemy> enemies;
        private List<Coin> coins;
        private List<TextTip> tips;

        public Level100()
        {
            number = 100;
            walls = new List<Wall>();
            enemies = new List<Enemy>();
            coins = new List<Coin>();
            tips = new List<TextTip>();
        }

        public int GetNumber() { return number; }

        public Point Create()
        {
            Console.BackgroundColor = GameSettings.Background;
            Console.Clear();
            
            SpawnWalls();
            SpawnEnemies();
            SpawnCoins();
            SpawnTips();

            return new Point(5, 26);
        }

        public Point Restart()
        {
            RemoveEnemies();
            RemoveCoins();

            SpawnEnemies();
            SpawnCoins();

            return new Point(5, 26);
        }

        public void Remove()
        {
            RemoveWalls();
            RemoveEnemies();
            RemoveCoins();
            SpawnTips();
        }

        public void SpawnWalls()
        {
            walls.Add(new Wall(new Point(0, 0), new Point(119, 0)));
            walls.Add(new Wall(new Point(0, 0), new Point(0, 29)));
            walls.Add(new Wall(new Point(0, 29), new Point(120, 29)));
            walls.Add(new Wall(new Point(119, 0), new Point(120, 29)));

            walls.Add(new Wall(new Point(107, 1), new Point(107, 4)));
            walls.Add(new Wall(new Point(107, 4), new Point(120, 4)));

            walls.Add(new Wall(new Point(0, 23), new Point(11, 23)));
            walls.Add(new Wall(new Point(11, 23), new Point(11, 27)));
            walls.Add(new Wall(new Point(11, 18), new Point(11, 23)));
            walls.Add(new Wall(new Point(11, 18), new Point(47, 18)));
            walls.Add(new Wall(new Point(29, 21), new Point(29, 29)));
            walls.Add(new Wall(new Point(47, 18), new Point(47, 27)));

            walls.Add(new Wall(new Point(47, 23), new Point(55, 23)));
            walls.Add(new Wall(new Point(56, 23), new Point(64, 23)));
            walls.Add(new Wall(new Point(63, 25), new Point(63, 30)));
            walls.Add(new Wall(new Point(47, 18), new Point(63, 18)));
            walls.Add(new Wall(new Point(63, 18), new Point(63, 24)));

            foreach (Wall w in walls)
                w.Paint();
        }

        public void SpawnEnemies()
        {
            // Room 1.1
            enemies.Add(new Enemy(new Point(13, 28), Enemy.FacingVertical));
            enemies.Add(new Enemy(new Point(15, 25), Enemy.FacingVertical));
            enemies.Add(new Enemy(new Point(17, 22), Enemy.FacingVertical));
            enemies.Add(new Enemy(new Point(19, 20), Enemy.FacingVertical));
            enemies.Add(new Enemy(new Point(21, 28), Enemy.FacingVertical));
            enemies.Add(new Enemy(new Point(23, 25), Enemy.FacingVertical));
            enemies.Add(new Enemy(new Point(25, 22), Enemy.FacingVertical));
            enemies.Add(new Enemy(new Point(27, 20), Enemy.FacingVertical));

            // Room 1.2
            enemies.Add(new Enemy(new Point(31, 20), Enemy.FacingVertical));
            enemies.Add(new Enemy(new Point(33, 22), Enemy.FacingVertical));
            enemies.Add(new Enemy(new Point(35, 25), Enemy.FacingVertical));
            enemies.Add(new Enemy(new Point(37, 28), Enemy.FacingVertical));
            enemies.Add(new Enemy(new Point(39, 20), Enemy.FacingVertical));
            enemies.Add(new Enemy(new Point(41, 22), Enemy.FacingVertical));
            enemies.Add(new Enemy(new Point(43, 25), Enemy.FacingVertical));
            enemies.Add(new Enemy(new Point(45, 28), Enemy.FacingVertical));

            // Room 2.1
            enemies.Add(new Enemy(new Point(49, 28), Enemy.FacingVertical));
            enemies.Add(new Enemy(new Point(50, 28), Enemy.FacingVertical));
            enemies.Add(new Enemy(new Point(52, 28), Enemy.FacingVertical));
            enemies.Add(new Enemy(new Point(53, 28), Enemy.FacingVertical));
            enemies.Add(new Enemy(new Point(57, 28), Enemy.FacingVertical));
            enemies.Add(new Enemy(new Point(58, 28), Enemy.FacingVertical));
            enemies.Add(new Enemy(new Point(60, 28), Enemy.FacingVertical));
            enemies.Add(new Enemy(new Point(61, 28), Enemy.FacingVertical));

            // Room 2.2
            enemies.Add(new Enemy(new Point(48, 20), Enemy.FacingHorizontal));
            enemies.Add(new Enemy(new Point(62, 21), Enemy.FacingHorizontal));
            enemies.Add(new Enemy(new Point(48, 22), Enemy.FacingHorizontal));
        }

        public void SpawnCoins()
        {
            // Room spawn
            coins.Add(new Coin(new Point(1, 28), 1));

            // Room 1.1
            coins.Add(new Coin(new Point(14, 19), 1));
            coins.Add(new Coin(new Point(26, 28), 1));

            // Room 1.2
            coins.Add(new Coin(new Point(32, 28), 1));
            coins.Add(new Coin(new Point(44, 19), 1));

            // Room 2.1
            coins.Add(new Coin(new Point(51, 24), 1));
            coins.Add(new Coin(new Point(59, 28), 1));

            // Room 2.2
            coins.Add(new Coin(new Point(48, 19), 1));
            coins.Add(new Coin(new Point(62, 19), 1));

            foreach (Coin c in coins)
                c.Paint();
        }

        public void SpawnTips()
        {
            foreach (TextTip t in tips)
                t.Paint();
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
                if(e.IsOccupying(p)) return true;
            return false;
        }

        public bool CheckCoinCollision(Point p)
        {
            foreach (Coin c in coins)
                if (c.IsOccupying(p)) return true;
            return false;
        }
    }
}
