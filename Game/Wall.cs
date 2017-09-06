using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class Wall
    {
        public List<Obstacle> Obstacles;

        public Wall(Point sp, Point ep)
        {
            if (sp.PosY == ep.PosY)
                for (int i = sp.PosX; i < ep.PosX; i++) 
                    Obstacles.Add(new Obstacle(new Point(i, sp.PosY)));
            else
                for (int i = sp.PosY; i < ep.PosY; i++)
                    Obstacles.Add(new Obstacle(new Point(sp.PosX, i)));
        }

        public void Paint()
        {
            foreach (Obstacle o in Obstacles)
                o.Paint();
        }

        public bool IsOccupying(Point p)
        {
            foreach (Obstacle o in Obstacles)
                if (o.IsOccupying(p)) return true;
            return false;
        }
    }
}
