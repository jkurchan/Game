using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    static class Level
    {
        public static List<Wall> Walls = new List<Wall>();

        public static Player CreateLevel()
        {
            Walls.Add(new Wall(new Point(0, 0), new Point(30, 0)));
            Walls.Add(new Wall(new Point(0, 0), new Point(0, 15)));
            Walls.Add(new Wall(new Point(0, 15), new Point(30, 15)));
            Walls.Add(new Wall(new Point(30, 0), new Point(30, 16)));

            foreach (Wall w in Walls)
                w.Paint();

            return new Player(new Point(2, 2));
        }

        public static bool IsOccupying(Point p)
        {
            foreach (Wall w in Walls)
                if (w.IsOccupying(p)) return true;
            return false;
        }
    }
}
