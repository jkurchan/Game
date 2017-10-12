using Game.Game;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Game
{
    class Wall : IGameObject
    {
        [JsonProperty]
        public List<Obstacle> Obstacles;

        public Wall(Point sp, Point ep)
        {
            Obstacles = new List<Obstacle>();
            if (sp.Y == ep.Y)
            {
                if (sp.X <= ep.X)
                {
                    for (int i = sp.X; i <= ep.X; i++)
                        Obstacles.Add(new Obstacle(new Point(i, sp.Y)));
                }
                else
                {
                    for (int i = ep.X; i <= sp.X; i++)
                        Obstacles.Add(new Obstacle(new Point(i, sp.Y)));
                }
            }
            else if (sp.X == ep.X)
            {
                if(sp.Y <= ep.Y)
                {
                    for (int i = sp.Y; i <= ep.Y; i++)
                        Obstacles.Add(new Obstacle(new Point(sp.X, i)));
                }
                else
                {
                    for (int i = ep.Y; i <= sp.Y; i++)
                        Obstacles.Add(new Obstacle(new Point(sp.X, i)));
                }
            }
        }

        public void Move(Point p, Level level, long time)
        {

        }

        public void MoveToPosition(Point p, Level level)
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
