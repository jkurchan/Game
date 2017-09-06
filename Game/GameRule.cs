using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    static class GameRule
    {
        public static bool CanDraw(Point coordinate)
        {
            if (coordinate.PosX < 0 || coordinate.PosX >= Console.WindowWidth) return false;
            if (coordinate.PosY < 0 || coordinate.PosY >= Console.WindowHeight) return false;
            return true;
        }
    }
}
