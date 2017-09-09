using Game.Level;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class Player : IGameObject
    {
        public Point Pos { get; set; }
        
        private Point oldPos;

        public Player(Point p)
        {
            Pos = p;
            oldPos = p;
        }

        public void Move(Point p, ILevel level, long time)
        {
            Point newPos = new Point(p.X + Pos.X, p.Y + Pos.Y);
            if (GameRule.CanDraw(newPos, level))
            {
                oldPos = Pos;
                Pos = newPos;
                Paint();
            }
        }

        public void MoveToPosition(Point p, ILevel level)
        {
            if (GameRule.CanDraw(p, level))
            {
                oldPos = p;
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
            Console.SetCursorPosition(oldPos.X, oldPos.Y);
            Console.Write(' ');
            Console.SetCursorPosition(Pos.X, Pos.Y);
            Console.ForegroundColor = GameSettings.PlayerColor;
            Console.Write(GameSettings.PlayerAvatar);
        }

        public void Remove()
        {
            Console.SetCursorPosition(Pos.X, Pos.Y);
            Console.Write(' ');
        }
    }
}
