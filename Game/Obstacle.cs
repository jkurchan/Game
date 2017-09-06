using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class Obstacle
    {
        public Point Point;

        public Obstacle(Point point)
        {
            Point = point;
        }

        public void Paint()
        {
            Console.SetCursorPosition(Point.PosX, Point.PosY);
            Console.Write(GameSettings.Wall);
        }

        public bool IsOccupying(Point p)
        {
            if (p.PosX == Point.PosX && p.PosY == Point.PosY) return true;
            return false;
        }
    }
}
