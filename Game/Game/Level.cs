using Game.GameObjects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Game.Game
{
    class Level
    {
        [JsonProperty]
        public string Name { get; set; }

        [JsonProperty]
        List<Wall> walls;

        [JsonProperty]
        List<Enemy> enemies;

        [JsonProperty]
        List<Coin> coins;

        [JsonProperty]
        Player player;

        [JsonProperty]
        List<Finish> finishes;

        public Point PlayerSpawn { get; }

        public Level(List<Wall> walls, List<Enemy> enemies, List<Coin> coins, Player player, List<Finish> finishes)
        {
            this.walls = walls;
            this.enemies = enemies;
            this.coins = coins;
            this.player = player;
            this.finishes = finishes;
            PlayerSpawn = new Point(player.Pos.X, player.Pos.Y);
        }

        public void Paint()
        {
            Console.BackgroundColor = GameSettings.Background;
            Console.Clear();

            PaintWalls();
            PaintCoins();
            PaintEnemies();
            PaintFinish();
        }

        public void Remove()
        {
            RemoveWalls();
            RemoveEnemies();
            RemoveCoins();
            RemoveFinishes();
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

        public void PaintFinish()
        {
            foreach (Finish f in finishes)
                f.Paint();
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

        public void RemoveFinishes()
        {
            foreach (Finish f in finishes)
                f.Remove();
            finishes.Clear();
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
            foreach (Finish f in finishes)
                if (f.IsOccupying(p)) return true;
            return false;
        }
    }
}
