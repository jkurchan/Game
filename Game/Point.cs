using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class Point
    {
        public int PosX { get; set; }
        public int PosY { get; set; }

        public Point(int posX, int posY)
        {
            PosX = posX;
            PosY = posY;
        }
    }
}
