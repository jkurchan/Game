using Game.Game;
using System;

namespace Game
{
    public class Coin : IGameObject
    {
        public long Points { get; set; }
        public Point Pos { get; set; }

        public Coin() { }

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
