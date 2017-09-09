using Game.Level;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    interface IGameObject
    {
        void Move(Point p, ILevel level, long time);
        void MoveToPosition(Point p, ILevel level);
        bool IsOccupying(Point p);
        void Paint();
        void Remove();
    }
}
