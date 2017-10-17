using Game.GameObjects;
using Game.Util;
using System;
using System.Collections.Generic;
using System.IO;

namespace Game.Game
{
    public class Level
    {
        public string Name { get; set; }
        public List<Enemy> Enemies { get; set; }
        public List<Coin> Coins { get; set; }
        public List<Finish> Finishes { get; set; }
        public Point Spawn { get; set; }
        public Player Player { get; set; }
        public List<Wall> Walls { get; set; }

        public Level() { }

        public Level(List<Wall> walls, List<Enemy> enemies, List<Coin> coins, Player player, List<Finish> finishes)
        {
            Walls = walls;
            Enemies = enemies;
            Coins = coins;
            Player = player;
            Finishes = finishes;
            Spawn = new Point(player.Pos.X, player.Pos.Y);
        }

        public static Level Load(string filepath)
        {
            string json = File.ReadAllText(filepath);
            return (Level) XmlConvert.Deserialize(json, typeof(Level));
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
            foreach (Wall w in Walls)
                w.Paint();
        }

        public void PaintEnemies()
        {
            foreach (Enemy e in Enemies)
                e.Paint();
        }

        public void PaintCoins()
        {
            foreach (Coin c in Coins)
                c.Paint();
        }

        public void PaintFinish()
        {
            foreach (Finish f in Finishes)
                f.Paint();
        }

        public void RemoveWalls()
        {
            foreach (Wall w in Walls)
                w.Remove();
            Walls.Clear();
        }

        public void RemoveEnemies()
        {
            foreach (Enemy e in Enemies)
                e.Remove();
            Enemies.Clear();
        }

        public void RemoveCoins()
        {
            foreach (Coin c in Coins)
                c.Remove();
            Coins.Clear();
        }

        public void RemoveFinishes()
        {
            foreach (Finish f in Finishes)
                f.Remove();
            Finishes.Clear();
        }

        public void MoveEnemies(long time)
        {
            foreach (Enemy e in Enemies)
                e.Move(null, this, time);
        }

        public bool CheckWallCollision(Point p)
        {
            foreach (Wall w in Walls)
                if (w.IsOccupying(p)) return true;
            return false;
        }

        public bool CheckEnemyCollision(Point p)
        {
            foreach (Enemy e in Enemies)
                if (e.IsOccupying(p)) return true;
            return false;
        }

        public bool CheckCoinCollision(Point p)
        {
            foreach (Coin c in Coins)
                if (c.IsOccupying(p))
                {
                    Coins.Remove(c);
                    return true;
                }

            return false;
        }

        public bool CheckFinishCollision(Point p)
        {
            foreach (Finish f in Finishes)
                if (f.IsOccupying(p)) return true;
            return false;
        }
    }
}
