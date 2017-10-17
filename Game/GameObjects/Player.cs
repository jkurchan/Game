using Game.Game;
using Newtonsoft.Json;
using System;

namespace Game
{
    public class Player : IGameObject
    {
        public Point OldPos { get; set; }
        public Point Pos { get; set; }

        public Player() { }

        public Player(Point p)
        {
            Pos = p;
            OldPos = p;
        }

        public void Move(Point p, Level level, long time)
        {
            Point newPos = new Point(p.X + Pos.X, p.Y + Pos.Y);
            if (GameRule.CanDraw(newPos, level))
            {
                OldPos = Pos;
                Pos = newPos;
                Paint();
            }
        }

        public void MoveToPosition(Point p, Level level)
        {
            if (GameRule.CanDraw(p, level))
            {
                OldPos = p;
                Pos = p;
                Paint();
            }
        }

        public bool IsOccupying(Point p)
        {
            if (Pos.X == p.X && Pos.Y == p.Y) return true;
            return false;
        }

        public void Paint()
        {
            Console.SetCursorPosition(OldPos.X, OldPos.Y);
            Console.Write(' ');
            Console.SetCursorPosition(Pos.X, Pos.Y);
            Console.ForegroundColor = GameSettings.PlayerColor;
            Console.Write(GameSettings.PlayerAvatar);
        }

        public void Remove()
        {
            Console.SetCursorPosition(Pos.X, Pos.Y);
            Console.Write(' ');
        }
    }
}
