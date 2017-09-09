using Game.Level;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class Enemy : IGameObject
    {
        public const int FacingHorizontal = 1;
        public const int FacingVertical = 2;

        public Point Pos { get; set; }

        private int cooldown = 3;
        private Point oldPos;
        private int facing;
        private Point movingPoint;

        public Enemy(Point p, int facing)
        {
            Pos = p;
            oldPos = p;

            this.facing = facing;
            if (facing == FacingHorizontal)
                movingPoint = new Point(1, 0);
            else
                movingPoint = new Point(0, 1);
        }

        public void Move(Point p, ILevel level, long time)
        {
            if (time % cooldown != 0) return;

            Point newPos = new Point(Pos.X + movingPoint.X, Pos.Y + movingPoint.Y);
            if (GameRule.CanDraw(newPos, level))
            {
                oldPos = Pos;
                Pos = newPos;
                Paint();
            }
            else
            {
                if (facing == FacingHorizontal)
                    movingPoint.X = movingPoint.X == 1 ? -1 : 1;
                else
                    movingPoint.Y = movingPoint.Y == 1 ? -1 : 1;

                if (GameRule.CanDraw(newPos, level))
                {
                    oldPos = Pos;
                    Pos = newPos;
                    Paint();
                }
            }
        }

        public void MoveToPosition(Point p, ILevel level)
        {
            if (GameRule.CanDraw(p, level))
            {
                oldPos = Pos;
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
            Console.ForegroundColor = GameSettings.EnemyColor;
            Console.Write(GameSettings.EnemyAvatar);
        }

        public void Remove()
        {
            Console.SetCursorPosition(Pos.X, Pos.Y);
            Console.Write(' ');
        }
    }
}
