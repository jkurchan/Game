using Game.Level;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class Obstacle : IGameObject
    {
        public Point Pos;

        public Obstacle(Point point)
        {
            Pos = point;
        }

        public void Move(Point p, ILevel level, long time)
        {
            Point newPos = new Point(p.X + Pos.X, p.Y + Pos.Y);
            if (GameRule.CanDraw(newPos, level))
            {
                Pos = newPos;
                Paint();
            }
        }

        public void MoveToPosition(Point p, ILevel level)
        {
            if (GameRule.CanDraw(p, level))
            {
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
            Console.SetCursorPosition(Pos.X, Pos.Y);
            Console.ForegroundColor = GameSettings.ObstacleColor;
            Console.Write(GameSettings.ObstacleAvatar);
        }

        public void Remove()
        {
            Console.SetCursorPosition(Pos.X, Pos.Y);
            Console.Write(' ');
        }
    }
}
