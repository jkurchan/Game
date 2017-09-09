using Game.Level;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    static class GameRule
    {
        public static bool CanDraw(Point p, ILevel level)
        {
            if (level.CheckWallCollision(p)) return false;
            if (p.X < 0 || p.X >= Console.WindowWidth) return false;
            if (p.Y < 0 || p.Y >= Console.WindowHeight) return false;
            return true;
        }
    }
}
