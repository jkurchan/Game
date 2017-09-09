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
        private Point playerSpawn;

        public Level0()
        {
            number = 0;
            walls = new List<Wall>();
            enemies = new List<Enemy>();
            coins = new List<Coin>();
            tips = new List<TextTip>();
            playerSpawn = new Point(32, 14);
        }

        public int GetNumber() { return number; }

        public Point Create()
        {
            Console.BackgroundColor = GameSettings.Background;
            Console.Clear();

            SpawnWalls();
            SpawnEnemies();
            SpawnCoins();
            if(GameSettings.TipsOn)
                SpawnTips();

            return playerSpawn;
        }

        public Point Restart()
        {
            RemoveEnemies();
            RemoveCoins();

            SpawnEnemies();
            SpawnCoins();

            return playerSpawn;
        }

        public void Remove()
        {
            RemoveWalls();
            RemoveEnemies();
            RemoveCoins();
            RemoveTips();
        }

        public void SpawnWalls()
        {
            // Main frame
            walls.Add(new Wall(new Point(0, 0), new Point(119, 0)));
            walls.Add(new Wall(new Point(0, 0), new Point(0, 29)));
            walls.Add(new Wall(new Point(0, 29), new Point(120, 29)));
            walls.Add(new Wall(new Point(119, 0), new Point(120, 29)));
            
            // Top strip
            walls.Add(new Wall(new Point(107, 1), new Point(107, 4)));
            walls.Add(new Wall(new Point(107, 4), new Point(120, 4)));
            walls.Add(new Wall(new Point(0, 2), new Point(107, 2)));

            walls.Add(new Wall(new Point(30, 13), new Point(90, 13)));
            walls.Add(new Wall(new Point(30, 15), new Point(90, 15)));
            walls.Add(new Wall(new Point(30, 13), new Point(30, 15)));
            walls.Add(new Wall(new Point(89, 13), new Point(89, 15)));

            foreach (Wall w in walls)
                w.Paint();
        }

        public void SpawnEnemies()
        {
            foreach (Enemy e in enemies)
                e.Paint();
        }

        public void SpawnCoins()
        {
            foreach (Coin c in coins)
                c.Paint();
        }

        public void SpawnTips()
        {
            tips.Add(new TextTip("This is you", new Point(30, 17), 2, TextTip.SideUp, true));
            tips.Add(new TextTip("This is where you have to get", new Point(70, 17), 17, TextTip.SideUp, true));
            tips.Add(new TextTip("Move by using arrow keys", new Point(48, 11), 0, 0, false));

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
                if (e.IsOccupying(p)) return true;
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
