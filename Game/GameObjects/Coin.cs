using Game.Game;
using Newtonsoft.Json;
using System;

namespace Game
{
    class Coin : IGameObject
    {
        [JsonProperty]
        public Point Pos;

        [JsonProperty]
        public int Points;

        public Coin(Point p, int points)
        {
            Pos = p;
            Points = points;
        }

        public void Move(Point p, Level level, long time)
        {
            Point newPos = new Point(p.X + Pos.X, p.Y + Pos.Y);
            if (GameRule.CanDraw(newPos, level))
            {
                Pos = newPos;
                Paint();
            }
        }

        public void MoveToPosition(Point p, Level level)
        {
            if (GameRule.CanDraw(p, level))
            {
                Pos = p;
                Paint();
            }
        }

        public bool IsOccupying(Point p)
        {
            if (p.X == Pos.X && p.Y == Pos.Y) return true;
            return false;
        }

        public void Paint()
        {
            Console.SetCursorPosition(Pos.X, Pos.Y);
            Console.ForegroundColor = GameSettings.CoinColor;
            Console.Write(GameSettings.CoinAvatar);
        }

        public void Remove()
        {
            Console.SetCursorPosition(Pos.X, Pos.Y);
            Console.Write(' ');
        }
    }
}
