using Game.Level;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class Wall : IGameObject
    {
        public List<Obstacle> Obstacles;

        public Wall(Point sp, Point ep)
        {
            Obstacles = new List<Obstacle>();
            if (sp.Y == ep.Y)
                for (int i = sp.X; i < ep.X; i++) 
                    Obstacles.Add(new Obstacle(new Point(i, sp.Y)));
            else
                for (int i = sp.Y; i < ep.Y; i++)
                    Obstacles.Add(new Obstacle(new Point(sp.X, i)));
        }

        public void Move(Point p, ILevel level, long time)
        {

        }

        public void MoveToPosition(Point p, ILevel level)
        {

        }

        public bool IsOccupying(Point p)
        {
            foreach (Obstacle o in Obstacles)
                if (o.IsOccupying(p)) return true;
            return false;
        }

        public void Paint()
        {
            foreach (Obstacle o in Obstacles)
                o.Paint();
        }

        public void Remove()
        {
            foreach (Obstacle o in Obstacles)
                o.Remove();
        }
    }
}
